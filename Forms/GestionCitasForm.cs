using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;
using Desafio1App.Data;

namespace Desafio1App.Forms
{
    public class GestionCitasForm : Form
    {
        private readonly CitaRepository citaRepository;
        private readonly bool esAdministrador;

        private DataGridView dgvCitas;
        private DateTimePicker dtpFiltroFecha;
        private ComboBox cmbFiltroEstado;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnNueva;
        private Button btnEditar;
        private Button btnCambiarEstado;
        private Button btnEliminar;
        private Button btnCerrar;
        private CheckBox chkFiltrarPorFecha;

        public GestionCitasForm(bool esAdmin = false)
        {
            citaRepository = new CitaRepository();
            esAdministrador = esAdmin;
            ConfigurarInterfaz();
            CargarCitas();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Gestión de Citas";
            this.Size = new Size(1050, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Gestión de Citas Médicas",
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
                Size = new Size(995, 70)
            };
            this.Controls.Add(panelFiltros);

            // Filtro por fecha
            chkFiltrarPorFecha = new CheckBox
            {
                Text = "Filtrar por fecha:",
                Font = new Font("Segoe UI", 9),
                Location = new Point(15, 25),
                AutoSize = true
            };
            chkFiltrarPorFecha.CheckedChanged += (s, e) => dtpFiltroFecha.Enabled = chkFiltrarPorFecha.Checked;
            panelFiltros.Controls.Add(chkFiltrarPorFecha);

            dtpFiltroFecha = new DateTimePicker
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(140, 22),
                Size = new Size(130, 25),
                Format = DateTimePickerFormat.Short,
                Enabled = false
            };
            panelFiltros.Controls.Add(dtpFiltroFecha);

            // Filtro estado
            Label lblEstado = new Label
            {
                Text = "Estado:",
                Font = new Font("Segoe UI", 9),
                Location = new Point(290, 25),
                AutoSize = true
            };
            panelFiltros.Controls.Add(lblEstado);

            cmbFiltroEstado = new ComboBox
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(345, 22),
                Size = new Size(130, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFiltroEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "Confirmada", "Completada", "Cancelada" });
            cmbFiltroEstado.SelectedIndex = 0;
            panelFiltros.Controls.Add(cmbFiltroEstado);

            // Botones de búsqueda
            btnBuscar = CrearBoton("Buscar", new Point(500, 18), new Size(90, 35), Color.FromArgb(0, 123, 255));
            btnBuscar.Click += BtnBuscar_Click;
            panelFiltros.Controls.Add(btnBuscar);

            btnLimpiar = CrearBoton("Limpiar", new Point(600, 18), new Size(90, 35), Color.FromArgb(108, 117, 125));
            btnLimpiar.Click += BtnLimpiar_Click;
            panelFiltros.Controls.Add(btnLimpiar);

            // Botón nueva cita
            btnNueva = CrearBoton("+ Nueva Cita", new Point(780, 18), new Size(120, 35), Color.FromArgb(40, 167, 69));
            btnNueva.Click += BtnNueva_Click;
            panelFiltros.Controls.Add(btnNueva);

            // DataGridView
            dgvCitas = new DataGridView
            {
                Location = new Point(20, 135),
                Size = new Size(995, 380),
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

            dgvCitas.Columns.Add("Id", "ID");
            dgvCitas.Columns.Add("NombrePaciente", "Paciente");
            dgvCitas.Columns.Add("FechaHora", "Fecha y Hora");
            dgvCitas.Columns.Add("Motivo", "Motivo");
            dgvCitas.Columns.Add("Estado", "Estado");
            dgvCitas.Columns.Add("Observaciones", "Observaciones");

            dgvCitas.Columns["Id"].Width = 50;
            dgvCitas.Columns["FechaHora"].Width = 140;
            dgvCitas.Columns["Estado"].Width = 100;

            dgvCitas.EnableHeadersVisualStyles = false;
            dgvCitas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvCitas.ColumnHeadersHeight = 35;

            this.Controls.Add(dgvCitas);

            // Panel de botones de acción
            int btnY = 530;
            int btnX = 150;

            btnEditar = CrearBoton("Editar", new Point(btnX, btnY), new Size(120, 45), Color.FromArgb(255, 193, 7));
            btnEditar.ForeColor = Color.Black;
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            btnX += 140;
            btnCambiarEstado = CrearBoton("Cambiar Estado", new Point(btnX, btnY), new Size(140, 45), Color.FromArgb(23, 162, 184));
            btnCambiarEstado.Click += BtnCambiarEstado_Click;
            this.Controls.Add(btnCambiarEstado);

            btnX += 160;
            btnEliminar = CrearBoton("Eliminar", new Point(btnX, btnY), new Size(120, 45), Color.FromArgb(220, 53, 69));
            btnEliminar.Click += BtnEliminar_Click;
            btnEliminar.Enabled = esAdministrador;
            this.Controls.Add(btnEliminar);

            btnX += 140;
            btnCerrar = CrearBoton("Cerrar", new Point(btnX, btnY), new Size(120, 45), Color.FromArgb(108, 117, 125));
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            if (!esAdministrador)
            {
                Label lblInfo = new Label
                {
                    Text = "Solo los administradores pueden eliminar citas",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    AutoSize = true,
                    Location = new Point(350, 585)
                };
                this.Controls.Add(lblInfo);
            }
        }

        private Button CrearBoton(string texto, Point ubicacion, Size tamaño, Color colorFondo)
        {
            var btn = new Button
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
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void CargarCitas()
        {
            CargarCitasEnGrid(citaRepository.ObtenerTodas());
        }

        private void CargarCitasEnGrid(List<Cita> citas)
        {
            dgvCitas.Rows.Clear();
            foreach (var c in citas)
            {
                int rowIndex = dgvCitas.Rows.Add(
                    c.Id,
                    c.NombrePaciente,
                    c.FechaFormateada,
                    c.Motivo,
                    c.DescripcionEstado,
                    c.Observaciones
                );

                // Colorear según estado
                switch (c.Estado)
                {
                    case EstadoCita.Pendiente:
                        dgvCitas.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                        break;
                    case EstadoCita.Confirmada:
                        dgvCitas.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(212, 237, 218);
                        break;
                    case EstadoCita.Completada:
                        dgvCitas.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(209, 236, 241);
                        break;
                    case EstadoCita.Cancelada:
                        dgvCitas.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 215, 218);
                        break;
                }
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            List<Cita> citas;

            if (chkFiltrarPorFecha.Checked)
            {
                citas = citaRepository.ObtenerPorFecha(dtpFiltroFecha.Value);
            }
            else if (cmbFiltroEstado.SelectedIndex > 0)
            {
                citas = citaRepository.ObtenerPorEstado((EstadoCita)(cmbFiltroEstado.SelectedIndex - 1));
            }
            else
            {
                citas = citaRepository.ObtenerTodas();
            }

            // Filtrar por estado si está seleccionado y también por fecha
            if (chkFiltrarPorFecha.Checked && cmbFiltroEstado.SelectedIndex > 0)
            {
                var estadoFiltro = (EstadoCita)(cmbFiltroEstado.SelectedIndex - 1);
                citas = citas.FindAll(c => c.Estado == estadoFiltro);
            }

            CargarCitasEnGrid(citas);
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            chkFiltrarPorFecha.Checked = false;
            dtpFiltroFecha.Value = DateTime.Today;
            cmbFiltroEstado.SelectedIndex = 0;
            CargarCitas();
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            var form = new CitaForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarCitas();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cita para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvCitas.SelectedRows[0].Cells["Id"].Value);
            var cita = citaRepository.ObtenerPorId(id);

            if (cita != null)
            {
                var form = new CitaForm(cita);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CargarCitas();
                }
            }
        }

        private void BtnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cita para cambiar su estado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvCitas.SelectedRows[0].Cells["Id"].Value);
            string estadoActual = dgvCitas.SelectedRows[0].Cells["Estado"].Value.ToString();

            using (var form = new Form())
            {
                form.Text = "Cambiar Estado de Cita";
                form.Size = new Size(300, 180);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label lbl = new Label
                {
                    Text = $"Estado actual: {estadoActual}\n\nSeleccione el nuevo estado:",
                    Location = new Point(20, 15),
                    Size = new Size(250, 45)
                };
                form.Controls.Add(lbl);

                ComboBox cmb = new ComboBox
                {
                    Location = new Point(20, 65),
                    Size = new Size(240, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmb.Items.AddRange(new object[] { "Pendiente", "Confirmada", "Completada", "Cancelada" });
                form.Controls.Add(cmb);

                Button btnAceptar = new Button
                {
                    Text = "Aceptar",
                    DialogResult = DialogResult.OK,
                    Location = new Point(50, 105),
                    Size = new Size(80, 30)
                };
                form.Controls.Add(btnAceptar);

                Button btnCancelarDlg = new Button
                {
                    Text = "Cancelar",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(150, 105),
                    Size = new Size(80, 30)
                };
                form.Controls.Add(btnCancelarDlg);

                form.AcceptButton = btnAceptar;
                form.CancelButton = btnCancelarDlg;

                if (form.ShowDialog() == DialogResult.OK && cmb.SelectedIndex >= 0)
                {
                    citaRepository.CambiarEstado(id, (EstadoCita)cmb.SelectedIndex);
                    MessageBox.Show("Estado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCitas();
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cita para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paciente = dgvCitas.SelectedRows[0].Cells["NombrePaciente"].Value.ToString();
            string fecha = dgvCitas.SelectedRows[0].Cells["FechaHora"].Value.ToString();

            var resultado = MessageBox.Show(
                $"¿Está seguro de eliminar la cita de '{paciente}' del {fecha}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvCitas.SelectedRows[0].Cells["Id"].Value);
                citaRepository.Eliminar(id);
                MessageBox.Show("Cita eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCitas();
            }
        }
    }
}

