namespace Desafio1App.Forms
{
    partial class PacienteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.nudEdad = new System.Windows.Forms.NumericUpDown();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.cmbTipoSangre = new System.Windows.Forms.ComboBox();
            this.cmbPresion = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.lblPresion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.grpContacto = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).BeginInit();
            this.panelFormulario.SuspendLayout();
            this.grpContacto.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(180, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(290, 37);
            this.lblTitulo.TabIndex = 12;
            this.lblTitulo.Text = "Registro de Paciente";
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblInstrucciones.Location = new System.Drawing.Point(100, 60);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(450, 40);
            this.lblInstrucciones.TabIndex = 13;
            this.lblInstrucciones.Text = "Complete los siguientes datos del paciente. Todos los campos son obligatorios.";
            this.lblInstrucciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFormulario
            // 
            this.panelFormulario.BackColor = System.Drawing.Color.White;
            this.panelFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormulario.Controls.Add(this.lblNombre);
            this.panelFormulario.Controls.Add(this.txtNombre);
            this.panelFormulario.Controls.Add(this.lblEdad);
            this.panelFormulario.Controls.Add(this.nudEdad);
            this.panelFormulario.Controls.Add(this.lblGenero);
            this.panelFormulario.Controls.Add(this.cmbGenero);
            this.panelFormulario.Controls.Add(this.lblTipoSangre);
            this.panelFormulario.Controls.Add(this.cmbTipoSangre);
            this.panelFormulario.Controls.Add(this.lblPresion);
            this.panelFormulario.Controls.Add(this.cmbPresion);
            this.panelFormulario.Location = new System.Drawing.Point(75, 110);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(500, 360);
            this.panelFormulario.TabIndex = 14;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNombre.Location = new System.Drawing.Point(30, 30);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(195, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre del Paciente:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(35, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(430, 30);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEdad.Location = new System.Drawing.Point(30, 105);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(101, 25);
            this.lblEdad.TabIndex = 2;
            this.lblEdad.Text = "Edad (años):";
            // 
            // nudEdad
            // 
            this.nudEdad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudEdad.Location = new System.Drawing.Point(35, 135);
            this.nudEdad.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudEdad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEdad.Name = "nudEdad";
            this.nudEdad.Size = new System.Drawing.Size(150, 30);
            this.nudEdad.TabIndex = 3;
            this.nudEdad.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGenero.Location = new System.Drawing.Point(30, 180);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(77, 25);
            this.lblGenero.TabIndex = 4;
            this.lblGenero.Text = "Género:";
            // 
            // cmbGenero
            // 
            this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenero.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.cmbGenero.Location = new System.Drawing.Point(35, 210);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(200, 31);
            this.cmbGenero.TabIndex = 5;
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoSangre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTipoSangre.Location = new System.Drawing.Point(250, 180);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(142, 25);
            this.lblTipoSangre.TabIndex = 6;
            this.lblTipoSangre.Text = "Tipo de Sangre:";
            // 
            // cmbTipoSangre
            // 
            this.cmbTipoSangre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoSangre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoSangre.FormattingEnabled = true;
            this.cmbTipoSangre.Items.AddRange(new object[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.cmbTipoSangre.Location = new System.Drawing.Point(255, 210);
            this.cmbTipoSangre.Name = "cmbTipoSangre";
            this.cmbTipoSangre.Size = new System.Drawing.Size(210, 31);
            this.cmbTipoSangre.TabIndex = 7;
            // 
            // lblPresion
            // 
            this.lblPresion.AutoSize = true;
            this.lblPresion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPresion.Location = new System.Drawing.Point(30, 260);
            this.lblPresion.Name = "lblPresion";
            this.lblPresion.Size = new System.Drawing.Size(150, 25);
            this.lblPresion.TabIndex = 8;
            this.lblPresion.Text = "Presión Arterial:";
            // 
            // cmbPresion
            // 
            this.cmbPresion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPresion.FormattingEnabled = true;
            this.cmbPresion.Items.AddRange(new object[] {
            "Alta",
            "Normal",
            "Baja"});
            this.cmbPresion.Location = new System.Drawing.Point(35, 290);
            this.cmbPresion.Name = "cmbPresion";
            this.cmbPresion.Size = new System.Drawing.Size(200, 31);
            this.cmbPresion.TabIndex = 9;
            // 
            // grpContacto
            // 
            this.grpContacto.BackColor = System.Drawing.Color.White;
            this.grpContacto.Controls.Add(this.lblTelefono);
            this.grpContacto.Controls.Add(this.txtTelefono);
            this.grpContacto.Controls.Add(this.lblEmail);
            this.grpContacto.Controls.Add(this.txtEmail);
            this.grpContacto.Controls.Add(this.lblDireccion);
            this.grpContacto.Controls.Add(this.txtDireccion);
            this.grpContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContacto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.grpContacto.Location = new System.Drawing.Point(75, 480);
            this.grpContacto.Name = "grpContacto";
            this.grpContacto.Size = new System.Drawing.Size(500, 180);
            this.grpContacto.TabIndex = 15;
            this.grpContacto.TabStop = false;
            this.grpContacto.Text = "  Datos de Contacto (Opcional)  ";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTelefono.Location = new System.Drawing.Point(20, 30);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(78, 23);
            this.lblTelefono.TabIndex = 0;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(20, 55);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(200, 30);
            this.txtTelefono.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmail.Location = new System.Drawing.Point(250, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(54, 23);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(250, 55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(230, 30);
            this.txtEmail.TabIndex = 3;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDireccion.Location = new System.Drawing.Point(20, 100);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(82, 23);
            this.lblDireccion.TabIndex = 4;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(20, 125);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(460, 45);
            this.txtDireccion.TabIndex = 5;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(150, 680);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(200, 50);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "✓ Guardar Paciente";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(370, 680);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 50);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "✕ Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // PacienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(650, 760);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.grpContacto);
            this.Controls.Add(this.panelFormulario);
            this.Controls.Add(this.lblInstrucciones);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PacienteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCS V1.0 - Registro de Paciente";
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).EndInit();
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.grpContacto.ResumeLayout(false);
            this.grpContacto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.ComboBox cmbTipoSangre;
        private System.Windows.Forms.ComboBox cmbPresion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblTipoSangre;
        private System.Windows.Forms.Label lblPresion;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpContacto;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
    }
}