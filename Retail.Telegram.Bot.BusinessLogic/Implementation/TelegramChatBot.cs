using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Retail.Telegram.Bot.BusinessLogic.Interface;
using Retail.Telegram.Bot.BusinessLogic.Properties;
using Retail.Telegram.Bot.DataAccess.DataObject.Enum;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Implementation;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Retail.Telegram.Bot.BusinessLogic.Implementation
{
    /// <summary>
    /// Chat bot for Telegram implementation
    /// </summary>
    public class TelegramChatBot: IChatBot<TelegramBotClient, Message>
    {
        #region public properties
        public TelegramBotClient Api { get; set; }
        #endregion

        #region private properties
        private UserRepository UserRepository { get; set; }
        private RefusalWorkflowStatusRepository UserProcessStatusRepository { get; set; }
        private ProductRepository ProductRepository { get; set; }
        private RefusalRepository RefusalRepository { get; set; }
        private RefusalTypeRepository RefusalTypeRepository { get; set; }
        private ErrorRepository ErrorRepository { get; set; }
        private BarCodeRecognition BarCodeReader { get; set; }
        #endregion

        #region constructor
        public TelegramChatBot()
        {
            Api = new TelegramBotClient(Settings.Default.TELEGRAM_API_KEY);

            UserRepository = new UserRepository();
            UserProcessStatusRepository = new RefusalWorkflowStatusRepository();
            ProductRepository = new ProductRepository();
            RefusalRepository = new RefusalRepository();
            RefusalTypeRepository = new RefusalTypeRepository();
            ErrorRepository = new ErrorRepository();

            BarCodeReader = new BarCodeRecognition();
        }
        #endregion

        public void ProcessRequest(Message message)
        {
            var currentState = GetCurrentState(message.Chat.Id);
            //if (currentState.UserId == 0)
            //{
            //    UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.PreAuthorisation, int userId = 0)
            //    //LeaveChat(message);
            //    //return;
            //}

            switch (currentState.ProcessStage)
            {
                case RefusalWorkflowStatusType.PreAuthorisation:
                    UserSignIn(message);
                    break;
                case RefusalWorkflowStatusType.Authorised:
                    AskPhoto(message);
                    break;
                case RefusalWorkflowStatusType.NotAuthorised:
                    UserSignIn(message);
                    break;
                case RefusalWorkflowStatusType.AskedPhoto:
                    if(SavePhoto(message))
                        ShowOptions(message);
                    else
                        AskPhotoError(message);
                    break;
                case RefusalWorkflowStatusType.SavedPhoto:
                    ShowOptions(message);
                    break;
                case RefusalWorkflowStatusType.ShowedOptions:
                    if (message.Text.Contains(TelegramBotMessages.AskRefusalOther))
                        AskOther(message);
                    else
                    {
                        SaveOption(message);
                        SayGoodBye(message);
                    }
                    break;
                case RefusalWorkflowStatusType.AskedOther:
                    SaveOther(message);
                    SayGoodBye(message);
                    break;
                case RefusalWorkflowStatusType.SayedGoodBye:
                    AskPhoto(message);
                    break;
                default:
                    SayHi(message);
                    break;
            }
        }

        public void ProcessCommand(Message message)
        {
            if (message.Text.ToLower().Contains("/start"))
                SayHi(message);
            else if (message.Text.ToLower().Contains("/stop"))
                SayGoodBye(message);
            else
                SayCommandNotFound(message);
        }

        #region Protected methods
        protected void SayHi(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.SayHi);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.PreAuthorisation);
        }

        protected bool UserSignIn(Message message)
        {
            DataAccess.DataObject.Implementation.User user = null;

            try
            {
                user = UserRepository.Login(message.Text);
            }
            catch
            {
                user = null;
            }

            if (string.IsNullOrEmpty(user?.AccessCode))
            {
                SayUserSignInError(message);
                UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.NotAuthorised);
                return false;
            }

            AskPhoto(message);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.AskedPhoto, user.Id);
            return true;
        }

        protected void SayUserSignInError(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.UserSignInFalse);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.NotAuthorised);
        }

        protected void SayCommandNotFound(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.CommandNotFound);
        }

        protected void AskPhoto(Message message)
        {  
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskPhoto);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.AskedPhoto);
        }

        protected void AskPhotoError(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskPhotoError);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.AskedPhoto);
        }

        protected bool SavePhoto(Message message)
        {
            bool result = false;
            var currentState = new RefusalWorkflowStatus();

            try
            {
                Task<File> file = Api.GetFileAsync(message.Photo.LastOrDefault()?.FileId);
                string barcode = BarCodeReader.GetBarCode(file.Result.FileStream);

                if (barcode != string.Empty)
                {
                    currentState = GetCurrentState(message.Chat.Id);

                    //create or update product
                    Product product = ProductRepository.AddEdit(new Product()
                    {
                        ProductBarcode = barcode
                    });

                    Api.SendTextMessageAsync(message.Chat.Id, String.Format(TelegramBotMessages.BarCodeText, barcode));

                    //update state
                    currentState.ProductId = product.Id;
                    currentState.ProcessStage = RefusalWorkflowStatusType.SavedPhoto;
                    UserProcessStatusRepository.AddEdit(currentState);
                    
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;

                ErrorRepository.AddEdit(new Error()
                {
                    ChatId = message.Chat.Id,
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RefusalWorkflowStatusId = currentState.ProcessStage
                });
            }

            return result;
        }

        protected void ShowOptions(Message message)
        {
            //get selected option
            var options = RefusalTypeRepository.List();
            
            var keyboard = new ReplyKeyboardMarkup(GetKeyboard(options), false, true);

            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskRefusalText, false, false, 0,
                keyboard, ParseMode.Default, CancellationToken.None);

            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.ShowedOptions);

        }

        public KeyboardButton[][] GetKeyboard(List<RefusalType> options)
        {
            var rows = Convert.ToInt32(Math.Ceiling((double)options.Count / 2));
            var buttons = 2;
            var keyboardRows = new KeyboardButton[rows][];

            int current = 0;
            for (int i = 0; i < rows; i++)
            {
                var keyboardButtons = new KeyboardButton[buttons];
                for (int j = 0; j < buttons; j++)
                {
                    if(options.Count > current)
                        keyboardButtons[j] = new KeyboardButton(options[current].Name);
                    else
                        keyboardButtons[j] = new KeyboardButton(string.Empty);

                    current++;
                }
                keyboardRows[i] = keyboardButtons;
            }

            return keyboardRows;
        }

        protected bool SaveOption(Message message)
        {
            bool result = false;
            var currentState = new RefusalWorkflowStatus();
            try
            {
                currentState = GetCurrentState(message.Chat.Id);

                //get selected option
                var refusalTypes = RefusalTypeRepository.List();
                var type = refusalTypes.Find(itm => itm.Name.ToLower().Contains(message.Text.ToLower()));

                //save feedback
                RefusalRepository.AddEdit(new Refusal()
                {
                   UserId = currentState.UserId,
                   ProductId = currentState.ProductId,
                   RefusalTypeId = type?.Id ?? 0
                });

                Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskRefusalSuccess);

                //update current state
                UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.SavedOptions);

                result = true;
            }
            catch (Exception ex)
            {
                ErrorRepository.AddEdit(new Error()
                {
                    ChatId = message.Chat.Id,
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RefusalWorkflowStatusId = currentState.ProcessStage
                });
            }
            return result;
        }

        protected bool HideOptions(Message message)
        {
            if (message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption1)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption2)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption3)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption4)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption5)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOption6)
                || message.Text.ToLower().Contains(TelegramBotMessages.AskRefusalOther))
            {
                Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskRefusalSuccess, false,
                    false, 0,
                    new ReplyKeyboardHide() {HideKeyboard = true, Selective = false}, ParseMode.Default,
                    CancellationToken.None);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void AskOther(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskRefusalOtherText);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.AskedOther);
        }

        protected bool SaveOther(Message message)
        {
            var currentState = new RefusalWorkflowStatus();
            try
            {
                currentState = GetCurrentState(message.Chat.Id);

                //get selected option
                var refusalTypes = RefusalTypeRepository.List();
                var type = refusalTypes.Find(itm => itm.HasComment);

                //save feedback
                RefusalRepository.AddEdit(new Refusal()
                {
                    UserId = currentState.UserId,
                    ProductId = currentState.ProductId,
                    RefusalTypeId = type?.Id ?? 0,
                    RefusalComment = message.Text
                });

                Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.AskRefusalSuccess);
                UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.SavedOther);
            }
            catch (Exception ex)
            {
                ErrorRepository.AddEdit(new Error()
                {
                    ChatId = message.Chat.Id,
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RefusalWorkflowStatusId = currentState.ProcessStage
                });
            }

            return true;
        }

        protected void SayGoodBye(Message message)
        {
            Api.SendTextMessageAsync(message.Chat.Id, TelegramBotMessages.SayGoodBye);
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.SayedGoodBye);
        }

        protected async Task<bool> LeaveChat(Message message)
        {
            UpdateCurrentState(message.Chat.Id, RefusalWorkflowStatusType.LeftChat);
            return await Api.LeaveChatAsync(message.Chat.Id);
        }

        protected RefusalWorkflowStatus GetCurrentState(long chatId)
        {
            var currentState = new RefusalWorkflowStatus();
            try
            {
                currentState = UserProcessStatusRepository.Get((int) chatId);
            }
            catch (Exception ex)
            {
                ErrorRepository.AddEdit(new Error()
                {
                    ChatId = chatId,
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RefusalWorkflowStatusId = currentState.ProcessStage
                });
            }
            return currentState;
        }

        protected RefusalWorkflowStatus UpdateCurrentState(long chatId, RefusalWorkflowStatusType state, int userId = 0)
        {
            var currentState = new RefusalWorkflowStatus();

            try
            {
                currentState = GetCurrentState(chatId);

                currentState.ChatId = chatId;
                currentState.ProcessStage = state;
                if (userId > 0)
                    currentState.UserId = userId;

                UserProcessStatusRepository.AddEdit(currentState);
            }
            catch (Exception ex)
            {
                ErrorRepository.AddEdit(new Error()
                {
                    ChatId = chatId,
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RefusalWorkflowStatusId = currentState.ProcessStage
                });
            }

            return currentState;
        }
        #endregion
    }
}
