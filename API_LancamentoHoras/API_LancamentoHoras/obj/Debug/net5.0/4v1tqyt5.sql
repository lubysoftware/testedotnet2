﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

CREATE TABLE [LancamentoHorass] (
    [Id] int NOT NULL IDENTITY,
    [DataInicial] datetime2 NOT NULL,
    [DataFinal] datetime2 NOT NULL,
    [Desenvolvedor] nvarchar(max) NULL,
    CONSTRAINT [PK_LancamentoHorass] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Projetos] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NULL,
    [DesenvolvedorId] int NOT NULL,
    CONSTRAINT [PK_Projetos] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210129230419_Inicial_1', N'5.0.2');
GO

COMMIT;
GO

