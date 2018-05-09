using System;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class Busqueda : Form
    {
        private Ayuda ventanaPadre;
        private bool up = false;
        private string busqueda = string.Empty;
        private bool busquedaFin = false;
        public Busqueda(Ayuda ventanaPadre)
        {
            InitializeComponent();
            this.ventanaPadre = ventanaPadre;
        }


        private void Busqueda_Shown(object sender, EventArgs e)
        {
            textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = ventanaPadre.Buscar(textBox1.Text, checkBox1.Checked, radioButton1.Checked);
            busquedaFin = !button1.Enabled;
            if (busquedaFin)
            {
                up = radioButton1.Checked;
                busqueda = textBox1.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(busquedaFin)
                button1.Enabled = busqueda != textBox1.Text;
            else 
                button1.Enabled = !string.IsNullOrEmpty(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (busquedaFin)
                button1.Enabled = radioButton1.Checked != up;
            else button1.Enabled = true;
        }
    }
}
