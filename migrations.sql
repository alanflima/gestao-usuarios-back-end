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
CREATE TABLE [Usuarios] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [SenhaHash] nvarchar(200) NOT NULL,
    [Ativo] bit NOT NULL,
    [CriadoEm] datetime2(0) NOT NULL,
    [AtualizadoEm] datetime2(0) NULL,
    [Cargo] nvarchar(100) NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

CREATE UNIQUE INDEX [IX_Usuarios_Email] ON [Usuarios] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260428123131_001_CriarTabelaUsuarios', N'10.0.7');

COMMIT;
GO

