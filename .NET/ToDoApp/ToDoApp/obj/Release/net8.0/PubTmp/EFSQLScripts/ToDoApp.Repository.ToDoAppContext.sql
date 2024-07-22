IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE TABLE [Status] (
        [StatusId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK__Status__C8EE2063956ED592] PRIMARY KEY ([StatusId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE TABLE [TaskInfo] (
        [TaskId] int NOT NULL IDENTITY,
        [TaskName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK__TaskTabl__7C6949B1708A6B36] PRIMARY KEY ([TaskId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        CONSTRAINT [PK__Users__1788CC4CD18FFE6E] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE TABLE [UserTasks] (
        [UserTaskId] int NOT NULL IDENTITY,
        [UserId] int NULL,
        [TaskId] int NULL,
        [CreatedOn] datetime NULL,
        [CompletedOn] datetime NULL,
        [StatusId] int NULL,
        CONSTRAINT [PK__UserTask__4EF5961FC10484ED] PRIMARY KEY ([UserTaskId]),
        CONSTRAINT [FK__UserTasks__Statu__3F466844] FOREIGN KEY ([StatusId]) REFERENCES [Status] ([StatusId]),
        CONSTRAINT [FK__UserTasks__TaskI__3E52440B] FOREIGN KEY ([TaskId]) REFERENCES [TaskInfo] ([TaskId]),
        CONSTRAINT [FK__UserTasks__UserI__3D5E1FD2] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE INDEX [IX_UserTasks_StatusId] ON [UserTasks] ([StatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE INDEX [IX_UserTasks_TaskId] ON [UserTasks] ([TaskId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    CREATE INDEX [IX_UserTasks_UserId] ON [UserTasks] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701082726_azure'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240701082726_azure', N'8.0.6');
END;
GO

COMMIT;
GO

