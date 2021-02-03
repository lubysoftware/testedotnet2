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

BEGIN TRANSACTION;
GO

ALTER TABLE [Projetos] DROP CONSTRAINT [PK_Projetos];
GO

ALTER TABLE [LancamentoHorass] DROP CONSTRAINT [PK_LancamentoHorass];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projetos]') AND [c].[name] = N'DesenvolvedorId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Projetos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Projetos] DROP COLUMN [DesenvolvedorId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LancamentoHorass]') AND [c].[name] = N'Desenvolvedor');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LancamentoHorass] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [LancamentoHorass] DROP COLUMN [Desenvolvedor];
GO

EXEC sp_rename N'[Projetos]', N'Projeto';
GO

EXEC sp_rename N'[LancamentoHorass]', N'LancamentoHoras';
GO

ALTER TABLE [LancamentoHoras] ADD [DesenvolvedorId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [LancamentoHoras] ADD [ProjetoId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Projeto] ADD CONSTRAINT [PK_Projeto] PRIMARY KEY ([Id]);
GO

ALTER TABLE [LancamentoHoras] ADD CONSTRAINT [PK_LancamentoHoras] PRIMARY KEY ([Id]);
GO

CREATE TABLE [Desenvolvedor] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    CONSTRAINT [PK_Desenvolvedor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProjetoDesenvolvedor] (
    [ProjetoId] int NOT NULL,
    [DesenvolvedorId] int NOT NULL,
    CONSTRAINT [PK_ProjetoDesenvolvedor] PRIMARY KEY ([ProjetoId], [DesenvolvedorId]),
    CONSTRAINT [FK_ProjetoDesenvolvedor_Desenvolvedor_DesenvolvedorId] FOREIGN KEY ([DesenvolvedorId]) REFERENCES [Desenvolvedor] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjetoDesenvolvedor_Projeto_ProjetoId] FOREIGN KEY ([ProjetoId]) REFERENCES [Projeto] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_LancamentoHoras_DesenvolvedorId] ON [LancamentoHoras] ([DesenvolvedorId]);
GO

CREATE INDEX [IX_LancamentoHoras_ProjetoId] ON [LancamentoHoras] ([ProjetoId]);
GO

CREATE INDEX [IX_ProjetoDesenvolvedor_DesenvolvedorId] ON [ProjetoDesenvolvedor] ([DesenvolvedorId]);
GO

ALTER TABLE [LancamentoHoras] ADD CONSTRAINT [FK_LancamentoHoras_Desenvolvedor_DesenvolvedorId] FOREIGN KEY ([DesenvolvedorId]) REFERENCES [Desenvolvedor] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [LancamentoHoras] ADD CONSTRAINT [FK_LancamentoHoras_Projeto_ProjetoId] FOREIGN KEY ([ProjetoId]) REFERENCES [Projeto] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210130012502_Inicial_2', N'5.0.2');
GO

COMMIT;
GO

