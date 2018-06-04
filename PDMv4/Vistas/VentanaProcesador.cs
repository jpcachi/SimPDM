using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class VentanaProcesador : Form
    {
        public VentanaProcesador()
        {
            InitializeComponent();
        }

        private void VentanaProcesador_FormClosed(object sender, FormClosedEventArgs e)
        {
            //287; 55
            (Owner as SimPDM).Controls.Add(Controls[0]);
        }
    }
}
