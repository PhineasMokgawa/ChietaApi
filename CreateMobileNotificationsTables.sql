-- SQL Script to create mobile notifications tables
-- Run this script on the ims_dev database

-- Create tbl_mobile_notifications table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_mobile_notifications]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[tbl_mobile_notifications](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [UserId] [int] NOT NULL,
        [Title] [nvarchar](max) NULL,
        [Message] [nvarchar](max) NULL,
        [IsRead] [bit] NOT NULL DEFAULT 0,
        [IsPushSent] [bit] NOT NULL DEFAULT 0,
        [CreatedAt] [datetime2](7) NOT NULL,
        [UpdatedAt] [datetime2](7) NULL,
        [Source] [nvarchar](max) NULL,
        CONSTRAINT [PK_tbl_mobile_notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    PRINT 'Table tbl_mobile_notifications created successfully'
END
ELSE
BEGIN
    PRINT 'Table tbl_mobile_notifications already exists'
END
GO

-- Create tbl_PushNotifications table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_PushNotifications]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[tbl_PushNotifications](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [UserId] [int] NOT NULL,
        [Token] [nvarchar](max) NULL,
        CONSTRAINT [PK_tbl_PushNotifications] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    PRINT 'Table tbl_PushNotifications created successfully'
END
ELSE
BEGIN
    PRINT 'Table tbl_PushNotifications already exists'
END
GO

PRINT 'Script execution completed!'
