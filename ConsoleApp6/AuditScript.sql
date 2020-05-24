USE [cv]
GO

/****** Object:  Table [dbo].[rabbitAudit]    Script Date: 23-05-2020 23:26:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rabbitAudit](
	[AuditRecordId] [int] IDENTITY(1,1) NOT NULL,
	[MessageId] [uniqueidentifier] NULL,
	[ConversationId] [uniqueidentifier] NULL,
	[CorrelationId] [uniqueidentifier] NULL,
	[InitiatorId] [uniqueidentifier] NULL,
	[RequestId] [uniqueidentifier] NULL,
	[SentTime] [datetime2](7) NULL,
	[SourceAddress] [nvarchar](max) NULL,
	[DestinationAddress] [nvarchar](max) NULL,
	[ResponseAddress] [nvarchar](max) NULL,
	[FaultAddress] [nvarchar](max) NULL,
	[InputAddress] [nvarchar](max) NULL,
	[ContextType] [nvarchar](max) NULL,
	[MessageType] [nvarchar](max) NULL,
	[Headers] [nvarchar](max) NULL,
	[Custom] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AuditRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

