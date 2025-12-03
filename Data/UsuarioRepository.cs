using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Desafio1App.Modelos;
using Desafio1App.Utils;

namespace Desafio1App.Data
{
    public class UsuarioRepository
    {
        public Usuario ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            string contrasenaEncriptada = EncriptacionHelper.EncriptarContrasena(contrasena);
            
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT Id, NombreUsuario, Contrasena, NombreCompleto, Rol, Activo, FechaCreacion 
                              FROM Usuarios WHERE NombreUsuario = @Usuario AND Contrasena = @Contrasena AND Activo = 1";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasenaEncriptada);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                (RolUsuario)reader.GetInt32(4),
                                reader.GetBoolean(5),
                                reader.GetDateTime(6)
                            );
                        }
                    }
                }
            }
            return null;
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, NombreUsuario, Contrasena, NombreCompleto, Rol, Activo, FechaCreacion FROM Usuarios WHERE Activo = 1 ORDER BY NombreCompleto";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            (RolUsuario)reader.GetInt32(4),
                            reader.GetBoolean(5),
                            reader.GetDateTime(6)
                        ));
                    }
                }
            }
            return lista;
        }

        public Usuario ObtenerPorId(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, NombreUsuario, Contrasena, NombreCompleto, Rol, Activo, FechaCreacion FROM Usuarios WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                (RolUsuario)reader.GetInt32(4),
                                reader.GetBoolean(5),
                                reader.GetDateTime(6)
                            );
                        }
                    }
                }
            }
            return null;
        }

        public bool Agregar(Usuario usuario)
        {
            string contrasenaEncriptada = EncriptacionHelper.EncriptarContrasena(usuario.Contraseña);
            
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"INSERT INTO Usuarios (NombreUsuario, Contrasena, NombreCompleto, Rol, Activo, FechaCreacion) 
                              VALUES (@NombreUsuario, @Contrasena, @NombreCompleto, @Rol, 1, GETDATE())";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasenaEncriptada);
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Rol", (int)usuario.Rol);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql;
                
                // Si la contraseña está vacía, no la actualizamos
                if (string.IsNullOrEmpty(usuario.Contraseña))
                {
                    sql = @"UPDATE Usuarios SET NombreUsuario = @NombreUsuario, NombreCompleto = @NombreCompleto, Rol = @Rol 
                           WHERE Id = @Id";
                }
                else
                {
                    sql = @"UPDATE Usuarios SET NombreUsuario = @NombreUsuario, Contrasena = @Contrasena, 
                           NombreCompleto = @NombreCompleto, Rol = @Rol WHERE Id = @Id";
                }
                
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Rol", (int)usuario.Rol);
                    
                    if (!string.IsNullOrEmpty(usuario.Contraseña))
                    {
                        string contrasenaEncriptada = EncriptacionHelper.EncriptarContrasena(usuario.Contraseña);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasenaEncriptada);
                    }
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Usuarios SET Activo = 0 WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ExisteNombreUsuario(string nombreUsuario, int? exceptoId = null)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Activo = 1";
                if (exceptoId.HasValue)
                {
                    sql += " AND Id <> @ExceptoId";
                }
                
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    if (exceptoId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@ExceptoId", exceptoId.Value);
                    }
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}

