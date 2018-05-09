using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;
using PDMv4.Procesador;

namespace PDMv4.Controles
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class MapaProcesador : UserControl
    {
        /*PictureBox LAc;
        PictureBox LCP;
        PictureBox LF;
        PictureBox LH;
        PictureBox LL;
        PictureBox LR;
        PictureBox TAc;
        PictureBox TER;
        PictureBox TF;
        PictureBox TMEM;
        PictureBox TSR;
        PictureBox TUAL;
        PictureBox LUAL;
        PictureBox L;
        PictureBox E;
        PictureBox MEM;
        PictureBox NO_MEM;*/
        Vistas.VerRegistro visor;

        private int indice;

        public int Indice
        {
            get
            {
                return indice;
            }
            set
            {
                indice = value;
                ActualizarBotonMapaProcesadorActivo();
            }
        }

        public MapaProcesador()
        {
            InitializeComponent();
            /*LAc = new PictureBox();
            LCP = new PictureBox();
            LF = new PictureBox();
            LH = new PictureBox();
            LL = new PictureBox();
            LR = new PictureBox();
            LUAL = new PictureBox();
            TAc = new PictureBox();
            TER = new PictureBox();
            TF = new PictureBox();
            TMEM = new PictureBox();
            TSR = new PictureBox();
            TUAL = new PictureBox();
            L = new PictureBox();
            E = new PictureBox();
            MEM = new PictureBox();
            NO_MEM = new PictureBox();
            InicializarBuses();*/
            visor = new Vistas.VerRegistro();
            visor.Show();
            visor.VisibleChanged += Visor_VisibleChanged;
            visor.Visible = false;
            button1InitialPoint = button1.Location;
            button1InitialSize = button1.Size;
            //AñadirPictureBox(TMEM);
            //AñadirPictureBox(TER);
            //EliminarPictureBox(TMEM);


        }

        private void Visor_VisibleChanged(object sender, EventArgs e)
        {
            if (!visor.Visible)
                Indice = -1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //DibujarLinea(e);
        }

        private void ActivarTER(bool activado)
        {
            etiquetaTER.Activado = activado;
            //if (activado)
                //AñadirPictureBox(TER);
            //else EliminarPictureBox(TER);
        }

        private void ActivarTSR(bool activado)
        {
            etiquetaTSR.Activado = activado;
            //if (activado)
                //AñadirPictureBox(TSR);
            //else EliminarPictureBox(TSR);
        }

        private void ActivarLAc(bool activado)
        {
            etiquetaLAc.Activado = activado;
            //if (activado)
                //AñadirPictureBox(LAc);
            //else EliminarPictureBox(LAc);
        }

        private void ActivarTAc(bool activado)
        {
            etiquetaTAc.Activado = activado;
            //if (activado)
                //AñadirPictureBox(TAc);
            //else EliminarPictureBox(TAc);
        }

        private void ActivarTUAL(bool activado)
        {
            etiquetaTUAL.Activado = activado;
            //if (activado)
                //AñadirPictureBox(TUAL);
            //else EliminarPictureBox(TUAL);
        }

        private void ActivarTF(bool activado)
        {
            etiquetaTF.Activado = activado;
            //if (activado)
                //AñadirPictureBox(TF);
            //else EliminarPictureBox(TF);
        }

        private void ActivarUAL(bool activado)
        {
            etiquetaUAL.Activado = activado;
        }

        private void ActivarLUAL(bool activado)
        {
            etiquetaLUAL.Activado = activado;
            //if (activado)
                //AñadirPictureBox(LUAL);
            //else EliminarPictureBox(LUAL);
        }

        private void ActivarLF(bool activado)
        {
            etiquetaLF.Activado = activado;
            if (activado)
            {
                //AñadirPictureBox(LF);
                ActivarFCFZ();
            }
            //else EliminarPictureBox(LF);
        }

        private void ActivarLCP(bool activado)
        {
            etiquetaLCP.Activado = activado;
            //if (activado)
                //AñadirPictureBox(LCP);
            //else EliminarPictureBox(LCP);
        }

        private void ActivarLH(bool activado)
        {
            etiquetaLH.Activado = activado;
            //if (activado)
                //AñadirPictureBox(LH);
            //else EliminarPictureBox(LH);
        }

        private void ActivarLL(bool activado)
        {
            etiquetaLL.Activado = activado;
            //if (activado)
                //AñadirPictureBox(LL);
            //else EliminarPictureBox(LL);
        }

        private void ActivarMEM(bool activado)
        {
            etiquetaMEM.Activado = activado;
            /*if (activado)
            {
                EliminarPictureBox(NO_MEM);
                AñadirPictureBox(MEM);
            }
            else
            {
                EliminarPictureBox(MEM);
                AñadirPictureBox(NO_MEM);
            }*/
        }

        private void ActivarLRI(bool activado)
        {
            etiquetaLRI.Activado = activado;
            etiquetaCR.Activado = activado;
            etiquetaCO.Activado = activado;
            /*if (activado)
            {
                AñadirPictureBox(LR);
            }
            else EliminarPictureBox(LR);*/
        }

        private void ActivarLE(bool activado)
        {
            etiquetaLE.Activado = activado;
            /*if(activado)
            {
                EliminarPictureBox(L);
                AñadirPictureBox(E);
            }
            else
            {
                EliminarPictureBox(E);
                AñadirPictureBox(L);
            }*/
        }

        private void ActivarTMEM(bool activado)
        {
            etiquetaTMEM.Activado = activado;
            /*if (activado)
                AñadirPictureBox(TMEM);
            else EliminarPictureBox(TMEM);*/
        }

        private void InicializarBuses()
        {
            /*RenderizarPictureBox(LAc, Properties.Resources.Lac);
            RenderizarPictureBox(LCP, Properties.Resources.LCP);
            RenderizarPictureBox(LF, Properties.Resources.LF);
            RenderizarPictureBox(LH, Properties.Resources.LH);
            RenderizarPictureBox(LL, Properties.Resources.LL);
            RenderizarPictureBox(LR, Properties.Resources.LRI);
            RenderizarPictureBox(LUAL, Properties.Resources.LUAL);
            RenderizarPictureBox(TAc, Properties.Resources.TAc);
            RenderizarPictureBox(TER, Properties.Resources.TER);
            RenderizarPictureBox(TF, Properties.Resources.TF);
            RenderizarPictureBox(TMEM, Properties.Resources.TMEM);
            RenderizarPictureBox(TSR, Properties.Resources.TSR);
            RenderizarPictureBox(TUAL, Properties.Resources.TUAL);
            RenderizarPictureBox(L, Properties.Resources.L);
            RenderizarPictureBox(E, Properties.Resources.E);
            RenderizarPictureBox(MEM, Properties.Resources.MEM);
            RenderizarPictureBox(NO_MEM, Properties.Resources.NO_MEM);*/
        }

        private void RenderizarPictureBox(PictureBox picture, Bitmap bitmap)
        {
            picture.Image = bitmap;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.BackColor = Color.Transparent;
            picture.Dock = DockStyle.Fill;
            picture.Visible = true;
        }

        private void AñadirPictureBox(PictureBox picture)
        {
            Control c = pictureBox1;
            while(c.Controls.Count > 0)
            {
                if (c.Controls[0] == picture) return;
                c = c.Controls[0];
            }

            c.Controls.Add(picture);
        }

        private void EliminarPictureBox(PictureBox picture)
        {
            bool eliminado = false;

            Control c = pictureBox1;
            Control hijo = new Control();
            while (c.Controls.Count > 0 && !eliminado)
            {
                if (c.Controls.Contains(picture))
                {
                    bool tieneHijos = c.Controls[0].Controls.Count > 0;
                    if(tieneHijos)
                        hijo = c.Controls[0].Controls[0];
                    c.Controls.Remove(picture);
                    if (tieneHijos)
                        c.Controls.Add(hijo);
                    eliminado = true;
                }
                else c = c.Controls[0];
            }
        }

        private void ActivarCR01()
        {
            int registro = Main.NumRegistroSeleccionado;
            switch(registro)
            {
                case 1:
                    etiquetaCR1.Activado = false;
                    etiquetaCR0.Activado = true;
                    break;
                case 2:
                    etiquetaCR1.Activado = true;
                    etiquetaCR0.Activado = false;
                    break;
                case 3:
                    etiquetaCR1.Activado = true;
                    etiquetaCR0.Activado = true;
                    break;
                default:
                    etiquetaCR1.Activado = false;
                    etiquetaCR0.Activado = false;
                    break;
            }
        }

        private void ActivarFCFZ()
        {
            etiquetaFZ.Activado = Main.FlagZero;
            etiquetaFC.Activado = Main.FlagCarry;
        }

        public void ActualizarMapaPDM(sbyte[] valores)
        {
            string señalesTexto = string.Empty;
            foreach (sbyte señal in valores)
                señalesTexto += señal.ToString();
            switch (señalesTexto)
            {
                case "1100000000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S0;
                    break;
                case "0000000000000-1-1-100":
                    pictureBox1.Image = null;
                    break;
                case "0010000010000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S2;
                    break;
                case "0001000001000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S3;
                    break;
                case "0101000000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S4;
                    break;
                case "0100100000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S5_6_24_25;
                    break;
                case "0100010000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S5_6_24_25;
                    break;
                case "0001000000011-1-1-100":
                    pictureBox1.Image = Properties.Resources.S7;
                    break;
                case "0010000000110-1-1-100":
                    pictureBox1.Image = Properties.Resources.S8;
                    break;
                case "001000100000000001":
                    pictureBox1.Image = Properties.Resources.S9_11_12_13;
                    break;
                case "0000000010000-1-1-110":
                    pictureBox1.Image = Properties.Resources.S10;
                    break;
                case "001000100000000101":
                    pictureBox1.Image = Properties.Resources.S9_11_12_13;
                    break;
                case "001000100000000100":
                    pictureBox1.Image = Properties.Resources.S9_11_12_13;
                    break;
                case "000000100000011101":
                    pictureBox1.Image = Properties.Resources.S9_11_12_13;
                    break;
                case "010000100000100001":
                    pictureBox1.Image = Properties.Resources.S14_15_16;
                    break;
                case "010000100000100101":
                    pictureBox1.Image = Properties.Resources.S14_15_16;
                    break;
                case "010000100000100100":
                    pictureBox1.Image = Properties.Resources.S14_15_16;
                    break;
                case "001000000000001001":
                    pictureBox1.Image = Properties.Resources.S17_18_19_20;
                    break;
                case "001000000000001101":
                    pictureBox1.Image = Properties.Resources.S17_18_19_20;
                    break;
                case "001000000000010001":
                    pictureBox1.Image = Properties.Resources.S17_18_19_20;
                    break;
                case "000000000000010101":
                    pictureBox1.Image = Properties.Resources.S17_18_19_20;
                    break;
                case "010000000000101001":
                    pictureBox1.Image = Properties.Resources.S21_22_23;
                    break;
                case "010000000000101101":
                    pictureBox1.Image = Properties.Resources.S21_22_23;
                    break;
                case "010000000000110001":
                    pictureBox1.Image = Properties.Resources.S21_22_23;
                    break;
                case "1100000000011-1-1-100":
                    pictureBox1.Image = Properties.Resources.S26;
                    break;
                case "0000000110000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S27;
                    break;
            }

            //pictureBox1.Controls.Clear();

            //if(!valores.Contains<sbyte>(1))
            ActivarCR01();

            ActivarLRI(ConvertirEnBool(valores[0]));
            ActivarLCP(ConvertirEnBool(valores[1]));
            ActivarTSR(ConvertirEnBool(valores[2]));
            ActivarTER(ConvertirEnBool(valores[3]));
            ActivarLH(ConvertirEnBool(valores[4]));
            ActivarLL(ConvertirEnBool(valores[5]));
            ActivarLF(ConvertirEnBool(valores[6]));
            ActivarTF(ConvertirEnBool(valores[7]));
            ActivarLAc(ConvertirEnBool(valores[8]));
            ActivarTAc(ConvertirEnBool(valores[9]));
            ActivarLE(ConvertirEnBool(valores[10]));
            ActivarMEM(ConvertirEnBool(valores[11]));
            ActivarTMEM(ConvertirEnBool(valores[12]));
            ActivarUAL(ConvertirEnBool(valores[15], true) || ConvertirEnBool(valores[14], true) || ConvertirEnBool(valores[13], true));
            ActivarTUAL(ConvertirEnBool(valores[16]));
            ActivarLUAL(ConvertirEnBool(valores[17]));
        }

        public void RestablecerMapaPDM()
        {
            pictureBox1.Image = null;
            //pictureBox1.Controls.Clear();

            ActivarCR01();
            ActivarFCFZ();
            ActivarLRI(false);
            ActivarLCP(false);
            ActivarTSR(false);
            ActivarTER(false);
            ActivarLH(false);
            ActivarLL(false);
            ActivarLF(false);
            ActivarTF(false);
            ActivarLAc(false);
            ActivarTAc(false);
            ActivarLE(false);
            ActivarMEM(false);
            ActivarTMEM(false);
            ActivarUAL(false);
            ActivarTUAL(false);
            ActivarLUAL(false);

            visor.Visible = false;
            Indice = -1;
        }

        private bool ConvertirEnBool(sbyte valor, bool ual = false)
        {
            return ual ? valor != - 1 : valor == 1;
        }

        private void button_Click(object sender, EventArgs e)
        {
            ActualizarBotonMapaProcesadorActivo();
            visor.Owner = ParentForm;
            visor.StartPosition = FormStartPosition.Manual;
            visor.Location = new Point(MousePosition.X, MousePosition.Y);
            visor.Visible = true;
            ParentForm.Activate();
            //visor.Activate();
        }

        private void button_RegistroB(object sender, EventArgs e)
        {
            Indice = 0;
            visor.ModificarVista("Registro B", Main.ObtenerRegistro(0).Contenido);
            button_Click(sender, e);
        }

        private void button_RegistroC(object sender, EventArgs e)
        {
            Indice = 1;
            visor.ModificarVista("Registro C", Main.ObtenerRegistro(1).Contenido);
            button_Click(sender, e);
        }

        private void button_RegistroD(object sender, EventArgs e)
        {
            Indice = 2;
            visor.ModificarVista("Registro D", Main.ObtenerRegistro(2).Contenido);
            button_Click(sender, e);
        }

        private void button_RegistroE(object sender, EventArgs e)
        {
            Indice = 3;
            visor.ModificarVista("Registro E", Main.ObtenerRegistro(3).Contenido);
            button_Click(sender, e);
        }

        private void button_Acumulador(object sender, EventArgs e)
        {
            Indice = 4;
            visor.ModificarVista("Acumulador", Main.ObtenerRegistro(4).Contenido);
            button_Click(sender, e);
        }

        private void button_RegistroInstrucciones(object sender, EventArgs e)
        {
            Indice = 5;
            visor.ModificarVista("Registro Instrucciones", Main.RegistroInstruccion.Contenido);
            button_Click(sender, e);
        }

        private void button_ContadorPrograma(object sender, EventArgs e)
        {
            Indice = 6;
            visor.ModificarVista("Registro ContadorPrograma", Main.ContadorPrograma, true);
            button_Click(sender, e);
        }

        private void button_RegistroDirecciones(object sender, EventArgs e)
        {
            Indice = 7;
            visor.ModificarVista("Registro Direcciones", (Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido, true);
            button_Click(sender, e);
        }

        private void button_RegistroFlags(object sender, EventArgs e)
        {
            Indice = 8;
            visor.ModificarVistaFlags("Registro Flags", Main.FlagCarry, Main.FlagZero);
            button_Click(sender, e);
        }

        private void button_RegistroUAL(object sender, EventArgs e)
        {
            Indice = 9;
            visor.ModificarVista("Registro UAL", Main.UnidadAritmeticoLogica.Resultado);
            button_Click(sender, e);
        }

        private void button_BusDatos(object sender, EventArgs e)
        {
            Indice = 10;
            visor.ModificarVista("Bus de Datos", Main.BusesDatosYDireccion.Contenido);
            button_Click(sender, e);
        }

        private void button_BusDirecciones(object sender, EventArgs e)
        {
            Indice = 11;
            visor.ModificarVista("Bus de Direcciones", Main.BusesDatosYDireccion.ContenidoDireccion, true);
            button_Click(sender, e);
        }

        public void ActualizarVentanaVistaContenido()
        {
            switch (indice)
            {
                case 0:
                    visor.ModificarVista("Registro B", Main.ObtenerRegistro(0).Contenido);
                    break;
                case 1:
                    visor.ModificarVista("Registro C", Main.ObtenerRegistro(1).Contenido);
                    break;
                case 2:
                    visor.ModificarVista("Registro D", Main.ObtenerRegistro(2).Contenido);
                    break;
                case 3:
                    visor.ModificarVista("Registro E", Main.ObtenerRegistro(3).Contenido);
                    break;
                case 4:
                    visor.ModificarVista("Acumulador", Main.ObtenerRegistro(4).Contenido);
                    break;
                case 5:
                    visor.ModificarVista("Registro Instrucciones", Main.RegistroInstruccion.Contenido);
                    break;
                case 6:
                    visor.ModificarVista("Registro ContadorPrograma", Main.ContadorPrograma, true);
                    break;
                case 7:
                    visor.ModificarVista("Registro Direcciones", (Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido, true);
                    break;
                case 8:
                    visor.ModificarVistaFlags("Registro Flags", Main.FlagCarry, Main.FlagZero);
                    break;
                case 9:
                    visor.ModificarVista("Registro UAL", Main.UnidadAritmeticoLogica.Resultado);
                    break;
                case 10:
                    visor.ModificarVista("Bus de Datos", Main.BusesDatosYDireccion.Contenido);
                    break;
                case 11:
                    visor.ModificarVista("Bus de Direcciones", Main.BusesDatosYDireccion.ContenidoDireccion, true);
                    break;
            }
        }

        public void ActualizarBotonMapaProcesadorActivo()
        {
            QuitarBordesBotonesMapaProcesador();
            switch (indice)
            {
                case 0:
                    button1.FlatAppearance.BorderSize = 1;
                    break;
                case 1:
                    button2.FlatAppearance.BorderSize = 1;
                    break;
                case 2:
                    button3.FlatAppearance.BorderSize = 1;
                    break;
                case 3:
                    button4.FlatAppearance.BorderSize = 1;
                    break;
                case 4:
                    button5.FlatAppearance.BorderSize = 1;
                    break;
                case 5:
                    button6.FlatAppearance.BorderSize = 1;
                    break;
                case 6:
                    button7.FlatAppearance.BorderSize = 1;
                    break;
                case 7:
                    button8.FlatAppearance.BorderSize = 1;
                    break;
                case 8:
                    button9.FlatAppearance.BorderSize = 1;
                    break;
                case 9:
                    button10.FlatAppearance.BorderSize = 1;
                    break;
                case 10:
                    button11.FlatAppearance.BorderSize = 1;
                    break;
                case 11:
                    button12.FlatAppearance.BorderSize = 1;
                    break;
            }
        }

        public void QuitarBordesBotonesMapaProcesador()
        {
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.BorderSize = 0;
            button8.FlatAppearance.BorderSize = 0;
            button9.FlatAppearance.BorderSize = 0;
            button10.FlatAppearance.BorderSize = 0;
            button11.FlatAppearance.BorderSize = 0;
            button12.FlatAppearance.BorderSize = 0;
        }

        private Point button1InitialPoint;
        private Size button1InitialSize;
        private void MapaProcesador_Resize(object sender, EventArgs e)
        {/*
            float aspectoIdeal = 1.59659091f;
            float aspectoActual = (Width * 1.0f / Height);

            int alturaBase = 528;
            int widthBase = 843;

            int alturaIdeal = Height;
            int widthIdeal = Width;

            int offsetX = 0;
            int offsetY = 0;

            if(aspectoActual > aspectoIdeal)
            {
                widthIdeal = (int) Math.Round(aspectoIdeal * Height);
                offsetX = (int)((Width - widthIdeal) / 2.0f);
            }
            else if (aspectoActual < aspectoIdeal)
            {
                alturaIdeal = (int)Math.Round(Width/aspectoIdeal);
                offsetY = (int)((Height - alturaIdeal) / 2.0f);
            }

            button1.Location = new Point(button1InitialPoint.X + offsetX, button1InitialPoint.Y + offsetY);
            //button1.Size = new Size(button1InitialSize.Width + (Width - widthIdeal), button1InitialSize.Height + (Height - alturaIdeal));*/
        }
    }
}
