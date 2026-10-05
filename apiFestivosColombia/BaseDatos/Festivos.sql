-- Base de datos FESTIVOS (estructura + datos)
IF DB_ID('Festivos') IS NULL
	CREATE DATABASE Festivos
GO

USE Festivos
GO

IF OBJECT_ID('Tipo') IS NULL
	CREATE TABLE Tipo(
		Id int IDENTITY(1,1) NOT NULL,
		CONSTRAINT pkTipo_Id PRIMARY KEY (Id),
		Tipo VARCHAR(100) NOT NULL
		);

IF OBJECT_ID('Pais') IS NULL
	CREATE TABLE Pais(
		Id int IDENTITY(1,1) NOT NULL,
		CONSTRAINT pkPais_Id PRIMARY KEY (Id),
		Nombre VARCHAR(100) NOT NULL
		);

IF OBJECT_ID('Festivo') IS NULL
	CREATE TABLE Festivo(
		Id int IDENTITY(1,1) NOT NULL,
		CONSTRAINT pkFestivo_Id PRIMARY KEY (Id),
		Nombre VARCHAR(100) NOT NULL,
		Dia INT NOT NULL,
		Mes INT NOT NULL,
		DiasPascua INT NOT NULL,
		IdTipo INT NOT NULL,
		CONSTRAINT fkFestivo_Tipo FOREIGN KEY (IdTipo) REFERENCES Tipo(Id),
		IdPais INT NOT NULL,
		CONSTRAINT fkFestivo_Pais FOREIGN KEY (IdPais) REFERENCES Pais(Id)
		);
GO

-- Registros tabla TIPO
IF NOT EXISTS(SELECT * FROM Tipo)
BEGIN
	SET IDENTITY_INSERT Tipo ON;
	INSERT INTO Tipo(Id, Tipo) VALUES(1, 'Fijo');
	INSERT INTO Tipo(Id, Tipo) VALUES(2, 'Ley Puente Festivo');
	INSERT INTO Tipo(Id, Tipo) VALUES(3, 'Basado en Pascua');
	INSERT INTO Tipo(Id, Tipo) VALUES(4, 'Basado en Pascua y Ley Puente Festivo');
	INSERT INTO Tipo(Id, Tipo) VALUES(5, 'Ley Puente Festivo Viernes');
	SET IDENTITY_INSERT Tipo OFF;
END
GO

-- Registros tabla PAIS
IF NOT EXISTS(SELECT * FROM Pais)
BEGIN
	SET IDENTITY_INSERT Pais ON;
	INSERT INTO Pais (Id, Nombre) VALUES( 1,'COLOMBIA');
	INSERT INTO Pais (Id, Nombre) VALUES( 2,'ARGENTINA');
	INSERT INTO Pais (Id, Nombre) VALUES( 3,'BOLIVIA');
	INSERT INTO Pais (Id, Nombre) VALUES( 4,'BRASIL');
	INSERT INTO Pais (Id, Nombre) VALUES( 5,'CANADA');
	INSERT INTO Pais (Id, Nombre) VALUES( 6,'COSTA RICA');
	INSERT INTO Pais (Id, Nombre) VALUES( 7,'REPUBLICA DOMINICANA');
	INSERT INTO Pais (Id, Nombre) VALUES( 8,'CUBA');
	INSERT INTO Pais (Id, Nombre) VALUES( 9,'CHILE');
	INSERT INTO Pais (Id, Nombre) VALUES(10,'ECUADOR');
	INSERT INTO Pais (Id, Nombre) VALUES(11,'ESTADOS UNIDOS DE AMÉRICA');
	INSERT INTO Pais (Id, Nombre) VALUES(12,'GUATEMALA');
	INSERT INTO Pais (Id, Nombre) VALUES(13,'HONDURAS');
	INSERT INTO Pais (Id, Nombre) VALUES(14,'MÉXICO');
	INSERT INTO Pais (Id, Nombre) VALUES(15,'NICARAGUA');
	INSERT INTO Pais (Id, Nombre) VALUES(16,'PANAMA');
	INSERT INTO Pais (Id, Nombre) VALUES(17,'PARAGUAY');
	INSERT INTO Pais (Id, Nombre) VALUES(18,'PERU');
	INSERT INTO Pais (Id, Nombre) VALUES(19,'URUGUAY');
	INSERT INTO Pais (Id, Nombre) VALUES(20,'VENEZUELA');
	INSERT INTO Pais (Id, Nombre) VALUES(21,'ESPAÑA');
	SET IDENTITY_INSERT Pais OFF;
END
GO

-- Registros tabla FESTIVO
IF NOT EXISTS(SELECT * FROM Festivo)
BEGIN
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 1, 1, 'Año nuevo', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 6, 1, 'Santos Reyes', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 19, 3, 'San José', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Jueves Santo', 3, -3);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Viernes Santo', 3, -2);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Domingo de Pascua', 3, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 1, 5, 'Día del Trabajo', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Ascensión del Señor', 4, 40);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Corpus Christi', 4, 61);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 0, 0, 'Sagrado Corazón de Jesús', 4, 68);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 29, 6, 'San Pedro y San Pablo', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 20, 7, 'Independencia Colombia', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 7, 8, 'Batalla de Boyacá', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 15, 8, 'Asunción de la Virgen', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 12, 10, 'Día de la Raza', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 1, 11, 'Todos los santos', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 11, 11, 'Independencia de Cartagena', 2, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 8, 12, 'Inmaculada Concepción', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(1, 25, 12, 'Navidad', 1, 0);

	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 1, 1, 'Año nuevo', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 0, 0, 'Carnaval 1', 3, -43);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 0, 0, 'Carnaval 2', 3, -42);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 0, 0, 'Viernes Santo', 3, -2);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 1, 5, 'Día del Trabajo', 5, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 24, 5, 'Batalla de Pichincha', 1, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 10, 8, 'Primer Grito de Independencia', 5, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 9, 10, 'Independencia de Guayaquil', 5, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 2, 11, 'Día de los Difuntos', 5, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 3, 11, 'Independencia de Cuenca', 5, 0);
	INSERT INTO Festivo (IdPais, Dia, Mes, Nombre, IdTipo, DiasPascua) VALUES(10, 25, 12, 'Navidad', 5, 0);
END
GO
