CREATE   PROCEDURE sp_ManteGeneros
    @Id int,
    @Nombre NVARCHAR(100),
    @Tipo  NVARCHAR(100)
AS
BEGIN


    IF NOT EXISTS (
        SELECT 1 
        FROM Generos 
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(LTRIM(RTRIM(@Nombre)))
    )
    BEGIN
        -- Si no existe, insertarlo
        INSERT INTO Generos (IdGenero, Nombre,TipoContenido)
        VALUES (@Id, LTRIM(RTRIM(@Nombre)),@tipo);

        -- Retornar el ID del género insertado
        SELECT @Id as IdGenerado, 'INSERTADO' as Resultado;
    END
    ELSE
    BEGIN
        DECLARE @IdExistente int;

        SELECT @IdExistente = IdGenero 
        FROM Generos 
        WHERE LOWER(LTRIM(RTRIM(Nombre))) = LOWER(LTRIM(RTRIM(@Nombre)));

        SELECT @IdExistente as IdGenerado, 'EXISTE' as Resultado;
    END
END