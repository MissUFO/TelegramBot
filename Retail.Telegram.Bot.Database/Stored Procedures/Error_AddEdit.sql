CREATE PROCEDURE [bot].[Error_AddEdit]
		   @Id int = null
		  ,@ChatId bigint = null
		  ,@RefusalWorkflowStatusId int = null
		  ,@ErrorCode bigint = null
		  ,@ErrorMessage nvarchar(1024) = null
		  ,@StackTrace nvarchar(max) = null
		  ,@CreatedOn datetime = null
		  ,@ModifiedOn datetime = null
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [bot].[Error] WHERE ChatId = @ChatId and (ErrorCode = @ErrorCode or ErrorMessage = @ErrorMessage))
	BEGIN

	-- Update if exists
	   UPDATE [bot].[Error]
	   SET StackTrace = @StackTrace
		  ,ModifiedOn = getdate()
	   WHERE ChatId = @ChatId and (ErrorCode = @ErrorCode or ErrorMessage = @ErrorMessage)

	END
    ELSE
    BEGIN

		-- Create a new record
		INSERT INTO [bot].[Error]
           ([ChatId]
           ,[RefusalWorkflowStatusId]
           ,[ErrorCode]
           ,[ErrorMessage]
           ,[StackTrace]
           ,[CreatedOn]
           ,[ModifiedOn])
     VALUES
           (@ChatId
           ,@RefusalWorkflowStatusId
           ,@ErrorCode
           ,@ErrorMessage
           ,@StackTrace
           ,ISNULL(@CreatedOn, GETDATE())
		   ,ISNULL(@ModifiedOn, GETDATE()))

	END
END