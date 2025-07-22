
CREATE PROCEDURE sp_ObtenerFavorito
    @IdFavorito UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        f.IdFavorito,
        f.IdUsuario,
        u.Nombre AS NombreUsuario,
        f.IdPelicula,
        p.Titulo AS TituloPelicula,
        f.IdSerie,
        s.Titulo AS TituloSerie,
        f.Comentario,
        f.Puntuacion,
        f.FechaRegistro
    FROM Favoritos f
    LEFT JOIN Usuario u ON f.IdUsuario = u.IdUsuario
    LEFT JOIN Peliculas p ON f.IdPelicula = p.IdPelicula
    LEFT JOIN Series s ON f.IdSerie = s.IdSerie
    WHERE f.IdFavorito = @IdFavorito;
END;