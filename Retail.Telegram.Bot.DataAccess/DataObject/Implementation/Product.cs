using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// Product object
    /// </summary>
    public class Product : Entity
    {
        public string ProductBarcode { get; set; }

        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.ProductBarcode = xml.Attribute("ProductBarcode").ToType<string>();
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
        }

    }
}
