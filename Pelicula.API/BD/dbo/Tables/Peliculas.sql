CREATE TABLE [dbo].[Peliculas] (
    [IdPelicula]   INT            NOT NULL,
    [Titulo]       NVARCHAR (200) NOT NULL,
    [ImagenUrl]    NVARCHAR (MAX) NULL,
    [Descripcion]  NVARCHAR (MAX) NULL,
    [FechaEstreno] DATE           NULL,
    [Calificacion] FLOAT (53)     NULL,
    [IdGenero]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPelicula] ASC),
    FOREIGN KEY ([IdGenero]) REFERENCES [dbo].[Generos] ([IdGenero])
);

