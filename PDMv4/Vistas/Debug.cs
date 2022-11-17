using PDMv4.Procesador;
using PDMv4.Temas;
using PDMv4.Utilidades;
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
    public partial class Debug : Form
    {
        public Debug()
        {
            InitializeComponent();
        }

        public void Actualizar()
        {
            numericUpDownB.Value = Main.ObtenerRegistro(0).Contenido;
            numericUpDownC.Value = Main.ObtenerRegistro(1).Contenido;
            numericUpDownD.Value = Main.ObtenerRegistro(2).Contenido;
            numericUpDownE.Value = Main.ObtenerRegistro(3).Contenido;

            numericUpDownDatos.Value = Main.BusesDatosYDireccion.Contenido;
            numericUpDownDirecciones.Value = Main.BusesDatosYDireccion.ContenidoDireccion;

            numericUpDownRI.Value = Main.RegistroInstruccion.Contenido;
            numericUpDownUAL.Value = Main.UnidadAritmeticoLogica.Resultado;
            numericUpDownAc.Value = Main.Acumulador.Contenido;
            numericUpDownRDD.Value = Main.RegistroDirecciones;
            numericUpDownCP.Value = Main.ContadorPrograma;

            checkBoxCarry.Checked = Main.FlagCarry;
            checkBoxZero.Checked = Main.FlagZero;

            numericUpDownCR.Value = Main.NumRegistroSeleccionado;
            textBoxRegistro.Text = Main.ObtenerNombreRegistro(Main.NumRegistroSeleccionado);

            sbyte[] flags = Main.IndiceMicroinstruccionActual < Main.ListaMicroinstrucciones.Count ? Main.ListaMicroinstrucciones[Main.IndiceMicroinstruccionActual] : new sbyte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0 };

            LRI.Checked = flags[0] == 1;//estado.LRI;
            LCP.Checked = flags[1] == 1;//estado.LCP;
            TSR.Checked = flags[2] == 1;//estado.TSR;
            TER.Checked = flags[3] == 1;//estado.TER;
            LH.Checked = flags[4] == 1;//estado.LH;
            LL.Checked = flags[5] == 1;//estado.LL;
            LF.Checked = flags[6] == 1;//estado.LF;
            TF.Checked = flags[7] == 1;//estado.TF;
            LAc.Checked = flags[8] == 1;//estado.LAc;
            TAc.Checked = flags[9] == 1;//estado.TAc;
            LE.Checked = flags[10] == 1;//estado.L_E;
            MEM.Checked = flags[11] == 1;//estado.MEM;
            TMEM.Checked = flags[12] == 1;//estado.TMEM;
            TUAL.Checked = flags[16] == 1;//estado.TUAL;
            LUAL.Checked = flags[17] == 1;//estado.LUAL;

            listView_Microinstrucciones.VirtualListSize = Main.ListaMicroinstrucciones.Count;

            if (flags[12] == 1 && flags[10] == 0)
                numericUpDownLectura.Value = Main.BusesDatosYDireccion.ContenidoDireccion;

            if (flags[10] == 1)
                numericUpDownEscritura.Value = Main.BusesDatosYDireccion.ContenidoDireccion;


            listView_MemoriaPrincipal.VirtualListSize = Main.ObtenerMemoria.Tamaño;
            if (listView_MemoriaPrincipal.VirtualListSize > 0)
            {
                int direccionMemoria = Main.ObtenerDireccionMemoriaLEaPartirMicroinstruccion(out bool escritura);
                listView_MemoriaPrincipal.RedrawItems(0, listView_MemoriaPrincipal.VirtualListSize - 1, false);

                if (direccionMemoria != -1)
                    listView_MemoriaPrincipal.EnsureVisible(direccionMemoria);
            }

        }

        private void listView_MemoriaPrincipal_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            int contenidoDireccion = Main.ObtenerMemoria.ObtenerDireccion((ushort)e.ItemIndex).Contenido;
            ListViewItem itemLineaMemoria = new ListViewItem(e.ItemIndex.ToString("X4"));
            itemLineaMemoria.SubItems.AddRange
                (new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(itemLineaMemoria, contenidoDireccion.ToString("D3")),
                    new ListViewItem.ListViewSubItem(itemLineaMemoria, contenidoDireccion.ToCa2().ToString("D3")),
                    new ListViewItem.ListViewSubItem(itemLineaMemoria, contenidoDireccion.ToString("X2")),
                    new ListViewItem.ListViewSubItem(itemLineaMemoria, contenidoDireccion.ToBin(8)),
                    new ListViewItem.ListViewSubItem(itemLineaMemoria, Main.ObtenerTextoInstruccion((ushort)e.ItemIndex))
                });
            e.Item = itemLineaMemoria;
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            ListViewVisualStyles.DibujarCabeceras(sender, e);
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e);
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            (sender as ListView).Columns[(sender as ListView).Columns.Count - 1].Width = -2;
        }

        private void listView_Microinstrucciones_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            
            sbyte[] microinstruccion = Main.ListaMicroinstrucciones[e.ItemIndex];
            ListViewItem itemMicroinstruccion = new ListViewItem(microinstruccion[0].ToString());
            itemMicroinstruccion.SubItems.AddRange
                (new ListViewItem.ListViewSubItem[] {
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[1].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[2].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[3].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[4].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[5].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[6].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[7].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[8].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[9].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[10].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[11].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[12].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[13] < 0 ? "X" : microinstruccion[13].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[14] < 0 ? "X" : microinstruccion[14].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[15] < 0 ? "X" : microinstruccion[15].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[16].ToString()),
                new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[17].ToString())
                });

            e.Item = itemMicroinstruccion;
            
        }

        private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = (sender as ListView).Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indice = (int)numericUpDown1.Value;
            listView_MemoriaPrincipal.EnsureVisible(indice);
            listView_MemoriaPrincipal.SelectedIndices.Clear();

            listView_MemoriaPrincipal.SelectedIndices.Add(indice);

        }

        private void Debug_Enter(object sender, EventArgs e)
        {
            Opacity = 100;
        }

        private void Debug_Leave(object sender, EventArgs e)
        {
            Opacity = 50;
        }

        private void Debug_Shown(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
