CREATE PROCEDURE [bot].[User_Login]
				@AccessCode	    NVARCHAR(50)
			  , @Xml XML output
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ID INT

	SELECT @ID = usr.[Id]
	  FROM bot.[User] as usr
	WHERE usr.[AccessCode] = @AccessCode and usr.[Status] = 1
	 
	IF(@ID IS NULL)
	BEGIN
		RETURN -1
	END

    UPDATE bot.[User]
		SET LastLoginOn = GETDATE()
	WHERE Id = @ID

	SET @Xml = (SELECT (SELECT	   usr.[Id]
								  ,usr.[UserName]
								  ,usr.[EmployeeId]
								  ,usr.[AccessCode]
								  ,usr.[PhoneNumber]
								  ,usr.[StoreId]
								  ,usr.[Status]
								  ,usr.[CreatedOn]
								  ,usr.[ModifiedOn]
								  ,usr.[LastLoginOn]
								  ,(SELECT	 
									   store.[Id]
									  ,store.[StoreId]
									  ,store.[Name]
									  ,store.[Address]
									  ,store.[CreatedOn]
									  ,store.[ModifiedOn]
									FROM bot.[Store] AS store
									WHERE store.[Id] = usr.[StoreId]
									FOR XML RAW('Store'), TYPE) 
						FROM bot.[User] AS usr
						WHERE usr.[Id]=@ID
						FOR XML RAW('User'), TYPE)
				FOR XML PATH('Users'),TYPE)

END