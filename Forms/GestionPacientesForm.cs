using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Desafio1App.Arbol;
using Desafio1App.Modelos;

namespace Desafio1App.Forms
{
    public class GestionPacientesForm : Form
    {
        private readonly ArbolClasificador arbol;
        private readonly bool esAdministrador;
        
        private DataGridView dgvPacientes;
        private TextBox txtBuscarNombre;
        private ComboBox cmbFiltroGenero;
        private ComboBox cmbFiltroSangre;
        private ComboBox cmbFiltroPresion;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCerrar;

        public GestionPacientesForm(ArbolClasificador arbol, bool esAdministrador)
        {
            this.arbol = arbol;
            this.esAdministrador = esAdministrador;
            ConfigurarInterfaz();
            CargarPacientes();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Gestión de Pacientes";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Gestión de Pacientes",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(380, 15)
            };
            this.Controls.Add(lblTitulo);

            // Panel de filtros
            Panel panelFiltros = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 55),
                Size = new Size(945, 80)
            };
            this.Controls.Add(panelFiltros);

            // Búsqueda por nombre
            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(15, 28), AutoSize = true, Font = new Font("Segoe UI", 9) };
            txtBuscarNombre = new TextBox { Location = new Point(75, 25), Size = new Size(150, 25), Font = new Font("Segoe UI", 9) };
            panelFiltros.Controls.Add(lblNombre);
            panelFiltros.Controls.Add(txtBuscarNombre);

            // Filtro género
            Label lblGenero = new Label { Text = "Género:", Location = new Point(240, 28), AutoSize = true, Font = new Font("Segoe UI", 9) };
            cmbFiltroGenero = new ComboBox
            {
                Location = new Point(300, 25),
                Size = new Size(110, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroGenero.Items.AddRange(new object[] { "Todos", "Masculino", "Femenino" });
            cmbFiltroGenero.SelectedIndex = 0;
            panelFiltros.Controls.Add(lblGenero);
            panelFiltros.Controls.Add(cmbFiltroGenero);

            // Filtro tipo sangre
            Label lblSangre = new Label { Text = "Sangre:", Location = new Point(425, 28), AutoSize = true, Font = new Font("Segoe UI", 9) };
            cmbFiltroSangre = new ComboBox
            {
                Location = new Point(480, 25),
                Size = new Size(80, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroSangre.Items.AddRange(new object[] { "Todos", "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" });
            cmbFiltroSangre.SelectedIndex = 0;
            panelFiltros.Controls.Add(lblSangre);
            panelFiltros.Controls.Add(cmbFiltroSangre);

            // Filtro presión
            Label lblPresion = new Label { Text = "Presión:", Location = new Point(575, 28), AutoSize = true, Font = new Font("Segoe UI", 9) };
            cmbFiltroPresion = new ComboBox
            {
                Location = new Point(635, 25),
                Size = new Size(90, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroPresion.Items.AddRange(new object[] { "Todos", "Alta", "Normal", "Baja" });
            cmbFiltroPresion.SelectedIndex = 0;
            panelFiltros.Controls.Add(lblPresion);
            panelFiltros.Controls.Add(cmbFiltroPresion);

            // Botones de búsqueda
            btnBuscar = CrearBoton("🔍 Buscar", new Point(745, 22), new Size(90, 32), Color.FromArgb(0, 123, 255));
            btnBuscar.Click += BtnBuscar_Click;
            panelFiltros.Controls.Add(btnBuscar);

            btnLimpiar = CrearBoton("✕ Limpiar", new Point(845, 22), new Size(90, 32), Color.FromArgb(108, 117, 125));
            btnLimpiar.Click += BtnLimpiar_Click;
            panelFiltros.Controls.Add(btnLimpiar);

            // DataGridView
            dgvPacientes = new DataGridView
            {
                Location = new Point(20, 145),
                Size = new Size(945, 380),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9)
            };
            
            dgvPacientes.Columns.Add("Id", "ID");
            dgvPacientes.Columns.Add("Nombre", "Nombre");
            dgvPacientes.Columns.Add("Edad", "Edad");
            dgvPacientes.Columns.Add("Genero", "Género");
            dgvPacientes.Columns.Add("TipoSangre", "Tipo Sangre");
            dgvPacientes.Columns.Add("Presion", "Presión");
            dgvPacientes.Columns.Add("FechaRegistro", "Fecha Registro");

            dgvPacientes.Columns["Id"].Width = 50;
            dgvPacientes.Columns["Edad"].Width = 60;
            dgvPacientes.Columns["Genero"].Width = 90;
            dgvPacientes.Columns["TipoSangre"].Width = 90;
            dgvPacientes.Columns["Presion"].Width = 80;

            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvPacientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvPacientes.ColumnHeadersHeight = 35;

            this.Controls.Add(dgvPacientes);

            // Panel de botones de acción
            int btnY = 540;
            
            btnEditar = CrearBoton("✏️ Editar", new Point(300, btnY), new Size(130, 45), Color.FromArgb(255, 193, 7));
            btnEditar.ForeColor = Color.Black;
            btnEditar.Click += BtnEditar_Click;
            btnEditar.Enabled = esAdministrador;
            this.Controls.Add(btnEditar);

            btnEliminar = CrearBoton("🗑️ Eliminar", new Point(450, btnY), new Size(130, 45), Color.FromArgb(220, 53, 69));
            btnEliminar.Click += BtnEliminar_Click;
            btnEliminar.Enabled = esAdministrador;
            this.Controls.Add(btnEliminar);

            btnCerrar = CrearBoton("✓ Cerrar", new Point(600, btnY), new Size(130, 45), Color.FromArgb(108, 117, 125));
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            if (!esAdministrador)
            {
                Label lblInfo = new Label
                {
                    Text = "ℹ️ Solo los administradores pueden editar o eliminar pacientes",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    AutoSize = true,
                    Location = new Point(280, 590)
                };
                this.Controls.Add(lblInfo);
            }
        }

        private Button CrearBoton(string texto, Point ubicacion, Size tamaño, Color colorFondo)
        {
            return new Button
            {
                Text = texto,
                Location = ubicacion,
                Size = tamaño,
                BackColor = colorFondo,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void CargarPacientes()
        {
            CargarPacientesEnGrid(arbol.ObtenerListaPacientes());
        }

        private void CargarPacientesEnGrid(List<Paciente> pacientes)
        {
            dgvPacientes.Rows.Clear();
            foreach (var p in pacientes)
            {
                dgvPacientes.Rows.Add(p.Id, p.Nombre, p.Edad, p.Genero, p.TipoSangre, p.PresionArterial, p.FechaRegistro.ToString("dd/MM/yyyy HH:mm"));
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var pacientes = arbol.BuscarPacientes(
                cmbFiltroGenero.SelectedItem?.ToString(),
                cmbFiltroSangre.SelectedItem?.ToString(),
                cmbFiltroPresion.SelectedItem?.ToString(),
                txtBuscarNombre.Text.Trim()
            );
            CargarPacientesEnGrid(pacientes);
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Text = "";
            cmbFiltroGenero.SelectedIndex = 0;
            cmbFiltroSangre.SelectedIndex = 0;
            cmbFiltroPresion.SelectedIndex = 0;
            CargarPacientes();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un paciente para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvPacientes.SelectedRows[0].Cells["Id"].Value);
            var paciente = arbol.ObtenerPacientePorId(id);

            if (paciente != null)
            {
                PacienteForm form = new PacienteForm(paciente, arbol);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CargarPacientes();
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un paciente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvPacientes.SelectedRows[0].Cells["Nombre"].Value.ToString();
            var resultado = MessageBox.Show($"¿Está seguro de eliminar al paciente '{nombre}'?", 
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvPacientes.SelectedRows[0].Cells["Id"].Value);
                arbol.EliminarPaciente(id);
                CargarPacientes();
                MessageBox.Show("Paciente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

