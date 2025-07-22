
CREATE PROCEDURE dbo.sp_GestionarPendientesVisualizacion
    @Accion       NVARCHAR(10),
    @IdPendiente  UNIQUEIDENTIFIER = NULL,
    @IdUsuario    UNIQUEIDENTIFIER = NULL,
    @IdPelicula   INT               = NULL,
    @IdSerie      INT               = NULL,
    @Prioridad    INT               = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'Insertar'
    BEGIN
        INSERT INTO dbo.VisualizacionesPendientes
            (IdPendiente, IdUsuario, IdPelicula, IdSerie, Prioridad)
        VALUES
            (NEWID(), @IdUsuario, @IdPelicula, @IdSerie, @Prioridad);
        RETURN;
    END

    IF @Accion = 'Actualizar'
    BEGIN
        UPDATE dbo.VisualizacionesPendientes
           SET Prioridad = @Prioridad
         WHERE IdPendiente = @IdPendiente;
        RETURN;
    END

    IF @Accion = 'Eliminar'
    BEGIN
        DELETE FROM dbo.VisualizacionesPendientes
         WHERE IdPendiente = @IdPendiente;
    END
END;