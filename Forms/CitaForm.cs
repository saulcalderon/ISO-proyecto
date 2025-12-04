using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;
using Desafio1App.Data;

namespace Desafio1App.Forms
{
    public class CitaForm : Form
    {
        private readonly CitaRepository citaRepository;
        private readonly PacienteRepository pacienteRepository;
        private readonly Cita citaEdicion;
        private readonly bool modoEdicion;

        private ComboBox cmbPaciente;
        private DateTimePicker dtpFecha;
        private DateTimePicker dtpHora;
        private TextBox txtMotivo;
        private ComboBox cmbEstado;
        private TextBox txtObservaciones;
        private Button btnGuardar;
        private Button btnCancelar;

        public CitaForm()
        {
            citaRepository = new CitaRepository();
            pacienteRepository = new PacienteRepository();
            modoEdicion = false;
            ConfigurarInterfaz();
            CargarPacientes();
        }

        public CitaForm(Cita cita)
        {
            citaRepository = new CitaRepository();
            pacienteRepository = new PacienteRepository();
            citaEdicion = cita;
            modoEdicion = true;
            ConfigurarInterfaz();
            CargarPacientes();
            CargarDatosCita();
            this.Text = "SCS V1.0 - Editar Cita";
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Nueva Cita";
            this.Size = new Size(500, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Título
            Label lblTitulo = new Label
            {
                Text = modoEdicion ? "Editar Cita" : "Nueva Cita Médica",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(150, 20)
            };
            this.Controls.Add(lblTitulo);

            // Panel del formulario
            Panel panelForm = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(30, 60),
                Size = new Size(420, 350)
            };
            this.Controls.Add(panelForm);

            int y = 20;
            int labelWidth = 100;
            int controlX = 115;

            // Paciente
            Label lblPaciente = new Label
            {
                Text = "Paciente:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y + 3),
                AutoSize = true
            };
            panelForm.Controls.Add(lblPaciente);

            cmbPaciente = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(controlX, y),
                Size = new Size(280, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelForm.Controls.Add(cmbPaciente);

            // Fecha
            y += 45;
            Label lblFecha = new Label
            {
                Text = "Fecha:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y + 3),
                AutoSize = true
            };
            panelForm.Controls.Add(lblFecha);

            dtpFecha = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(controlX, y),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today
            };
            panelForm.Controls.Add(dtpFecha);

            // Hora
            y += 45;
            Label lblHora = new Label
            {
                Text = "Hora:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y + 3),
                AutoSize = true
            };
            panelForm.Controls.Add(lblHora);

            dtpHora = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(controlX, y),
                Size = new Size(120, 30),
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };
            panelForm.Controls.Add(dtpHora);

            // Motivo
            y += 45;
            Label lblMotivo = new Label
            {
                Text = "Motivo:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y + 3),
                AutoSize = true
            };
            panelForm.Controls.Add(lblMotivo);

            txtMotivo = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(controlX, y),
                Size = new Size(280, 30)
            };
            panelForm.Controls.Add(txtMotivo);

            // Estado
            y += 45;
            Label lblEstado = new Label
            {
                Text = "Estado:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y + 3),
                AutoSize = true
            };
            panelForm.Controls.Add(lblEstado);

            cmbEstado = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(controlX, y),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "Confirmada", "Completada", "Cancelada" });
            cmbEstado.SelectedIndex = 0;
            panelForm.Controls.Add(cmbEstado);

            // Observaciones
            y += 45;
            Label lblObservaciones = new Label
            {
                Text = "Observaciones:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(15, y),
                AutoSize = true
            };
            panelForm.Controls.Add(lblObservaciones);

            y += 25;
            txtObservaciones = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(15, y),
                Size = new Size(380, 70),
                Multiline = true
            };
            panelForm.Controls.Add(txtObservaciones);

            // Botones
            btnGuardar = new Button
            {
                Text = modoEdicion ? "Actualizar" : "Guardar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 45),
                Location = new Point(120, 425),
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 45),
                Location = new Point(260, 425),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);
        }

        private void CargarPacientes()
        {
            var pacientes = pacienteRepository.ObtenerTodos();
            cmbPaciente.DisplayMember = "DescripcionCorta";
            cmbPaciente.ValueMember = "Id";
            cmbPaciente.DataSource = pacientes;
        }

        private void CargarDatosCita()
        {
            if (citaEdicion != null)
            {
                foreach (Paciente p in cmbPaciente.Items)
                {
                    if (p.Id == citaEdicion.PacienteId)
                    {
                        cmbPaciente.SelectedItem = p;
                        break;
                    }
                }
                dtpFecha.Value = citaEdicion.FechaHora.Date;
                dtpHora.Value = citaEdicion.FechaHora;
                txtMotivo.Text = citaEdicion.Motivo;
                cmbEstado.SelectedIndex = (int)citaEdicion.Estado;
                txtObservaciones.Text = citaEdicion.Observaciones;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (cmbPaciente.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Ingrese el motivo de la cita.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaHora = dtpFecha.Value.Date.Add(dtpHora.Value.TimeOfDay);

            if (fechaHora < DateTime.Now && !modoEdicion)
            {
                MessageBox.Show("La fecha y hora de la cita debe ser futura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var paciente = (Paciente)cmbPaciente.SelectedItem;

                if (modoEdicion)
                {
                    citaEdicion.PacienteId = paciente.Id;
                    citaEdicion.FechaHora = fechaHora;
                    citaEdicion.Motivo = txtMotivo.Text.Trim();
                    citaEdicion.Estado = (EstadoCita)cmbEstado.SelectedIndex;
                    citaEdicion.Observaciones = txtObservaciones.Text.Trim();

                    citaRepository.Actualizar(citaEdicion);
                    MessageBox.Show("Cita actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var nuevaCita = new Cita(
                        paciente.Id,
                        fechaHora,
                        txtMotivo.Text.Trim(),
                        txtObservaciones.Text.Trim()
                    );
                    nuevaCita.Estado = (EstadoCita)cmbEstado.SelectedIndex;

                    citaRepository.Insertar(nuevaCita);
                    MessageBox.Show("Cita creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

