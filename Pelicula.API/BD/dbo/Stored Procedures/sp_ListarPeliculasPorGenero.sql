CREATE PROCEDURE sp_ListarPeliculasPorGenero
	@IdGenero int
AS
BEGIN 
	SELECT
		Titulo,
		ImagenUrl,
		Descripcion,
		FechaEstreno,
		Calificacion
	FROM Peliculas
	WHERE IdGenero = @IdGenero;
END;