CREATE DATABASE GestionContenido;

USE GestionContenido;

-- Usuarios
CREATE TABLE Usuario (
    IdUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

ALTER TABLE Usuario
ALTER COLUMN Nombre NVARCHAR(100) NULL;


CREATE TABLE Generos (
    IdGenero INT PRIMARY KEY,  -- sin IDENTITY
    Nombre NVARCHAR(100) NOT NULL,
    TipoContenido NVARCHAR(10) CHECK (TipoContenido IN ('Pelicula', 'Serie')) NOT NULL
);

-- 4. Verifica los datos y si todo está bien, puedes eliminar la tabla antigua
-- DROP TABLE Generos_Old;

-- Películas
CREATE TABLE Peliculas (
    IdPelicula INT PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    ImagenUrl NVARCHAR(MAX),
    Descripcion NVARCHAR(MAX),
    FechaEstreno DATE,
    Calificacion FLOAT,
    IdGenero INT NOT NULL,
    FOREIGN KEY (IdGenero) REFERENCES Generos(IdGenero)
);

-- Series
CREATE TABLE Series (
    IdSerie INT PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    ImagenUrl NVARCHAR(MAX),
    Descripcion NVARCHAR(MAX),
    FechaEstreno DATE,
    Calificacion FLOAT,
    IdGenero INT NOT NULL,
    FOREIGN KEY (IdGenero) REFERENCES Generos(IdGenero)
);

USE GestionContenido;
-- Favoritos
CREATE TABLE Favoritos (
    IdFavorito UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    IdUsuario UNIQUEIDENTIFIER NOT NULL,
    IdPelicula INT NULL,
    IdSerie INT NULL,
    Comentario NVARCHAR(MAX),
    Puntuacion INT CHECK (Puntuacion BETWEEN 1 AND 10),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdPelicula) REFERENCES Peliculas(IdPelicula),
    FOREIGN KEY (IdSerie) REFERENCES Series(IdSerie)
);

-- Visualizaciones pendientes
CREATE TABLE VisualizacionesPendientes (
    IdPendiente UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    IdUsuario UNIQUEIDENTIFIER NOT NULL,
    IdPelicula INT NULL,
    IdSerie INT NULL,
    Prioridad INT CHECK (Prioridad BETWEEN 1 AND 3),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdPelicula) REFERENCES Peliculas(IdPelicula),
    FOREIGN KEY (IdSerie) REFERENCES Series(IdSerie)
);

INSERT INTO Usuario (Nombre, Email)
VALUES 
('Carlos Ramírez', 'carlos.ramirez@example.com'),
('Ana Fernández', 'ana.fernandez@example.com'),
('Luis Mora', 'luis.mora@example.com'),
('María Gómez', 'maria.gomez@example.com');

CREATE PROCEDURE [dbo].[sp_ObtenerFavoritos]
AS
BEGIN
    SET NOCOUNT ON;

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
    LEFT JOIN Series s ON f.IdSerie = s.IdSerie;
END
GO

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
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_ObtenerFavoritosPorUsuario]
    @IdUsuario varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

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
    WHERE u.Email = @IdUsuario;
END
GO



CREATE OR ALTER PROCEDURE sp_ManteGeneros
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

CREATE OR ALTER PROCEDURE sp_InsertarPelicula
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

USE GestionContenido;
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


-- Triggers

-- Validar solo una referencia en Pendientes
CREATE TRIGGER tr_ValidarPendientes
ON VisualizacionesPendientes
INSTEAD OF INSERT
AS
BEGIN
	IF EXISTS (
		SELECT * FROM inserted
		WHERE (IdPelicula IS NULL AND IdSerie IS NULL) OR
			(IdPelicula IS NOT NULL AND IdSerie IS NOT NULL)
	)
	BEGIN 
		RAISERROR ('Debe especificarse solo una referencia: Pelicula o Serie.', 16, 1);
		RETURN;
	END

	INSERT INTO VisualizacionesPendientes (IdPendiente, IdUsuario, IdPelicula, IdSerie, Prioridad, FechaRegistro)
	SELECT IdPendiente, IdUsuario, IdPelicula, IdSerie, Prioridad, FechaRegistro
	FROM inserted;
END;

-- Procedimientos Almacenados
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



CREATE PROCEDURE sp_ListarSeriesPorGenero
	@IdGenero int
AS
BEGIN
	SELECT
		Titulo,
		ImagenUrl,
		Descripcion,
		FechaEstreno,
		Calificacion
	FROM Series
	WHERE IdGenero = @IdGenero;
END;



CREATE PROCEDURE sp_ListarGenerosPeliculas
AS
BEGIN
	SELECT IdGenero, Nombre
	FROM Generos
	WHERE TipoContenido = 'Pelicula';
END;



CREATE PROCEDURE sp_ListarGenerosSeries
AS
BEGIN
	SELECT IdGenero, Nombre
	FROM Generos
	WHERE TipoContenido = 'Serie';
END;

SELECT * FROM usuario;


USE GestionContenido;
GO

IF OBJECT_ID('dbo.VisualizacionesPendientes','U') IS NOT NULL
    DROP TABLE dbo.VisualizacionesPendientes;
GO


USE GestionContenido;
GO
CREATE TABLE dbo.VisualizacionesPendientes (
    IdPendiente   UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    IdUsuario     UNIQUEIDENTIFIER NOT NULL,
    IdPelicula    INT               NULL,
    IdSerie       INT               NULL,
    Prioridad     INT     NOT NULL  CHECK (Prioridad BETWEEN 1 AND 3),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Visualiza_IdUsuario FOREIGN KEY (IdUsuario)
        REFERENCES dbo.Usuario (IdUsuario)
);
GO



IF OBJECT_ID('dbo.sp_GestionarPendientesVisualizacion','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GestionarPendientesVisualizacion;
GO

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
GO

use GestionContenido
CREATE PROCEDURE sp_ObtenerVisualizacionesPorUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM VisualizacionesPendientes
    WHERE IdUsuario = @IdUsuario
END
