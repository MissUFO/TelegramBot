CREATE TABLE [bot].[Error](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChatId] [bigint] NULL,
	[RefusalWorkflowStatusId] [int] NULL,
	[ErrorCode] [bigint] NULL,
	[ErrorMessage] [nvarchar](1024) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Error] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)


GO
ALTER TABLE [bot].[Error] ADD  CONSTRAINT [DF_Error_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[Error] ADD  CONSTRAINT [DF_Error_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]