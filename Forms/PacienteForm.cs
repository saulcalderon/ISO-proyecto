using System;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;
using Desafio1App.Arbol;

namespace Desafio1App.Forms
{
    public partial class PacienteForm : Form
    {
        public static ArbolClasificador arbol = new ArbolClasificador();
        
        private Paciente pacienteEdicion;
        private ArbolClasificador arbolReferencia;
        private bool modoEdicion;

        // Constructor para agregar nuevo paciente
        public PacienteForm()
        {
            InitializeComponent();
            modoEdicion = false;
            arbolReferencia = arbol;
        }

        // Constructor para editar paciente existente
        public PacienteForm(Paciente paciente, ArbolClasificador arbolRef)
        {
            InitializeComponent();
            modoEdicion = true;
            pacienteEdicion = paciente;
            arbolReferencia = arbolRef;
            CargarDatosPaciente();
            
            this.Text = "SCS V1.0 - Editar Paciente";
            lblTitulo.Text = "Editar Paciente";
            btnGuardar.Text = "💾 Actualizar";
        }

        private void CargarDatosPaciente()
        {
            txtNombre.Text = pacienteEdicion.Nombre;
            nudEdad.Value = pacienteEdicion.Edad;
            cmbGenero.SelectedItem = pacienteEdicion.Genero;
            cmbTipoSangre.SelectedItem = pacienteEdicion.TipoSangre;
            cmbPresion.SelectedItem = pacienteEdicion.PresionArterial;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbTipoSangre.SelectedItem == null ||
                cmbPresion.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modoEdicion)
            {
                pacienteEdicion.Nombre = txtNombre.Text.Trim();
                pacienteEdicion.Edad = (int)nudEdad.Value;
                pacienteEdicion.Genero = cmbGenero.SelectedItem.ToString();
                pacienteEdicion.TipoSangre = cmbTipoSangre.SelectedItem.ToString();
                pacienteEdicion.PresionArterial = cmbPresion.SelectedItem.ToString();

                arbolReferencia.EditarPaciente(pacienteEdicion);
                MessageBox.Show("Paciente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Paciente nuevo = new Paciente(
                    txtNombre.Text.Trim(),
                    (int)nudEdad.Value,
                    cmbGenero.SelectedItem.ToString(),
                    cmbTipoSangre.SelectedItem.ToString(),
                    cmbPresion.SelectedItem.ToString()
                );

                arbolReferencia.InsertarPaciente(nuevo);
                string categoria = $"Clasificado en: Género - {nuevo.Genero}, Sangre - {nuevo.TipoSangre}, Presión - {nuevo.PresionArterial}";
                MessageBox.Show($"Paciente registrado exitosamente.\n\n{categoria}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        
        private void btnGuardar_Click_1(object sender, EventArgs e) { }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
