
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
    Telefono NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Direccion NVARCHAR(200) NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

-- Tabla de Citas
CREATE TABLE Citas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PacienteId INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    Motivo NVARCHAR(200) NOT NULL,
    Estado INT NOT NULL DEFAULT 0, -- 0: Pendiente, 1: Confirmada, 2: Completada, 3: Cancelada
    Observaciones NVARCHAR(500) NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Citas_Pacientes FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id)
);

-- Índices para búsquedas
CREATE INDEX IX_Pacientes_Genero ON Pacientes(Genero);
CREATE INDEX IX_Pacientes_TipoSangre ON Pacientes(TipoSangre);
CREATE INDEX IX_Pacientes_PresionArterial ON Pacientes(PresionArterial);
CREATE INDEX IX_Citas_PacienteId ON Citas(PacienteId);
CREATE INDEX IX_Citas_FechaHora ON Citas(FechaHora);
CREATE INDEX IX_Citas_Estado ON Citas(Estado);

-- Insertar usuarios por defecto
-- admin: admin123
-- medico: medico123
-- enfermera: enfermera123
INSERT INTO Usuarios (NombreUsuario, Contrasena, NombreCompleto, Rol) VALUES
('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Administrador del Sistema', 0),
('medico', '673ab82a6530ee3bd9b04ee72a4d66afa7fa059aedc685cf44e35d29d90ebafa', 'Dr. Juan Pérez', 1),
('enfermera', '5355f2cc3fde92dad92ea6470dca32fab53351d8d243cc7467334e56b9c1a381', 'María García', 1);

-- Insertar algunos pacientes de prueba
INSERT INTO Pacientes (Nombre, Edad, Genero, TipoSangre, PresionArterial, Telefono, Email, Direccion) VALUES
('Carlos Martínez', 35, 'Masculino', 'O+', 'Normal', '7890-1234', 'carlos.martinez@email.com', 'Col. Escalón, San Salvador'),
('Ana López', 28, 'Femenino', 'A+', 'Baja', '7890-5678', 'ana.lopez@email.com', 'Col. Miramonte, San Salvador'),
('Pedro Hernández', 45, 'Masculino', 'B+', 'Alta', '7890-9012', 'pedro.hernandez@email.com', 'Santa Tecla, La Libertad'),
('María Rodríguez', 52, 'Femenino', 'O+', 'Alta', '7890-3456', 'maria.rodriguez@email.com', 'Antiguo Cuscatlán, La Libertad'),
('José García', 31, 'Masculino', 'AB+', 'Normal', '7890-7890', 'jose.garcia@email.com', 'Soyapango, San Salvador');

-- Insertar algunas citas de prueba
INSERT INTO Citas (PacienteId, FechaHora, Motivo, Estado, Observaciones) VALUES
(1, DATEADD(day, 1, GETDATE()), 'Consulta de rutina', 0, 'Primera visita del año'),
(2, DATEADD(day, 2, GETDATE()), 'Control de presión arterial', 1, 'Seguimiento mensual'),
(3, DATEADD(day, -1, GETDATE()), 'Examen de sangre', 2, 'Resultados pendientes');
