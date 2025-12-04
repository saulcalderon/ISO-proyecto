using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Desafio1App.Modelos;

namespace Desafio1App.Data
{
    public class CitaRepository
    {
        public List<Cita> ObtenerTodas()
        {
            var lista = new List<Cita>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              ORDER BY c.FechaHora DESC";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearCita(reader));
                    }
                }
            }
            return lista;
        }

        public Cita ObtenerPorId(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              WHERE c.Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapearCita(reader);
                    }
                }
            }
            return null;
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            var lista = new List<Cita>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              WHERE c.PacienteId = @PacienteId 
                              ORDER BY c.FechaHora DESC";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PacienteId", pacienteId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearCita(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cita> ObtenerPorFecha(DateTime fecha)
        {
            var lista = new List<Cita>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              WHERE CAST(c.FechaHora AS DATE) = @Fecha 
                              ORDER BY c.FechaHora";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearCita(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cita> ObtenerPorEstado(EstadoCita estado)
        {
            var lista = new List<Cita>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              WHERE c.Estado = @Estado 
                              ORDER BY c.FechaHora";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Estado", (int)estado);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearCita(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cita> ObtenerCitasFuturas()
        {
            var lista = new List<Cita>();
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT c.Id, c.PacienteId, c.FechaHora, c.Motivo, c.Estado, c.Observaciones, c.FechaCreacion, p.Nombre 
                              FROM Citas c 
                              INNER JOIN Pacientes p ON c.PacienteId = p.Id 
                              WHERE c.FechaHora > GETDATE() AND c.Estado IN (0, 1)
                              ORDER BY c.FechaHora";
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearCita(reader));
                    }
                }
            }
            return lista;
        }

        public int Insertar(Cita cita)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"INSERT INTO Citas (PacienteId, FechaHora, Motivo, Estado, Observaciones) 
                              VALUES (@PacienteId, @FechaHora, @Motivo, @Estado, @Observaciones);
                              SELECT SCOPE_IDENTITY();";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PacienteId", cita.PacienteId);
                    cmd.Parameters.AddWithValue("@FechaHora", cita.FechaHora);
                    cmd.Parameters.AddWithValue("@Motivo", cita.Motivo);
                    cmd.Parameters.AddWithValue("@Estado", (int)cita.Estado);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)cita.Observaciones ?? DBNull.Value);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void Actualizar(Cita cita)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"UPDATE Citas SET 
                              PacienteId = @PacienteId, FechaHora = @FechaHora, 
                              Motivo = @Motivo, Estado = @Estado, Observaciones = @Observaciones 
                              WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", cita.Id);
                    cmd.Parameters.AddWithValue("@PacienteId", cita.PacienteId);
                    cmd.Parameters.AddWithValue("@FechaHora", cita.FechaHora);
                    cmd.Parameters.AddWithValue("@Motivo", cita.Motivo);
                    cmd.Parameters.AddWithValue("@Estado", (int)cita.Estado);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)cita.Observaciones ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstado(int id, EstadoCita nuevoEstado)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Citas SET Estado = @Estado WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Estado", (int)nuevoEstado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "DELETE FROM Citas WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ExisteCitaEnHorario(DateTime fechaHora, int? excluirCitaId = null)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Citas 
                              WHERE FechaHora = @FechaHora AND Estado IN (0, 1)";
                if (excluirCitaId.HasValue)
                    sql += " AND Id <> @ExcluirId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FechaHora", fechaHora);
                    if (excluirCitaId.HasValue)
                        cmd.Parameters.AddWithValue("@ExcluirId", excluirCitaId.Value);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public int ObtenerTotalCitas()
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Citas";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public int ObtenerCitasPendientes()
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Citas WHERE Estado IN (0, 1) AND FechaHora > GETDATE()";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private Cita MapearCita(SqlDataReader reader)
        {
            return new Cita
            {
                Id = reader.GetInt32(0),
                PacienteId = reader.GetInt32(1),
                FechaHora = reader.GetDateTime(2),
                Motivo = reader.GetString(3),
                Estado = (EstadoCita)reader.GetInt32(4),
                Observaciones = reader.IsDBNull(5) ? "" : reader.GetString(5),
                FechaCreacion = reader.GetDateTime(6),
                NombrePaciente = reader.GetString(7)
            };
        }
    }
}

