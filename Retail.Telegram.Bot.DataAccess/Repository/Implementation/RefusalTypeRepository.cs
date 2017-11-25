using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Refusal reasons. For now the only getting list of reasons from database
    /// </summary>
    public class RefusalTypeRepository : IRepository<RefusalType>
    {
        public string ConnectionString { get; set; }

        public RefusalTypeRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get list
        /// </summary>
        public List<RefusalType> List()
        {
            var entities = new List<RefusalType>();

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[RefusalType_List]";
                dataManager.Add("@Xml", SqlDbType.Xml, ParameterDirection.Output);
                dataManager.ExecuteReader();
                XElement xmlOut = XElement.Parse(dataManager["@Xml"].Value.ToString());
                entities.UnpackXML(xmlOut);
            }

            return entities;
        }

        /// <summary>
        /// Get single item (NOT IMPLEMENTED)
        /// </summary>
        public RefusalType Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add or update item (NOT IMPLEMENTED)
        /// </summary>
        public RefusalType AddEdit(RefusalType entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete single item (NOT IMPLEMENTED)
        /// </summary>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
       
    }
}
