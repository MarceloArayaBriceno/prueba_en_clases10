CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Nombre]    NVARCHAR (100)   NULL,
    [Email]     NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

