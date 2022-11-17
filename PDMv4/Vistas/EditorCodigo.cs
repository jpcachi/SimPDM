using FastColoredTextBoxNS;
using PDMv4.Argumentos;
using PDMv4.Temas;
using PDMv4.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace PDMv4.Vistas
{
    public partial class EditorCodigo : Form
    {
        private string _ruta;
        private string programaOriginal;
        private StreamWriter writer;

        public string Ruta { get => _ruta; set => _ruta = value; }
        public string Programa { get; private set; }

        public EditorCodigo(string programa = null, string ruta = null)
        {
            InitializeComponent();
            string[] instrucciones = UtilidadesInstruccion.InstruccionesTexto.ToArray();
            Array.Sort(instrucciones);
            comboBox1.Items.AddRange(instrucciones);
            editorTexto.SyntaxHighlighter = new AsmPdmHighlighter(editorTexto);

            panelMejorado1.ContentControls.Add(editorTexto);

            toolStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            contextMenuStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            seleccionartodoToolStripMenuItem.Enabled = !string.IsNullOrEmpty(editorTexto.Text);

            if(programa != null)
            {
                programaOriginal = programa;
                editorTexto.Text = programaOriginal;

                editorTexto.SelectionStart = editorTexto.Text.Length;
                editorTexto.SelectionLength = 0;
            }
            if (ruta != null)
            {
                _ruta = ruta;
                guardarToolStripButton.Enabled = true;
            }
        }


        private void TextBoxEditor_LostFocus(object sender, EventArgs e)
        {
            pegarToolStripButton.Enabled = false;
            pegarToolStripMenuItem.Enabled = false;
        }

        private void TextBoxEditor_GotFocus(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                Clipboard.SetText(Clipboard.GetText());
                pegarToolStripButton.Enabled = true;
                pegarToolStripMenuItem.Enabled = true;
            }
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

            editorTexto.InsertText(instruccion + "\n");
            editorTexto.Select();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = comboBox1.SelectedIndex != -1 ? UtilidadesInstruccion.InstruccionesDescripcion[(string)comboBox1.SelectedItem] : string.Empty;
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
                    MostrarSelectorArgumentos(Argumento.Tipo.Literal, Argumento.Tipo.Registro);
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
            bool programaCorrecto = Fichero.ComprobarProgramaCorrecto.Comprobar(editorTexto.Text, out int numLinea, out string lineaError);
            if (programaCorrecto)
            {
                if (string.IsNullOrWhiteSpace(editorTexto.Text) || programaOriginal == editorTexto.Text)
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
                    foreach (string linea in editorTexto.Lines)
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
                MessageBox.Show("Error en la línea " + numLinea + ": El formato de la instrucción " + lineaError + " no es el correcto. Por favor, compruebe que la instrucción y/o la etiqueta estén correctamente escritas.\r\n\r\nSi necesita ayuda sobre la sintaxis de las instrucciones puede acceder al fichero de ayuda desde el menú principal o la barra de herramientas.", "Comprobar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editorTexto.Focus();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            editorTexto.Clear();
            button2.Enabled = false;
        }

        private void CopiarToolStripButton_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^C");
            editorTexto.Copy();
        }

        private void CortarToolStripButton_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^X");
            editorTexto.Cut();
        }

        private void PegarToolStripButton_Click(object sender, EventArgs e)
        {
            //SendKeys.Send("^V");
            editorTexto.Paste();
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
            string[] instruccionesTransferencia = new string[] { "LD", "ST", "LDI", "LDM", "STM" };
            string[] instruccionesAritmeticas   = new string[] { "ADD", "SUB", "CMP", "INC", "ADI", "SUI", "CMI" };
            string[] instruccionesLogicas       = new string[] { "AND", "ORA", "CRA", "CMA", "ANI", "ORI", "XRI" };
            string[] instruccionesSalto         = new string[] { "JMP", "BEQ", "BC" };
            string[] instruccionesEntradaSalida = new string[] { "IN", "OUT" };
            string instruccionFlag = "LF";


            if (instruccionesTransferencia.Contains(comboBox1.SelectedItem)) numIndiceAyuda = 0;
            else if (instruccionesAritmeticas.Contains(comboBox1.SelectedItem)) numIndiceAyuda = 1;
            else if (instruccionesLogicas.Contains(comboBox1.SelectedItem)) numIndiceAyuda = 2;
            else if (instruccionesSalto.Contains(comboBox1.SelectedItem)) numIndiceAyuda = 3;
            else if ((string)comboBox1.SelectedItem == instruccionFlag) numIndiceAyuda = 4;
            else if (instruccionesEntradaSalida.Contains(comboBox1.SelectedItem)) numIndiceAyuda = 5;

            new Ayuda(3, numIndiceAyuda).ShowDialog();
        }

        private void MostrarSelectorArgumentos(params Argumento.Tipo[] argumentos)
        {
            OcultarSelectorArgumentos();
            if (argumentos.Length < 1 || argumentos.Length > 2)
            {
                label3.Visible = false;
                return;
            }

            label3.Visible = true;
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
            editorTexto.SelectAll();
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
            bool programaCorrecto = Fichero.ComprobarProgramaCorrecto.Comprobar(editorTexto.Text, out int numLinea, out string lineaError);
            if (programaCorrecto)
            {
                if (string.IsNullOrWhiteSpace(editorTexto.Text) || programaOriginal == editorTexto.Text)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }


                foreach (string linea in editorTexto.Lines)
                {
                    Programa += linea + "\r\n";
                }
                DialogResult = DialogResult.OK;
                

                Close();
            }
            else
            {
                MessageBox.Show("Error en la línea " + numLinea + ": El formato de la instrucción " + lineaError + " no es el correcto. Por favor, compruebe que la instrucción y/o la etiqueta estén correctamente escritas.\r\n\r\nSi necesita ayuda sobre la sintaxis de las instrucciones puede acceder al fichero de ayuda desde el menú principal o la barra de herramientas.", "Comprobar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editorTexto.Focus();
            }
        }


        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            bool activarGuardarSeleccionar = !string.IsNullOrWhiteSpace(editorTexto.Text);
            button2.Enabled = activarGuardarSeleccionar;
            button4.Enabled = activarGuardarSeleccionar;
            guardarToolStripButton.Enabled = activarGuardarSeleccionar;
            seleccionartodoToolStripMenuItem.Enabled = activarGuardarSeleccionar;
        }

        private void editorTexto_SelectionChanged(object sender, EventArgs e)
        {
            bool activarCopiarCortar = editorTexto.SelectionLength > 0;
            copiarToolStripButton.Enabled = activarCopiarCortar;
            copiarToolStripMenuItem.Enabled = activarCopiarCortar;
            cortarToolStripButton.Enabled = activarCopiarCortar;
            cortarToolStripMenuItem.Enabled = activarCopiarCortar;
        }

        private void editorTexto_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(editorTexto.PointToScreen(e.Location));
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            editorTexto.CommentSelected();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("+{TAB}");
        }

        private void editorTexto_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string nombreArchivo = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                editorTexto.Text = File.ReadAllText(nombreArchivo);
            }
        }

        private void editorTexto_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }
    }
}
