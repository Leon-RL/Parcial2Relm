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
    public partial class frmProgramaEdicion : Form
    {
        private Programa programa;

        public frmProgramaEdicion(Programa programaActual)
        {
            InitializeComponent();
            this.programa = programaActual;
        }

      

        private void frmProgramaEdicion_Load(object sender, EventArgs e)
        {
            cargarCanales();

            if (programa.id != 0)
            {
                txtTitulo.Text = programa.titulo;
                txtProductor.Text = programa.productor;
                nudDuracion.Value = Convert.ToDecimal(programa.duracion);
                dtpFechaEstreno.Value = programa.fechaEstreno.HasValue ? programa.fechaEstreno.Value : DateTime.Now;

                cboCanal.SelectedValue = programa.idCanal;
            }
        }

        private void cargarCanales()
        {
            try
            {
                var listaCanales = ProgramaCln.listarCanales().ToList();

                cboCanal.DataSource = listaCanales;
                cboCanal.DisplayMember = "nombre"; 
                cboCanal.ValueMember = "id";     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar canales: " + ex.Message);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Recoger los datos del formulario 
                programa.idCanal = Convert.ToInt32(cboCanal.SelectedValue);
                programa.titulo = txtTitulo.Text.Trim();
                programa.productor = txtProductor.Text.Trim();
                programa.duracion = Convert.ToInt32(nudDuracion.Value);
                programa.fechaEstreno = dtpFechaEstreno.Value;

                // 2. Llamar a la lógica de negocio
                if (programa.id == 0)
                {
                    ProgramaCln.insertar(programa);
                }
                else
                {
                    ProgramaCln.actualizar(programa);
                }

                MessageBox.Show("Guardado exitosamente.", "Éxito");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
