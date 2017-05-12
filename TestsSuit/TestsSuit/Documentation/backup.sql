USE [CAMPOI]
GO
/****** Object:  Table [dbo].[label]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[label](
	[id] [varchar](30) NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_label] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[language]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[language](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_language] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[logs]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loglevel] [int] NOT NULL,
	[action] [varchar](50) NOT NULL,
	[description] [varchar](200) NOT NULL,
	[entity] [varchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[permission]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[permission](
	[id] [varchar](5) NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_permission_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[permission_hierarchy]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[permission_hierarchy](
	[permissionIdBranch] [varchar](5) NOT NULL,
	[permissionIdLeaf] [varchar](5) NOT NULL,
 CONSTRAINT [PK_privilege] PRIMARY KEY CLUSTERED 
(
	[permissionIdBranch] ASC,
	[permissionIdLeaf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[translation]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[translation](
	[languageId] [int] NOT NULL,
	[labelId] [varchar](30) NOT NULL,
	[translation] [varchar](100) NOT NULL,
 CONSTRAINT [PK_translation] PRIMARY KEY CLUSTERED 
(
	[languageId] ASC,
	[labelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_user_active]  DEFAULT ((1)),
	[languageId] [int] NOT NULL,
	[permissionId] [varchar](5) NOT NULL,
	[hvd] [varchar](32) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vdv]    Script Date: 12/05/2017 00:40:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vdv](
	[entity] [varchar](50) NOT NULL,
	[vdv] [varchar](32) NOT NULL,
 CONSTRAINT [PK_vdv] PRIMARY KEY CLUSTERED 
(
	[entity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[permission_hierarchy]  WITH CHECK ADD  CONSTRAINT [FK_privilege_permission_b] FOREIGN KEY([permissionIdBranch])
REFERENCES [dbo].[permission] ([id])
GO
ALTER TABLE [dbo].[permission_hierarchy] CHECK CONSTRAINT [FK_privilege_permission_b]
GO
ALTER TABLE [dbo].[permission_hierarchy]  WITH CHECK ADD  CONSTRAINT [FK_privilege_permission_l] FOREIGN KEY([permissionIdLeaf])
REFERENCES [dbo].[permission] ([id])
GO
ALTER TABLE [dbo].[permission_hierarchy] CHECK CONSTRAINT [FK_privilege_permission_l]
GO
ALTER TABLE [dbo].[translation]  WITH CHECK ADD  CONSTRAINT [FK_translation_label] FOREIGN KEY([labelId])
REFERENCES [dbo].[label] ([id])
GO
ALTER TABLE [dbo].[translation] CHECK CONSTRAINT [FK_translation_label]
GO
ALTER TABLE [dbo].[translation]  WITH CHECK ADD  CONSTRAINT [FK_translation_language] FOREIGN KEY([languageId])
REFERENCES [dbo].[language] ([id])
GO
ALTER TABLE [dbo].[translation] CHECK CONSTRAINT [FK_translation_language]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_language] FOREIGN KEY([languageId])
REFERENCES [dbo].[language] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_language]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_permission] FOREIGN KEY([permissionId])
REFERENCES [dbo].[permission] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_permission]
GO
