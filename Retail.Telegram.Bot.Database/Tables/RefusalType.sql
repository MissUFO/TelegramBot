CREATE TABLE [bot].[RefusalType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[HasComment] [bit] NOT NULL,
 CONSTRAINT [PK_RefusalType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


GO
ALTER TABLE [bot].[RefusalType] ADD  CONSTRAINT [DF_RefusalType_HasComment]  DEFAULT ((0)) FOR [HasComment]
GO
ALTER TABLE [bot].[RefusalType] ADD  CONSTRAINT [DF_RefusalType_Order]  DEFAULT ((0)) FOR [Order]