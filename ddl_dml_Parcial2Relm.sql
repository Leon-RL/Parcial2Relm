CREATE DATABASE Parcial2Relm;
GO

USE master
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'usrparcial')
    DROP LOGIN usrparcial; 
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'usrparcial2')
    DROP LOGIN usrparcial2;
GO

CREATE LOGIN usrparcial2 WITH PASSWORD = '12345678',
	CHECK_POLICY = ON,
	CHECK_EXPIRATION = OFF,
	DEFAULT_DATABASE = Parcial2Relm
GO

USE Parcial2Relm 
GO

IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'usrparcial')
    DROP USER usrparcial;
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'usrparcial2')
    DROP USER usrparcial2;
GO

CREATE USER usrparcial2 FOR LOGIN usrparcial2
GO

ALTER ROLE db_owner ADD MEMBER usrparcial2
GO

DROP TABLE IF EXISTS Programa;
DROP TABLE IF EXISTS Canal;

CREATE TABLE Canal (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  nombre VARCHAR(50) NOT NULL,
  frecuencia VARCHAR(20) NULL,
  estado SMALLINT NOT NULL DEFAULT 1,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Programa (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
  idCanal INT NOT NULL,
  titulo VARCHAR(100) NOT NULL,
  descripcion VARCHAR(250) NULL,
  duracion INT NULL, 
  productor VARCHAR(100) NULL,
  fechaEstreno DATE NULL,
  estado SMALLINT NOT NULL DEFAULT 1,
  usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
  fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
  clasificacion VARCHAR(15) NULL
);
GO

ALTER TABLE Programa
ADD CONSTRAINT FK_Programa_Canal
FOREIGN KEY (idCanal) REFERENCES Canal(id);
GO

DROP PROC IF EXISTS paProgramaListar;
GO
CREATE PROC paProgramaListar @parametro VARCHAR(100)
AS
  SELECT p.id, p.idCanal, p.titulo, p.descripcion, p.duracion, p.productor, p.fechaEstreno, p.estado
  FROM Programa p
  WHERE p.estado > -1 AND p.titulo + ISNULL(p.productor,'') LIKE '%'+REPLACE(@parametro,' ','%')+'%'
  ORDER BY p.titulo ASC;
GO

DROP PROC IF EXISTS paProgramaInsertar;
GO
CREATE PROC paProgramaInsertar
    @idCanal INT,
    @titulo VARCHAR(100),
    @descripcion VARCHAR(250),
    @duracion INT,
    @productor VARCHAR(100),
    @fechaEstreno DATE,
    @clasificacion VARCHAR(15)
AS
BEGIN
    INSERT INTO Programa (idCanal, titulo, descripcion, duracion, productor, fechaEstreno)
    VALUES (@idCanal, @titulo, @descripcion, @duracion, @productor, @fechaEstreno,@clasificaacion);

    SELECT SCOPE_IDENTITY() AS Id; 
END;
GO

DROP PROC IF EXISTS paProgramaActualizar;
GO
CREATE PROC paProgramaActualizar
    @id INT,
    @idCanal INT,
    @titulo VARCHAR(100),
    @descripcion VARCHAR(250),
    @duracion INT,
    @productor VARCHAR(100),
    @fechaEstreno DATE,
    @clasificaacion VARCHAR(15)
AS
BEGIN
    UPDATE Programa
    SET 
        idCanal = @idCanal,
        titulo = @titulo,
        descripcion = @descripcion,
        duracion = @duracion,
        productor = @productor,
        fechaEstreno = @fechaEstreno,
        clasificacion = @clasificaacion
    WHERE id = @id AND estado > -1;
END;
GO

DROP PROC IF EXISTS paProgramaEliminarLogico;
GO
CREATE PROC paProgramaEliminarLogico 
    @idPrograma INT
AS
    UPDATE Programa
    SET estado = -1
    WHERE id = @idPrograma AND estado > -1;
GO

DROP PROC IF EXISTS paProgramaListar;
GO
CREATE PROC paProgramaListar @parametro VARCHAR(100)
AS
  SELECT p.id, p.idCanal, p.titulo, p.descripcion, p.duracion, p.productor, p.fechaEstreno, p.estado, 
  p.clasificacion 
  FROM Programa p
  WHERE p.estado > -1 AND p.titulo + ISNULL(p.productor,'') LIKE '%'+REPLACE(@parametro,' ','%')+'%'
  ORDER BY p.titulo ASC;
GO

DROP PROC IF EXISTS paCanalInsertar;
GO
CREATE PROC paCanalInsertar
    @nombre VARCHAR(50),
    @frecuencia VARCHAR(20)
AS
BEGIN
    INSERT INTO Canal (nombre, frecuencia)
    VALUES (@nombre, @frecuencia);
    SELECT SCOPE_IDENTITY() AS Id; 
END;
GO

DROP PROC IF EXISTS paCanalActualizar;
GO
CREATE PROC paCanalActualizar
    @id INT,
    @nombre VARCHAR(50),
    @frecuencia VARCHAR(20)
AS
BEGIN
    UPDATE Canal
    SET 
        nombre = @nombre,
        frecuencia = @frecuencia
    WHERE id = @id AND estado > -1;
END;
GO

DROP PROC IF EXISTS paCanalEliminarLogico;
GO
CREATE PROC paCanalEliminarLogico 
    @idCanal INT
AS
    UPDATE Canal
    SET estado = -1
    WHERE id = @idCanal AND estado > -1;
GO

INSERT INTO Canal (nombre, frecuencia) VALUES 
('Noticias TV', '101.5 FM'), 
('Deportes Ficción', 'Canal 4 HD'), 
('Música Clásica', 'Canal 8'),
('Canal Eliminado (ID 4)', '99.9'),
('Documentales Plus', 'Canal 15 HD'),
('Cine y Series', 'Canal 12 SD');

INSERT INTO Programa (idCanal, titulo, descripcion, duracion, productor, fechaEstreno, clasificacion)
VALUES
(1, 'Noticiero Central 19h', 'Resumen de la política nacional.', 60, 'ProdNews', GETDATE(), 'ATP'),
(2, 'Futbol Hoy', 'Análisis del partido de la jornada.', 45, 'ProdSports', '2025-11-01', '13+'),
(3, 'Vida Salvaje', 'Documental sobre animales en su hábitat natural.', 50, 'ProdDocs', '2025-12-15', 'ATP'),
(4, 'Serie: El Misterio', 'Thriller de suspenso con giros inesperados.', 40, 'ProdCine', '2025-09-20', '18+'),
(5, 'Beethoven Sinfonía', 'Concierto en vivo desde Viena.', 90, 'ProdCultural', '2025-10-31', 'ATP');
EXEC paCanalEliminarLogico @idCanal = 4;
EXEC paProgramaEliminarLogico @idPrograma = 1;

SELECT * FROM Canal;
SELECT * FROM Programa;
GO