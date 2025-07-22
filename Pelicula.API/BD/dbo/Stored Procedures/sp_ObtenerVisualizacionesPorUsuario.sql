CREATE PROCEDURE sp_ObtenerVisualizacionesPorUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM VisualizacionesPendientes
    WHERE IdUsuario = @IdUsuario
END