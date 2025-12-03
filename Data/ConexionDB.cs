using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Desafio1App.Data
{
    public static class ConexionDB
    {
        private static string connectionString = 
            "Server=localhost,1433;Database=SCS_DB;User Id=sa;Password=ScsAdmin123!;";

        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }

        public static bool ProbarConexion()
        {
            try
            {
                using (var conn = ObtenerConexion())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ValidarConexion()
        {
            if (!ProbarConexion())
            {
                MessageBox.Show(
                    "No se puede conectar a la base de datos.\nContacte al administrador del sistema.",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(1);
            }
        }
    }
}
