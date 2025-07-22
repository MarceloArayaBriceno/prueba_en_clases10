
--autor marcelo araya
CREATE TABLE [dbo].[VisualizacionesPendientes] (
    [IdPendiente]   UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [IdUsuario]     UNIQUEIDENTIFIER NOT NULL,
    [IdPelicula]    INT              NULL,
    [IdSerie]       INT              NULL,
    [Prioridad]     INT              NULL,
    [FechaRegistro] DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([IdPendiente] ASC),
    CHECK ([Prioridad]>=(1) AND [Prioridad]<=(3)),
    FOREIGN KEY ([IdPelicula]) REFERENCES [dbo].[Peliculas] ([IdPelicula]),
    FOREIGN KEY ([IdSerie]) REFERENCES [dbo].[Series] ([IdSerie]),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);


GO
CREATE TRIGGER tr_ValidarPendientes
ON VisualizacionesPendientes
INSTEAD OF INSERT
AS
BEGIN
	IF EXISTS (
		SELECT * FROM inserted
		WHERE (IdPelicula IS NULL AND IdSerie IS NULL) OR
			(IdPelicula IS NOT NULL AND IdSerie IS NOT NULL)
	)
	BEGIN 
		RAISERROR ('Debe especificarse solo una referencia: Pelicula o Serie.', 16, 1);
		RETURN;
	END

	INSERT INTO VisualizacionesPendientes (IdPendiente, IdUsuario, IdPelicula, IdSerie, Prioridad, FechaRegistro)
	SELECT IdPendiente, IdUsuario, IdPelicula, IdSerie, Prioridad, FechaRegistro
	FROM inserted;
END;