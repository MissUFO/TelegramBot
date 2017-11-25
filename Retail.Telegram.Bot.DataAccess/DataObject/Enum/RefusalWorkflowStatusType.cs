namespace Retail.Telegram.Bot.DataAccess.DataObject.Enum
{
    /// <summary>
    /// Workflow statuses enum
    /// </summary>
    public enum RefusalWorkflowStatusType : byte
    {
        Start = 0,
        PreAuthorisation = 1,
        Authorised = 2,
        NotAuthorised = 3,
        AskedPhoto = 4,
        SavedPhoto = 5,
        ShowedOptions = 6,
        SavedOptions = 7,
        AskedOther = 8,
        SavedOther = 9,
        SayedGoodBye = 10,
        LeftChat = 11
    }
}
