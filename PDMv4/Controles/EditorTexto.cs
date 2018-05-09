using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDMv4.Controles
{
    public partial class EditorTexto : UserControl
    {
        private Color numeroLinea = Color.Black;
        private float offsetYbase = 6.5f;
        
        public EditorTexto()
        {
            InitializeComponent();
        }

        public Color NumeroLinea { get => numeroLinea; set => numeroLinea = value; }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.GetPositionFromCharIndex(0).Y >= 0)
                offsetYbase = 6.5f;
            canvas.Refresh();
        }

        private void EditorTexto_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            int cont = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                float offsetY = offsetYbase + i * (richTextBox1.Font.Size + 5.25f);
                if (!string.IsNullOrWhiteSpace(richTextBox1.Lines[i]))
                {
                    float offsetX = ++cont > 9 ? 24 : 30;
                    e.Graphics.DrawString(cont.ToString(), Font, new SolidBrush(numeroLinea), offsetX, offsetY);
                }
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            offsetYbase = 6.5f + richTextBox1.GetPositionFromCharIndex(0).Y;
            canvas.Refresh();
        }

        public void AñadirInstruccion(string instruccion)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) || string.IsNullOrWhiteSpace(richTextBox1.Lines[richTextBox1.Lines.Length - 1]))
                richTextBox1.AppendText(instruccion);
            else richTextBox1.AppendText(Environment.NewLine + instruccion);
        }

        public RichTextBox richTextBoxEditor { get => richTextBox1; }

        public int ConvertirNumeroLinea(int numLinea)
        {
            int numLineasVacias = 0;
            for (int i = 0; i < numLinea; i++)
                if (string.IsNullOrWhiteSpace(richTextBox1.Lines[i]))
                    numLineasVacias++;
            return numLinea - numLineasVacias;
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            if (richTextBox1.GetPositionFromCharIndex(0).Y >= 0)
                offsetYbase = 6.5f;
            canvas.Refresh();
        }
    }
}
