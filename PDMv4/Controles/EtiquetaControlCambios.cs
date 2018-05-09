using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PDMv4.Controles
{
    public partial class EtiquetaControlCambios : UserControl
    {
        private bool activado = false;
        public EtiquetaControlCambios()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return button1.Text;
            }
            set
            {
                button1.Text = value;
            }
        }

        public bool Activado
        {
            get
            {
                return activado;
            }
            set
            {
                activado = value;
                button1.FlatAppearance.BorderColor = activado ? Color.ForestGreen : Color.Firebrick;
                button1.BackColor = activado ? Color.MintCream : Color.MistyRose;
            }
        }
    }
}
