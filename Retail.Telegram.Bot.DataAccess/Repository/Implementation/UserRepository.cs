using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// User repository with access for database
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        public string ConnectionString { get; set; }

        public UserRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get User by userid
        /// </summary>
        public User Get(int id)
        {
            var user = new User();

            return user;
        }

        /// <summary>
        /// Get user by accesscode if the user is exist
        /// </summary>
        public User Login(string accesscode)
        {
            var user = new User();

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[User_Login]";
                dataManager.Add("@AccessCode", SqlDbType.NVarChar, ParameterDirection.Input, accesscode);
                dataManager.Add("@Xml", SqlDbType.Xml, ParameterDirection.Output);
                dataManager.ExecuteReader();
                XElement xmlOut = XElement.Parse(dataManager["@Xml"].Value.ToString());
                user.UnpackXML(xmlOut.Element("User"));
            }

            return user;
        }

        /// <summary>
        /// AddEdit User
        /// </summary>
        /// <param name="user"></param>
        public User AddEdit(User user)
        {
            return new User();
        }

        /// <summary>
        /// Delete User
        /// </summary>
        public bool Delete(int id)
        {
            return true;
        }

        /// <summary>
        /// Get Users list
        /// </summary>
        public List<User> List()
        {
            var users = new List<User>();
            return users;
        }
    }
}