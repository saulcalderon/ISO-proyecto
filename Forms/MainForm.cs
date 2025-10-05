using System;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Forms;

namespace Desafio1App.Forms
{
    public partial class MainForm : Form
    {
        private Button btnAgregarPaciente;
        private Button btnVerArbol;

        public MainForm()
        {
            InitializeComponent();
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.SuspendLayout();

            // Propiedades del formulario
            this.Text = "Menú Principal";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 240, 255); // Fondo pastel suave
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Botón Agregar Paciente
            btnAgregarPaciente = new Button();
            btnAgregarPaciente.Text = "Agregar paciente";
            btnAgregarPaciente.Size = new Size(150, 40);
            btnAgregarPaciente.Location = new Point(120, 30);
            btnAgregarPaciente.BackColor = Color.FromArgb(220, 235, 255);
            btnAgregarPaciente.Click += BtnAgregarPaciente_Click;
            this.Controls.Add(btnAgregarPaciente);

            // Botón Ver Árbol
            btnVerArbol = new Button();
            btnVerArbol.Text = "Ver árbol";
            btnVerArbol.Size = new Size(150, 40);
            btnVerArbol.Location = new Point(120, 90);
            btnVerArbol.BackColor = Color.FromArgb(220, 255, 240);
            btnVerArbol.Click += BtnVerArbol_Click;
            this.Controls.Add(btnVerArbol);

            this.ResumeLayout(false);

            // Botón Cerrar sesión
            Button btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.Size = new Size(150, 40);
            btnCerrarSesion.Location = new Point(120, 150);
            btnCerrarSesion.BackColor = Color.FromArgb(255, 220, 220);
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            this.Controls.Add(btnCerrarSesion);

        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta el main
            LoginForm login = new LoginForm();
            login.Show(); // Muestra el login nuevamente
        }



        // Evento para abrir el formulario de pacientes
        private void BtnAgregarPaciente_Click(object sender, EventArgs e)
        {
            PacienteForm form = new PacienteForm();
            form.ShowDialog();
        }

        // Evento para abrir el visualizador del árbol
        private void BtnVerArbol_Click(object sender, EventArgs e)
        {
            TreeViewForm treeViewForm = new TreeViewForm(PacienteForm.arbol);
            treeViewForm.ShowDialog();
        }
    }
}
