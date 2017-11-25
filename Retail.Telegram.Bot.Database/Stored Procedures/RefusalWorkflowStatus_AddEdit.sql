CREATE PROCEDURE [bot].[RefusalWorkflowStatus_AddEdit]
		   @Id int = null
		  ,@ChatId bigint = null
		  ,@ProcessStage int = null
		  ,@UserId int = null
		  ,@ProductId int = null
		  ,@CreatedOn datetime = null
		  ,@ModifiedOn datetime = null
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [bot].[RefusalWorkflowStatus] WHERE ChatId = @ChatId)
	BEGIN

	-- Update if exists
	   UPDATE [bot].[RefusalWorkflowStatus]
	   SET ProcessStage = @ProcessStage
		  ,UserId = @UserId
	      ,ProductId = @ProductId
		  ,ModifiedOn = getdate()
	   WHERE ChatId = @ChatId

	   SELECT top(1) Id FROM [bot].[RefusalWorkflowStatus] WHERE ChatId = @ChatId

	END
    ELSE
    BEGIN

		-- Create a new record
		INSERT INTO [bot].[RefusalWorkflowStatus]
			   ([ChatId]
			   ,[ProcessStage]
			   ,[UserId]
			   ,[ProductId]
			   ,[CreateOn]
			   ,[ModifiedOn])
		 VALUES
			   (@ChatId
			   ,@ProcessStage
			   ,@UserId
			   ,@ProductId
			   ,ISNULL(@CreatedOn, GETDATE())
			   ,ISNULL(@ModifiedOn, GETDATE()))

		SELECT CAST(scope_identity() AS int);
	END
END