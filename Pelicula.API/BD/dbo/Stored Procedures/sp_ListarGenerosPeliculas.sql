CREATE PROCEDURE sp_ListarGenerosPeliculas
AS
BEGIN
	SELECT IdGenero, Nombre
	FROM Generos
	WHERE TipoContenido = 'Pelicula';
END;