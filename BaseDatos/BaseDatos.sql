
CREATE DATABASE MicroserviciosDevsu_Prac;
GO


USE MicroserviciosDevsu_Prac;
GO

-- una sola tabla x herencia entre cliente y persona dentro de las entidades
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),                  -- ID de la persona (clave primaria)
    Nombre NVARCHAR(100) NOT NULL,                       -- Nombre de la persona
    Genero NVARCHAR(20) NOT NULL,                       -- Género de la persona
    Edad INT NOT NULL,                                  -- Edad de la persona
    Identificacion NVARCHAR(50) NOT NULL,               -- Identificación de la persona
    Direccion NVARCHAR(200) NOT NULL,                   -- Dirección de la persona
    Telefono NVARCHAR(20) NOT NULL,                     -- Teléfono de la persona
    Contrasena NVARCHAR(100) NOT NULL,                  -- Contraseña del cliente
    Estado NVARCHAR(50) NOT NULL,                       -- Estado del cliente (activo, inactivo, etc.)
    Tipo NVARCHAR(50) NOT NULL,                         -- Discriminador para tipo (Cliente o Persona)
);

--  Cuentas
CREATE TABLE Cuentas (
    CuentaId INT PRIMARY KEY IDENTITY(1,1),            -- ID de la cuenta (clave primaria)
    NumeroCuenta NVARCHAR(20) NOT NULL,                 -- Número de la cuenta
    TipoCuenta NVARCHAR(50) NOT NULL,                   -- Tipo de cuenta (ahorro, corriente, etc.)
    SaldoInicial DECIMAL(18,2) NOT NULL,                -- Saldo inicial de la cuenta
    SaldoDisponible DECIMAL(18,2) NOT NULL,             -- Saldo disponible de la cuenta
    Estado NVARCHAR(20) NOT NULL,                       -- Estado de la cuenta (activa, inactiva, etc.)
    ClienteId INT NULL,                                 -- Relación con la tabla Clientes (clave foránea)
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)  -- Relación con la tabla Clientes
);

-- Movimientos
CREATE TABLE Movimientos (
    MovimientoId INT PRIMARY KEY IDENTITY(1,1),        -- ID del movimiento (clave primaria)
    FechaMovimiento DATETIME NOT NULL,                  -- Fecha del movimiento
    TipoMovimiento NVARCHAR(20) NOT NULL,               -- Tipo de movimiento (depósito, retiro, etc.)
    Valor DECIMAL(18,2) NOT NULL,                       -- Valor del movimiento
    Saldo DECIMAL(18,2) NOT NULL,                       -- Saldo resultante después del movimiento
    CuentaId INT NOT NULL,                              -- Relación con la cuenta (clave foránea)
    FOREIGN KEY (CuentaId) REFERENCES Cuentas(CuentaId)  -- Relación con la tabla Cuentas
);



