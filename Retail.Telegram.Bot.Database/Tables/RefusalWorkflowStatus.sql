CREATE TABLE [bot].[RefusalWorkflowStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChatId] [bigint] NOT NULL,
	[ProcessStage] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[CreateOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_RefusalWorkflowStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)




GO
ALTER TABLE [bot].[RefusalWorkflowStatus] ADD  CONSTRAINT [DF_RefusalWorkflowStatus_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
ALTER TABLE [bot].[RefusalWorkflowStatus] ADD  CONSTRAINT [DF_RefusalWorkflowStatus_CreateOn]  DEFAULT (getdate()) FOR [CreateOn]