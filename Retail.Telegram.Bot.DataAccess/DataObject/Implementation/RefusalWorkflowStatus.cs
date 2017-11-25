using System;
using System.Xml.Linq;
using Retail.Telegram.Bot.DataAccess.DataManager.Extension;
using Retail.Telegram.Bot.DataAccess.DataObject.Enum;

namespace Retail.Telegram.Bot.DataAccess.DataObject.Implementation
{
    /// <summary>
    /// Workflow status object for Telegram chat
    /// </summary>
    public class RefusalWorkflowStatus : Entity
    {
        public long ChatId { get; set; }
        public RefusalWorkflowStatusType ProcessStage { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        protected override void CreateObjectFromXml(XElement xml)
        {
            this.Id = xml.Attribute("Id").ToType<int>();
            this.ChatId = xml.Attribute("ChatId").ToType<long>();
            this.ProcessStage = xml.Attribute("ProcessStage").ToEnum<RefusalWorkflowStatusType>();
            this.UserId = xml.Attribute("UserId").ToType<int>();
            this.ProductId = xml.Attribute("ProductId").ToType<int>();
            this.CreatedOn = xml.Attribute("CreatedOn").ToType<DateTime>();
            this.ModifiedOn = xml.Attribute("ModifiedOn").ToType<DateTime>();
        }
    }
}
