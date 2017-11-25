using System.Web.Http;

namespace Retail.Telegram.Bot.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //TelegramBot.CurrentStateList.Clear();
            //TelegramBot.RegisterWebHook();
        }
    }
}
