using PDMv4.Procesador;

namespace PDMv4.Vistas
{
    partial class SimPDM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (hiloEjecucion != null && hiloEjecucion.Status == System.Threading.Tasks.TaskStatus.Canceled)
            {
                DetenerHiloEjecucionInstrucciones();
                hiloEjecucion.Dispose();
                tokenSource.Dispose();
            }

            if (OpcionesPrograma.EntradaSalida)
            {
                UC.ArchivoES?.CerrarFichero();
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimPDM));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.etiquetaRuta_BarraEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.etiquetaNumeroLineas_BarraEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.etiquetaEstado_BarraEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarcomoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microinstruccionSiguienteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microinstruccionAnteriorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.instruccionSiguienteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instruccionAnteriorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ejecutarHastaElFinalPasoPorPasoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.reiniciarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contenidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.acercadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.nuevoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.abrirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.guardarToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.microinstruccionAnteriorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.microinstruccionSiguienteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.instruccionAnteriorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.instruccionSiguienteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.reiniciarProgramaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ejecutarHastaElFinalPasoStripButton = new System.Windows.Forms.ToolStripButton();
            this.ejecutarHastaFinalStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listView_Programa = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_Microinstrucciones = new System.Windows.Forms.ListView();
            this.Fase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LRI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LCP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TSR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TER = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LAc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TAc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MEM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TMEM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UAL2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UAL1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UAL0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TUAL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LUAL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView_Registros = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_MemoriaPrincipal = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_Flags = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restablecerDireccionesSeleccionadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarContenidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarContenidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ca2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarInstrucciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarContenidoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.copiarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarContenidoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.decimalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ca2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hexadecimalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.binarioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMejorado6 = new PDMv4.Controles.PanelMejorado();
            this.panelMejorado5 = new PDMv4.Controles.PanelMejorado();
            this.panelMejorado4 = new PDMv4.Controles.PanelMejorado();
            this.panelIrA = new System.Windows.Forms.Panel();
            this.button1 = new PDMv4.Controles.CustomButton();
            this.comboBox4 = new PDMv4.Controles.FlatComboBox();
            this.comboBox3 = new PDMv4.Controles.FlatComboBox();
            this.comboBox2 = new PDMv4.Controles.FlatComboBox();
            this.comboBox1 = new PDMv4.Controles.FlatComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMejorado3 = new PDMv4.Controles.PanelMejorado();
            this.panelMejorado2 = new PDMv4.Controles.PanelMejorado();
            this.panelMejorado1 = new PDMv4.Controles.PanelMejorado();
            this.mapaProcesador = new PDMv4.Controles.MapaProcesador();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panelMejorado4.SuspendLayout();
            this.panelIrA.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado6);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado5);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado4);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado3);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMejorado1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1521, 738);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1521, 809);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.etiquetaRuta_BarraEstado,
            this.etiquetaNumeroLineas_BarraEstado,
            this.toolStripStatusLabel2,
            this.etiquetaEstado_BarraEstado,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1521, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // etiquetaRuta_BarraEstado
            // 
            this.etiquetaRuta_BarraEstado.Name = "etiquetaRuta_BarraEstado";
            this.etiquetaRuta_BarraEstado.Size = new System.Drawing.Size(1124, 17);
            this.etiquetaRuta_BarraEstado.Spring = true;
            this.etiquetaRuta_BarraEstado.Text = "Nuevo";
            this.etiquetaRuta_BarraEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // etiquetaNumeroLineas_BarraEstado
            // 
            this.etiquetaNumeroLineas_BarraEstado.BackColor = System.Drawing.Color.Transparent;
            this.etiquetaNumeroLineas_BarraEstado.Name = "etiquetaNumeroLineas_BarraEstado";
            this.etiquetaNumeroLineas_BarraEstado.Size = new System.Drawing.Size(46, 17);
            this.etiquetaNumeroLineas_BarraEstado.Text = "0 líneas";
            this.etiquetaNumeroLineas_BarraEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 17);
            // 
            // etiquetaEstado_BarraEstado
            // 
            this.etiquetaEstado_BarraEstado.Name = "etiquetaEstado_BarraEstado";
            this.etiquetaEstado_BarraEstado.Padding = new System.Windows.Forms.Padding(0, 0, 300, 0);
            this.etiquetaEstado_BarraEstado.Size = new System.Drawing.Size(332, 17);
            this.etiquetaEstado_BarraEstado.Text = "Listo";
            this.etiquetaEstado_BarraEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.herramientasToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1521, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.toolStripSeparator11,
            this.guardarToolStripMenuItem1,
            this.guardarcomoToolStripMenuItem,
            this.toolStripSeparator,
            this.guardarToolStripMenuItem,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::PDMv4.Properties.Resources.fileIcon;
            this.nuevoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.nuevoToolStripMenuItem.Text = "&Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.NuevoToolStripButton_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.abrirToolStripMenuItem.Text = "&Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripButton_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(172, 6);
            // 
            // guardarToolStripMenuItem1
            // 
            this.guardarToolStripMenuItem1.Image = global::PDMv4.Properties.Resources.Save_16x16;
            this.guardarToolStripMenuItem1.Name = "guardarToolStripMenuItem1";
            this.guardarToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.guardarToolStripMenuItem1.Text = "&Guardar";
            this.guardarToolStripMenuItem1.Click += new System.EventHandler(this.guardarToolStripMenuItem1_Click);
            // 
            // guardarcomoToolStripMenuItem
            // 
            this.guardarcomoToolStripMenuItem.Name = "guardarcomoToolStripMenuItem";
            this.guardarcomoToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.guardarcomoToolStripMenuItem.Text = "Guardar &como";
            this.guardarcomoToolStripMenuItem.Click += new System.EventHandler(this.guardarcomoToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(172, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = global::PDMv4.Properties.Resources.sm_edit;
            this.guardarToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.guardarToolStripMenuItem.Text = "Abrir &editor";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.Editor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(172, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.microinstruccionSiguienteMenuItem,
            this.microinstruccionAnteriorMenuItem,
            this.toolStripSeparator3,
            this.instruccionSiguienteMenuItem,
            this.instruccionAnteriorMenuItem,
            this.toolStripSeparator4,
            this.ejecutarHastaElFinalPasoPorPasoMenuItem,
            this.ejecutarMenuItem,
            this.toolStripSeparator1,
            this.reiniciarMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.editarToolStripMenuItem.Text = "&Ejecutar";
            // 
            // microinstruccionSiguienteMenuItem
            // 
            this.microinstruccionSiguienteMenuItem.Enabled = false;
            this.microinstruccionSiguienteMenuItem.Image = global::PDMv4.Properties.Resources._24x24_chevron_green;
            this.microinstruccionSiguienteMenuItem.Name = "microinstruccionSiguienteMenuItem";
            this.microinstruccionSiguienteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.microinstruccionSiguienteMenuItem.Size = new System.Drawing.Size(296, 22);
            this.microinstruccionSiguienteMenuItem.Text = "Microinstrucción &siguiente";
            this.microinstruccionSiguienteMenuItem.Click += new System.EventHandler(this.MicroinstruccionSiguiente_Click);
            // 
            // microinstruccionAnteriorMenuItem
            // 
            this.microinstruccionAnteriorMenuItem.Enabled = false;
            this.microinstruccionAnteriorMenuItem.Image = global::PDMv4.Properties.Resources._24x24_chevron_remove_green;
            this.microinstruccionAnteriorMenuItem.Name = "microinstruccionAnteriorMenuItem";
            this.microinstruccionAnteriorMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.microinstruccionAnteriorMenuItem.Size = new System.Drawing.Size(296, 22);
            this.microinstruccionAnteriorMenuItem.Text = "Microinstrucción a&nterior";
            this.microinstruccionAnteriorMenuItem.Click += new System.EventHandler(this.MicroinstruccionAnterior_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(293, 6);
            // 
            // instruccionSiguienteMenuItem
            // 
            this.instruccionSiguienteMenuItem.Enabled = false;
            this.instruccionSiguienteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("instruccionSiguienteMenuItem.Image")));
            this.instruccionSiguienteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.instruccionSiguienteMenuItem.Name = "instruccionSiguienteMenuItem";
            this.instruccionSiguienteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F7)));
            this.instruccionSiguienteMenuItem.Size = new System.Drawing.Size(296, 22);
            this.instruccionSiguienteMenuItem.Text = "&Instrucción siguiente";
            this.instruccionSiguienteMenuItem.Click += new System.EventHandler(this.InstruccionSiguiente_Click);
            // 
            // instruccionAnteriorMenuItem
            // 
            this.instruccionAnteriorMenuItem.Enabled = false;
            this.instruccionAnteriorMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("instruccionAnteriorMenuItem.Image")));
            this.instruccionAnteriorMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.instruccionAnteriorMenuItem.Name = "instruccionAnteriorMenuItem";
            this.instruccionAnteriorMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.instruccionAnteriorMenuItem.Size = new System.Drawing.Size(296, 22);
            this.instruccionAnteriorMenuItem.Text = "Instrucción ante&rior";
            this.instruccionAnteriorMenuItem.Click += new System.EventHandler(this.InstruccionAnterior_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(293, 6);
            // 
            // ejecutarHastaElFinalPasoPorPasoMenuItem
            // 
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Enabled = false;
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Image = global::PDMv4.Properties.Resources.run_small;
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Name = "ejecutarHastaElFinalPasoPorPasoMenuItem";
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Size = new System.Drawing.Size(296, 22);
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Text = "Ejecutar hasta el final &paso a paso";
            this.ejecutarHastaElFinalPasoPorPasoMenuItem.Click += new System.EventHandler(this.EjecutarHastaElFinalPasoStripButton_Click);
            // 
            // ejecutarMenuItem
            // 
            this.ejecutarMenuItem.Enabled = false;
            this.ejecutarMenuItem.Image = global::PDMv4.Properties.Resources.execute_small;
            this.ejecutarMenuItem.Name = "ejecutarMenuItem";
            this.ejecutarMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ejecutarMenuItem.Size = new System.Drawing.Size(296, 22);
            this.ejecutarMenuItem.Text = "&Ejecutar hasta el final";
            this.ejecutarMenuItem.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(293, 6);
            // 
            // reiniciarMenuItem
            // 
            this.reiniciarMenuItem.Enabled = false;
            this.reiniciarMenuItem.Image = global::PDMv4.Properties.Resources.stop_small;
            this.reiniciarMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reiniciarMenuItem.Name = "reiniciarMenuItem";
            this.reiniciarMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.reiniciarMenuItem.Size = new System.Drawing.Size(296, 22);
            this.reiniciarMenuItem.Text = "Reiniciar/&Detener";
            this.reiniciarMenuItem.Click += new System.EventHandler(this.ReiniciarPrograma_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem,
            this.personalizarToolStripMenuItem,
            this.toolStripSeparator12,
            this.debugToolStripMenuItem});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "&Herramientas";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.Image = global::PDMv4.Properties.Resources.Settings;
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.opcionesToolStripMenuItem.Text = "&Opciones";
            this.opcionesToolStripMenuItem.Click += new System.EventHandler(this.Opciones_Click);
            // 
            // personalizarToolStripMenuItem
            // 
            this.personalizarToolStripMenuItem.Image = global::PDMv4.Properties.Resources.Actions_transform_scale_Icon_541953;
            this.personalizarToolStripMenuItem.Name = "personalizarToolStripMenuItem";
            this.personalizarToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.personalizarToolStripMenuItem.Text = "&Restablecer tamaño de ventana";
            this.personalizarToolStripMenuItem.Click += new System.EventHandler(this.DefaultSize_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(236, 6);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contenidoToolStripMenuItem,
            this.toolStripSeparator5,
            this.acercadeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ay&uda";
            // 
            // contenidoToolStripMenuItem
            // 
            this.contenidoToolStripMenuItem.Image = global::PDMv4.Properties.Resources.Help2;
            this.contenidoToolStripMenuItem.Name = "contenidoToolStripMenuItem";
            this.contenidoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contenidoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.contenidoToolStripMenuItem.Text = "&Contenido";
            this.contenidoToolStripMenuItem.Click += new System.EventHandler(this.Ayuda_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(173, 6);
            // 
            // acercadeToolStripMenuItem
            // 
            this.acercadeToolStripMenuItem.Image = global::PDMv4.Properties.Resources.document_properties;
            this.acercadeToolStripMenuItem.Name = "acercadeToolStripMenuItem";
            this.acercadeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.acercadeToolStripMenuItem.Text = "&Acerca de...";
            this.acercadeToolStripMenuItem.Click += new System.EventHandler(this.Acercade_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripButton,
            this.abrirToolStripButton,
            this.guardarToolStripButton,
            this.editorToolStripButton,
            this.toolStripSeparator6,
            this.microinstruccionAnteriorToolStripButton,
            this.microinstruccionSiguienteToolStripButton,
            this.instruccionAnteriorToolStripButton,
            this.instruccionSiguienteToolStripButton,
            this.reiniciarProgramaToolStripButton,
            this.ejecutarHastaElFinalPasoStripButton,
            this.ejecutarHastaFinalStripButton,
            this.toolStripSeparator7,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator8,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(352, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // nuevoToolStripButton
            // 
            this.nuevoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nuevoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripButton.Image")));
            this.nuevoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoToolStripButton.Name = "nuevoToolStripButton";
            this.nuevoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.nuevoToolStripButton.Text = "&Nuevo";
            this.nuevoToolStripButton.Click += new System.EventHandler(this.NuevoToolStripButton_Click);
            // 
            // abrirToolStripButton
            // 
            this.abrirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abrirToolStripButton.Image = global::PDMv4.Properties.Resources.open_16;
            this.abrirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripButton.Name = "abrirToolStripButton";
            this.abrirToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.abrirToolStripButton.Text = "&Abrir";
            this.abrirToolStripButton.Click += new System.EventHandler(this.AbrirToolStripButton_Click);
            // 
            // guardarToolStripButton
            // 
            this.guardarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.guardarToolStripButton.Image = global::PDMv4.Properties.Resources.Save_16x16;
            this.guardarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarToolStripButton.Name = "guardarToolStripButton";
            this.guardarToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.guardarToolStripButton.Text = "&Guardar";
            this.guardarToolStripButton.Click += new System.EventHandler(this.guardarToolStripMenuItem1_Click);
            // 
            // editorToolStripButton
            // 
            this.editorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editorToolStripButton.Image")));
            this.editorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editorToolStripButton.Name = "editorToolStripButton";
            this.editorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editorToolStripButton.Text = "&Editor";
            this.editorToolStripButton.Click += new System.EventHandler(this.Editor_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // microinstruccionAnteriorToolStripButton
            // 
            this.microinstruccionAnteriorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.microinstruccionAnteriorToolStripButton.Enabled = false;
            this.microinstruccionAnteriorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("microinstruccionAnteriorToolStripButton.Image")));
            this.microinstruccionAnteriorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.microinstruccionAnteriorToolStripButton.Name = "microinstruccionAnteriorToolStripButton";
            this.microinstruccionAnteriorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.microinstruccionAnteriorToolStripButton.Text = "Microinstrucción &anterior";
            this.microinstruccionAnteriorToolStripButton.Click += new System.EventHandler(this.MicroinstruccionAnterior_Click);
            // 
            // microinstruccionSiguienteToolStripButton
            // 
            this.microinstruccionSiguienteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.microinstruccionSiguienteToolStripButton.Enabled = false;
            this.microinstruccionSiguienteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("microinstruccionSiguienteToolStripButton.Image")));
            this.microinstruccionSiguienteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.microinstruccionSiguienteToolStripButton.Name = "microinstruccionSiguienteToolStripButton";
            this.microinstruccionSiguienteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.microinstruccionSiguienteToolStripButton.Text = "Microinstrucción &siguiente";
            this.microinstruccionSiguienteToolStripButton.Click += new System.EventHandler(this.MicroinstruccionSiguiente_Click);
            // 
            // instruccionAnteriorToolStripButton
            // 
            this.instruccionAnteriorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.instruccionAnteriorToolStripButton.Enabled = false;
            this.instruccionAnteriorToolStripButton.Image = global::PDMv4.Properties.Resources.accum_remove_16_all;
            this.instruccionAnteriorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.instruccionAnteriorToolStripButton.Name = "instruccionAnteriorToolStripButton";
            this.instruccionAnteriorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.instruccionAnteriorToolStripButton.Text = "Instrucción an&terior";
            this.instruccionAnteriorToolStripButton.Click += new System.EventHandler(this.InstruccionAnterior_Click);
            // 
            // instruccionSiguienteToolStripButton
            // 
            this.instruccionSiguienteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.instruccionSiguienteToolStripButton.Enabled = false;
            this.instruccionSiguienteToolStripButton.Image = global::PDMv4.Properties.Resources.accum_add_16_all;
            this.instruccionSiguienteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.instruccionSiguienteToolStripButton.Name = "instruccionSiguienteToolStripButton";
            this.instruccionSiguienteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.instruccionSiguienteToolStripButton.Text = "&Instrucción siguiente";
            this.instruccionSiguienteToolStripButton.Click += new System.EventHandler(this.InstruccionSiguiente_Click);
            // 
            // reiniciarProgramaToolStripButton
            // 
            this.reiniciarProgramaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reiniciarProgramaToolStripButton.Enabled = false;
            this.reiniciarProgramaToolStripButton.Image = global::PDMv4.Properties.Resources.HistoryItem_16x16;
            this.reiniciarProgramaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reiniciarProgramaToolStripButton.Name = "reiniciarProgramaToolStripButton";
            this.reiniciarProgramaToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.reiniciarProgramaToolStripButton.Text = "Reiniciar / &Detener";
            this.reiniciarProgramaToolStripButton.Click += new System.EventHandler(this.ReiniciarPrograma_Click);
            // 
            // ejecutarHastaElFinalPasoStripButton
            // 
            this.ejecutarHastaElFinalPasoStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ejecutarHastaElFinalPasoStripButton.Enabled = false;
            this.ejecutarHastaElFinalPasoStripButton.Image = global::PDMv4.Properties.Resources.run_small;
            this.ejecutarHastaElFinalPasoStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ejecutarHastaElFinalPasoStripButton.Name = "ejecutarHastaElFinalPasoStripButton";
            this.ejecutarHastaElFinalPasoStripButton.Size = new System.Drawing.Size(23, 22);
            this.ejecutarHastaElFinalPasoStripButton.Text = "Ejecutar &paso a paso";
            this.ejecutarHastaElFinalPasoStripButton.Click += new System.EventHandler(this.EjecutarHastaElFinalPasoStripButton_Click);
            // 
            // ejecutarHastaFinalStripButton
            // 
            this.ejecutarHastaFinalStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ejecutarHastaFinalStripButton.Enabled = false;
            this.ejecutarHastaFinalStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ejecutarHastaFinalStripButton.Image")));
            this.ejecutarHastaFinalStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ejecutarHastaFinalStripButton.Name = "ejecutarHastaFinalStripButton";
            this.ejecutarHastaFinalStripButton.Size = new System.Drawing.Size(23, 22);
            this.ejecutarHastaFinalStripButton.Text = "&Ejecutar hasta el final";
            this.ejecutarHastaFinalStripButton.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::PDMv4.Properties.Resources.Settings;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "&Opciones";
            this.toolStripButton2.Click += new System.EventHandler(this.Opciones_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::PDMv4.Properties.Resources.Actions_transform_scale_Icon_541953;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Restablecer tamaño de ventana";
            this.toolStripButton3.Click += new System.EventHandler(this.DefaultSize_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::PDMv4.Properties.Resources.Help2;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Ay&uda";
            this.toolStripButton1.Click += new System.EventHandler(this.Ayuda_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Programas de PDM (*.pdm)|*.pdm|Todos los archivos (*.*)|*.*";
            // 
            // listView_Programa
            // 
            this.listView_Programa.AllowDrop = true;
            this.listView_Programa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Programa.BackColor = System.Drawing.SystemColors.Control;
            this.listView_Programa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Programa.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_Programa.FullRowSelect = true;
            this.listView_Programa.HideSelection = false;
            this.listView_Programa.Location = new System.Drawing.Point(5, 5);
            this.listView_Programa.MultiSelect = false;
            this.listView_Programa.Name = "listView_Programa";
            this.listView_Programa.OwnerDraw = true;
            this.listView_Programa.Size = new System.Drawing.Size(259, 530);
            this.listView_Programa.TabIndex = 10;
            this.listView_Programa.UseCompatibleStateImageBehavior = false;
            this.listView_Programa.View = System.Windows.Forms.View.Details;
            this.listView_Programa.VirtualMode = true;
            this.listView_Programa.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            this.listView_Programa.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
            this.listView_Programa.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_Programa_DrawSubItem);
            this.listView_Programa.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListView_Programa_RetrieveVirtualItem);
            this.listView_Programa.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.listView_Programa.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            this.listView_Programa.Resize += new System.EventHandler(this.ListView_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Etiqueta";
            this.columnHeader1.Width = 112;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Instrucción";
            this.columnHeader2.Width = 147;
            // 
            // listView_Microinstrucciones
            // 
            this.listView_Microinstrucciones.AllowDrop = true;
            this.listView_Microinstrucciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Microinstrucciones.BackColor = System.Drawing.SystemColors.Control;
            this.listView_Microinstrucciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Microinstrucciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Fase,
            this.LRI,
            this.LCP,
            this.TSR,
            this.TER,
            this.LH,
            this.LL,
            this.LF,
            this.TF,
            this.LAc,
            this.TAc,
            this.LE,
            this.MEM,
            this.TMEM,
            this.UAL2,
            this.UAL1,
            this.UAL0,
            this.TUAL,
            this.LUAL});
            this.listView_Microinstrucciones.FullRowSelect = true;
            this.listView_Microinstrucciones.GridLines = true;
            this.listView_Microinstrucciones.HideSelection = false;
            this.listView_Microinstrucciones.Location = new System.Drawing.Point(5, 5);
            this.listView_Microinstrucciones.MultiSelect = false;
            this.listView_Microinstrucciones.Name = "listView_Microinstrucciones";
            this.listView_Microinstrucciones.OwnerDraw = true;
            this.listView_Microinstrucciones.Size = new System.Drawing.Size(1120, 112);
            this.listView_Microinstrucciones.TabIndex = 9;
            this.listView_Microinstrucciones.UseCompatibleStateImageBehavior = false;
            this.listView_Microinstrucciones.View = System.Windows.Forms.View.Details;
            this.listView_Microinstrucciones.VirtualMode = true;
            this.listView_Microinstrucciones.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            this.listView_Microinstrucciones.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
            this.listView_Microinstrucciones.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_Microinstrucciones_DrawSubItem);
            this.listView_Microinstrucciones.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListView2_RetrieveVirtualItem);
            this.listView_Microinstrucciones.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.listView_Microinstrucciones.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            this.listView_Microinstrucciones.Resize += new System.EventHandler(this.ListView_Microinstrucciones_Resize);
            // 
            // Fase
            // 
            this.Fase.Text = "Fase";
            this.Fase.Width = 219;
            // 
            // LRI
            // 
            this.LRI.Text = "   LRI";
            this.LRI.Width = 50;
            // 
            // LCP
            // 
            this.LCP.Text = "   LCP";
            this.LCP.Width = 50;
            // 
            // TSR
            // 
            this.TSR.Text = "  TSR";
            this.TSR.Width = 50;
            // 
            // TER
            // 
            this.TER.Text = "  TER";
            this.TER.Width = 50;
            // 
            // LH
            // 
            this.LH.Text = "    LH";
            this.LH.Width = 50;
            // 
            // LL
            // 
            this.LL.Text = "    LL";
            this.LL.Width = 50;
            // 
            // LF
            // 
            this.LF.Text = "    LF";
            this.LF.Width = 50;
            // 
            // TF
            // 
            this.TF.Text = "    TF";
            this.TF.Width = 50;
            // 
            // LAc
            // 
            this.LAc.Text = "   LAc";
            this.LAc.Width = 50;
            // 
            // TAc
            // 
            this.TAc.Text = "  TAc";
            this.TAc.Width = 50;
            // 
            // LE
            // 
            this.LE.Text = "   L/E";
            this.LE.Width = 50;
            // 
            // MEM
            // 
            this.MEM.Text = "  MEM";
            this.MEM.Width = 50;
            // 
            // TMEM
            // 
            this.TMEM.Text = " TMEM";
            this.TMEM.Width = 50;
            // 
            // UAL2
            // 
            this.UAL2.Text = " UAL2";
            this.UAL2.Width = 50;
            // 
            // UAL1
            // 
            this.UAL1.Text = " UAL1";
            this.UAL1.Width = 50;
            // 
            // UAL0
            // 
            this.UAL0.Text = " UAL0";
            this.UAL0.Width = 50;
            // 
            // TUAL
            // 
            this.TUAL.Text = " TUAL";
            this.TUAL.Width = 50;
            // 
            // LUAL
            // 
            this.LUAL.Text = " LUAL";
            this.LUAL.Width = 50;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(845, 530);
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.mapaProcesador);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 530);
            this.panel1.TabIndex = 2;
            // 
            // listView_Registros
            // 
            this.listView_Registros.AllowDrop = true;
            this.listView_Registros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Registros.BackColor = System.Drawing.SystemColors.Control;
            this.listView_Registros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Registros.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.listView_Registros.FullRowSelect = true;
            this.listView_Registros.GridLines = true;
            this.listView_Registros.HideSelection = false;
            this.listView_Registros.Location = new System.Drawing.Point(5, 5);
            this.listView_Registros.Name = "listView_Registros";
            this.listView_Registros.OwnerDraw = true;
            this.listView_Registros.Size = new System.Drawing.Size(350, 110);
            this.listView_Registros.TabIndex = 6;
            this.listView_Registros.UseCompatibleStateImageBehavior = false;
            this.listView_Registros.View = System.Windows.Forms.View.Details;
            this.listView_Registros.VirtualListSize = 5;
            this.listView_Registros.VirtualMode = true;
            this.listView_Registros.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            this.listView_Registros.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
            this.listView_Registros.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_Registros_DrawSubItem);
            this.listView_Registros.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListView_Registros_RetrieveVirtualItem);
            this.listView_Registros.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.listView_Registros.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            this.listView_Registros.DoubleClick += new System.EventHandler(this.ListView_Registros_DoubleClick);
            this.listView_Registros.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_Registros_MouseClick);
            this.listView_Registros.Resize += new System.EventHandler(this.ListView_Resize);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Registro";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Decimal";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Ca2";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Hex";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Binario";
            this.columnHeader12.Width = 108;
            // 
            // listView_MemoriaPrincipal
            // 
            this.listView_MemoriaPrincipal.AllowDrop = true;
            this.listView_MemoriaPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_MemoriaPrincipal.BackColor = System.Drawing.SystemColors.Control;
            this.listView_MemoriaPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_MemoriaPrincipal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.listView_MemoriaPrincipal.FullRowSelect = true;
            this.listView_MemoriaPrincipal.GridLines = true;
            this.listView_MemoriaPrincipal.HideSelection = false;
            this.listView_MemoriaPrincipal.Location = new System.Drawing.Point(5, 5);
            this.listView_MemoriaPrincipal.Name = "listView_MemoriaPrincipal";
            this.listView_MemoriaPrincipal.OwnerDraw = true;
            this.listView_MemoriaPrincipal.Size = new System.Drawing.Size(350, 349);
            this.listView_MemoriaPrincipal.TabIndex = 7;
            this.listView_MemoriaPrincipal.UseCompatibleStateImageBehavior = false;
            this.listView_MemoriaPrincipal.View = System.Windows.Forms.View.Details;
            this.listView_MemoriaPrincipal.VirtualMode = true;
            this.listView_MemoriaPrincipal.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            this.listView_MemoriaPrincipal.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
            this.listView_MemoriaPrincipal.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_MemoriaPrincipal_DrawSubItem);
            this.listView_MemoriaPrincipal.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListView_MemoriaPrincipal_RetrieveVirtualItem);
            this.listView_MemoriaPrincipal.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.listView_MemoriaPrincipal.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            this.listView_MemoriaPrincipal.DoubleClick += new System.EventHandler(this.ListView_MemoriaPrincipal_DoubleClick);
            this.listView_MemoriaPrincipal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MemoriaPrincipal_MouseClick);
            this.listView_MemoriaPrincipal.Resize += new System.EventHandler(this.ListView_Resize);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Dirección";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Decimal";
            this.columnHeader4.Width = 55;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ca2";
            this.columnHeader5.Width = 40;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Hex";
            this.columnHeader13.Width = 40;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Binario";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Instruccion";
            this.columnHeader15.Width = 78;
            // 
            // listView_Flags
            // 
            this.listView_Flags.AllowDrop = true;
            this.listView_Flags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Flags.BackColor = System.Drawing.SystemColors.Control;
            this.listView_Flags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Flags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.listView_Flags.FullRowSelect = true;
            this.listView_Flags.GridLines = true;
            this.listView_Flags.HideSelection = false;
            this.listView_Flags.Location = new System.Drawing.Point(5, 5);
            this.listView_Flags.Name = "listView_Flags";
            this.listView_Flags.OwnerDraw = true;
            this.listView_Flags.Size = new System.Drawing.Size(350, 112);
            this.listView_Flags.TabIndex = 8;
            this.listView_Flags.UseCompatibleStateImageBehavior = false;
            this.listView_Flags.View = System.Windows.Forms.View.Details;
            this.listView_Flags.VirtualListSize = 2;
            this.listView_Flags.VirtualMode = true;
            this.listView_Flags.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            this.listView_Flags.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
            this.listView_Flags.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_Flags_DrawSubItem);
            this.listView_Flags.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListView_Flags_RetrieveVirtualItem);
            this.listView_Flags.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.listView_Flags.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            this.listView_Flags.Resize += new System.EventHandler(this.ListView_Microinstrucciones_Resize);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Flags";
            this.columnHeader6.Width = 158;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Valor";
            this.columnHeader7.Width = 190;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restablecerDireccionesSeleccionadasToolStripMenuItem,
            this.modificarContenidoToolStripMenuItem,
            this.toolStripSeparator9,
            this.copiarToolStripMenuItem,
            this.copiarContenidoToolStripMenuItem,
            this.copiarInstrucciónToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 120);
            // 
            // restablecerDireccionesSeleccionadasToolStripMenuItem
            // 
            this.restablecerDireccionesSeleccionadasToolStripMenuItem.Name = "restablecerDireccionesSeleccionadasToolStripMenuItem";
            this.restablecerDireccionesSeleccionadasToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.restablecerDireccionesSeleccionadasToolStripMenuItem.Text = "Borrar seleccionadas";
            this.restablecerDireccionesSeleccionadasToolStripMenuItem.Click += new System.EventHandler(this.restablecerDireccionesSeleccionadasToolStripMenuItem_Click);
            // 
            // modificarContenidoToolStripMenuItem
            // 
            this.modificarContenidoToolStripMenuItem.Name = "modificarContenidoToolStripMenuItem";
            this.modificarContenidoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.modificarContenidoToolStripMenuItem.Text = "Modificar contenido";
            this.modificarContenidoToolStripMenuItem.Click += new System.EventHandler(this.ModificarContenidoToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(179, 6);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.copiarToolStripMenuItem.Text = "Copiar dirección";
            // 
            // copiarContenidoToolStripMenuItem
            // 
            this.copiarContenidoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decimalToolStripMenuItem,
            this.ca2ToolStripMenuItem,
            this.hexadecimalToolStripMenuItem,
            this.binarioToolStripMenuItem});
            this.copiarContenidoToolStripMenuItem.Name = "copiarContenidoToolStripMenuItem";
            this.copiarContenidoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.copiarContenidoToolStripMenuItem.Text = "Copiar contenido";
            // 
            // decimalToolStripMenuItem
            // 
            this.decimalToolStripMenuItem.Name = "decimalToolStripMenuItem";
            this.decimalToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.decimalToolStripMenuItem.Text = "Decimal";
            // 
            // ca2ToolStripMenuItem
            // 
            this.ca2ToolStripMenuItem.Name = "ca2ToolStripMenuItem";
            this.ca2ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ca2ToolStripMenuItem.Text = "Ca2";
            // 
            // hexadecimalToolStripMenuItem
            // 
            this.hexadecimalToolStripMenuItem.Name = "hexadecimalToolStripMenuItem";
            this.hexadecimalToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.hexadecimalToolStripMenuItem.Text = "Hexadecimal";
            // 
            // binarioToolStripMenuItem
            // 
            this.binarioToolStripMenuItem.Name = "binarioToolStripMenuItem";
            this.binarioToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.binarioToolStripMenuItem.Text = "Binario";
            // 
            // copiarInstrucciónToolStripMenuItem
            // 
            this.copiarInstrucciónToolStripMenuItem.Name = "copiarInstrucciónToolStripMenuItem";
            this.copiarInstrucciónToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.copiarInstrucciónToolStripMenuItem.Text = "Copiar instrucción";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarContenidoToolStripMenuItem1,
            this.toolStripSeparator10,
            this.copiarRegistroToolStripMenuItem,
            this.copiarContenidoToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(214, 76);
            // 
            // modificarContenidoToolStripMenuItem1
            // 
            this.modificarContenidoToolStripMenuItem1.Name = "modificarContenidoToolStripMenuItem1";
            this.modificarContenidoToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.modificarContenidoToolStripMenuItem1.Text = "Modificar contenido";
            this.modificarContenidoToolStripMenuItem1.Click += new System.EventHandler(this.ModificarContenidoToolStripMenuItem1_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(210, 6);
            // 
            // copiarRegistroToolStripMenuItem
            // 
            this.copiarRegistroToolStripMenuItem.Name = "copiarRegistroToolStripMenuItem";
            this.copiarRegistroToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.copiarRegistroToolStripMenuItem.Text = "Copiar nombre de registro";
            this.copiarRegistroToolStripMenuItem.Click += new System.EventHandler(this.CopiarRegistroToolStripMenuItem_Click);
            // 
            // copiarContenidoToolStripMenuItem1
            // 
            this.copiarContenidoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decimalToolStripMenuItem1,
            this.ca2ToolStripMenuItem1,
            this.hexadecimalToolStripMenuItem1,
            this.binarioToolStripMenuItem1});
            this.copiarContenidoToolStripMenuItem1.Name = "copiarContenidoToolStripMenuItem1";
            this.copiarContenidoToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.copiarContenidoToolStripMenuItem1.Text = "Copiar contenido";
            // 
            // decimalToolStripMenuItem1
            // 
            this.decimalToolStripMenuItem1.Name = "decimalToolStripMenuItem1";
            this.decimalToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.decimalToolStripMenuItem1.Text = "Decimal";
            this.decimalToolStripMenuItem1.Click += new System.EventHandler(this.DecimalToolStripMenuItem1_Click);
            // 
            // ca2ToolStripMenuItem1
            // 
            this.ca2ToolStripMenuItem1.Name = "ca2ToolStripMenuItem1";
            this.ca2ToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.ca2ToolStripMenuItem1.Text = "Ca2";
            this.ca2ToolStripMenuItem1.Click += new System.EventHandler(this.Ca2ToolStripMenuItem1_Click);
            // 
            // hexadecimalToolStripMenuItem1
            // 
            this.hexadecimalToolStripMenuItem1.Name = "hexadecimalToolStripMenuItem1";
            this.hexadecimalToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.hexadecimalToolStripMenuItem1.Text = "Hexadecimal";
            this.hexadecimalToolStripMenuItem1.Click += new System.EventHandler(this.HexadecimalToolStripMenuItem1_Click);
            // 
            // binarioToolStripMenuItem1
            // 
            this.binarioToolStripMenuItem1.Name = "binarioToolStripMenuItem1";
            this.binarioToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.binarioToolStripMenuItem1.Text = "Binario";
            this.binarioToolStripMenuItem1.Click += new System.EventHandler(this.BinarioToolStripMenuItem1_Click);
            // 
            // panelMejorado6
            // 
            this.panelMejorado6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMejorado6.ColorFondoControladoPorEstilo = true;
            this.panelMejorado6.CustomBorders = true;
            this.panelMejorado6.Location = new System.Drawing.Point(12, 578);
            this.panelMejorado6.Name = "panelMejorado6";
            this.panelMejorado6.Size = new System.Drawing.Size(1130, 148);
            this.panelMejorado6.TabIndex = 9;
            this.panelMejorado6.Text = "Microinstrucciones";
            // 
            // panelMejorado5
            // 
            this.panelMejorado5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMejorado5.ColorFondoControladoPorEstilo = true;
            this.panelMejorado5.CustomBorders = true;
            this.panelMejorado5.Location = new System.Drawing.Point(1148, 578);
            this.panelMejorado5.Name = "panelMejorado5";
            this.panelMejorado5.Size = new System.Drawing.Size(360, 148);
            this.panelMejorado5.TabIndex = 8;
            this.panelMejorado5.Text = "Flags";
            // 
            // panelMejorado4
            // 
            this.panelMejorado4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMejorado4.ColorFondoControladoPorEstilo = true;
            this.panelMejorado4.Controls.Add(this.panelIrA);
            this.panelMejorado4.CustomBorders = true;
            this.panelMejorado4.Location = new System.Drawing.Point(1148, 158);
            this.panelMejorado4.Name = "panelMejorado4";
            this.panelMejorado4.Size = new System.Drawing.Size(360, 414);
            this.panelMejorado4.TabIndex = 7;
            this.panelMejorado4.Text = "Memoria";
            // 
            // panelIrA
            // 
            this.panelIrA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIrA.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelIrA.Controls.Add(this.button1);
            this.panelIrA.Controls.Add(this.comboBox4);
            this.panelIrA.Controls.Add(this.comboBox3);
            this.panelIrA.Controls.Add(this.comboBox2);
            this.panelIrA.Controls.Add(this.comboBox1);
            this.panelIrA.Controls.Add(this.label1);
            this.panelIrA.Location = new System.Drawing.Point(0, 359);
            this.panelIrA.Name = "panelIrA";
            this.panelIrA.Size = new System.Drawing.Size(360, 29);
            this.panelIrA.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(243, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ir";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.IrADireccionMemoriaPrincipalButton_Click);
            // 
            // comboBox4
            // 
            this.comboBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox4.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.comboBox4.Location = new System.Drawing.Point(205, 4);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(32, 21);
            this.comboBox4.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox3.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.comboBox3.Location = new System.Drawing.Point(167, 4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(32, 21);
            this.comboBox3.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.comboBox2.Location = new System.Drawing.Point(129, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(32, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.comboBox1.Location = new System.Drawing.Point(91, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(32, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ir a dirección:";
            // 
            // panelMejorado3
            // 
            this.panelMejorado3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMejorado3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMejorado3.ColorFondoControladoPorEstilo = true;
            this.panelMejorado3.CustomBorders = true;
            this.panelMejorado3.Location = new System.Drawing.Point(1148, 6);
            this.panelMejorado3.Name = "panelMejorado3";
            this.panelMejorado3.Size = new System.Drawing.Size(360, 146);
            this.panelMejorado3.TabIndex = 6;
            this.panelMejorado3.Text = "Registros";
            // 
            // panelMejorado2
            // 
            this.panelMejorado2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMejorado2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelMejorado2.ColorFondoControladoPorEstilo = false;
            this.panelMejorado2.CustomBorders = true;
            this.panelMejorado2.Location = new System.Drawing.Point(287, 6);
            this.panelMejorado2.Name = "panelMejorado2";
            this.panelMejorado2.Size = new System.Drawing.Size(855, 566);
            this.panelMejorado2.TabIndex = 5;
            this.panelMejorado2.Text = "Procesador";
            // 
            // panelMejorado1
            // 
            this.panelMejorado1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMejorado1.ColorFondoControladoPorEstilo = true;
            this.panelMejorado1.CustomBorders = true;
            this.panelMejorado1.Location = new System.Drawing.Point(12, 6);
            this.panelMejorado1.Name = "panelMejorado1";
            this.panelMejorado1.Size = new System.Drawing.Size(269, 566);
            this.panelMejorado1.TabIndex = 4;
            this.panelMejorado1.Text = "Programa";
            // 
            // mapaProcesador
            // 
            this.mapaProcesador.AllowDrop = true;
            this.mapaProcesador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mapaProcesador.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mapaProcesador.BackgroundImage = global::PDMv4.Properties.Resources.PDM_Image_fix;
            this.mapaProcesador.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mapaProcesador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapaProcesador.Indice = -1;
            this.mapaProcesador.Location = new System.Drawing.Point(0, 0);
            this.mapaProcesador.MinimumSize = new System.Drawing.Size(168, 105);
            this.mapaProcesador.Name = "mapaProcesador";
            this.mapaProcesador.Size = new System.Drawing.Size(845, 530);
            this.mapaProcesador.TabIndex = 5;
            this.mapaProcesador.DragDrop += new System.Windows.Forms.DragEventHandler(this.Programa_DragDrop);
            this.mapaProcesador.DragEnter += new System.Windows.Forms.DragEventHandler(this.Programa_DragEnter);
            // 
            // SimPDM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1521, 809);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimPDM";
            this.Text = "SimPDM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimPDM_FormClosing);
            this.Load += new System.EventHandler(this.SimPDM_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panelMejorado4.ResumeLayout(false);
            this.panelIrA.ResumeLayout(false);
            this.panelIrA.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem microinstruccionSiguienteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem microinstruccionAnteriorMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem instruccionSiguienteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instruccionAnteriorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reiniciarMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ejecutarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contenidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem acercadeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton nuevoToolStripButton;
        private System.Windows.Forms.ToolStripButton abrirToolStripButton;
        private System.Windows.Forms.ToolStripButton editorToolStripButton;
        private System.Windows.Forms.ToolStripButton reiniciarProgramaToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton instruccionSiguienteToolStripButton;
        private System.Windows.Forms.ToolStripButton microinstruccionSiguienteToolStripButton;
        private System.Windows.Forms.ToolStripButton microinstruccionAnteriorToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton instruccionAnteriorToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel etiquetaRuta_BarraEstado;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel etiquetaEstado_BarraEstado;
        private System.Windows.Forms.ToolStripButton ejecutarHastaFinalStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private Controles.PanelMejorado panelMejorado1;
        private Controles.PanelMejorado panelMejorado2;
        private Controles.PanelMejorado panelMejorado3;
        private Controles.PanelMejorado panelMejorado4;
        private Controles.PanelMejorado panelMejorado5;
        private Controles.PanelMejorado panelMejorado6;
        private System.Windows.Forms.ListView listView_Programa;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listView_Microinstrucciones;
        private System.Windows.Forms.ColumnHeader Fase;
        private System.Windows.Forms.ColumnHeader LRI;
        private System.Windows.Forms.ColumnHeader LCP;
        private System.Windows.Forms.ColumnHeader TSR;
        private System.Windows.Forms.ColumnHeader TER;
        private System.Windows.Forms.ColumnHeader LH;
        private System.Windows.Forms.ColumnHeader LL;
        private System.Windows.Forms.ColumnHeader LF;
        private System.Windows.Forms.ColumnHeader TF;
        private System.Windows.Forms.ColumnHeader LAc;
        private System.Windows.Forms.ColumnHeader TAc;
        private System.Windows.Forms.ColumnHeader LE;
        private System.Windows.Forms.ColumnHeader MEM;
        private System.Windows.Forms.ColumnHeader TMEM;
        private System.Windows.Forms.ColumnHeader UAL2;
        private System.Windows.Forms.ColumnHeader UAL1;
        private System.Windows.Forms.ColumnHeader UAL0;
        private System.Windows.Forms.ColumnHeader TUAL;
        private System.Windows.Forms.ColumnHeader LUAL;
        private System.Windows.Forms.Panel panel1;
        private Controles.MapaProcesador mapaProcesador;
        private System.Windows.Forms.ListView listView_Registros;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ListView listView_MemoriaPrincipal;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ListView listView_Flags;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStripStatusLabel etiquetaNumeroLineas_BarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panelIrA;
        private System.Windows.Forms.Label label1;
        private PDMv4.Controles.FlatComboBox comboBox1;
        private PDMv4.Controles.FlatComboBox comboBox4;
        private PDMv4.Controles.FlatComboBox comboBox3;
        private PDMv4.Controles.FlatComboBox comboBox2;
        private PDMv4.Controles.CustomButton button1;
        private System.Windows.Forms.ToolStripButton ejecutarHastaElFinalPasoStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem ejecutarHastaElFinalPasoPorPasoMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modificarContenidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarContenidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ca2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexadecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem copiarInstrucciónToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem modificarContenidoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem copiarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarContenidoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem decimalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ca2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hexadecimalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem binarioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem guardarcomoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton guardarToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem restablecerDireccionesSeleccionadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
    }
}