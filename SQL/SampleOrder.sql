ALTER TABLE [dbo].[sampleorder] DROP CONSTRAINT [DefaultModified]
GO

ALTER TABLE [dbo].[sampleorder] DROP CONSTRAINT [DefaultCreated]
GO

/****** Object:  Table [dbo].[sampleorder]    Script Date: 02/04/2018 20:46:35 ******/
DROP TABLE [dbo].[sampleorder]
GO

/****** Object:  Table [dbo].[sampleorder]    Script Date: 02/04/2018 20:46:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sampleorder](
	[OrderId] [int] IDENTITY(500000,1) NOT NULL,
	[OrderRef] [varchar](100) NOT NULL,
	[CustomerId] [varchar](100) NOT NULL,
	[Currency] [varchar](50) NULL,
	[Price] DECIMAL(4, 2) NULL,
	[CreatedDateTime] [datetimeoffset](2) NULL,
	[ModifiedDateTime] [datetimeoffset](2) NULL,
 CONSTRAINT [PK_sampleorder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[sampleorder] ADD  CONSTRAINT [DefaultCreated]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO

ALTER TABLE [dbo].[sampleorder] ADD  CONSTRAINT [DefaultModified]  DEFAULT (getdate()) FOR [ModifiedDateTime]
GO


