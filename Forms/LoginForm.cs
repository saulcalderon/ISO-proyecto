// Forms/LoginForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desafio1App.Forms
{
    public partial class LoginForm : Form
    {
        // Constructor del formulario
        public LoginForm()
        {
            InitializeComponent();
        }

        // Evento de clic del botón Ingresar
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            // Validación con datos predefinidos
            if (usuario == "desafio" && contraseña == "2025")
            {
                // Si son válidos, se abre el MainForm
                MainForm main = new MainForm();
                this.Hide(); // Oculta el Login
                main.ShowDialog();
                this.Close(); // Cierra el formulario al volver
            }
            else
            {
                // Si no coinciden, muestra error
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblContraseña_Click(object sender, EventArgs e)
        {

        }
    }
}

