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

CREATE TABLE [Desenvolvedor] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    CONSTRAINT [PK_Desenvolvedor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Projeto] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NULL,
    CONSTRAINT [PK_Projeto] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DesenvolvedorProjeto] (
    [DesenvolvedorId] int NOT NULL,
    [ProjetoId] int NOT NULL,
    CONSTRAINT [PK_DesenvolvedorProjeto] PRIMARY KEY ([ProjetoId], [DesenvolvedorId]),
    CONSTRAINT [FK_DesenvolvedorProjeto_Desenvolvedor_DesenvolvedorId] FOREIGN KEY ([DesenvolvedorId]) REFERENCES [Desenvolvedor] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DesenvolvedorProjeto_Projeto_ProjetoId] FOREIGN KEY ([ProjetoId]) REFERENCES [Projeto] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LancamentoHoras] (
    [Id] int NOT NULL IDENTITY,
    [DataInicial] datetime2 NOT NULL,
    [DataFinal] datetime2 NOT NULL,
    [DesenvolvedorId] int NOT NULL,
    [ProjetoId] int NOT NULL,
    CONSTRAINT [PK_LancamentoHoras] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LancamentoHoras_Desenvolvedor_DesenvolvedorId] FOREIGN KEY ([DesenvolvedorId]) REFERENCES [Desenvolvedor] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LancamentoHoras_Projeto_ProjetoId] FOREIGN KEY ([ProjetoId]) REFERENCES [Projeto] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'Nome') AND [object_id] = OBJECT_ID(N'[Desenvolvedor]'))
    SET IDENTITY_INSERT [Desenvolvedor] ON;
INSERT INTO [Desenvolvedor] ([Id], [Cpf], [Nome])
VALUES (1, N'15648548545', N'Lauro'),
(2, N'94851451545', N'Roberto'),
(3, N'45180084610', N'Ronaldo'),
(4, N'00451104001', N'Rodrigo'),
(5, N'74050048122', N'Alexandre');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'Nome') AND [object_id] = OBJECT_ID(N'[Desenvolvedor]'))
    SET IDENTITY_INSERT [Desenvolvedor] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[Projeto]'))
    SET IDENTITY_INSERT [Projeto] ON;
INSERT INTO [Projeto] ([Id], [Descricao])
VALUES (1, N'Agendamento e Horas'),
(2, N'Bar e Mercadinhos'),
(3, N'Empresa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descricao') AND [object_id] = OBJECT_ID(N'[Projeto]'))
    SET IDENTITY_INSERT [Projeto] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DesenvolvedorId', N'ProjetoId') AND [object_id] = OBJECT_ID(N'[DesenvolvedorProjeto]'))
    SET IDENTITY_INSERT [DesenvolvedorProjeto] ON;
INSERT INTO [DesenvolvedorProjeto] ([DesenvolvedorId], [ProjetoId])
VALUES (2, 1),
(3, 1),
(1, 1),
(5, 2),
(1, 2),
(4, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DesenvolvedorId', N'ProjetoId') AND [object_id] = OBJECT_ID(N'[DesenvolvedorProjeto]'))
    SET IDENTITY_INSERT [DesenvolvedorProjeto] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataFinal', N'DataInicial', N'DesenvolvedorId', N'ProjetoId') AND [object_id] = OBJECT_ID(N'[LancamentoHoras]'))
    SET IDENTITY_INSERT [LancamentoHoras] ON;
INSERT INTO [LancamentoHoras] ([Id], [DataFinal], [DataInicial], [DesenvolvedorId], [ProjetoId])
VALUES (3, '2021-01-30T10:25:00.0000000', '2021-01-30T08:00:00.0000000', 2, 1),
(6, '2021-02-01T20:10:00.0000000', '2021-02-01T08:10:00.0000000', 3, 1),
(7, '2021-02-01T20:10:00.0000000', '2021-02-01T18:10:00.0000000', 1, 1),
(8, '2021-02-02T20:10:00.0000000', '2021-02-02T18:10:00.0000000', 1, 1),
(2, '2021-01-29T15:20:00.0000000', '2021-01-29T13:20:00.0000000', 5, 2),
(4, '2021-01-31T18:50:00.0000000', '2021-01-31T14:30:00.0000000', 5, 2),
(5, '2021-01-31T15:00:00.0000000', '2021-01-31T10:15:00.0000000', 1, 2),
(9, '2021-02-02T17:30:00.0000000', '2021-02-02T08:10:00.0000000', 5, 2),
(1, '2021-01-29T14:50:00.0000000', '2021-01-29T13:25:50.0000000', 4, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataFinal', N'DataInicial', N'DesenvolvedorId', N'ProjetoId') AND [object_id] = OBJECT_ID(N'[LancamentoHoras]'))
    SET IDENTITY_INSERT [LancamentoHoras] OFF;
GO

CREATE INDEX [IX_DesenvolvedorProjeto_DesenvolvedorId] ON [DesenvolvedorProjeto] ([DesenvolvedorId]);
GO

CREATE INDEX [IX_LancamentoHoras_DesenvolvedorId] ON [LancamentoHoras] ([DesenvolvedorId]);
GO

CREATE INDEX [IX_LancamentoHoras_ProjetoId] ON [LancamentoHoras] ([ProjetoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210201002425_inicial', N'5.0.2');
GO

COMMIT;
GO

