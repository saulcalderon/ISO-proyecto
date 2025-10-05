using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Arbol;
using Desafio1App.Modelos;

namespace Desafio1App.Forms
{
    public partial class TreeViewForm : Form
    {
        private readonly ArbolClasificador arbol;
        private TreeView treeViewPacientes;
        private Button btnCerrar;
        private GroupBox grpEstadisticas;
        private Label lblEstadisticas;

        public TreeViewForm(ArbolClasificador arbol)
        {
            this.arbol = arbol;
            InitializeComponent();
            ConfigurarInterfaz();
            MostrarArbol();
        }

        private void InitializeComponent()
        {
            // Este método es necesario para evitar errores si no estás usando el diseñador
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "SCS V1.0 - Clasificación de Pacientes";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "Clasificación Jerárquica de Pacientes";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(220, 20);
            this.Controls.Add(lblTitulo);

            // Subtítulo explicativo
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Organización por Género → Tipo de Sangre → Presión Arterial";
            lblSubtitulo.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblSubtitulo.ForeColor = Color.FromArgb(100, 100, 100);
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(240, 55);
            this.Controls.Add(lblSubtitulo);

            // Panel para el TreeView
            Panel panelTree = new Panel();
            panelTree.BackColor = Color.White;
            panelTree.BorderStyle = BorderStyle.FixedSingle;
            panelTree.Location = new Point(20, 90);
            panelTree.Size = new Size(850, 330);
            this.Controls.Add(panelTree);

            treeViewPacientes = new TreeView();
            treeViewPacientes.Dock = DockStyle.Fill;
            treeViewPacientes.Font = new Font("Segoe UI", 10);
            treeViewPacientes.BorderStyle = BorderStyle.None;
            treeViewPacientes.BackColor = Color.White;
            panelTree.Controls.Add(treeViewPacientes);

            // Panel de estadísticas
            grpEstadisticas = new GroupBox();
            grpEstadisticas.Text = "  📊 Datos Estadísticos - Tipos de Sangre en El Salvador  ";
            grpEstadisticas.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpEstadisticas.ForeColor = Color.FromArgb(0, 102, 204);
            grpEstadisticas.BackColor = Color.White;
            grpEstadisticas.Size = new Size(850, 195);
            grpEstadisticas.Location = new Point(20, 440);
            
            lblEstadisticas = new Label();
            lblEstadisticas.AutoSize = false;
            lblEstadisticas.Size = new Size(820, 160);
            lblEstadisticas.Location = new Point(15, 28);
            lblEstadisticas.Font = new Font("Segoe UI", 9);
            lblEstadisticas.ForeColor = Color.FromArgb(64, 64, 64);
            lblEstadisticas.Text =
                "• Tipo de sangre O+: 62% (el más común en la población salvadoreña)\n" +
                "• Tipo de sangre A+: 23%\n" +
                "• Tipo de sangre B+: 11%\n" +
                "• Tipo de sangre AB+: 1%\n" +
                "• Otros tipos (negativos): 3%\n\n" +
                "La hipertensión (presión alta) afecta aproximadamente al 28% de los adultos\n" +
                "en El Salvador.\n\n" +
                "Fuente: Cruz Roja Salvadoreña (2025). Estadísticas de donación de sangre.";

            grpEstadisticas.Controls.Add(lblEstadisticas);
            this.Controls.Add(grpEstadisticas);

            // Botón cerrar
            btnCerrar = new Button();
            btnCerrar.Text = "✓ Cerrar";
            btnCerrar.BackColor = Color.FromArgb(0, 123, 255);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCerrar.Size = new Size(150, 45);
            btnCerrar.Location = new Point(375, 655);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void MostrarArbol()
        {
            treeViewPacientes.Nodes.Clear();
            TreeNode raizNode = new TreeNode("Pacientes");

            var datos = arbol.ObtenerTodos();

            foreach (var genero in datos)
            {
                TreeNode nodoGenero = new TreeNode(genero.Key);

                foreach (var tipoSangre in genero.Value)
                {
                    TreeNode nodoSangre = new TreeNode(tipoSangre.Key);

                    foreach (var presion in tipoSangre.Value)
                    {
                        TreeNode nodoPresion = new TreeNode(presion.Key);

                        foreach (Paciente paciente in presion.Value)
                        {
                            TreeNode nodoPaciente = new TreeNode(paciente.ToString());
                            nodoPresion.Nodes.Add(nodoPaciente);
                        }

                        nodoSangre.Nodes.Add(nodoPresion);
                    }

                    nodoGenero.Nodes.Add(nodoSangre);
                }

                raizNode.Nodes.Add(nodoGenero);
            }

            treeViewPacientes.Nodes.Add(raizNode);
            treeViewPacientes.ExpandAll();
        }
    }
}