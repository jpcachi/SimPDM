using PDMv4.Procesador;
using System;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class ModificarContenido : Form
    {

        private int valor;
        private int pos;
        private bool memoria;
        public ModificarContenido(int pos, bool memoria = false)
        {
            InitializeComponent();
            this.pos = pos;
            this.memoria = memoria;
            Text = "Modificar contenido de " + (memoria ? ("dirección de memoria 0x" + pos.ToString("X4")) : ("registro " + Main.ObtenerNombreRegistro(pos)));
            byte valorActual = memoria ? Main.ObtenerMemoria.ObtenerDireccion((ushort)pos).Contenido : Main.ObtenerRegistro((ushort)pos).Contenido;
            textBox1.Text = valorActual.ToString("D3");
            textBox2.Text = Utilidades.UtilidadesConversion.ToCa2(valorActual).ToString("D3");
            textBox3.Text = valorActual.ToString("X2");
            textBox4.Text = Utilidades.UtilidadesConversion.ToBin(valorActual, 8);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int num) && num < 256 && num > -1)
            {
                textBox3.Text = ((byte)num).ToString("X2");
                textBox2.Text = Utilidades.UtilidadesConversion.ToCa2(num).ToString("D3");
                textBox4.Text = Utilidades.UtilidadesConversion.ToBin(num, 8);
                valor = num;
                button1.Enabled = true;
            }
            else
            {
                textBox3.Clear();
                textBox2.Clear();
                textBox4.Clear();
                button1.Enabled = false;
            }
            textBox1.Select(textBox1.Text.Length, 0);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int num) && num < 128 && num > -129)
            {
                textBox3.Text = ((byte)num).ToString("X2");
                textBox1.Text = Utilidades.UtilidadesConversion.ToDecimal(num).ToString("D3");
                textBox4.Text = Utilidades.UtilidadesConversion.ToBin(num, 8);
                valor = Utilidades.UtilidadesConversion.ToDecimal(num);
                button1.Enabled = true;
            }
            else
            {
                textBox3.Clear();
                textBox1.Clear();
                textBox4.Clear();
                button1.Enabled = false;
            }
            textBox2.Select(textBox2.Text.Length, 0);
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out int num) && num < 256)
            {
                textBox1.Text = num.ToString("D3");
                textBox2.Text = Utilidades.UtilidadesConversion.ToCa2(num).ToString("D3");
                textBox4.Text = Utilidades.UtilidadesConversion.ToBin(num, 8);
                valor = num;
                button1.Enabled = true;
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();
                button1.Enabled = false;
            }
            textBox3.Select(textBox3.Text.Length, 0);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            int num = -1;
            try
            {
                num = Utilidades.UtilidadesConversion.FromBinary(textBox4.Text, 8);
            }
            catch
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                button1.Enabled = false;
            }
            if (num < 256 && num > -1)
            {
                textBox3.Text = ((byte)num).ToString("X2");
                textBox1.Text = Utilidades.UtilidadesConversion.ToDecimal(num).ToString("D3");
                textBox2.Text = Utilidades.UtilidadesConversion.ToCa2(num).ToString("D3");
                textBox4.Select(textBox4.Text.Length, 0);
                valor = num;
                button1.Enabled = true;
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                button1.Enabled = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(memoria)
                Main.ObtenerMemoria.EscribirMemoria((byte)valor, pos);
            else 
                Main.ObtenerRegistro(pos).Contenido = (byte)valor;

            Main.EditadaMemoriaManualmente = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
