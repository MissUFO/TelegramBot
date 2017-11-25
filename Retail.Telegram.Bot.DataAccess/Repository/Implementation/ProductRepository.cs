using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataObject.Implementation;
using Retail.Telegram.Bot.DataAccess.Repository.Interface;

namespace Retail.Telegram.Bot.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Repository for collecting information about products which was refused
    /// </summary>
    public class ProductRepository : IRepository<Product>
    {
        public string ConnectionString { get; set; }

        public ProductRepository()
        {
            ConnectionString = DataAccess.ConnectionString.DbConnection;
        }

        /// <summary>
        /// Get list (NOT IMPLEMENTED)
        /// </summary>
        public List<Product> List()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get single item (NOT IMPLEMENTED)
        /// </summary>
        public Product Get(int id)
        {
            var entity = new Product();

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[Product_Get]";
                dataManager.Add("@Id", SqlDbType.Int, ParameterDirection.Input, id);
                dataManager.Add("@Xml", SqlDbType.Xml, ParameterDirection.Output);
                dataManager.ExecuteReader();
                XElement xmlOut = XElement.Parse(dataManager["@Xml"].Value.ToString());
                entity.UnpackXML(xmlOut.Element("Product"));
            }

            return entity;
        }

        /// <summary>
        /// Add or update item (NOT IMPLEMENTED)
        /// </summary>
        public Product AddEdit(Product entity)
        {
            var result = entity;

            using (var dataManager = new DataManager.Implementation.DataManager(ConnectionString))
            {
                dataManager.ExecuteString = "[bot].[Product_AddEdit]";
                dataManager.Add("@ProductBarcode", SqlDbType.NVarChar, ParameterDirection.Input, entity.ProductBarcode);

                var id = dataManager.ExecuteScalar();

                result = Get((int)id);
            }

            return result;
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