using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;

namespace Desafio1App.Forms
{
    public partial class MainForm : Form
    {
        private Usuario usuarioActual;

        public MainForm() : this(null) { }

        public MainForm(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.SuspendLayout();

            this.Text = "SCS V1.0 - Menú Principal";
            this.Size = new Size(600, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Info usuario
            if (usuarioActual != null)
            {
                Label lblUsuario = new Label
                {
                    Text = $"👤 {usuarioActual.NombreCompleto} ({usuarioActual.DescripcionRol})",
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    AutoSize = true,
                    Location = new Point(20, 10)
                };
                this.Controls.Add(lblUsuario);
            }

            // Título principal
            Label lblTitulo = new Label
            {
                Text = "Sistema de Clasificación de Sangre",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(110, 45)
            };
            this.Controls.Add(lblTitulo);

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Seleccione una opción",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(215, 85)
            };
            this.Controls.Add(lblSubtitulo);

            // Panel contenedor
            int panelHeight = usuarioActual?.EsAdministrador == true ? 495 : 420;
            Panel panelBotones = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(450, panelHeight),
                Location = new Point(75, 115)
            };
            this.Controls.Add(panelBotones);

            // Ajustar tamaño del formulario si es admin
            if (usuarioActual?.EsAdministrador == true)
            {
                this.Size = new Size(600, 695);
            }

            int btnY = 25;
            int btnSpacing = 75;

            // Botón Agregar Paciente
            Button btnAgregarPaciente = CrearBoton("➕ Agregar Paciente", new Point(50, btnY), Color.FromArgb(40, 167, 69));
            btnAgregarPaciente.Click += (s, e) => {
                PacienteForm form = new PacienteForm();
                form.ShowDialog();
            };
            panelBotones.Controls.Add(btnAgregarPaciente);

            // Botón Gestión de Pacientes
            btnY += btnSpacing;
            Button btnGestionPacientes = CrearBoton("📋 Gestión de Pacientes", new Point(50, btnY), Color.FromArgb(23, 162, 184));
            btnGestionPacientes.Click += (s, e) => {
                bool esAdmin = usuarioActual?.EsAdministrador ?? false;
                GestionPacientesForm form = new GestionPacientesForm(PacienteForm.arbol, esAdmin);
                form.ShowDialog();
            };
            panelBotones.Controls.Add(btnGestionPacientes);

            // Botón Ver Árbol
            btnY += btnSpacing;
            Button btnVerArbol = CrearBoton("🌳 Ver Clasificación de Pacientes", new Point(50, btnY), Color.FromArgb(0, 123, 255));
            btnVerArbol.Click += (s, e) => {
                TreeViewForm treeViewForm = new TreeViewForm(PacienteForm.arbol);
                treeViewForm.ShowDialog();
            };
            panelBotones.Controls.Add(btnVerArbol);

            // Botón Estadísticas
            btnY += btnSpacing;
            Button btnEstadisticas = CrearBoton("📊 Ver Estadísticas", new Point(50, btnY), Color.FromArgb(111, 66, 193));
            btnEstadisticas.Click += (s, e) => {
                EstadisticasForm form = new EstadisticasForm(PacienteForm.arbol);
                form.ShowDialog();
            };
            panelBotones.Controls.Add(btnEstadisticas);

            // Botón Gestión de Usuarios (solo Administrador)
            if (usuarioActual?.EsAdministrador == true)
            {
                btnY += btnSpacing;
                Button btnGestionUsuarios = CrearBoton("👥 Gestión de Usuarios", new Point(50, btnY), Color.FromArgb(255, 152, 0));
                btnGestionUsuarios.Click += (s, e) => {
                    GestionUsuariosForm form = new GestionUsuariosForm();
                    form.ShowDialog();
                };
                panelBotones.Controls.Add(btnGestionUsuarios);
            }

            // Botón Cerrar sesión
            btnY += btnSpacing;
            Button btnCerrarSesion = CrearBoton("🚪 Cerrar Sesión", new Point(50, btnY), Color.FromArgb(220, 53, 69));
            btnCerrarSesion.Click += (s, e) => {
                this.Hide();
                LoginForm login = new LoginForm();
                login.Show();
            };
            panelBotones.Controls.Add(btnCerrarSesion);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button CrearBoton(string texto, Point ubicacion, Color colorFondo)
        {
            return new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(350, 60),
                Location = ubicacion,
                BackColor = colorFondo,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }
    }
}
