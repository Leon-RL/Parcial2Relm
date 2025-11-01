using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Archivo: Program.cs (Dentro de la función Main)



namespace CpParcial2Relm // Asegúrate que este sea tu namespace
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- ¡LA LÍNEA CLAVE ES ESTA! ---
            Application.Run(new frmProgramaListado());
        }
    }
}
