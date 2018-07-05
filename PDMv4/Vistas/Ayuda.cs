using PDMv4.Utilidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class Ayuda : Form
    {
        private int indiceUltimoNodo = -1;
        private Busqueda ventanaBuscar;
        private int indiceBusqueda = 0;

        public Ayuda(int numNodo = 0, int nodoHijo = 0)
        {
            InitializeComponent();
            ventanaBuscar = new Busqueda(this);
            toolStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            toolStrip1.BackColor = SystemColors.Control;

            treeView1.SelectedNode = treeView1.Nodes[numNodo].Nodes[nodoHijo];
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Properties.Resources.ResourceManager.ReleaseAllResources();
            GC.Collect();
            Dispose();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void TreeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            }
        }

        
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int indiceNodo = e.Node.Index * (e.Node.Parent == null ? 1 : 10) + (e.Node.Parent == null ? 0 : e.Node.Parent.Index);
            indiceBusqueda = 0;

            atrasToolStripMenuItem.Enabled = indiceNodo != 0;
            siguienteToolStripMenuItem.Enabled = indiceNodo != 44;
            toolStripButton2.Enabled = indiceNodo != 44;
            toolStripButton1.Enabled = indiceNodo != 0;

            if (indiceNodo != indiceUltimoNodo)
            {
                richTextBox1.ClearUndo();
                richTextBox1.Clear();

                switch (indiceNodo)
                {
                    case 00: richTextBox1.Rtf = Properties.Resources.Acerca_de_SimPDM; break;
                    case 10: richTextBox1.Rtf = Properties.Resources.Creditos; break;
                    case 01: richTextBox1.Rtf = Properties.Resources.Ventana_Principal; break;
                    case 11: richTextBox1.Rtf = Properties.Resources.Editor_código; break;
                    case 21: richTextBox1.Rtf = Properties.Resources.Opciones; break;
                    case 31: richTextBox1.Rtf = Properties.Resources.Barra_de_herramientas; break;
                    case 02: richTextBox1.Rtf = Properties.Resources.Registros; break;
                    case 12: richTextBox1.Rtf = Properties.Resources.Buses_de_datos_y_direcciones; break;
                    case 22: richTextBox1.Rtf = Properties.Resources.UAL; break;
                    case 32: richTextBox1.Rtf = Properties.Resources.Memoria_Principal; break;
                    case 03: richTextBox1.Rtf = Properties.Resources.Transferencia; break;
                    case 13: richTextBox1.Rtf = Properties.Resources.Aritmetica; break;
                    case 23: richTextBox1.Rtf = Properties.Resources.Logicas; break;
                    case 33: richTextBox1.Rtf = Properties.Resources.Saltos; break;
                    case 43: richTextBox1.Rtf = Properties.Resources.Señalizadores; break;
                    case 53: richTextBox1.Rtf = Properties.Resources.ES; break;
                    case 04: richTextBox1.Rtf = Properties.Resources.Abrir_programa_de_prueba; break;
                    case 14: richTextBox1.Rtf = Properties.Resources.Ejecutar_hasta_el_final; break;
                    case 24: richTextBox1.Rtf = Properties.Resources.Programa_editor; break;
                    case 34: richTextBox1.Rtf = Properties.Resources.Visualizar_Registros_Internos; break;
                    case 44: richTextBox1.Rtf = Properties.Resources.Programa_ES; break;
                }

                indiceUltimoNodo = indiceNodo;
                richTextBox1.Select(0, 0);
                richTextBox1.ScrollToCaret();
                Properties.Resources.ResourceManager.ReleaseAllResources();
                GC.Collect();
            }
        }

        private void SeleccionarNodoSiguiente(bool childOnly = false)
        {
            TreeNode nextNode = treeView1.SelectedNode;
            if (nextNode != null)
            {
                if (nextNode.Level == 0)
                {
                    nextNode = treeView1.SelectedNode.Nodes[0];
                }
                else
                {
                    if (nextNode.NextNode == null)
                        nextNode = childOnly ? nextNode.Parent.NextNode.Nodes[0] : nextNode.Parent.NextNode;
                    else nextNode = nextNode.NextNode;
                }

                treeView1.SelectedNode = nextNode == treeView1.Nodes[0].Nodes[0] ? treeView1.Nodes[0].Nodes[1] : nextNode;
            }
        }

        private void SeleccionarNodoAnterior(bool childOnly = false)
        {
            TreeNode prevNode = treeView1.SelectedNode;
            if (prevNode != null)
            {
                if (prevNode.Level == 0)
                {
                    prevNode = treeView1.SelectedNode.PrevNode.Nodes[treeView1.SelectedNode.PrevNode.Nodes.Count - 1];
                }
                else
                {
                    if (prevNode.PrevNode == null)
                        prevNode = childOnly ? prevNode.Parent.PrevNode.Nodes[prevNode.Parent.PrevNode.Nodes.Count - 1] : prevNode.Parent;
                    else prevNode = prevNode.PrevNode;
                }

                treeView1.SelectedNode = prevNode ?? treeView1.Nodes[0].Nodes[0];
            }
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            SeleccionarNodoSiguiente();
        }


        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            SeleccionarNodoAnterior();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            if (ventanaBuscar == null || ventanaBuscar.IsDisposed)
                ventanaBuscar = new Busqueda(this);

            ventanaBuscar.Show(this);
            ventanaBuscar.Activate();
        }

        public bool Buscar(string texto, bool caseSensitive, bool up = false)
        {
            RichTextBoxFinds opcionesBusqueda = caseSensitive ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;
            if (up)
            {
                opcionesBusqueda |= RichTextBoxFinds.Reverse;
            }

            indiceBusqueda = richTextBox1.Find(texto, up ? 0 : indiceBusqueda , up ? indiceBusqueda : richTextBox1.TextLength, opcionesBusqueda);
            
            while (indiceBusqueda == -1)
            {
                if (up && toolStripButton1.Enabled)
                {
                    SeleccionarNodoAnterior(true);
                    indiceBusqueda = richTextBox1.TextLength;
                }

                else if (toolStripButton2.Enabled && !up)
                {
                    SeleccionarNodoSiguiente(true);
                    indiceBusqueda = 0;
                }

                if (indiceBusqueda == -1 && ((!toolStripButton2.Enabled && !up) || (!toolStripButton1.Enabled && up)))
                {
                    indiceBusqueda = 0;
                    MessageBox.Show(string.Format("No se han encontrado más apariciones de \"{0}\" en el documento de ayuda.", texto), "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    indiceBusqueda = up ? richTextBox1.TextLength : 0;
                }

                indiceBusqueda = richTextBox1.Find(texto, up ? 0 : indiceBusqueda, up ? indiceBusqueda : richTextBox1.TextLength, opcionesBusqueda);
            }

            indiceBusqueda  = up ? indiceBusqueda : indiceBusqueda + texto.Length;

            Activate();
            richTextBox1.Focus();

            return true;
        }

        private void Ayuda_FormClosed(object sender, FormClosedEventArgs e)
        {
            richTextBox1.Clear();
            Properties.Resources.ResourceManager.ReleaseAllResources();
            GC.Collect();
        }
    }
}
