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
            this.Text = "Árbol de Clasificación de Pacientes";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            treeViewPacientes = new TreeView();
            treeViewPacientes.Dock = DockStyle.Top;
            treeViewPacientes.Height = 350;
            this.Controls.Add(treeViewPacientes);

            grpEstadisticas = new GroupBox();
            grpEstadisticas.Text = "Datos Estadísticos en El Salvador";
            grpEstadisticas.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grpEstadisticas.ForeColor = Color.Black;
            grpEstadisticas.BackColor = Color.FromArgb(230, 240, 255);
            grpEstadisticas.Size = new Size(450, 130);
            grpEstadisticas.Location = new Point(10, 360);

            lblEstadisticas = new Label();
            lblEstadisticas.AutoSize = false;
            lblEstadisticas.Size = new Size(650, 600);
            lblEstadisticas.Location = new Point(10, 20);
            lblEstadisticas.Font = new Font("Segoe UI", 7);
            lblEstadisticas.Text =
                "- Tipo de sangre O+: 62%\n" +
                "- Tipo de sangre A+: 23%\n" +
                "- Tipo de sangre B+: 11%\n" +
                "- Tipo de sangre AB+: 1%\n" +
                "- Otros: 3%\n\n" +
                "Presión alta afecta al 28% de los adultos en El Salvador.\n" +
                "Fuente: YSKL - Cruz Roja Salvadoreña (2025). Estadísticas de sangre. https://cruzrojasal.org.sv/";

            grpEstadisticas.Controls.Add(lblEstadisticas);
            this.Controls.Add(grpEstadisticas);






            btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.BackColor = Color.LightCoral;
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.Location = new Point((this.ClientSize.Width - btnCerrar.Width) / 2, 420);
            btnCerrar.Anchor = AnchorStyles.Bottom;
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

