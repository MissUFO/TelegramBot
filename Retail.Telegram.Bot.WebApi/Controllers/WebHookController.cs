using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Retail.Telegram.Bot.BusinessLogic.Implementation;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Retail.Telegram.Bot.WebApi.Controllers
{
    /// <summary>
    /// Web API for Retail Telegram Bot.
    /// Telegram API:https://core.telegram.org/bots/api
    /// WebHook registration post request: https://api.telegram.org/bot327682208:AAHv2ALZWBo48HjNH347rJZ3-r_bTwznaBQ/setWebhook?url=https://adidasretailtelegrambot.azurewebsites.net/api/webhook&max_connections=100
    /// </summary>
    public class WebHookController : ApiController
    {
        /// <summary>
        /// WebHook post. Consumed by Telegram
        /// </summary>
        public async Task<IHttpActionResult> Post(Update update)
        {
            var bot = new TelegramChatBot();
            var message = update.Message;

            if (message.Entities.Any(item => item.Type == MessageEntityType.BotCommand))
                bot.ProcessCommand(message);
            else
                bot.ProcessRequest(message);

            return Ok();
        }
    }
}
