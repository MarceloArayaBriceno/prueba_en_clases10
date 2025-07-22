
CREATE   PROCEDURE sp_InsertarPelicula
    @IdPelicula int ,
    @Titulo NVARCHAR(255)=null,
    @ImagenUrl NVARCHAR(500) = NULL,
    @Descripcion NVARCHAR(1000) = NULL,
    @FechaEstreno DATE = NULL,
    @Calificacion DECIMAL(10,4) = NULL,
    @IdGenero INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdPelicula3 INT;

    -- Verificar si la película ya existe por título (case insensitive)
    SELECT @IdPelicula3 = IdPelicula 
    FROM Peliculas 
    WHERE IdPelicula = @IdPelicula;

    IF @IdPelicula3 IS NULL
    BEGIN
        -- Si no existe, insertarla
        INSERT INTO Peliculas (IdPelicula,Titulo, ImagenUrl, Descripcion, FechaEstreno, Calificacion, IdGenero)
        VALUES (
            @IdPelicula,
            LTRIM(RTRIM(@Titulo)), 
            @ImagenUrl, 
            @Descripcion, 
            @FechaEstreno, 
            @Calificacion, 
            @IdGenero
        );

        -- Obtener el ID generado automáticamente
        SET @IdPelicula = SCOPE_IDENTITY();

        -- Retornar el ID de la película insertada
        SELECT @IdPelicula as IdPelicula, 'INSERTADA' as Resultado;
    END
    ELSE
    BEGIN
        -- Retornar el ID existente
        SELECT @IdPelicula3 as IdPelicula, 'EXISTE' as Resultado;
    END
END