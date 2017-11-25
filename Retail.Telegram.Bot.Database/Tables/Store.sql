CREATE TABLE [bot].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


GO
ALTER TABLE [bot].[Store] ADD  CONSTRAINT [DF_Store_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[Store] ADD  CONSTRAINT [DF_Store_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]