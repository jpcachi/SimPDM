using PDMv4.Argumentos;
using PDMv4.Utilidades;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class EditorCodigo : Form
    {
        private string _ruta;
        private string programaOriginal;
        private StreamWriter writer;
        private StreamReader reader;

        public string Ruta { get => _ruta; set => _ruta = value; }

        public EditorCodigo(string ruta = null)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(UtilidadesInstruccion.InstruccionesTexto.ToArray());
            panelMejorado1.ContentControls.Add(editorTexto1);
            toolStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            contextMenuStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            seleccionartodoToolStripMenuItem.Enabled = !string.IsNullOrEmpty(editorTexto1.richTextBoxEditor.Text);

            editorTexto1.richTextBoxEditor.ForeColor = Color.ForestGreen;
            editorTexto1.richTextBoxEditor.Focus();
            editorTexto1.richTextBoxEditor.TextChanged += RichTextBoxEditor_TextChanged;
            editorTexto1.richTextBoxEditor.SelectionChanged += RichTextBoxEditor_SelectionChanged;
            editorTexto1.richTextBoxEditor.GotFocus += RichTextBoxEditor_GotFocus;
            editorTexto1.richTextBoxEditor.LostFocus += RichTextBoxEditor_LostFocus;
            editorTexto1.richTextBoxEditor.MouseUp+= RichTextBoxEditor_MouseUp;

            if (ruta != null)
            {
                reader = new StreamReader(ruta);
                programaOriginal = reader.ReadToEnd();
                programaOriginal = programaOriginal.Replace("\r\n", "\n");
                editorTexto1.richTextBoxEditor.Text = programaOriginal;
                reader.Close();
                _ruta = ruta;
                guardarToolStripButton.Enabled = true;
            }
        }

        private void RichTextBoxEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(editorTexto1.richTextBoxEditor.PointToScreen(e.Location));
            }
        }

        private void RichTextBoxEditor_LostFocus(object sender, EventArgs e)
        {
            pegarToolStripButton.Enabled = false;
            pegarToolStripMenuItem.Enabled = false;
        }

        private void RichTextBoxEditor_GotFocus(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                Clipboard.SetText(Clipboard.GetText());
                pegarToolStripButton.Enabled = true;
                pegarToolStripMenuItem.Enabled = true;
            }
        }

        private void RichTextBoxEditor_SelectionChanged(object sender, EventArgs e)
        {
            bool activarCopiarCortar = editorTexto1.richTextBoxEditor.SelectionLength > 0;
            copiarToolStripButton.Enabled = activarCopiarCortar;
            copiarToolStripMenuItem.Enabled = activarCopiarCortar;
            cortarToolStripButton.Enabled = activarCopiarCortar;
            cortarToolStripMenuItem.Enabled = activarCopiarCortar;
        }

        private void RichTextBoxEditor_TextChanged(object sender, EventArgs e)
        {
            bool activarGuardarSeleccionar = !string.IsNullOrWhiteSpace(editorTexto1.richTextBoxEditor.Text);
            button2.Enabled = activarGuardarSeleccionar;
            button4.Enabled = activarGuardarSeleccionar;
            guardarToolStripButton.Enabled = activarGuardarSeleccionar;
            seleccionartodoToolStripMenuItem.Enabled = activarGuardarSeleccionar;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string argumento1 = string.Empty;
            string argumento2 = string.Empty;

            if (textBox2.Visible)
                argumento1 = textBox2.Text;
            else if (comboBox2.Visible)
                argumento1 = comboBox2.Text;
            else if (numericUpDown1.Visible)
                argumento1 = numericUpDown1.Text + (checkBox1.Checked ? "h" : string.Empty);

            if (textBox3.Visible)
                argumento2 = textBox3.Text;
            else if (comboBox3.Visible)
                argumento2 = comboBox3.Text;

            string instruccion = (string.IsNullOrWhiteSpace(textBox1.Text) ? string.Empty : (textBox1.Text + " ")) + 
                comboBox1.Text + (argumento1 != string.Empty ? " " + argumento1 : string.Empty) + 
                (argumento2 != string.Empty ? ", " + argumento2 : string.Empty);

            editorTexto1.AñadirInstruccion(instruccion);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = comboBox1.SelectedIndex != -1 ? UtilidadesInstruccion.DescripcionesInstruccion[comboBox1.SelectedIndex] : string.Empty;
            switch (comboBox1.SelectedItem)
            {
                case "LD":
                case "ST":
                case "ADD":
                case "SUB":
                case "CMP":
                case "ANA":
                case "ORA":
                case "XRA":
                    MostrarSelectorArgumentos(Argumento.Tipo.Registro);
                    break;
                case "ADI":
                case "SUI":
                case "CMI":
                case "ANI":
                case "ORI":
                case "XRI":
                    MostrarSelectorArgumentos(Argumento.Tipo.Literal);
                    break;
                case "JMP":
                case "BEQ":
                case "BC":
                    MostrarSelectorArgumentos(Argumento.Tipo.Memoria);
                    break;
                case "INC":
                case "CMA":
                case "LF":
                    MostrarSelectorArgumentos();
                    break;
                case "LDI":
                    MostrarSelectorArgumentos(Argumento.Tipo.Literal, Argumento.Tipo.Memoria);
                    break;
                case "LDM":
                case "IN":
                    MostrarSelectorArgumentos(Argumento.Tipo.Memoria, Argumento.Tipo.Registro);
                    break;
                case "STM":
                case "OUT":
                    MostrarSelectorArgumentos(Argumento.Tipo.Registro, Argumento.Tipo.Memoria);
                    break;
            }
            button1.Enabled = DesactivarBotonAñadirInstruccion();
        }

        private void Argumento_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex != -1 && DesactivarBotonAñadirInstruccion();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bool programaCorrecto = Fichero.ComprobarProgramaCorrecto.Comprobar(editorTexto1.richTextBoxEditor.Text, out int numLinea);
            if (programaCorrecto)
            {
                if (string.IsNullOrWhiteSpace(editorTexto1.richTextBoxEditor.Text) || programaOriginal == editorTexto1.richTextBoxEditor.Text)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                SaveFileDialog guardar = new SaveFileDialog
                {
                    FileName = Path.GetFileName(_ruta),
                    Filter = "Programas de PDM (*.pdm)|*.pdm|Todos los archivos (*.*)|*.*"
                };
                if (guardar.ShowDialog(this) == DialogResult.OK)
                {
                    writer = new StreamWriter(guardar.FileName);
                    foreach (string linea in editorTexto1.richTextBoxEditor.Lines)
                    {
                        writer.Write(linea + "\r\n");
                    }
                    writer.Close();
                    _ruta = guardar.FileName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                editorTexto1.richTextBoxEditor.Select(editorTexto1.richTextBoxEditor.GetFirstCharIndexFromLine(numLinea - 1), editorTexto1.richTextBoxEditor.Lines[numLinea - 1].Length);
                MessageBox.Show("Error en la línea " + (editorTexto1.ConvertirNumeroLinea(numLinea)) + ": El formato de la instrucción " + editorTexto1.richTextBoxEditor.Lines[numLinea - 1] + " no es el correcto. Por favor, compruebe que la instrucción y/o la etiqueta estén correctamente escritas.\r\n\r\nSi necesita ayuda sobre la sintaxis de las instrucciones puede acceder al fichero de ayuda desde el menú principal o la barra de herramientas.", "Comprobar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                editorTexto1.richTextBoxEditor.Focus();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            editorTexto1.richTextBoxEditor.Clear();
            button2.Enabled = false;
        }

        private void CopiarToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^C");
        }

        private void CortarToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^X");
        }

        private void PegarToolStripButton_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^V");
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionFont = new Font("Tahoma", 9f, FontStyle.Regular);
            foreach (string linea in richTextBox1.Lines)
            {
                int bold = linea.IndexOf(':');
                richTextBox1.Select(richTextBox1.Text.IndexOf(linea), bold);
                richTextBox1.SelectionColor = Color.DarkBlue;
            }
        }

        private void AyudaToolStripButton_Click(object sender, EventArgs e)
        {
            int numIndiceAyuda = 0;
            if (comboBox1.SelectedIndex < 5)
                numIndiceAyuda = 0;
            else if (comboBox1.SelectedIndex < 12)
                numIndiceAyuda = 1;
            else if (comboBox1.SelectedIndex < 19)
                numIndiceAyuda = 2;
            else if (comboBox1.SelectedIndex < 22)
                numIndiceAyuda = 3;
            else if (comboBox1.SelectedIndex == 22)
                numIndiceAyuda = 4;
            else numIndiceAyuda = 5;

            new Ayuda(3, numIndiceAyuda).ShowDialog();
        }

        private void MostrarSelectorArgumentos(params Argumento.Tipo[] argumentos)
        {
            OcultarSelectorArgumentos();
            if (argumentos.Length < 1 || argumentos.Length > 2)
                return;

            int numArgumento = 0;
            foreach(Argumento.Tipo argumento in argumentos)
            {
                switch(argumento)
                {
                    case Argumento.Tipo.Literal:
                        if (numArgumento == 0)
                        {
                            numericUpDown1.Visible = true;
                            checkBox1.Visible = true;
                        }
                        break;
                    case Argumento.Tipo.Memoria:
                        if (numArgumento == 0)
                            textBox2.Visible = true;
                        else textBox3.Visible = true;
                        break;
                    case Argumento.Tipo.Registro:
                        if (numArgumento == 0)
                            comboBox2.Visible = true;
                        else comboBox3.Visible = true;
                        break;
                }
                numArgumento++;
            }
        }

        private void OcultarSelectorArgumentos()
        {
            //primer argumento
            numericUpDown1.Visible = false;
            textBox2.Visible = false;
            comboBox2.Visible = false;
            checkBox1.Visible = false;

            //segundo argumento
            comboBox3.Visible = false;
            textBox3.Visible = false;
        }

        private bool DesactivarBotonAñadirInstruccion()
        {
            bool activado = true;
            if (textBox2.Visible && string.IsNullOrWhiteSpace(textBox2.Text))
                activado = false;
            else if (comboBox2.Visible && comboBox2.SelectedIndex == -1)
                activado = false;

            if (textBox3.Visible && string.IsNullOrWhiteSpace(textBox3.Text))
                activado = false;
            else if (comboBox3.Visible && comboBox3.SelectedIndex == -1)
                activado = false;

            return activado;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Hexadecimal = checkBox1.Checked;
        }

        private void SeleccionartodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorTexto1.richTextBoxEditor.SelectAll();
        }

        private void Argumentos_TextBoxes_Enter(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                pegarToolStripButton.Enabled = true;
                pegarToolStripMenuItem.Enabled = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            bool programaCorrecto = Fichero.ComprobarProgramaCorrecto.Comprobar(editorTexto1.richTextBoxEditor.Text, out int numLinea);
            if (programaCorrecto)
            {
                if (string.IsNullOrWhiteSpace(editorTexto1.richTextBoxEditor.Text) || programaOriginal == editorTexto1.richTextBoxEditor.Text)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                DialogResult guardar = MessageBox.Show("El archivo " + Path.GetFileName(_ruta) + " ya existe. ¿Desea sobreescribirlo?", "Confirmar Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (guardar == DialogResult.Yes)
                {
                    writer = new StreamWriter(_ruta);
                    foreach (string linea in editorTexto1.richTextBoxEditor.Lines)
                    {
                        writer.Write(linea + "\r\n");
                    }
                    writer.Close();
                    DialogResult = DialogResult.OK;
                }
                else if (guardar == DialogResult.No)
                    return;

                Close();
            }
            else
            {
                editorTexto1.richTextBoxEditor.Select(editorTexto1.richTextBoxEditor.GetFirstCharIndexFromLine(numLinea - 1), editorTexto1.richTextBoxEditor.Lines[numLinea - 1].Length);
                MessageBox.Show("Error en la línea " + (editorTexto1.ConvertirNumeroLinea(numLinea)) + ": El formato de la instrucción " + editorTexto1.richTextBoxEditor.Lines[numLinea - 1] + " no es el correcto. Por favor, compruebe que la instrucción y/o la etiqueta estén correctamente escritas.\r\n\r\nSi necesita ayuda sobre la sintaxis de las instrucciones puede acceder al fichero de ayuda desde el menú principal o la barra de herramientas.", "Comprobar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                editorTexto1.richTextBoxEditor.Focus();
            }
        }
    }
}
