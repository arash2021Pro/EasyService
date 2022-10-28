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

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [PhoneNumber] nvarchar(11) NULL,
    [Password] nvarchar(max) NULL,
    [IsAdmin] nvarchar(max) NULL,
    [IsGender] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [ModificationTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppServices] (
    [Id] int NOT NULL IDENTITY,
    [Gmail] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(11) NULL,
    [UserId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [ModificationTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AppServices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AppServices_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [CommentMessage] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [ModificationTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AppServices_UserId] ON [AppServices] ([UserId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221024183351_init', N'6.0.10');
GO

COMMIT;
GO

