CREATE TABLE [bot].[Refusal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefusalTypeId] [int] NOT NULL,
	[RefusalComment] [nvarchar](1024) NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Refusal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)




GO
ALTER TABLE [bot].[Refusal] ADD  CONSTRAINT [DF_Refusal_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[Refusal] ADD  CONSTRAINT [DF_Refusal_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]