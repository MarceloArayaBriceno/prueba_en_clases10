CREATE PROCEDURE sp_InsertarSerie
    @IdSerie INT,
    @Titulo NVARCHAR(255),
    @ImagenUrl NVARCHAR(500),
    @Descripcion NVARCHAR(MAX),
    @FechaEstreno DATE,
    @Calificacion FLOAT,
    @IdGenero INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Series WHERE IdSerie = @IdSerie)
    BEGIN
        INSERT INTO Series (IdSerie, Titulo, ImagenUrl, Descripcion, FechaEstreno, Calificacion, IdGenero)
        VALUES (@IdSerie, @Titulo, @ImagenUrl, @Descripcion, @FechaEstreno, @Calificacion, @IdGenero);
    END
END;