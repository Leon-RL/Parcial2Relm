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
    public partial class frmCanalEdicion : Form
    {
        private Canal canal;

        public frmCanalEdicion(Canal canalActual)
        {
            InitializeComponent();
            this.canal = canalActual;
        }

        private void frmCanalEdicion_Load(object sender, EventArgs e)
        {
            if (canal.id != 0)
            {
      
                txtTitulo.Text = canal.nombre;
                txtProductor.Text = canal.frecuencia;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                canal.nombre = txtTitulo.Text.Trim();
                canal.frecuencia = txtProductor.Text.Trim();

                
                if (canal.id == 0)
                {
                    CanalCln.insertar(canal);
                }
                else
                {
                    CanalCln.actualizar(canal);
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
