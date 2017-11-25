namespace Retail.Telegram.Bot.BusinessLogic.Interface
{
    /// <summary>
    /// Basic interface for chat bot
    /// </summary>
    public interface IChatBot<B,M>
    {
        B Api { get; set; }

        void ProcessRequest(M message);

        void ProcessCommand(M message);
    }
}
