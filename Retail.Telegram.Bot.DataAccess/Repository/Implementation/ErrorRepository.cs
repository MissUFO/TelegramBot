using System;
using System.Collections.Generic;
using System.Data;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Saving user exceptions info in database for support reasons
    /// </summary>
    public class ErrorRepository : IRepository<Error>
    {
        public string ConnectionString { get; set; }

        public ErrorRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get list (NOT IMPLEMENTED)
        /// </summary>
        public List<Error> List()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get single item (NOT IMPLEMENTED)
        /// </summary>
        public Error Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add or update item (NOT IMPLEMENTED)
        /// </summary>
        public Error AddEdit(Error entity)
        {
           
            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[Product_AddEdit]";
                dataManager.Add("@ChatId", SqlDbType.BigInt, ParameterDirection.Input, entity.ChatId);
                dataManager.Add("@RefusalWorkflowStatusId", SqlDbType.Int, ParameterDirection.Input, (int)entity.RefusalWorkflowStatusId);
                dataManager.Add("@ErrorCode", SqlDbType.BigInt, ParameterDirection.Input, entity.ErrorCode);
                dataManager.Add("@ErrorMessage", SqlDbType.NVarChar, ParameterDirection.Input, entity.ErrorMessage);
                dataManager.Add("@StackTrace", SqlDbType.NVarChar, ParameterDirection.Input, entity.StackTrace);
                
                dataManager.ExecuteNonQuery();
                
            }

            return entity;
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