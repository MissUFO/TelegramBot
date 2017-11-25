using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// Refusal request object
    /// </summary>
    public class Refusal : Entity
    {
        public int RefusalTypeId { get; set; }
        public string RefusalComment { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
       
        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.RefusalTypeId = xml.Attribute("RefusalTypeId").ToType<int>();
            this.RefusalComment = xml.Attribute("RefusalComment").ToType<string>();
            this.UserId = xml.Attribute("UserId").ToType<int>();
            this.ProductId = xml.Attribute("ProductId").ToType<int>();
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
        }
    }
}
