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
            this.Text = "SCS V1.0 - Menú Principal";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título principal
            Label lblTitulo = new Label();
            lblTitulo.Text = "Sistema de Clasificación de Sangre";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(110, 30);
            this.Controls.Add(lblTitulo);

            // Subtítulo
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Seleccione una opción";
            lblSubtitulo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.FromArgb(100, 100, 100);
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(215, 70);
            this.Controls.Add(lblSubtitulo);

            // Panel contenedor
            Panel panelBotones = new Panel();
            panelBotones.BackColor = Color.White;
            panelBotones.BorderStyle = BorderStyle.FixedSingle;
            panelBotones.Size = new Size(450, 300);
            panelBotones.Location = new Point(75, 110);
            this.Controls.Add(panelBotones);

            // Botón Agregar Paciente
            btnAgregarPaciente = new Button();
            btnAgregarPaciente.Text = "➕ Agregar Paciente";
            btnAgregarPaciente.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAgregarPaciente.Size = new Size(350, 60);
            btnAgregarPaciente.Location = new Point(50, 30);
            btnAgregarPaciente.BackColor = Color.FromArgb(40, 167, 69);
            btnAgregarPaciente.ForeColor = Color.White;
            btnAgregarPaciente.FlatStyle = FlatStyle.Flat;
            btnAgregarPaciente.FlatAppearance.BorderSize = 0;
            btnAgregarPaciente.Cursor = Cursors.Hand;
            btnAgregarPaciente.Click += BtnAgregarPaciente_Click;
            panelBotones.Controls.Add(btnAgregarPaciente);

            // Botón Ver Árbol
            btnVerArbol = new Button();
            btnVerArbol.Text = "🌳 Ver Clasificación de Pacientes";
            btnVerArbol.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnVerArbol.Size = new Size(350, 60);
            btnVerArbol.Location = new Point(50, 110);
            btnVerArbol.BackColor = Color.FromArgb(0, 123, 255);
            btnVerArbol.ForeColor = Color.White;
            btnVerArbol.FlatStyle = FlatStyle.Flat;
            btnVerArbol.FlatAppearance.BorderSize = 0;
            btnVerArbol.Cursor = Cursors.Hand;
            btnVerArbol.Click += BtnVerArbol_Click;
            panelBotones.Controls.Add(btnVerArbol);

            // Botón Cerrar sesión
            Button btnCerrarSesion = new Button();
            btnCerrarSesion.Text = "🚪 Cerrar Sesión";
            btnCerrarSesion.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCerrarSesion.Size = new Size(350, 60);
            btnCerrarSesion.Location = new Point(50, 190);
            btnCerrarSesion.BackColor = Color.FromArgb(220, 53, 69);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            panelBotones.Controls.Add(btnCerrarSesion);

            this.ResumeLayout(false);
            this.PerformLayout();

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
