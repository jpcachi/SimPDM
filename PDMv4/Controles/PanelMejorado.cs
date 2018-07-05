using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;

namespace PDMv4.Controles
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class PanelMejorado : UserControl
    {
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }
        private bool colorFondoControladoPorEstilo = true;
        public bool ColorFondoControladoPorEstilo
        {
            get => colorFondoControladoPorEstilo;
            set
            {
                colorFondoControladoPorEstilo = value;
                if (value)
                    panel2.BackColor = Estilos.DEFAULT_PANEL_COLOR;
            }
        }

        public PanelMejorado()
        {
            InitializeComponent();
            panel1.BackColor = Estilos.DEFAULT_PANEL_HEADER_COLOR;
            panel3.BackColor = Estilos.DEFAULT_PANEL_HEADER_COLOR;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            int offsetY = 11;
            int offsetX = (int)Graphics.FromHwnd(ParentForm.Handle).MeasureString(Text, Font).Width + 6;
            e.Graphics.DrawString(Text, panel1.Font, new SolidBrush(Estilos.DEFAULT_PANEL_TEXT_HEADER_COLOR), new Point(5, 6));
            GraphicsPath graphicsPath = new GraphicsPath(FillMode.Alternate);
            
            for (int i = offsetX; i < panel1.Width; i++)
            {
                if(i % 2 == 0 && i % 4 != 0)
                {
                    graphicsPath.AddRectangle(new RectangleF(i, offsetY + 2, 0.5f, 0.5f));
                }
                    
                else if (i % 4 == 0)
                {
                    graphicsPath.AddRectangle(new RectangleF(i, offsetY, 0.5f, 0.5f));
                    graphicsPath.AddRectangle(new RectangleF(i, offsetY + 4, 0.5f, 0.5f));
                    
                }
            }
            e.Graphics.FillPath(new SolidBrush(Estilos.DEFAULT_PANEL_TEXT_HEADER_COLOR), graphicsPath);
        }

        public ControlCollection ContentControls
        {
            get
            {
                return panel2.Controls;
            }
        }
    }
}
