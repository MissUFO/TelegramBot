using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Repository for maintaining by storing in database current workflow state in Telegram chat
    /// </summary>
    public class RefusalWorkflowStatusRepository : IRepository<RefusalWorkflowStatus>
    {
        public string ConnectionString { get; set; }

        public RefusalWorkflowStatusRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get list (NOT IMPLEMENTED)
        /// </summary>
        public List<RefusalWorkflowStatus> List()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get single item by ChatID
        /// </summary>
        public RefusalWorkflowStatus Get(int id)
        {
            var entity = new RefusalWorkflowStatus();

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[RefusalWorkflowStatus_Get]";
                dataManager.Add("@ChatId", SqlDbType.BigInt, ParameterDirection.Input, id);
                dataManager.Add("@Xml", SqlDbType.Xml, ParameterDirection.Output);
                dataManager.ExecuteReader();
                XElement xmlOut = XElement.Parse(dataManager["@Xml"].Value.ToString());
                entity.UnpackXML(xmlOut.Element("RefusalWorkflowStatus"));
            }

            return entity;
        }

        /// <summary>
        /// Add or update item
        /// </summary>
        public RefusalWorkflowStatus AddEdit(RefusalWorkflowStatus entity)
        {
            var result = entity;

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[RefusalWorkflowStatus_AddEdit]";
                dataManager.Add("@ChatId", SqlDbType.BigInt, ParameterDirection.Input, entity.ChatId);
                dataManager.Add("@ProcessStage", SqlDbType.Int, ParameterDirection.Input, (int)entity.ProcessStage);
                dataManager.Add("@UserId", SqlDbType.Int, ParameterDirection.Input, entity.UserId);
                dataManager.Add("@ProductId", SqlDbType.Int, ParameterDirection.Input, entity.ProductId);

                var id = dataManager.ExecuteScalar();

                result = Get((int)id);
            }

            return result;
        }

        /// <summary>
        /// Delete single item by ChatID
        /// </summary>
        public bool Delete(int id)
        {
            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[RefusalWorkflowStatus_Delete]";
                dataManager.Add("@ChatId", SqlDbType.BigInt, ParameterDirection.Input, id);
                dataManager.ExecuteNonQuery();
            }

            return true;
        }

    }
}