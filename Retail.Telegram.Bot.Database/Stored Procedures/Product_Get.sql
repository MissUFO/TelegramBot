CREATE PROCEDURE [bot].[Product_Get]
			  @Id int
		    , @Xml XML output
AS
BEGIN
	SET NOCOUNT ON;

   SET @Xml = (SELECT (SELECT  p.[Id]
							  ,p.[ProductBarcode]
							  ,p.[CreatedOn]
							  ,p.[ModifiedOn]
						FROM [bot].[Product] AS p
						WHERE p.Id = @Id
						FOR XML RAW('Product'), TYPE)
				FOR XML PATH('Products'),TYPE)

END