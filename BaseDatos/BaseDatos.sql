
CREATE DATABASE MicroserviciosDevsu_Prac;
GO


USE MicroserviciosDevsu_Prac;
GO

-- una sola tabla x herencia entre cliente y persona dentro de las entidades
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),                  -- ID de la persona (clave primaria)
    Nombre NVARCHAR(100) NOT NULL,                       -- Nombre de la persona
    Genero NVARCHAR(20) NOT NULL,                       -- G�nero de la persona
    Edad INT NOT NULL,                                  -- Edad de la persona
    Identificacion NVARCHAR(50) NOT NULL,               -- Identificaci�n de la persona
    Direccion NVARCHAR(200) NOT NULL,                   -- Direcci�n de la persona
    Telefono NVARCHAR(20) NOT NULL,                     -- Tel�fono de la persona
    Contrasena NVARCHAR(100) NOT NULL,                  -- Contrase�a del cliente
    Estado NVARCHAR(50) NOT NULL,                       -- Estado del cliente (activo, inactivo, etc.)
    Tipo NVARCHAR(50) NOT NULL,                         -- Discriminador para tipo (Cliente o Persona)
);

--  Cuentas
CREATE TABLE Cuentas (
    CuentaId INT PRIMARY KEY IDENTITY(1,1),            -- ID de la cuenta (clave primaria)
    NumeroCuenta NVARCHAR(20) NOT NULL,                 -- N�mero de la cuenta
    TipoCuenta NVARCHAR(50) NOT NULL,                   -- Tipo de cuenta (ahorro, corriente, etc.)
    SaldoInicial DECIMAL(18,2) NOT NULL,                -- Saldo inicial de la cuenta
    SaldoDisponible DECIMAL(18,2) NOT NULL,             -- Saldo disponible de la cuenta
    Estado NVARCHAR(20) NOT NULL,                       -- Estado de la cuenta (activa, inactiva, etc.)
    ClienteId INT NULL,                                 -- Relaci�n con la tabla Clientes (clave for�nea)
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)  -- Relaci�n con la tabla Clientes
);

-- Movimientos
CREATE TABLE Movimientos (
    MovimientoId INT PRIMARY KEY IDENTITY(1,1),        -- ID del movimiento (clave primaria)
    FechaMovimiento DATETIME NOT NULL,                  -- Fecha del movimiento
    TipoMovimiento NVARCHAR(20) NOT NULL,               -- Tipo de movimiento (dep�sito, retiro, etc.)
    Valor DECIMAL(18,2) NOT NULL,                       -- Valor del movimiento
    Saldo DECIMAL(18,2) NOT NULL,                       -- Saldo resultante despu�s del movimiento
    CuentaId INT NOT NULL,                              -- Relaci�n con la cuenta (clave for�nea)
    FOREIGN KEY (CuentaId) REFERENCES Cuentas(CuentaId)  -- Relaci�n con la tabla Cuentas
);



