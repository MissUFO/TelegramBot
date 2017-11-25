using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;
using Retail.Telegram.Bot.DataAccess.DataObject.Enum;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// Custom error
    /// </summary>
    public class Error : Entity
    {
        public long ChatId { get; set; }
        public RefusalWorkflowStatusType RefusalWorkflowStatusId { get; set; }
        public long ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }

        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.ChatId = xml.Attribute("ChatId").ToType<long>();
            this.RefusalWorkflowStatusId = xml.Attribute("RefusalWorkflowStatusId").ToEnum<RefusalWorkflowStatusType>();
            this.ErrorCode = xml.Attribute("ErrorCode").ToType<long>();
            this.ErrorMessage = xml.Attribute("ErrorMessage").ToType<string>();
            this.StackTrace = xml.Attribute("StackTrace").ToType<string>();
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
        }

    }
}
