CREATE PROCEDURE [bot].[Refusal_AddEdit]	
				   @Id int = null
				  ,@RefusalTypeId int
				  ,@RefusalComment nvarchar(1024) = null
				  ,@UserId int
				  ,@ProductId int
				  ,@CreatedOn datetime = null
				  ,@ModifiedOn datetime = null			
AS
BEGIN	

	DECLARE @StoreId int = 0;
	SELECT @StoreId = StoreId from [bot].[User] WHERE Id = @UserId;

	--DECLARE @RefusalId int = 0;
	--SET @RefusalId = (SELECT top(1) Id FROM [bot].[Refusal] WHERE ProductId = @ProductId and UserId = @UserId and RefusalTypeId=@RefusalTypeId ORDER BY ModifiedOn DESC);

	--IF @RefusalId<>0 and @RefusalComment IS NOT NULL AND @RefusalComment<>''
	--BEGIN
	
	--   -- Update if exists
	--   UPDATE [bot].[Refusal]
	--   SET RefusalComment = @RefusalComment
	--      ,ModifiedOn = getdate()
	--   WHERE Id = @RefusalId

	--   SELECT @RefusalId

	--END
 --   ELSE
 --   BEGIN
		-- Create new
		INSERT INTO [bot].[Refusal]
           ([RefusalTypeId]
           ,[RefusalComment]
           ,[UserId]
           ,[ProductId]
           ,[StoreId]
           ,[CreatedOn]
           ,[ModifiedOn])
		VALUES
           (@RefusalTypeId
           ,@RefusalComment
           ,@UserId
           ,@ProductId
           ,@StoreId
           ,ISNULL(@CreatedOn, GETDATE())
           ,ISNULL(@ModifiedOn, GETDATE()))

		   SELECT CAST(scope_identity() AS int);
	--END
END