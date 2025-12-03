using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Desafio1App.Modelos;

namespace Desafio1App.Data
{
    public class PacienteRepository
    {
        public List<Paciente> ObtenerTodos()
        {
            var lista = new List<Paciente>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Edad, Genero, TipoSangre, PresionArterial, FechaRegistro FROM Pacientes WHERE Activo = 1";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearPaciente(reader));
                    }
                }
            }
            return lista;
        }

        public Paciente ObtenerPorId(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Edad, Genero, TipoSangre, PresionArterial, FechaRegistro FROM Pacientes WHERE Id = @Id AND Activo = 1";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapearPaciente(reader);
                    }
                }
            }
            return null;
        }

        public int Insertar(Paciente paciente)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"INSERT INTO Pacientes (Nombre, Edad, Genero, TipoSangre, PresionArterial) 
                              VALUES (@Nombre, @Edad, @Genero, @TipoSangre, @PresionArterial);
                              SELECT SCOPE_IDENTITY();";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", paciente.Edad);
                    cmd.Parameters.AddWithValue("@Genero", paciente.Genero);
                    cmd.Parameters.AddWithValue("@TipoSangre", paciente.TipoSangre);
                    cmd.Parameters.AddWithValue("@PresionArterial", paciente.PresionArterial);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void Actualizar(Paciente paciente)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"UPDATE Pacientes SET 
                              Nombre = @Nombre, Edad = @Edad, Genero = @Genero, 
                              TipoSangre = @TipoSangre, PresionArterial = @PresionArterial 
                              WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", paciente.Id);
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", paciente.Edad);
                    cmd.Parameters.AddWithValue("@Genero", paciente.Genero);
                    cmd.Parameters.AddWithValue("@TipoSangre", paciente.TipoSangre);
                    cmd.Parameters.AddWithValue("@PresionArterial", paciente.PresionArterial);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Pacientes SET Activo = 0 WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Paciente> Buscar(string genero, string tipoSangre, string presion, string nombre)
        {
            var lista = new List<Paciente>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Edad, Genero, TipoSangre, PresionArterial, FechaRegistro FROM Pacientes WHERE Activo = 1";
                
                if (!string.IsNullOrEmpty(genero) && genero != "Todos")
                    sql += " AND Genero = @Genero";
                if (!string.IsNullOrEmpty(tipoSangre) && tipoSangre != "Todos")
                    sql += " AND TipoSangre = @TipoSangre";
                if (!string.IsNullOrEmpty(presion) && presion != "Todos")
                    sql += " AND PresionArterial = @Presion";
                if (!string.IsNullOrEmpty(nombre))
                    sql += " AND Nombre LIKE @Nombre";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(genero) && genero != "Todos")
                        cmd.Parameters.AddWithValue("@Genero", genero);
                    if (!string.IsNullOrEmpty(tipoSangre) && tipoSangre != "Todos")
                        cmd.Parameters.AddWithValue("@TipoSangre", tipoSangre);
                    if (!string.IsNullOrEmpty(presion) && presion != "Todos")
                        cmd.Parameters.AddWithValue("@Presion", presion);
                    if (!string.IsNullOrEmpty(nombre))
                        cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearPaciente(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public int ObtenerTotal()
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Pacientes WHERE Activo = 1";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public Dictionary<string, int> ObtenerEstadisticasPor(string campo)
        {
            var stats = new Dictionary<string, int>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = $"SELECT {campo}, COUNT(*) as Cantidad FROM Pacientes WHERE Activo = 1 GROUP BY {campo}";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stats[reader.GetString(0)] = reader.GetInt32(1);
                    }
                }
            }
            return stats;
        }

        public double ObtenerEdadPromedio()
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT AVG(CAST(Edad AS FLOAT)) FROM Pacientes WHERE Activo = 1";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    var result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? 0 : Convert.ToDouble(result);
                }
            }
        }

        private Paciente MapearPaciente(SqlDataReader reader)
        {
            return new Paciente
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Edad = reader.GetInt32(2),
                Genero = reader.GetString(3),
                TipoSangre = reader.GetString(4),
                PresionArterial = reader.GetString(5),
                FechaRegistro = reader.GetDateTime(6)
            };
        }
    }
}

