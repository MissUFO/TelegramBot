using System.Configuration;

namespace Retail.Telegram.Bot.DataAccess
{
    public class ConnectionString
    {
        public static string DbConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            }
        }
    }
}
