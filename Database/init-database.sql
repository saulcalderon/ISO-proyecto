
-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    NombreCompleto NVARCHAR(100) NOT NULL,
    Rol INT NOT NULL, -- 0: Administrador, 1: PersonalSalud
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla de Pacientes
CREATE TABLE Pacientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Edad INT NOT NULL CHECK (Edad >= 1 AND Edad <= 120),
    Genero NVARCHAR(20) NOT NULL,
    TipoSangre NVARCHAR(5) NOT NULL,
    PresionArterial NVARCHAR(20) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

-- Índices para búsquedas
CREATE INDEX IX_Pacientes_Genero ON Pacientes(Genero);
CREATE INDEX IX_Pacientes_TipoSangre ON Pacientes(TipoSangre);
CREATE INDEX IX_Pacientes_PresionArterial ON Pacientes(PresionArterial);

-- Insertar usuarios por defecto
-- admin: admin123
-- medico: medico123
-- enfermera: enfermera123
INSERT INTO Usuarios (NombreUsuario, Contrasena, NombreCompleto, Rol) VALUES
('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Administrador del Sistema', 0),
('medico', '673ab82a6530ee3bd9b04ee72a4d66afa7fa059aedc685cf44e35d29d90ebafa', 'Dr. Juan Pérez', 1),
('enfermera', '5355f2cc3fde92dad92ea6470dca32fab53351d8d243cc7467334e56b9c1a381', 'María García', 1);

-- Insertar algunos pacientes de prueba
INSERT INTO Pacientes (Nombre, Edad, Genero, TipoSangre, PresionArterial) VALUES
('Carlos Martínez', 35, 'Masculino', 'O+', 'Normal'),
('Ana López', 28, 'Femenino', 'A+', 'Baja'),
('Pedro Hernández', 45, 'Masculino', 'B+', 'Alta'),
('María Rodríguez', 52, 'Femenino', 'O+', 'Alta'),
('José García', 31, 'Masculino', 'AB+', 'Normal');
