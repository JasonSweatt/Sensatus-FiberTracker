if db_id (N'SensatusFiberTracker') is not null drop database SensatusFiberTracker;
Create Database SensatusFiberTracker
Go

Use SensatusFiberTracker

/****** Object:  Table [dbo].[ExpenseDetails]    Script Date: 09/08/2009 11:15:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExpenseDetails](
    [ExpId] [int] IDENTITY(1,1) NOT NULL,
    [ItemId] [int] NULL DEFAULT ((0)),
    [ExpDesc] [nvarchar](255) NULL,
    [ExpAmount] [int] NULL DEFAULT ((0)),
    [ExpBy] [int] NULL DEFAULT ((0)),
    [ExpDate] [datetime] NULL,
    [MonthYear] [nvarchar](50) NULL,
    [Finalized] [int] NULL DEFAULT ((0)),
    [IsDeleted] [int] NULL DEFAULT ((0)),
 CONSTRAINT [PK_ExpenseDetails] PRIMARY KEY NONCLUSTERED 
(
    [ExpId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ItemDetails]    Script Date: 09/08/2009 11:17:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemDetails](
    [ItemId] [int] IDENTITY(1,1) NOT NULL,
    [ItemName] [nvarchar](100) NULL,
    [ItemDesc] [nvarchar](255) NULL,
    [CreatedBy] [nvarchar](50) NULL,
    [EntryDate] [datetime] NULL,
    [IsActive] [int] NULL DEFAULT ((0)),
 CONSTRAINT [PK_ItemDetails] PRIMARY KEY NONCLUSTERED 
(
    [ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[RoleDetails]    Script Date: 09/08/2009 11:17:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleDetails](
    [RoleId] [int] IDENTITY(1,1) NOT NULL,
    [Role] [nvarchar](255) NULL,
    [Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_RoleDetails] PRIMARY KEY NONCLUSTERED 
(
    [RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[UserInfo]    Script Date: 09/08/2009 11:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
    [UserId] [int] IDENTITY(1,1) NOT NULL,
    [RoleId] [int] NULL,
    [UserName] [nvarchar](50) NULL,
    [Pwd] [nvarchar](50) NULL,
    [FirstName] [nvarchar](50) NULL,
    [LastName] [nvarchar](50) NULL,
    [EMail] [nvarchar](255) NULL,
    [Mobile] [nvarchar](255) NULL,
    [LastLoginDate] [datetime] NULL,
    [PasswordChangeDate] [datetime] NULL,
    [IsActive] [int] NULL DEFAULT ((0)),
 CONSTRAINT [PK_UserInfo] PRIMARY KEY NONCLUSTERED 
(
    [UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- SCRIPT FOR ADDING MASTER DATA

--INSERT MASTER USER
INSERT INTO UserInfo (RoleId,UserName,Pwd,FirstName,LastName,EMail,Mobile,LastLoginDate,PasswordChangeDate,IsActive) VALUES 
 (1,'admin','a6jr0tclfyWJWKaPi9URIQ==','Admin','Admin','ak.tripathi@yahoo.com','9880946821','2009-08-24 00:00:00','2009-08-18 00:00:00',1);

--INSERT Roles
INSERT INTO RoleDetails (Role,Description) VALUES ('Admin','Super User')
INSERT INTO RoleDetails (Role,Description) VALUES  ('User',NULL)

--INSERT Test Item
INSERT INTO itemDetails (ItemName, ItemDesc, CreatedBy, IsActive)
VALUES('TestItem','Please edit this item details. Its simply added for test purpose.',1,1);