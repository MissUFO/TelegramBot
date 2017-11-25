CREATE PROCEDURE [bot].[Product_AddEdit]	
				   @Id int = null
				  ,@ProductBarcode nvarchar(255)
				  ,@CreatedOn datetime = null
				  ,@ModifiedOn datetime = null			
AS
BEGIN
	
    IF NOT EXISTS(SELECT 1 FROM [bot].[Product] WHERE ProductBarcode = @ProductBarcode)
	BEGIN

		-- Create new
	
		INSERT INTO [bot].[Product]
			   ([ProductBarcode]
			   ,[CreatedOn]
			   ,[ModifiedOn])
		 VALUES
			   (@ProductBarcode
			   ,ISNULL(@CreatedOn, GETDATE())
			   ,ISNULL(@ModifiedOn, GETDATE()))

		SELECT CAST(scope_identity() AS int);
    END
	ELSE
	BEGIN

		SELECT top(1) Id FROM [bot].[Product] WHERE ProductBarcode = @ProductBarcode

	END
END