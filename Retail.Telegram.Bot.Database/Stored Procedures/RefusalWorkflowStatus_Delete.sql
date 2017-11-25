CREATE PROCEDURE [bot].[RefusalWorkflowStatus_Delete]
			  @ChatId bigint
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM [bot].[RefusalWorkflowStatus]
      WHERE ChatId=@ChatId

END