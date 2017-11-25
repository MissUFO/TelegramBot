CREATE PROCEDURE [bot].[RefusalType_List]
				 @Xml XML output
AS
BEGIN
	SET NOCOUNT ON;

	SET @Xml = (SELECT (SELECT rt.[Id]
							  ,rt.[Name]
							  ,rt.[Order]
							  ,rt.[HasComment]
						FROM [bot].[RefusalType] AS rt
						ORDER BY rt.[Order] ASC
					FOR XML RAW('RefusalType'), TYPE)
				FOR XML PATH('RefusalTypes'),TYPE)
END