using PDMv4.Utilidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class VerRegistro : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        public VerRegistro()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(
                    (Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            Owner.Activate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            ListViewVisualStyles.DibujarCabeceras(sender, e);
        }

        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListVistaContenidoMapaProcesador(sender, e);
        }

        public void ModificarVista(string nombre, int contenido, bool direccion = false)
        {
            int len = direccion ? 16 : 8;
            string hex = "0x" + contenido.ToString(direccion ? "X4" : "X2"); 
            label1.Text = nombre;
            listView1.Items.Clear();
            ListViewItem item = new ListViewItem(contenido.ToCa2().ToString());
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, hex));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, contenido.ToBin(len)));

            listView1.Items.Add(item);
        }

        public void ModificarVistaFlags(string nombre, bool FC, bool FZ)
        {
            int contenido = (FC ? 2 : 0) + (FZ ? 1 : 0);
            int len = 2;
            string hex = "0x" + contenido.ToString("X1");
            label1.Text = nombre;
            listView1.Items.Clear();
            ListViewItem item = new ListViewItem(contenido.ToCa2().ToString());
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, hex));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, contenido.ToBin(len)));

            listView1.Items.Add(item);
        }

        private void VerRegistro_Deactivate(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                ParentForm.Activate();
            }
        }

        private void ListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = (sender as ListView).Columns[e.ColumnIndex].Width;
        }

        private void Button1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.Close_Over;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.Close;
        }
    }
}
