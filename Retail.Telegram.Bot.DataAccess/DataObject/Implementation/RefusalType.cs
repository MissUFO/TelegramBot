using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// Refulas reasin object
    /// </summary>
    public class RefusalType : Entity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool HasComment { get; set; }

        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.Name = xml.Attribute("Name").ToType<string>();
            this.Order = xml.Attribute("Order").ToType<int>();
            this.HasComment = Convert.ToBoolean(xml.Attribute("HasComment").ToType<int>());
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
        }
    }
}
