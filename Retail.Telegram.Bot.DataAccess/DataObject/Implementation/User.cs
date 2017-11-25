using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// User object
    /// </summary>
    public class User : Entity
    {
        public string UserName { get; set; }
        public long EmployeeId { get; set; }
        public string AccessCode { get; set; }
        public string PhoneNumber { get; set; }
        public int StoreId { get; set; }
        public bool Status { get; set; }
        public DateTime LastLoginOn { get; set; }

        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.UserName = xml.Attribute("UserName").ToType<string>();
            this.EmployeeId = xml.Attribute("EmployeeId").ToType<long>();
            this.AccessCode = xml.Attribute("AccessCode").ToType<string>();
            this.PhoneNumber = xml.Attribute("PhoneNumber").ToType<string>();
            this.StoreId = xml.Attribute("StoreId").ToType<int>();
            this.Status = Convert.ToBoolean(xml.Attribute("Status").ToType<int>());
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
            this.LastLoginOn = xml.Attribute("LastLoginOn").ToType<DateTime>();
}
    }
}
