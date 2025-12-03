using System;
using System.Collections.Generic;
using Desafio1App.Data;

namespace Desafio1App.Modelos
{
    public enum RolUsuario
    {
        Administrador = 0,
        PersonalSalud = 1
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string NombreCompleto { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Usuario() 
        {
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        public Usuario(string nombreUsuario, string contraseña, string nombreCompleto, RolUsuario rol)
            : this()
        {
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
            NombreCompleto = nombreCompleto;
            Rol = rol;
        }

        public Usuario(int id, string nombreUsuario, string contraseña, string nombreCompleto, RolUsuario rol, bool activo, DateTime fechaCreacion)
        {
            Id = id;
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
            NombreCompleto = nombreCompleto;
            Rol = rol;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }

        public bool EsAdministrador => Rol == RolUsuario.Administrador;
        public string DescripcionRol => Rol == RolUsuario.Administrador ? "Administrador" : "Personal de Salud";
    }

    public static class GestorUsuarios
    {
        private static UsuarioRepository repository = new UsuarioRepository();

        public static Usuario ValidarCredenciales(string nombreUsuario, string contraseña)
        {
            return repository.ValidarCredenciales(nombreUsuario, contraseña);
        }

        public static List<Usuario> ObtenerTodos()
        {
            return repository.ObtenerTodos();
        }

        public static bool Agregar(Usuario usuario)
        {
            return repository.Agregar(usuario);
        }

        public static bool Actualizar(Usuario usuario)
        {
            return repository.Actualizar(usuario);
        }

        public static bool Eliminar(int id)
        {
            return repository.Eliminar(id);
        }

        public static bool ExisteNombreUsuario(string nombreUsuario, int? exceptoId = null)
        {
            return repository.ExisteNombreUsuario(nombreUsuario, exceptoId);
        }

        public static Usuario ObtenerPorId(int id)
        {
            return repository.ObtenerPorId(id);
        }
    }
}
