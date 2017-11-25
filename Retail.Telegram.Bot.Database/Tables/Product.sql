CREATE TABLE [bot].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductBarcode] [nvarchar](255) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


GO
ALTER TABLE [bot].[Product] ADD  CONSTRAINT [DF_Product_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[Product] ADD  CONSTRAINT [DF_Product_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]