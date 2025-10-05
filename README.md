# Sistema de Clasificación de Sangre (SCS V1.0)

## Descripción

Sistema de escritorio para Windows que gestiona el registro de pacientes y los clasifica automáticamente según género, tipo de sangre y presión arterial. Diseñado para mejorar la toma de decisiones en instituciones de salud privadas.

## Tecnologías

- **Lenguaje**: C#
- **Framework**: .NET Framework 4.8+
- **Base de Datos**: SQL Server Express
- **Plataforma**: Windows 10 o superior

## Requisitos del Sistema

- **Procesador**: Intel i3 o superior
- **RAM**: 4 GB mínimo
- **Almacenamiento**: 500 MB disponibles
- **Sistema Operativo**: Windows 10 o superior

## Funcionalidades Principales

- **Autenticación de usuarios**: Control de acceso con credenciales y perfiles
- **Gestión de pacientes**: Alta, edición y baja de registros (ABM)
- **Búsqueda y consulta**: Filtrado por género, tipo de sangre y presión arterial
- **Clasificación automática**: Organización jerárquica en estructura de árbol
- **Reportes y estadísticas**: Generación de informes con gráficos y tablas

## Instalación y Ejecución

1. Asegúrese de tener instalado .NET Framework 4.8 o superior
2. Configure SQL Server Express en su equipo
3. Abra la solución `Desafio1App.sln` en Visual Studio
4. Compile y ejecute el proyecto

## Estructura del Proyecto

```
Desafio1App/
├── Forms/          # Formularios de la interfaz de usuario
├── Modelos/        # Clases de modelo de datos
├── Arbol/          # Lógica de clasificación jerárquica
└── Program.cs      # Punto de entrada de la aplicación
```

## Autor

Proyecto desarrollado como parte del desafío SCS V1.0
