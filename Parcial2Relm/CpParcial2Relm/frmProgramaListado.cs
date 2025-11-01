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
    public partial class frmProgramaListado : Form
    {
        public frmProgramaListado()
        {
            InitializeComponent();
        }

        private void frmProgramaListado_Load(object sender, EventArgs e)
        {
            ListarProgramas();
        }

        private void ListarProgramas()
        {
            try
            {
                dgvProgramas.DataSource = ProgramaCln.listar();

                dgvProgramas.Columns["id"].HeaderText = "ID";
                dgvProgramas.Columns["titulo"].HeaderText = "Título";
                dgvProgramas.Columns["duracion"].HeaderText = "Duración";
                dgvProgramas.Columns["productor"].HeaderText = "Productor";
                dgvProgramas.Columns["fechaEstreno"].HeaderText = "Fecha Estreno";

                dgvProgramas.Columns["clasificacion"].HeaderText = "Clasificación";

                dgvProgramas.Columns["idCanal"].Visible = false;
                dgvProgramas.Columns["estado"].Visible = false;
                dgvProgramas.Columns["Canal"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirFormularioEdicion(Programa programa)
        {
            frmProgramaEdicion frm = new frmProgramaEdicion(programa);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListarProgramas();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarProgramas();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirFormularioEdicion(new Programa());
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProgramas.SelectedRows.Count > 0)
            {
                int idPrograma = Convert.ToInt32(dgvProgramas.CurrentRow.Cells["id"].Value);
                Programa programaEditar = ProgramaCln.obtenerUno(idPrograma);
                if (programaEditar != null) AbrirFormularioEdicion(programaEditar);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro.", "Advertencia");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProgramas.SelectedRows.Count > 0 && MessageBox.Show("¿Seguro de eliminar?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int idPrograma = Convert.ToInt32(dgvProgramas.CurrentRow.Cells["id"].Value);
                    ProgramaCln.eliminar(idPrograma);
                    MessageBox.Show("Eliminado con éxito.");
                    ListarProgramas();
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
