using System;
using System.Collections.Generic;

namespace Desafio1App.Modelos
{
    public enum RolUsuario
    {
        Administrador,
        PersonalSalud
    }

    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string NombreCompleto { get; set; }
        public RolUsuario Rol { get; set; }

        public Usuario(string nombreUsuario, string contraseña, string nombreCompleto, RolUsuario rol)
        {
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
            NombreCompleto = nombreCompleto;
            Rol = rol;
        }

        public bool EsAdministrador => Rol == RolUsuario.Administrador;
        public string DescripcionRol => Rol == RolUsuario.Administrador ? "Administrador" : "Personal de Salud";
    }

    public static class GestorUsuarios
    {
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario("admin", "admin123", "Administrador del Sistema", RolUsuario.Administrador),
            new Usuario("medico", "medico123", "Dr. Juan Pérez", RolUsuario.PersonalSalud),
            new Usuario("enfermera", "enf123", "María García", RolUsuario.PersonalSalud)
        };

        public static Usuario ValidarCredenciales(string nombreUsuario, string contraseña)
        {
            return usuarios.Find(u => 
                u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase) && 
                u.Contraseña == contraseña);
        }

        public static List<Usuario> ObtenerTodos()
        {
            return new List<Usuario>(usuarios);
        }
    }
}
