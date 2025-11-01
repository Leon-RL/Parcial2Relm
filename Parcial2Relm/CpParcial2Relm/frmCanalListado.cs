using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ClnParcial2Relm;
using CadParcial2Relm;


namespace CpParcial2Relm
{
    public partial class frmCanalListado : Form
    {
        public frmCanalListado()
        {
            InitializeComponent();
        }

        private void frmCanalListado_Load(object sender, EventArgs e)
        {
            ListarCanales();
        }

        private void ListarCanales()
        {
            try
            {
                dgvCanal.DataSource = CanalCln.listar();

                // Configuración de Columnas (Ocultar y renombrar)
                dgvCanal.Columns["id"].HeaderText = "ID";
                dgvCanal.Columns["nombre"].HeaderText = "Nombre del Canal";
                dgvCanal.Columns["frecuencia"].HeaderText = "Frecuencia";

                // Ocultar campo de estado
                dgvCanal.Columns["estado"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirFormularioEdicion(Canal canal)
        {
            frmCanalEdicion frm = new frmCanalEdicion(canal);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListarCanales();
            }
        }

        // --- EVENTOS DE BOTONES ---

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarCanales();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirFormularioEdicion(new Canal());
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCanal.SelectedRows.Count > 0)
            {
                int idCanal = Convert.ToInt32(dgvCanal.CurrentRow.Cells["id"].Value);
                Canal canalEditar = CanalCln.obtenerUno(idCanal);
                if (canalEditar != null) AbrirFormularioEdicion(canalEditar);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro.", "Advertencia");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCanal.SelectedRows.Count > 0 && MessageBox.Show("¿Seguro de eliminar?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int idCanal = Convert.ToInt32(dgvCanal.CurrentRow.Cells["id"].Value);
                    CanalCln.eliminar(idCanal);
                    MessageBox.Show("Eliminado con éxito.");
                    ListarCanales();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error");
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
