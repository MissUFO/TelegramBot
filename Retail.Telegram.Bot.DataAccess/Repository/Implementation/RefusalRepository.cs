using System;
using System.Collections.Generic;
using System.Data;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Repository for refusal info based on Telegram chat user input
    /// </summary>
    public class RefusalRepository : IRepository<Refusal>
    {
        public string ConnectionString { get; set; }

        public RefusalRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get list (NOT IMPLEMENTED)
        /// </summary>
        public List<Refusal> List()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get single item (NOT IMPLEMENTED)
        /// </summary>
        public Refusal Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add or update item
        /// </summary>
        public Refusal AddEdit(Refusal entity)
        {   
            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[Refusal_AddEdit]";
                dataManager.Add("@Id", SqlDbType.Int, ParameterDirection.Input, entity.Id);
                dataManager.Add("@RefusalTypeId", SqlDbType.Int, ParameterDirection.Input, entity.RefusalTypeId);
                dataManager.Add("@RefusalComment", SqlDbType.NVarChar, ParameterDirection.Input, entity.RefusalComment);
                dataManager.Add("@UserId", SqlDbType.Int, ParameterDirection.Input, entity.UserId);
                dataManager.Add("@ProductId", SqlDbType.Int, ParameterDirection.Input, entity.ProductId);

                entity.Id = (int)dataManager.ExecuteScalar();
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