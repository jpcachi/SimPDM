using PDMv4.Procesador;
using PDMv4.Utilidades;
using System;
using System.Drawing;
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
        

        public SimPDM()
        {
            InitializeComponent();
            BackColor = Constants.DEFAULT_WINDOW_BACKGROUND_COLOR;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            panelMejorado1.ContentControls.Add(listView_Programa);
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

            tokenSource = new CancellationTokenSource();
            cancellation = tokenSource.Token;
        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            Main.Restablecer();
            archivoActual.ObtenerLineasPrograma.Clear();
            archivoActual.ResetearRuta();
            mapaProcesador.RestablecerMapaPDM();
            ListViewVisualStyles.LimpiarIndices();
            RefrescarListViews();
            ActualizarStatusStrip();
            ActualizarOpcionesEjecucion();
        }

        private void abrirToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (archivoActual.LeerPrograma(openFileDialog1.FileName))
                {
                    ListViewVisualStyles.LimpiarIndices();
                    mapaProcesador.RestablecerMapaPDM();
                    RefrescarListViews();
                    ActualizarOpcionesEjecucion();
                    ActualizarStatusStrip();
                }
            }
        }


        private void listView_Registros_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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

        private void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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

        private void listView_Flags_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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

        private void listView_Programa_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(archivoActual.ObtenerLineasPrograma[e.ItemIndex][0]);
            e.Item.SubItems.Add(new ListViewItem.ListViewSubItem(e.Item, archivoActual.ObtenerLineasPrograma[e.ItemIndex][1]));
        }

        private void listView_Microinstrucciones_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
                ReiniciarPrograma_Click(sender, e);
            else
                RevertirMicroInstruccion(); 
        }

        private void InstruccionAnterior_Click(object sender, EventArgs e)
        {
            if (Main.IndiceInstruccionActual == 0)
                ReiniciarPrograma_Click(sender, e);
            else
                RevertirInstruccion();
        }

        private void listView_Programa_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Programa);
        }

        private void listView_Registros_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Registros);
        }

        private void listView_MemoriaPrincipal_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Memoria);
        }

        private void listView_Flags_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewVisualStyles.DibujarSubItemListView(sender, e, TListView.Flags);
        }

        private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = (sender as ListView).Columns[e.ColumnIndex].Width;
        }

        private void listView_Registros_Resize(object sender, EventArgs e)
        {
            (sender as ListView).Columns[(sender as ListView).Columns.Count - 1].Width = -2;
        }

        private void SplitContainer_CancelSplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void listView_Microinstrucciones_Resize(object sender, EventArgs e)
        {
            int calculateFaseWidth = listView_Microinstrucciones.Width - (listView_Microinstrucciones.Columns.Count - 1) * 50;
            Fase.Width = calculateFaseWidth >= 205 ? calculateFaseWidth : 205;
            listView_Microinstrucciones.Scrollable = listView_Microinstrucciones.Width < 50 * (listView_Microinstrucciones.Columns.Count  - 1) + 205;
        }

        private void ActualizarStatusStrip()
        {
            etiquetaRuta_BarraEstado.Text = archivoActual.Ruta ?? "Nuevo";
            etiquetaEstado_BarraEstado.Text = "Listo";
            etiquetaNumeroLineas_BarraEstado.Text = archivoActual.ObtenerLineasPrograma.Count + " líneas";
            statusStrip1.BackColor = SystemColors.ActiveCaption;
            statusStrip1.ForeColor = SystemColors.ControlText;
        }

        private void ReiniciarPrograma_Click(object sender, EventArgs e)
        {
            DetenerHiloEjecucionInstrucciones();
            archivoActual.LeerPrograma(archivoActual.Ruta);
            ListViewVisualStyles.LimpiarIndices();
            mapaProcesador.RestablecerMapaPDM();
            RefrescarListViews();
            ActualizarOpcionesEjecucion();
            reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
            ActivarItemsArchivo(true);
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {        
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
            
            while (Main.IndiceInstruccionActual < Main.ListaInstrucciones.Count - 1 ||
                (Main.IndiceInstruccionActual == Main.ListaInstrucciones.Count - 1 && Main.IndiceMicroinstruccionActual < Main.ListaMicroinstrucciones.Count - 1))
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
            reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
            
            VisualizarMicroinstruccionEjecutada();
            ActualizarOpcionesEjecucion();
            ActualizarStatusStrip();
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
                bool activado = Main.IndiceMicroinstruccionActual < Main.ListaMicroinstrucciones.Count - 1;
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
            bool activado = Main.ListaInstrucciones.Count > 0 &&
               (Main.IndiceInstruccionActual < Main.ListaInstrucciones.Count - 1 ||
                (Main.IndiceInstruccionActual == Main.ListaInstrucciones.Count - 1 && Main.IndiceMicroinstruccionActual < Main.ListaMicroinstrucciones.Count - 1));
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
            statusStrip1.BackColor = Color.OrangeRed;
            statusStrip1.ForeColor = Color.White;
            etiquetaEstado_BarraEstado.Text = "En Ejecución...";
        }

        private void DetenerHiloEjecucionInstrucciones()
        {
            tokenSource.Cancel();
            try
            {
                hiloEjecucion.Wait(tokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                tokenSource.Dispose();
                tokenSource = new CancellationTokenSource();
                cancellation = tokenSource.Token;
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

            while (Main.IndiceInstruccionActual < Main.ListaInstrucciones.Count - 1 ||
                (Main.IndiceInstruccionActual == Main.ListaInstrucciones.Count - 1 && Main.IndiceMicroinstruccionActual < Main.ListaMicroinstrucciones.Count - 1))
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

            reiniciarProgramaToolStripButton.Image = Properties.Resources.HistoryItem_16x16;
            ActualizarOpcionesEjecucion();
            ActualizarStatusStrip();
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
            EditorCodigo editor = new EditorCodigo(archivoActual.Ruta);
            
            if(editor.ShowDialog(this) == DialogResult.OK)
            {
                string rutaEditor = editor.Ruta;
                if (archivoActual.LeerPrograma(rutaEditor))
                {
                    ListViewVisualStyles.LimpiarIndices();
                    mapaProcesador.RestablecerMapaPDM();
                    RefrescarListViews();
                    ActualizarOpcionesEjecucion();
                    ActualizarStatusStrip();
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listView_Registros_DoubleClick(object sender, EventArgs e)
        {
            ModificarContenido escribirRegistro = new ModificarContenido(listView_Registros.SelectedIndices[0])
            {
                StartPosition = FormStartPosition.Manual
            };
            escribirRegistro.Location = new Point(MousePosition.X - escribirRegistro.Width / 2, MousePosition.Y - escribirRegistro.Height / 2);
            escribirRegistro.ShowDialog(this);
        }

        private void listView_MemoriaPrincipal_DoubleClick(object sender, EventArgs e)
        {
            ModificarContenido escribirRegistro = new ModificarContenido(listView_MemoriaPrincipal.SelectedIndices[0], true)
            {
                StartPosition = FormStartPosition.Manual
            };
            escribirRegistro.Location = new Point(MousePosition.X - escribirRegistro.Width / 2, MousePosition.Y - escribirRegistro.Height / 2);
            escribirRegistro.ShowDialog(this);
        }

        private void listView_MemoriaPrincipal_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, PointToClient(MousePosition));
                copiarInstrucciónToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(listView_MemoriaPrincipal.Items[listView_MemoriaPrincipal.SelectedIndices[0]].SubItems[5].Text);
            }
        }

        private void listView_Registros_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(this, PointToClient(MousePosition));
            }
        }

        private void modificarContenidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_MemoriaPrincipal_DoubleClick(sender, e);
        }

        private void modificarContenidoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView_Registros_DoubleClick(sender, e);
        }

        private void copiarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[0].Text);
        }

        private void decimalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[1].Text);
        }

        private void ca2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[2].Text);
        }

        private void hexadecimalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[3].Text);
        }

        private void binarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView_Registros.Items[listView_Registros.SelectedIndices[0]].SubItems[4].Text);
        }

        private void DefaultSize_Click(object sender, EventArgs e)
        {
            Size = new Size(1537, 848);
        }
    }
}
