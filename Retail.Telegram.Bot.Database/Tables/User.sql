CREATE TABLE [bot].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[AccessCode] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[StoreId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[LastLoginOn] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


GO
ALTER TABLE [bot].[User] ADD  CONSTRAINT [DF_User_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[User] ADD  CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [bot].[User] ADD  CONSTRAINT [DF_User_Status]  DEFAULT ((1)) FOR [Status]