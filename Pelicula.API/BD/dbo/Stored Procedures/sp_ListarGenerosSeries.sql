


CREATE PROCEDURE sp_ListarGenerosSeries
AS
BEGIN
	SELECT IdGenero, Nombre
	FROM Generos
	WHERE TipoContenido = 'Serie';
END;