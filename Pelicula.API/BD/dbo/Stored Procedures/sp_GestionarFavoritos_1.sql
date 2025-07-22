CREATE PROCEDURE [dbo].[sp_GestionarFavoritos]
    @Accion NVARCHAR(20),
    @IdFavorito UNIQUEIDENTIFIER,
    @Email VARCHAR(100) = NULL,
    @IdPelicula INT = NULL,
    @IdSerie INT = NULL,
    @Comentario NVARCHAR(MAX) = NULL,
    @Puntuacion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
 
    DECLARE @IdUsuarioFinal UNIQUEIDENTIFIER;
 
    IF @Accion = 'Insertar'
    BEGIN
        SELECT @IdUsuarioFinal = IdUsuario
        FROM Usuario 
        WHERE Email = @Email;
 
        -- Si no existe el usuario con ese email
        IF @IdUsuarioFinal IS NULL
        BEGIN
            -- Crear nuevo usuario con el email proporcionado
            SET @IdUsuarioFinal = NEWID();
            INSERT INTO Usuario (IdUsuario, Email)
            VALUES (@IdUsuarioFinal, @Email);
            PRINT 'Usuario creado con ID: ' + CAST(@IdUsuarioFinal AS VARCHAR(36));
        END
        ELSE
        BEGIN
            PRINT 'Usuario existente encontrado con ID: ' + CAST(@IdUsuarioFinal AS VARCHAR(36));
        END
        INSERT INTO Favoritos (IdFavorito, IdUsuario, IdPelicula, IdSerie, Comentario, Puntuacion)
        VALUES (@IdFavorito, @IdUsuarioFinal, @IdPelicula, @IdSerie, @Comentario, @Puntuacion);
    END
 
    IF @Accion = 'Eliminar'
    BEGIN
        DELETE FROM Favoritos
        WHERE IdFavorito = @IdFavorito;
    END
END;