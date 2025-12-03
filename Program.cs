using System;
using System.Windows.Forms;
using Desafio1App.Forms;
using Desafio1App.Data;

namespace Desafio1App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Validar conexión a base de datos antes de iniciar
            ConexionDB.ValidarConexion();
            
            Application.Run(new LoginForm());
        }
    }
}
