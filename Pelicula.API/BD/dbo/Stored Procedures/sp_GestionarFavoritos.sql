

CREATE PROCEDURE sp_GestionarFavoritos
	@Accion NVARCHAR(10),
	@IdFavorito UNIQUEIDENTIFIER = NULL,
	@IdUsuario UNIQUEIDENTIFIER = NULL,
	@IdPelicula UNIQUEIDENTIFIER = NULL,
	@IdSerie UNIQUEIDENTIFIER = NULL,
	@Comentario NVARCHAR(MAX) = NULL,
	@Puntuacion INT = NULL
AS
BEGIN
	IF @Accion = 'Insertar'
	BEGIN
		INSERT INTO Favoritos (IdFavorito, IdUsuario, IdPelicula, IdSerie, Comentario, Puntuacion)
		VALUES (NEWID(), @IdUsuario, @IdPelicula, @IdSerie, @Comentario, @Puntuacion);
	END
	ELSE IF @Accion = 'Actualizar'
	BEGIN
		UPDATE Favoritos
		SET Comentario = @Comentario,
			Puntuacion = @Puntuacion
		WHERE IdFavorito = @IdFavorito;
	END
	ELSE IF @Accion = 'Eliminar'
	BEGIN
		DELETE FROM Favoritos
		WHERE IdFavorito = @IdFavorito;
	END
END;