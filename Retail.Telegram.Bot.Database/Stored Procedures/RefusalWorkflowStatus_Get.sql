CREATE PROCEDURE [bot].[RefusalWorkflowStatus_Get]
			  @ChatId bigint
		    , @Xml XML output
AS
BEGIN
	SET NOCOUNT ON;

   SET @Xml = (SELECT (SELECT   rws.[Id]
							  , rws.[ChatId]
							  , rws.[ProcessStage]
							  , rws.[UserId]
							  , rws.[ProductId]
							  , rws.[CreateOn]
							  , rws.[ModifiedOn]
						FROM [bot].[RefusalWorkflowStatus] AS rws
						WHERE rws.ChatId = @ChatId
						ORDER BY rws.[ModifiedOn] desc
						FOR XML RAW('RefusalWorkflowStatus'), TYPE)
				FOR XML PATH('RefusalWorkflowStatuses'),TYPE)

END