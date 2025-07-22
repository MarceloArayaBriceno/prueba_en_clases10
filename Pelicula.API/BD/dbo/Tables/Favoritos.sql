CREATE TABLE [dbo].[Favoritos] (
    [IdFavorito]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdUsuario]     UNIQUEIDENTIFIER NOT NULL,
    [IdPelicula]    UNIQUEIDENTIFIER NULL,
    [IdSerie]       UNIQUEIDENTIFIER NULL,
    [Comentario]    NVARCHAR (MAX)   NULL,
    [Puntuacion]    INT              NULL,
    [FechaRegistro] DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([IdFavorito] ASC),
    CHECK ([Puntuacion]>=(1) AND [Puntuacion]<=(10)),
    FOREIGN KEY ([IdPelicula]) REFERENCES [dbo].[Peliculas] ([IdPelicula]),
    FOREIGN KEY ([IdSerie]) REFERENCES [dbo].[Series] ([IdSerie]),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);

