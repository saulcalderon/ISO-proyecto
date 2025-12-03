using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Desafio1App.Modelos;

namespace Desafio1App.Forms
{
    public class GestionUsuariosForm : Form
    {
        private DataGridView dgvUsuarios;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCerrar;
        private List<Usuario> usuarios;

        public GestionUsuariosForm()
        {
            ConfigurarFormulario();
            CargarUsuarios();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Gestión de Usuarios";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "👥 Gestión de Usuarios",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            this.Controls.Add(lblTitulo);

            dgvUsuarios = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(745, 320),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            this.Controls.Add(dgvUsuarios);

            int btnY = 400;
            int btnWidth = 120;
            int btnHeight = 40;
            int spacing = 15;

            btnAgregar = CrearBoton("➕ Agregar", new Point(20, btnY), Color.FromArgb(40, 167, 69), btnWidth, btnHeight);
            btnAgregar.Click += BtnAgregar_Click;
            this.Controls.Add(btnAgregar);

            btnEditar = CrearBoton("✏️ Editar", new Point(20 + btnWidth + spacing, btnY), Color.FromArgb(0, 123, 255), btnWidth, btnHeight);
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            btnEliminar = CrearBoton("🗑️ Eliminar", new Point(20 + (btnWidth + spacing) * 2, btnY), Color.FromArgb(220, 53, 69), btnWidth, btnHeight);
            btnEliminar.Click += BtnEliminar_Click;
            this.Controls.Add(btnEliminar);

            btnCerrar = CrearBoton("Cerrar", new Point(645, btnY), Color.FromArgb(108, 117, 125), btnWidth, btnHeight);
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private Button CrearBoton(string texto, Point ubicacion, Color colorFondo, int ancho, int alto)
        {
            return new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(ancho, alto),
                Location = ubicacion,
                BackColor = colorFondo,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }

        private void CargarUsuarios()
        {
            usuarios = GestorUsuarios.ObtenerTodos();
            
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.Columns.Add("Id", "ID");
            dgvUsuarios.Columns.Add("NombreUsuario", "Usuario");
            dgvUsuarios.Columns.Add("NombreCompleto", "Nombre Completo");
            dgvUsuarios.Columns.Add("Rol", "Rol");
            dgvUsuarios.Columns.Add("FechaCreacion", "Fecha Creación");

            dgvUsuarios.Columns["Id"].Width = 50;
            dgvUsuarios.Columns["NombreUsuario"].Width = 120;
            dgvUsuarios.Columns["NombreCompleto"].Width = 200;
            dgvUsuarios.Columns["Rol"].Width = 120;
            dgvUsuarios.Columns["FechaCreacion"].Width = 150;

            dgvUsuarios.Rows.Clear();
            foreach (var usuario in usuarios)
            {
                dgvUsuarios.Rows.Add(
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.NombreCompleto,
                    usuario.DescripcionRol,
                    usuario.FechaCreacion.ToString("dd/MM/yyyy HH:mm")
                );
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new UsuarioEditorForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CargarUsuarios();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["Id"].Value);
            var usuario = GestorUsuarios.ObtenerPorId(id);
            
            if (usuario != null)
            {
                using (var form = new UsuarioEditorForm(usuario))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        CargarUsuarios();
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["Id"].Value);
            string nombreUsuario = dgvUsuarios.SelectedRows[0].Cells["NombreUsuario"].Value.ToString();

            var resultado = MessageBox.Show(
                $"¿Está seguro de eliminar al usuario '{nombreUsuario}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                if (GestorUsuarios.Eliminar(id))
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class UsuarioEditorForm : Form
    {
        private TextBox txtNombreUsuario;
        private TextBox txtContrasena;
        private TextBox txtConfirmarContrasena;
        private TextBox txtNombreCompleto;
        private ComboBox cmbRol;
        private Button btnGuardar;
        private Button btnCancelar;
        private Usuario usuarioEditar;
        private bool esEdicion;

        public UsuarioEditorForm() : this(null) { }

        public UsuarioEditorForm(Usuario usuario)
        {
            usuarioEditar = usuario;
            esEdicion = usuario != null;
            ConfigurarFormulario();
            
            if (esEdicion)
            {
                CargarDatosUsuario();
            }
        }

        private void ConfigurarFormulario()
        {
            this.Text = esEdicion ? "Editar Usuario" : "Nuevo Usuario";
            this.Size = new Size(400, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int lblX = 20;
            int txtX = 150;
            int y = 20;
            int spacing = 45;

            CrearCampo("Usuario:", ref txtNombreUsuario, lblX, txtX, ref y, spacing);
            CrearCampo("Contraseña:", ref txtContrasena, lblX, txtX, ref y, spacing, true);
            CrearCampo("Confirmar:", ref txtConfirmarContrasena, lblX, txtX, ref y, spacing, true);
            CrearCampo("Nombre Completo:", ref txtNombreCompleto, lblX, txtX, ref y, spacing);

            Label lblRol = new Label
            {
                Text = "Rol:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(lblX, y + 3),
                AutoSize = true
            };
            this.Controls.Add(lblRol);

            cmbRol = new ComboBox
            {
                Location = new Point(txtX, y),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Personal de Salud");
            cmbRol.SelectedIndex = 1;
            this.Controls.Add(cmbRol);

            y += spacing + 20;

            btnGuardar = new Button
            {
                Text = "💾 Guardar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(80, y),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(210, y),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);

            if (esEdicion)
            {
                Label lblNota = new Label
                {
                    Text = "* Deje vacío para mantener la contraseña actual",
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(lblX, y + 45),
                    AutoSize = true
                };
                this.Controls.Add(lblNota);
            }
        }

        private void CrearCampo(string etiqueta, ref TextBox textBox, int lblX, int txtX, ref int y, int spacing, bool esPassword = false)
        {
            Label lbl = new Label
            {
                Text = etiqueta,
                Font = new Font("Segoe UI", 10),
                Location = new Point(lblX, y + 3),
                AutoSize = true
            };
            this.Controls.Add(lbl);

            textBox = new TextBox
            {
                Location = new Point(txtX, y),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };
            if (esPassword)
            {
                textBox.PasswordChar = '●';
            }
            this.Controls.Add(textBox);

            y += spacing;
        }

        private void CargarDatosUsuario()
        {
            txtNombreUsuario.Text = usuarioEditar.NombreUsuario;
            txtNombreCompleto.Text = usuarioEditar.NombreCompleto;
            cmbRol.SelectedIndex = (int)usuarioEditar.Rol;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var usuario = new Usuario
            {
                Id = esEdicion ? usuarioEditar.Id : 0,
                NombreUsuario = txtNombreUsuario.Text.Trim(),
                Contraseña = txtContrasena.Text,
                NombreCompleto = txtNombreCompleto.Text.Trim(),
                Rol = (RolUsuario)cmbRol.SelectedIndex
            };

            bool exito;
            if (esEdicion)
            {
                exito = GestorUsuarios.Actualizar(usuario);
            }
            else
            {
                exito = GestorUsuarios.Agregar(usuario);
            }

            if (exito)
            {
                MessageBox.Show(
                    esEdicion ? "Usuario actualizado correctamente." : "Usuario creado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show("El nombre completo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return false;
            }

            if (!esEdicion && string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("La contraseña es obligatoria para nuevos usuarios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtContrasena.Text) && txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarContrasena.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtContrasena.Text) && txtContrasena.Text.Length < 4)
            {
                MessageBox.Show("La contraseña debe tener al menos 4 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }

            int? exceptoId = esEdicion ? usuarioEditar.Id : (int?)null;
            if (GestorUsuarios.ExisteNombreUsuario(txtNombreUsuario.Text.Trim(), exceptoId))
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreUsuario.Focus();
                return false;
            }

            return true;
        }
    }
}

