using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Desafio1App.Arbol;

namespace Desafio1App.Forms
{
    public class EstadisticasForm : Form
    {
        private readonly ArbolClasificador arbol;

        public EstadisticasForm(ArbolClasificador arbol)
        {
            this.arbol = arbol;
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Estadísticas";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label
            {
                Text = "📊 Estadísticas de Pacientes",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(290, 20)
            };
            this.Controls.Add(lblTitulo);

            int totalPacientes = arbol.TotalPacientes;

            if (totalPacientes == 0)
            {
                Label lblSinDatos = new Label
                {
                    Text = "No hay pacientes registrados.\n\nAgregue pacientes para ver las estadísticas.",
                    Font = new Font("Segoe UI", 14),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(400, 100),
                    Location = new Point(250, 280)
                };
                this.Controls.Add(lblSinDatos);
            }
            else
            {
                // Panel resumen general
                CrearPanelResumen(totalPacientes);

                // Panel estadísticas por tipo de sangre
                CrearPanelEstadisticas("Distribución por Tipo de Sangre", arbol.ObtenerEstadisticasPorTipoSangre(), 
                    new Point(20, 170), Color.FromArgb(220, 53, 69));

                // Panel estadísticas por género
                CrearPanelEstadisticas("Distribución por Género", arbol.ObtenerEstadisticasPorGenero(), 
                    new Point(305, 170), Color.FromArgb(0, 123, 255));

                // Panel estadísticas por presión
                CrearPanelEstadisticas("Distribución por Presión Arterial", arbol.ObtenerEstadisticasPorPresion(), 
                    new Point(590, 170), Color.FromArgb(40, 167, 69));
            }

            // Botón cerrar
            Button btnCerrar = new Button
            {
                Text = "✓ Cerrar",
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(150, 45),
                Location = new Point(375, 600),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CrearPanelResumen(int total)
        {
            Panel panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(855, 80),
                Location = new Point(20, 70)
            };

            Label lblTotal = new Label
            {
                Text = $"Total de Pacientes Registrados: {total}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(280, 15)
            };
            panel.Controls.Add(lblTotal);

            double edadPromedio = arbol.ObtenerEdadPromedio();
            Label lblEdad = new Label
            {
                Text = $"Edad Promedio: {edadPromedio:F1} años",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(340, 48)
            };
            panel.Controls.Add(lblEdad);

            this.Controls.Add(panel);
        }

        private void CrearPanelEstadisticas(string titulo, Dictionary<string, int> datos, Point ubicacion, Color colorBarra)
        {
            GroupBox grupo = new GroupBox
            {
                Text = $"  {titulo}  ",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                BackColor = Color.White,
                Size = new Size(275, 400),
                Location = ubicacion
            };

            int total = datos.Values.Sum();
            int y = 35;

            foreach (var item in datos.OrderByDescending(x => x.Value))
            {
                double porcentaje = total > 0 ? (double)item.Value / total * 100 : 0;

                // Etiqueta categoría
                Label lblCategoria = new Label
                {
                    Text = item.Key,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, y)
                };
                grupo.Controls.Add(lblCategoria);

                // Cantidad y porcentaje
                Label lblCantidad = new Label
                {
                    Text = $"{item.Value} ({porcentaje:F1}%)",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    AutoSize = true,
                    Location = new Point(180, y)
                };
                grupo.Controls.Add(lblCantidad);

                y += 22;

                // Barra visual
                Panel panelBarra = new Panel
                {
                    BackColor = Color.FromArgb(230, 230, 230),
                    Size = new Size(245, 18),
                    Location = new Point(15, y)
                };

                int anchoBarra = (int)(245 * porcentaje / 100);
                Panel barraLlena = new Panel
                {
                    BackColor = colorBarra,
                    Size = new Size(Math.Max(anchoBarra, 2), 18),
                    Location = new Point(0, 0)
                };
                panelBarra.Controls.Add(barraLlena);
                grupo.Controls.Add(panelBarra);

                y += 35;
            }

            this.Controls.Add(grupo);
        }
    }
}

