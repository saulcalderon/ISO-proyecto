using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Desafio1App.Modelos;
using Desafio1App.Data;
using Desafio1App.Utils;

namespace Desafio1App.Forms
{
    public class InformesForm : Form
    {
        private readonly GeneradorInformes generador;
        private readonly PacienteRepository pacienteRepository;
        private readonly CitaRepository citaRepository;

        private ComboBox cmbTipoInforme;
        private ComboBox cmbFormato;
        private GroupBox grpFiltros;
        private ComboBox cmbFiltroGenero;
        private ComboBox cmbFiltroSangre;
        private ComboBox cmbFiltroPresion;
        private ComboBox cmbFiltroCitaEstado;
        private DateTimePicker dtpFechaDesde;
        private DateTimePicker dtpFechaHasta;
        private CheckBox chkFiltrarFechas;
        private Button btnGenerar;
        private Button btnCerrar;
        private RichTextBox txtVistaPrevia;

        public InformesForm()
        {
            generador = new GeneradorInformes();
            pacienteRepository = new PacienteRepository();
            citaRepository = new CitaRepository();
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Generación de Informes";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label
            {
                Text = "Generación de Informes",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(280, 15)
            };
            this.Controls.Add(lblTitulo);

            // Panel de configuración
            Panel panelConfig = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 55),
                Size = new Size(745, 180)
            };
            this.Controls.Add(panelConfig);

            // Tipo de informe
            Label lblTipo = new Label
            {
                Text = "Tipo de Informe:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelConfig.Controls.Add(lblTipo);

            cmbTipoInforme = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(150, 17),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTipoInforme.Items.AddRange(new object[] {
                "Lista de Pacientes",
                "Estadísticas por Tipo de Sangre",
                "Estadísticas por Presión Arterial",
                "Estadísticas por Género",
                "Lista de Citas",
                "Resumen General"
            });
            cmbTipoInforme.SelectedIndex = 0;
            cmbTipoInforme.SelectedIndexChanged += CmbTipoInforme_SelectedIndexChanged;
            panelConfig.Controls.Add(cmbTipoInforme);

            // Formato
            Label lblFormato = new Label
            {
                Text = "Formato:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(430, 20),
                AutoSize = true
            };
            panelConfig.Controls.Add(lblFormato);

            cmbFormato = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(510, 17),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFormato.Items.AddRange(new object[] {
                "HTML (para imprimir/PDF)",
                "CSV (Excel compatible)"
            });
            cmbFormato.SelectedIndex = 0;
            panelConfig.Controls.Add(cmbFormato);

            // Grupo de filtros
            grpFiltros = new GroupBox
            {
                Text = "  Filtros  ",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(20, 55),
                Size = new Size(700, 110)
            };
            panelConfig.Controls.Add(grpFiltros);

            // Filtros para pacientes
            Label lblGenero = new Label { Text = "Género:", Location = new Point(15, 30), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblGenero);

            cmbFiltroGenero = new ComboBox
            {
                Location = new Point(75, 27),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroGenero.Items.AddRange(new object[] { "Todos", "Masculino", "Femenino" });
            cmbFiltroGenero.SelectedIndex = 0;
            grpFiltros.Controls.Add(cmbFiltroGenero);

            Label lblSangre = new Label { Text = "Sangre:", Location = new Point(190, 30), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblSangre);

            cmbFiltroSangre = new ComboBox
            {
                Location = new Point(250, 27),
                Size = new Size(70, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroSangre.Items.AddRange(new object[] { "Todos", "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" });
            cmbFiltroSangre.SelectedIndex = 0;
            grpFiltros.Controls.Add(cmbFiltroSangre);

            Label lblPresion = new Label { Text = "Presión:", Location = new Point(335, 30), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblPresion);

            cmbFiltroPresion = new ComboBox
            {
                Location = new Point(395, 27),
                Size = new Size(85, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFiltroPresion.Items.AddRange(new object[] { "Todos", "Alta", "Normal", "Baja" });
            cmbFiltroPresion.SelectedIndex = 0;
            grpFiltros.Controls.Add(cmbFiltroPresion);

            // Filtro de estado para citas
            Label lblEstadoCita = new Label { Text = "Estado Cita:", Location = new Point(495, 30), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblEstadoCita);

            cmbFiltroCitaEstado = new ComboBox
            {
                Location = new Point(580, 27),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Enabled = false
            };
            cmbFiltroCitaEstado.Items.AddRange(new object[] { "Todas", "Pendiente", "Confirmada", "Completada", "Cancelada" });
            cmbFiltroCitaEstado.SelectedIndex = 0;
            grpFiltros.Controls.Add(cmbFiltroCitaEstado);

            // Filtro de fechas
            chkFiltrarFechas = new CheckBox
            {
                Text = "Filtrar por fechas:",
                Location = new Point(15, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            chkFiltrarFechas.CheckedChanged += (s, e) => {
                dtpFechaDesde.Enabled = chkFiltrarFechas.Checked;
                dtpFechaHasta.Enabled = chkFiltrarFechas.Checked;
            };
            grpFiltros.Controls.Add(chkFiltrarFechas);

            Label lblDesde = new Label { Text = "Desde:", Location = new Point(145, 72), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblDesde);

            dtpFechaDesde = new DateTimePicker
            {
                Location = new Point(195, 68),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Short,
                Enabled = false,
                Font = new Font("Segoe UI", 9)
            };
            dtpFechaDesde.Value = DateTime.Today.AddMonths(-1);
            grpFiltros.Controls.Add(dtpFechaDesde);

            Label lblHasta = new Label { Text = "Hasta:", Location = new Point(330, 72), AutoSize = true, Font = new Font("Segoe UI", 9) };
            grpFiltros.Controls.Add(lblHasta);

            dtpFechaHasta = new DateTimePicker
            {
                Location = new Point(380, 68),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Short,
                Enabled = false,
                Font = new Font("Segoe UI", 9)
            };
            grpFiltros.Controls.Add(dtpFechaHasta);

            // Vista previa
            Label lblPrevia = new Label
            {
                Text = "Vista Previa:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 245),
                AutoSize = true
            };
            this.Controls.Add(lblPrevia);

            txtVistaPrevia = new RichTextBox
            {
                Location = new Point(20, 270),
                Size = new Size(745, 320),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtVistaPrevia);

            // Botones
            btnGenerar = new Button
            {
                Text = "Generar y Abrir",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 45),
                Location = new Point(250, 605),
                Cursor = Cursors.Hand
            };
            btnGenerar.FlatAppearance.BorderSize = 0;
            btnGenerar.Click += BtnGenerar_Click;
            this.Controls.Add(btnGenerar);

            btnCerrar = new Button
            {
                Text = "Cerrar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 45),
                Location = new Point(450, 605),
                Cursor = Cursors.Hand
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            ActualizarVistaPrevia();
        }

        private void CmbTipoInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esCitas = cmbTipoInforme.SelectedIndex == 4; // Lista de Citas
            bool esResumen = cmbTipoInforme.SelectedIndex == 5; // Resumen General

            cmbFiltroGenero.Enabled = !esCitas && !esResumen;
            cmbFiltroSangre.Enabled = !esCitas && !esResumen;
            cmbFiltroPresion.Enabled = !esCitas && !esResumen;
            cmbFiltroCitaEstado.Enabled = esCitas;

            ActualizarVistaPrevia();
        }

        private void ActualizarVistaPrevia()
        {
            try
            {
                string tipoInforme = cmbTipoInforme.SelectedItem?.ToString() ?? "Lista de Pacientes";
                int totalRegistros = 0;

                switch (cmbTipoInforme.SelectedIndex)
                {
                    case 0: // Lista de Pacientes
                    case 1: // Estadísticas por Tipo de Sangre
                    case 2: // Estadísticas por Presión
                    case 3: // Estadísticas por Género
                        totalRegistros = pacienteRepository.ObtenerTotal();
                        break;
                    case 4: // Lista de Citas
                        totalRegistros = citaRepository.ObtenerTotalCitas();
                        break;
                    case 5: // Resumen General
                        totalRegistros = pacienteRepository.ObtenerTotal();
                        break;
                }

                txtVistaPrevia.Text = $"=== VISTA PREVIA DEL INFORME ===\n\n" +
                                     $"Tipo de Informe: {tipoInforme}\n" +
                                     $"Formato: {cmbFormato.SelectedItem}\n" +
                                     $"Registros disponibles: {totalRegistros}\n\n" +
                                     $"Haga clic en 'Generar y Abrir' para crear el informe completo.\n\n" +
                                     $"NOTA:\n" +
                                     $"- Los archivos HTML se abrirán en su navegador web.\n" +
                                     $"- Para guardar como PDF, use Ctrl+P en el navegador y seleccione 'Guardar como PDF'.\n" +
                                     $"- Los archivos CSV se abrirán en Excel o el programa predeterminado.";
            }
            catch (Exception ex)
            {
                txtVistaPrevia.Text = $"Error al obtener vista previa: {ex.Message}";
            }
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaArchivo = "";
                string extension = cmbFormato.SelectedIndex == 0 ? ".html" : ".csv";
                string nombreBase = $"Informe_{DateTime.Now:yyyyMMdd_HHmmss}";

                switch (cmbTipoInforme.SelectedIndex)
                {
                    case 0: // Lista de Pacientes
                        var pacientes = ObtenerPacientesFiltrados();
                        if (extension == ".html")
                        {
                            rutaArchivo = generador.GenerarHTMLPacientes(pacientes, 
                                generador.ObtenerRutaTemporal($"Pacientes_{nombreBase}.html"));
                        }
                        else
                        {
                            rutaArchivo = generador.GenerarExcelPacientes(pacientes,
                                generador.ObtenerRutaTemporal($"Pacientes_{nombreBase}.csv"));
                        }
                        break;

                    case 1: // Estadísticas por Tipo de Sangre
                        var statsSangre = pacienteRepository.ObtenerEstadisticasPor("TipoSangre");
                        if (extension == ".html")
                        {
                            rutaArchivo = generador.GenerarHTMLEstadisticas(statsSangre, "Estadísticas por Tipo de Sangre",
                                generador.ObtenerRutaTemporal($"EstadisticasSangre_{nombreBase}.html"));
                        }
                        else
                        {
                            rutaArchivo = generador.GenerarExcelEstadisticas(statsSangre, "Estadísticas por Tipo de Sangre",
                                generador.ObtenerRutaTemporal($"EstadisticasSangre_{nombreBase}.csv"));
                        }
                        break;

                    case 2: // Estadísticas por Presión
                        var statsPresion = pacienteRepository.ObtenerEstadisticasPor("PresionArterial");
                        if (extension == ".html")
                        {
                            rutaArchivo = generador.GenerarHTMLEstadisticas(statsPresion, "Estadísticas por Presión Arterial",
                                generador.ObtenerRutaTemporal($"EstadisticasPresion_{nombreBase}.html"));
                        }
                        else
                        {
                            rutaArchivo = generador.GenerarExcelEstadisticas(statsPresion, "Estadísticas por Presión Arterial",
                                generador.ObtenerRutaTemporal($"EstadisticasPresion_{nombreBase}.csv"));
                        }
                        break;

                    case 3: // Estadísticas por Género
                        var statsGenero = pacienteRepository.ObtenerEstadisticasPor("Genero");
                        if (extension == ".html")
                        {
                            rutaArchivo = generador.GenerarHTMLEstadisticas(statsGenero, "Estadísticas por Género",
                                generador.ObtenerRutaTemporal($"EstadisticasGenero_{nombreBase}.html"));
                        }
                        else
                        {
                            rutaArchivo = generador.GenerarExcelEstadisticas(statsGenero, "Estadísticas por Género",
                                generador.ObtenerRutaTemporal($"EstadisticasGenero_{nombreBase}.csv"));
                        }
                        break;

                    case 4: // Lista de Citas
                        var citas = ObtenerCitasFiltradas();
                        if (extension == ".html")
                        {
                            rutaArchivo = generador.GenerarHTMLCitas(citas,
                                generador.ObtenerRutaTemporal($"Citas_{nombreBase}.html"));
                        }
                        else
                        {
                            rutaArchivo = generador.GenerarExcelCitas(citas,
                                generador.ObtenerRutaTemporal($"Citas_{nombreBase}.csv"));
                        }
                        break;

                    case 5: // Resumen General
                        rutaArchivo = generador.GenerarHTMLResumenGeneral(
                            generador.ObtenerRutaTemporal($"ResumenGeneral_{nombreBase}.html"));
                        break;
                }

                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    generador.AbrirArchivo(rutaArchivo);
                    MessageBox.Show($"Informe generado exitosamente.\n\nUbicación: {rutaArchivo}", 
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el informe: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Paciente> ObtenerPacientesFiltrados()
        {
            string genero = cmbFiltroGenero.SelectedIndex == 0 ? null : cmbFiltroGenero.SelectedItem.ToString();
            string sangre = cmbFiltroSangre.SelectedIndex == 0 ? null : cmbFiltroSangre.SelectedItem.ToString();
            string presion = cmbFiltroPresion.SelectedIndex == 0 ? null : cmbFiltroPresion.SelectedItem.ToString();

            var pacientes = pacienteRepository.Buscar(genero, sangre, presion, null);

            if (chkFiltrarFechas.Checked)
            {
                pacientes = pacientes.FindAll(p => 
                    p.FechaRegistro.Date >= dtpFechaDesde.Value.Date && 
                    p.FechaRegistro.Date <= dtpFechaHasta.Value.Date);
            }

            return pacientes;
        }

        private List<Cita> ObtenerCitasFiltradas()
        {
            List<Cita> citas;

            if (cmbFiltroCitaEstado.SelectedIndex == 0)
            {
                citas = citaRepository.ObtenerTodas();
            }
            else
            {
                citas = citaRepository.ObtenerPorEstado((EstadoCita)(cmbFiltroCitaEstado.SelectedIndex - 1));
            }

            if (chkFiltrarFechas.Checked)
            {
                citas = citas.FindAll(c => 
                    c.FechaHora.Date >= dtpFechaDesde.Value.Date && 
                    c.FechaHora.Date <= dtpFechaHasta.Value.Date);
            }

            return citas;
        }
    }
}

