// Forms/PacienteForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;
using Desafio1App.Arbol;

namespace Desafio1App.Forms
{
    public partial class PacienteForm : Form
    {

        public static ArbolClasificador arbol = new ArbolClasificador(); // Instancia del árbol

        public PacienteForm()
        {
            // Solo dejamos esta llamada, el diseñador se encargará del resto
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar entradas
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbTipoSangre.SelectedItem == null ||
                cmbPresion.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear paciente
            Paciente nuevo = new Paciente(
                txtNombre.Text.Trim(),
                (int)nudEdad.Value,
                cmbGenero.SelectedItem.ToString(),
                cmbTipoSangre.SelectedItem.ToString(),
                cmbPresion.SelectedItem.ToString()
            );




            // Insertar al árbol clasificador
            arbol.InsertarPaciente(nuevo);

            string categoria = $"Clasificado en: Género - {nuevo.Genero}, Sangre - {nuevo.TipoSangre}, Presión - {nuevo.PresionArterial}";
            MessageBox.Show($"Paciente registrado exitosamente.\n\n{categoria}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            MessageBox.Show("Paciente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
        }

    }
}
