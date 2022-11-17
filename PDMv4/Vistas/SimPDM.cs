using PDMv4.Instrucciones;
using PDMv4.Interfaces;
using PDMv4.Procesador;
using PDMv4.Temas;
using PDMv4.Utilidades;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMv4.Vistas
{
    public partial class SimPDM : Form
    {

        private Fichero archivoActual;
        private Task hiloEjecucion;
        private CancellationTokenSource tokenSource;
        private CancellationToken cancellation;
        private Debug debugWindow;
        private string startupFile = null;

        private bool ProgrmaConCambiosSinGuardar { get; set; }

        public SimPDM(string file = null)
        {
            
            InitializeComponent();

            BackColor = Estilos.GetStyle().DEFAULT_WINDOW_BACKGROUND_COLOR;
            panelIrA.BackColor = Estilos.GetStyle().DEFAULT_PANEL_HEADER_COLOR;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            panelMejorado1.ContentControls.Add(listView_Programa);
            panelMejorado2.CustomBorders = false;
            panelMejorado2.ContentControls.Add(panel1);
            panelMejorado3.ContentControls.Add(listView_Registros);
            panelMejorado4.ContentControls.Add(panelIrA);
            panelMejorado4.ContentControls.Add(listView_MemoriaPrincipal);          
            panelMejorado5.ContentControls.Add(listView_Flags);
            panelMejorado6.ContentControls.Add(listView_Microinstrucciones);

            contextMenuStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            contextMenuStrip2.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            menuStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            toolStrip1.Renderer = new ToolStripAeroRenderer(ToolbarTheme.HelpBar);
            archivoActual = new Fichero();
            listView_MemoriaPrincipal.VirtualListSize = Main.ObtenerMemoria.Tamaño;

            startupFile = file;

            listView_MemoriaPrincipal.GridLines = false;
            listView_Flags.GridLines = false;
            listView_Programa.GridLines = false;
            listView_Registros.GridLines = false;
            listView_Microinstrucciones.GridLines = false;

            listView_MemoriaPrincipal.BackColor = Estilos.GetStyle().DEFAULT_GRID_COLOR;
            listView_Flags.BackColor = Estilos.GetStyle().DEFAULT_GRID_COLOR;
            listView_Programa.BackColor = Estilos.GetStyle().DEFAULT_GRID_COLOR;
            listView_Registros.BackColor = Estilos.GetStyle().DEFAULT_GRID_COLOR;
            listView_Microinstrucciones.BackColor = Estilos.GetStyle().DEFAULT_GRID_COLOR;

            debugWindow = new Debug();
            
            ActualizarStatusStrip();
        }

        private void AbrirArchivo(string nombre)
        {
            if (archivoActual.LeerPrograma(nombre))
            {
                ListViewVisualStyles.LimpiarIndices();
                mapaProcesador.RestablecerMapaPDM();
                RefrescarListViews();
                ActualizarOpcionesEjecucion();
                ActualizarStatusStrip();
            }
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            Main.Restablecer();
            archivoActual.Restablecer();
            mapaProcesador.RestablecerMapaPDM();
            ListViewVisualStyles.LimpiarIndices();
            RefrescarListViews();
            ActualizarStatusStrip();
            ActualizarOpcionesEjecucion();
        }

        private void AbrirToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AbrirArchivo(openFileDialog1.FileName);
                listView_Programa.EnsureVisible(listView_Programa.Items.Count - 1);
                listView_Programa.EnsureVisible(0);
            }
        }


        private void ListView_Registros_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            int contenidoRegistro = Main.ObtenerRegistro(e.ItemIndex).Contenido;
            ListViewItem itemRegistro = new ListViewItem(Main.ObtenerNombreRegistro(e.ItemIndex));
            itemRegistro.SubItems.AddRange
                (new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(itemRegistro, contenidoRegistro.ToString("D3")),
                    new ListViewItem.ListViewSubItem(itemRegistro, contenidoRegistro.ToCa2().ToString("D3")),
                    new ListViewItem.ListViewSubItem(itemRegistro, contenidoRegistro.ToString("X2")),
                    new ListViewItem.ListViewSubItem(itemRegistro, contenidoRegistro.ToBin(8))
                });
            e.Item = itemRegistro;
        }

        private void ListView_MemoriaPrincipal_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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

        private void ListView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            sbyte[] microinstruccion = Main.ListaMicroinstrucciones[e.ItemIndex];
            ListViewItem itemMicroinstruccion = new ListViewItem(UC.ObtenerMicroInstruccionAPartirDeSeñales(microinstruccion));
            itemMicroinstruccion.SubItems.AddRange
                (new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[0].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[1].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[2].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[3].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[4].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[5].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[6].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[7].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[8].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[9].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[10].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[11].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[12].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[13] < 0 ? "     X" : "     " + microinstruccion[13].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[14] < 0 ? "     X" : "     " + microinstruccion[14].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, microinstruccion[15] < 0 ? "     X" : "     " + microinstruccion[15].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[16].ToString()),
                    new ListViewItem.ListViewSubItem(itemMicroinstruccion, "     " +microinstruccion[17].ToString())
                });

            e.Item = itemMicroinstruccion;
        }

        private void ListView_Flags_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item;
            if (e.ItemIndex == 0)
            {
                item = new ListViewItem("FZ");
                item.SubItems.Add(Main.FlagZero.ToString());
            }
            else
            {
                item = new ListViewItem("FC");
                item.SubItems.Add(Main.FlagCarry.ToString());
            }

            e.Item = item;
        }

        private void EjecutarMicroInstruccion()
        {
            //PRIMERO EJECUTAMOS MICROINSTRUCCION
            Main.EjecutarMicroinstruccion();

            //LUEGO VISUALIZAMOS
            VisualizarMicroinstruccionEjecutada();

            //ActualizamosBarraEstado
            ActualizarOpcionesEjecucion();
        }

        private void RevertirMicroInstruccion()
        {
            //PRIMERO REVERTIMOS MICROINSTRUCCION
            Main.RevertirMicroinstruccion();

            //LUEGO VISUALIZAMOS
            VisualizarMicroinstruccionEjecutada();

            //ActualizamosBarraEstado
            ActualizarOpcionesEjecucion();
        }

        private void EjecutarInstruccion()
        {
            //PRIMERO EJECUTAMOS INSTRUCCION
            Main.EjecutarInstruccion();

            //LUEGO VISUALIZAMOS
            VisualizarMicroinstruccionEjecutada();

            //ActualizamosBarraEstado
            ActualizarOpcionesEjecucion();
        }

        private void RevertirInstruccion()
        {
            //PRIMERO REVERTIMOS INSTRUCCION
            Main.RevertirInstruccion();

            //LUEGO VISUALIZAMOS
            VisualizarMicroinstruccionEjecutada();

            //ActualizamosBarraEstado
            ActualizarOpcionesEjecucion();
        }

        private void VisualizarMicroinstruccionEjecutada()
        {
            int[] numRegistroLeido = Main.ObtenerRegistroLeidoAPartirMicroinstruccion();
            int numRegistroEscrito = Main.ObtenerRegistroEscritoAPartirMicroinstruccion();
            int direccionMemoria = Main.ObtenerDireccionMemoriaLEaPartirMicroinstruccion(out bool escrituraMem);
            int[] flags = Main.ObtenerFlagsAPartirMicroinstruccion(out bool escrituraFlags);

            ListViewVisualStyles.AñadirIndices(TListView.Programa, false, Main.IndiceInstruccionActual);
            ListViewVisualStyles.AñadirIndices(TListView.Registros, false, numRegistroLeido);
            ListViewVisualStyles.AñadirIndices(TListView.Registros, true, numRegistroEscrito);
            ListViewVisualStyles.AñadirIndices(TListView.Memoria, escrituraMem, direccionMemoria);
            ListViewVisualStyles.AñadirIndices(TListView.Flags, escrituraFlags, flags);
            ListViewVisualStyles.EjecucionMicroInstruccion = Main.IndiceMicroinstruccionActual;

            RefrescarListViews();

            mapaProcesador.ActualizarMapaPDM(Main.ListaMicroinstrucciones[Main.IndiceMicroinstruccionActual]);
            mapaProcesador.ActualizarVentanaVistaContenido();

            if (debugWindow != null && !debugWindow.IsDisposed)
                debugWindow.Actualizar();
        }

        private void RefrescarListViewPrograma()
        {
            listView_Programa.VirtualListSize = archivoActual.ObtenerLineasPrograma.Count;
            if (listView_Programa.VirtualListSize > 0)
                listView_Programa.RedrawItems(0, listView_Programa.VirtualListSize - 1, false);   
        }

        private void RefrescarListViewDireccionMemoria()
        {
            if (listView_MemoriaPrincipal.VirtualListSize > 0)
            {
                int direccionMemoria = Main.ObtenerDireccionMemoriaLEaPartirMicroinstruccion(out bool escritura);
                listView_MemoriaPrincipal.RedrawItems(0, listView_MemoriaPrincipal.VirtualListSize - 1, false);

                if(direccionMemoria != -1)
                    listView_MemoriaPrincipal.EnsureVisible(direccionMemoria);                
            }
        }

        private void RefrescarListViewRegistros()
        {
            if (listView_Registros.VirtualListSize > 0)
                listView_Registros.RedrawItems(0, listView_Registros.VirtualListSize - 1, false);           
        }

        private void RefrescarListViewMicroinstrucciones()
        {
            listView_Microinstrucciones.VirtualListSize = Main.ListaMicroinstrucciones.Count;
            
            if(listView_Microinstrucciones.VirtualListSize > 0)
                listView_Microinstrucciones.RedrawItems(0, listView_Microinstrucciones.VirtualListSize - 1, false);

         }

        private void RefrescarListViewFlags()
        {
            if (listView_Flags.VirtualListSize > 0)
                listView_Flags.RedrawItems(0, 1, false); 
        }

        private void RefrescarListViews()
        {
            Invoke(new Action(() =>
            {
                RefrescarListViewPrograma();
                RefrescarListViewDireccionMemoria();
                RefrescarListViewRegistros();
                RefrescarListViewMicroinstrucciones();
                RefrescarListViewFlags();
            }));           
        }

        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            ListViewVisualStyles.DibujarCabeceras(sender, e);
        }

        private void ListView_Programa_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(archivoActual.ObtenerLineasPrograma[e.ItemIndex][0]);
            e.Item.SubItems.Add(new ListViewItem.ListViewSubItem(e.Item, archivoActual.ObtenerLineasPrograma[e.ItemIndex][1]));
        }

        private void ListView_Microinstrucciones_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Microinstrucciones);
        }

        private void MicroinstruccionSiguiente_Click(object sender, EventArgs e)
        {
            EjecutarMicroInstruccion();
        }

        private void InstruccionSiguiente_Click(object sender, EventArgs e)
        {
            EjecutarInstruccion();
        }

        private void MicroinstruccionAnterior_Click(object sender, EventArgs e)
        {
            if (Main.IndiceInstruccionActual == 0 && Main.IndiceMicroinstruccionActual == 0 && Main.ListaMicroinstrucciones.Count > 0)
                RestablecerPrograma();
            else
                RevertirMicroInstruccion(); 
        }

        private void InstruccionAnterior_Click(object sender, EventArgs e)
        {
            if (Main.IndiceInstruccionActual == 0)
                RestablecerPrograma();
            else
                RevertirInstruccion();
        }

        private void ListView_Programa_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Programa);
        }

        private void ListView_Registros_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Registros);
        }

        private void ListView_MemoriaPrincipal_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Memoria);
        }

        private void ListView_Flags_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Flags);
        }

        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = (sender as ListView).Columns[e.ColumnIndex].Width;
        }

        private void ListView_Resize(object sender, EventArgs e)
        {
            (sender as ListView).Columns[(sender as ListView).Columns.Count - 1].Width = -2;
        }

        private void SplitContainer_CancelSplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void ListView_Microinstrucciones_Resize(object sender, EventArgs e)
        {
            int calculateFaseWidth = listView_Microinstrucciones.Width - (listView_Microinstrucciones.Columns.Count - 1) * 50;
            Fase.Width = calculateFaseWidth >= 205 ? calculateFaseWidth : 205;
            listView_Microinstrucciones.Scrollable = listView_Microinstrucciones.Width < 50 * (listView_Microinstrucciones.Columns.Count  - 1) + 205;
        }

        private void ActualizarStatusStrip(bool editado = false)
        {
            etiquetaRuta_BarraEstado.Text = archivoActual.Ruta ?? "Nuevo";
            if (editado && etiquetaRuta_BarraEstado.Text != "Nuevo")
            {
                ProgrmaConCambiosSinGuardar = true;
                etiquetaRuta_BarraEstado.Text += "*";
                etiquetaRuta_BarraEstado.Font = new Font(etiquetaRuta_BarraEstado.Font, FontStyle.Italic);
            }
            else
            {
                ProgrmaConCambiosSinGuardar = false;
                etiquetaRuta_BarraEstado.Font = new Font(etiquetaRuta_BarraEstado.Font, FontStyle.Regular);
            }

            etiquetaEstado_BarraEstado.Text = "Listo";
            etiquetaNumeroLineas_BarraEstado.Text = archivoActual.ObtenerLineasPrograma.Count + " líneas";
            statusStrip1.BackColor = Estilos.GetStyle().DEFAULT_STATUS_STRIP_COLOR;
            statusStrip1.ForeColor = Estilos.EsColorOscuro(statusStrip1.BackColor) ? Color.White : Color.Black;
        }

        private void RestablecerPrograma()
        {
            archivoActual.LeerProgramaSinFichero(archivoActual.ObtenerPrograma, false);
            ListViewVisualStyles.LimpiarIndices();
            mapaProcesador.RestablecerMapaPDM();
            RefrescarListViews();
            ActualizarOpcionesEjecucion();
            reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
            ActivarItemsArchivo(true);
        }

        private void ReiniciarPrograma_Click(object sender, EventArgs e)
        {
            
            if (tokenSource != null)
                DetenerHiloEjecucionInstrucciones();

            RestablecerPrograma();
            ActualizarStatusStrip();
            mapaProcesador.RestablecerMapaPDM();
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            cancellation = tokenSource.Token;
            hiloEjecucion = new Task(new Action(EjecutarHastaElFinal), cancellation);
            hiloEjecucion.Start();
        }
        private void EjecutarHastaElFinal()
        {
            Invoke(new Action(() =>
            {
                ActivarItemsArchivo(false);
                ActivarItemsEjecucion();

            }));
            
            while (!FinalEjecucion())
            {
                if (cancellation.IsCancellationRequested == true)
                {
                    Invoke(new Action(() =>
                    {
                        ActualizarStatusStrip();
                    }));
                    return;
                }
                Main.EjecutarInstruccion();
                
            }

            Invoke(new Action(() =>
            {
                reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
            
                VisualizarMicroinstruccionEjecutada();
                ActualizarOpcionesEjecucion();
                ActualizarStatusStrip();

            }));
        }

        private bool FinalEjecucion()
        {
            int diferencia = (Main.ListaInstrucciones.Count - 1) - Main.IndiceInstruccionActual;

            if (diferencia == 0)
            {
                if (!(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion is IInstruccionSalto))
                    diferencia = (Main.ListaMicroinstrucciones.Count - 1) - Main.IndiceMicroinstruccionActual;
                else
                {
                    if((Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion is BEQ && !Main.FlagZero) || 
                        (Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion is BC && !Main.FlagCarry))
                       diferencia = (Main.ListaMicroinstrucciones.Count - 2) - Main.IndiceMicroinstruccionActual;
                    else diferencia = 1;
                }
            }
            return diferencia == 0;
        }

        private void ActualizarOpcionesEjecucion()
        {
            Invoke(new Action(() =>
            {
                ActivarMicroinstruccionSiguiente();
                ActivarMicroinstruccionAnterior();
                ActivarEjecutarHastaFinal();
                ActivarReiniciarPrograma();
                ActivarItemsArchivo(true);
            }));            
        }

        private void ActivarMicroinstruccionSiguiente()
        {
            if (Main.IndiceInstruccionActual < archivoActual.ObtenerLineasPrograma.Count - 1)
            {
                instruccionSiguienteToolStripButton.Enabled = true;
                microinstruccionSiguienteToolStripButton.Enabled = true;
                instruccionSiguienteMenuItem.Enabled = true;
                microinstruccionSiguienteMenuItem.Enabled = true;
            }
            else
            {
                bool activado = !FinalEjecucion() && Main.ListaInstrucciones.Count > 0;
                instruccionSiguienteToolStripButton.Enabled = activado;
                microinstruccionSiguienteToolStripButton.Enabled = activado;
                instruccionSiguienteMenuItem.Enabled = activado;
                microinstruccionSiguienteMenuItem.Enabled = activado;
            }
        }

        private void ActivarMicroinstruccionAnterior()
        {
            if (Main.IndiceInstruccionActual > 0)
            {
                instruccionAnteriorToolStripButton.Enabled = true;
                microinstruccionAnteriorToolStripButton.Enabled = true;
                instruccionAnteriorMenuItem.Enabled = true;
                microinstruccionAnteriorMenuItem.Enabled = true;
            }
            else
            {
                bool activado = Main.ListaMicroinstrucciones.Count > 0;
                instruccionAnteriorToolStripButton.Enabled = activado;
                microinstruccionAnteriorToolStripButton.Enabled = activado;
                instruccionAnteriorMenuItem.Enabled = activado;
                microinstruccionAnteriorMenuItem.Enabled = activado;
            }
        }

        private void ActivarEjecutarHastaFinal()
        {
            bool activado = Main.ListaInstrucciones.Count > 0 && !FinalEjecucion();
               
            ejecutarHastaFinalStripButton.Enabled = activado;
            ejecutarHastaElFinalPasoStripButton.Enabled = activado;
            ejecutarHastaElFinalPasoPorPasoMenuItem.Enabled = activado;
            ejecutarMenuItem.Enabled = activado;
        }

        private void ActivarReiniciarPrograma()
        {
            bool activado = Main.ListaInstrucciones.Count > 0 && (Main.IndiceInstruccionActual > 0 ||
                (Main.IndiceInstruccionActual == 0 && Main.ListaMicroinstrucciones.Count > 0));
            reiniciarProgramaToolStripButton.Enabled = activado;
            reiniciarMenuItem.Enabled = activado;

        }

        private void ActivarItemsArchivo(bool activado)
        {
            abrirToolStripButton.Enabled = activado;
            abrirToolStripMenuItem.Enabled = activado;
            nuevoToolStripButton.Enabled = activado;
            nuevoToolStripMenuItem.Enabled = activado;
            editorToolStripButton.Enabled = activado;
            guardarToolStripMenuItem.Enabled = activado;
            guardarToolStripMenuItem1.Enabled = activado;
            guardarToolStripButton.Enabled = activado;
            guardarcomoToolStripMenuItem.Enabled = activado;
        }

        private void ActivarItemsEjecucion()
        {
            ejecutarHastaFinalStripButton.Enabled = false;
            ejecutarMenuItem.Enabled = false;
            ejecutarHastaElFinalPasoStripButton.Enabled = false;
            ejecutarHastaElFinalPasoPorPasoMenuItem.Enabled = false;
            reiniciarProgramaToolStripButton.Image = Properties.Resources.stop_small;
            reiniciarProgramaToolStripButton.Enabled = true;
            reiniciarMenuItem.Enabled = true;
            instruccionSiguienteToolStripButton.Enabled = false;
            instruccionSiguienteMenuItem.Enabled = false;
            microinstruccionSiguienteToolStripButton.Enabled = false;
            microinstruccionSiguienteMenuItem.Enabled = false;
            instruccionAnteriorToolStripButton.Enabled = false;
            instruccionAnteriorMenuItem.Enabled = false;
            microinstruccionAnteriorToolStripButton.Enabled = false;
            microinstruccionAnteriorMenuItem.Enabled = false;
            statusStrip1.BackColor = Estilos.GetStyle().RUNNING_STATUS_STRIP_COLOR;
            statusStrip1.ForeColor = Estilos.EsColorOscuro(statusStrip1.BackColor) ? Color.White : Color.Black;
            etiquetaEstado_BarraEstado.Text = "En Ejecución...";
            statusStrip1.Refresh();
            toolStrip1.Refresh();
        }

        private void DetenerHiloEjecucionInstrucciones()
        {
            try
            {
                tokenSource.Cancel();
                hiloEjecucion.Wait(tokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(tokenSource!= null)
                    tokenSource.Dispose();

                ActualizarStatusStrip();
            }
        }

        private void Opciones_Click(object sender, EventArgs e)
        {
            new Opciones().ShowDialog();
        }

        private void IrADireccionMemoriaPrincipalButton_Click(object sender, EventArgs e)
        {
            int direccionMemoria = (ushort)(comboBox1.SelectedIndex * 4096 + comboBox2.SelectedIndex * 256 + comboBox3.SelectedIndex * 16 + comboBox4.SelectedIndex);

            listView_MemoriaPrincipal.EnsureVisible(direccionMemoria);
            listView_MemoriaPrincipal.SelectedIndices.Clear();
            listView_MemoriaPrincipal.SelectedIndices.Add(direccionMemoria);
        }

        private void EjecutarHastaElFinalPasoStripButton_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            cancellation = tokenSource.Token;
            hiloEjecucion = new Task(new Action(EjecutarHastaFinalPorPasos), cancellation);
            hiloEjecucion.Start();
        }
        
        private void EjecutarHastaFinalPorPasos()
        {
            Invoke(new Action(() =>
            {
                ActivarItemsArchivo(false);
                ActivarItemsEjecucion();
            }));

            while (!FinalEjecucion())
            {
                if (cancellation.IsCancellationRequested == true)
                {
                    Invoke(new Action(() =>
                    {
                        ActualizarStatusStrip();
                    }));
                    return;
                }
                Main.EjecutarInstruccion();
                VisualizarMicroinstruccionEjecutada();
            }

            Invoke(new Action(() =>
            {
                reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
                VisualizarMicroinstruccionEjecutada();
                ActualizarOpcionesEjecucion();
                ActualizarStatusStrip();
            }));
        }

        private void Ayuda_Click(object sender, EventArgs e)
        {
            new Ayuda().Show(this);
        }

        private void Acercade_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            EditorCodigo editor = new EditorCodigo(archivoActual.ObtenerPrograma, archivoActual.Ruta);
            
            if(editor.ShowDialog(this) == DialogResult.OK)
            {
                if (archivoActual.LeerProgramaSinFichero(editor.Programa))
                {
                    ListViewVisualStyles.LimpiarIndices();
                    mapaProcesador.RestablecerMapaPDM();
                    RefrescarListViews();
                    ActualizarOpcionesEjecucion();
                    ActualizarStatusStrip(true);

                    listView_Programa.EnsureVisible(listView_Programa.Items.Count - 1);
                    listView_Programa.EnsureVisible(0);
                }
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ListView_Registros_DoubleClick(object sender, EventArgs e)
        {
            ModificarContenido escribirRegistro = new ModificarContenido(listView_Registros.SelectedIndices[0])
            {
                StartPosition = FormStartPosition.Manual
            };
            escribirRegistro.Location = new Point(MousePosition.X - escribirRegistro.Width / 2, MousePosition.Y - escribirRegistro.Height / 2);
            escribirRegistro.ShowDialog(this);
        }

        private void ListView_MemoriaPrincipal_DoubleClick(object sender, EventArgs e)
        {
            ModificarContenido escribirRegistro = new ModificarContenido(listView_MemoriaPrincipal.SelectedIndices[0], true)
            {
                StartPosition = FormStartPosition.Manual
            };
            escribirRegistro.Location = new Point(MousePosition.X - escribirRegistro.Width / 2, MousePosition.Y - escribirRegistro.Height / 2);
            escribirRegistro.ShowDialog(this);
        }

        private void ListView_MemoriaPrincipal_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, PointToClient(MousePosition));
                copiarInstrucciónToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(listView_MemoriaPrincipal.Items[listView_MemoriaPrincipal.SelectedIndices[0]].SubItems[5].Text);
            }
        }

        private void ListView_Registros_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(this, PointToClient(MousePosition));
            }
        }

        private void ModificarContenidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView_MemoriaPrincipal_DoubleClick(sender, e);
        }

        private void ModificarContenidoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView_Registros_DoubleClick(sender, e);
        }

        private void CopiarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[0].Text);
        }

        private void DecimalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[1].Text);
        }

        private void Ca2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[2].Text);
        }

        private void HexadecimalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[3].Text);
        }

        private void BinarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[4].Text);
        }

        private void DefaultSize_Click(object sender, EventArgs e)
        {
            Size = new Size(1537, 848);
        }

        private void Programa_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string nombreArchivo = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (nombreArchivo == archivoActual.Ruta) return;
                AbrirArchivo(nombreArchivo);
                listView_Programa.EnsureVisible(listView_Programa.Items.Count - 1);
                listView_Programa.EnsureVisible(0);
            }
        }

        private void Programa_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void SimPDM_Load(object sender, EventArgs e)
        {
            if (startupFile != null)
            {
                AbrirArchivo(startupFile);
                startupFile = null;
            }
        }

        private void GuardarArchivo()
        {
            SaveFileDialog guardar = new SaveFileDialog
            {
                FileName = Path.GetFileName(archivoActual.Ruta ?? "Programa"),
                Filter = "Programas de PDM (*.pdm)|*.pdm|Todos los archivos (*.*)|*.*"
            };
            if (guardar.ShowDialog(this) == DialogResult.OK)
            {
                archivoActual.GuardarProgramaComo(guardar.FileName);
            }
        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (archivoActual.Ruta != null)
                archivoActual.GuardarPrograma();
            else
                GuardarArchivo();

            ActualizarStatusStrip();
        }

        private void guardarcomoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivo();
            ActualizarStatusStrip();

        }

        private void restablecerDireccionesSeleccionadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ObtenerMemoria.RestablecerMemoria(listView_MemoriaPrincipal.SelectedIndices.Cast<int>());
            listView_MemoriaPrincipal.Invalidate();
        }

        private void SimPDM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(archivoActual.Ruta == null && !string.IsNullOrEmpty(archivoActual.ObtenerPrograma))
            {
                DialogResult guardar = MessageBox.Show("¿Desea guardar el programa antes de salir?", "Guardar y salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (guardar)
                {
                    case DialogResult.Yes: GuardarArchivo(); break;
                    case DialogResult.Cancel: e.Cancel = true; break;
                }

            }
            else if (ProgrmaConCambiosSinGuardar)
            {
                DialogResult guardar = MessageBox.Show("El programa \"" + Path.GetFileName(archivoActual.Ruta) + "\" ha sido modificado. ¿Desea guardar los cambios antes de salir?", "Guardar y salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch(guardar)
                {
                    case DialogResult.Yes: archivoActual.GuardarPrograma();break;
                    case DialogResult.Cancel: e.Cancel = true; break;
                }
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (debugWindow == null || debugWindow.IsDisposed)
                debugWindow = new Debug();

            debugWindow.Show(this);

        }
    }
}
