using PDMv4.Procesador;
using System;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class Opciones : Form
    {
        public Opciones()
        {
            InitializeComponent();

            checkBox1.Checked = OpcionesPrograma.EntradaSalida;

            comboBox5.SelectedIndex = OpcionesPrograma.DireccionMemoriaComienzoPrograma / 4096;
            comboBox12.SelectedIndex = (OpcionesPrograma.DireccionMemoriaComienzoPrograma % 4096) / 256;
            comboBox13.SelectedIndex = (OpcionesPrograma.DireccionMemoriaComienzoPrograma % 256) / 16;
            comboBox14.SelectedIndex = OpcionesPrograma.DireccionMemoriaComienzoPrograma % 16;

            comboBox1.Text = OpcionesPrograma.FicheroEntrada;
            comboBox3.Text = OpcionesPrograma.FicheroSalida;

            comboBox2.SelectedIndex = OpcionesPrograma.DireccionMemoriaEntrada / 4096;
            comboBox6.SelectedIndex = (OpcionesPrograma.DireccionMemoriaEntrada % 4096) / 256;
            comboBox7.SelectedIndex = (OpcionesPrograma.DireccionMemoriaEntrada % 256) / 16;
            comboBox8.SelectedIndex = OpcionesPrograma.DireccionMemoriaEntrada % 16;

            comboBox4.SelectedIndex = OpcionesPrograma.DireccionMemoriaSalida / 4096;
            comboBox9.SelectedIndex = (OpcionesPrograma.DireccionMemoriaSalida % 4096) / 256;
            comboBox10.SelectedIndex = (OpcionesPrograma.DireccionMemoriaSalida % 256) / 16;
            comboBox11.SelectedIndex = OpcionesPrograma.DireccionMemoriaSalida % 16;
        }

        private void comboBox_Entrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCampoTextoMemoria(comboBox2.SelectedIndex, comboBox6.SelectedIndex, comboBox7.SelectedIndex, comboBox8.SelectedIndex, textBox2);
        }

        private void comboBox_Salida_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCampoTextoMemoria(comboBox4.SelectedIndex, comboBox9.SelectedIndex, comboBox10.SelectedIndex, comboBox11.SelectedIndex, textBox3);
        }

        private void comboBox_Memoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCampoTextoMemoria(comboBox5.SelectedIndex, comboBox12.SelectedIndex, comboBox13.SelectedIndex, comboBox14.SelectedIndex, textBox4);
        }

        private void ActualizarCampoTextoMemoria(int index1, int index2, int index3, int index4, TextBox textBox)
        {
            if (index1 == -1 || index2 == -1 || index3 == -1 || index4 == -1)
                textBox.Clear();
            else
                textBox.Text = "0x" + index1.ToString("X") + index2.ToString("X") + index3.ToString("X") + index4.ToString("X");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpcionesPrograma.EntradaSalida = checkBox1.Checked;

            if (OpcionesPrograma.EntradaSalida)
            {
                OpcionesPrograma.FicheroEntrada = comboBox1.Text.Trim();
                OpcionesPrograma.FicheroSalida = string.IsNullOrWhiteSpace(comboBox3.Text.Trim()) ? "out.txt" : comboBox3.Text.Trim();
                OpcionesPrograma.DireccionMemoriaEntrada = (ushort)(comboBox2.SelectedIndex * 4096 + comboBox6.SelectedIndex * 256 + comboBox7.SelectedIndex * 16 + comboBox8.SelectedIndex);
                OpcionesPrograma.DireccionMemoriaSalida = (ushort)(comboBox4.SelectedIndex * 4096 + comboBox9.SelectedIndex * 256 + comboBox10.SelectedIndex * 16 + comboBox11.SelectedIndex);

                try
                {
                    if (UC.ArchivoES == null)
                        UC.ArchivoES = new EntradaSalida.FicheroES(OpcionesPrograma.DireccionMemoriaEntrada, OpcionesPrograma.FicheroEntrada, OpcionesPrograma.DireccionMemoriaSalida, OpcionesPrograma.FicheroSalida);
                    else
                        UC.ArchivoES.ActualizarFicheroEntrada(OpcionesPrograma.DireccionMemoriaEntrada, OpcionesPrograma.FicheroEntrada, OpcionesPrograma.DireccionMemoriaSalida, OpcionesPrograma.FicheroSalida);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "No se pudo encontrar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UC.ArchivoES?.CerrarFichero();
                    return;
                }
            }
            else
            {
                UC.ArchivoES.CerrarFichero();
            }

            OpcionesPrograma.DireccionMemoriaComienzoPrograma = (ushort)(comboBox5.SelectedIndex * 4096 + comboBox12.SelectedIndex * 256 + comboBox13.SelectedIndex * 16 + comboBox14.SelectedIndex);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
            groupBox2.Enabled = checkBox1.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "in.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Text = openFileDialog1.FileName;
                button3.Enabled = comboBox1.Text != comboBox3.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "out.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                comboBox3.Text = openFileDialog1.FileName;
                button3.Enabled = comboBox1.Text != comboBox3.Text;
            }          
        }
    }
}
