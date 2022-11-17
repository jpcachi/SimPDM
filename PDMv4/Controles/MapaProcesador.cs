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
            visor = new Vistas.VerRegistro();
            visor.Show();
            visor.VisibleChanged += Visor_VisibleChanged;
            visor.Visible = false;
        }

        private void Visor_VisibleChanged(object sender, EventArgs e)
        {
            if (!visor.Visible)
                Indice = -1;
        }

        private void ActivarTER(bool activado)
        {
            etiquetaTER.Activado = activado;
        }

        private void ActivarTSR(bool activado)
        {
            etiquetaTSR.Activado = activado;
        }

        private void ActivarLAc(bool activado)
        {
            etiquetaLAc.Activado = activado;
        }

        private void ActivarTAc(bool activado)
        {
            etiquetaTAc.Activado = activado;
        }

        private void ActivarTUAL(bool activado)
        {
            etiquetaTUAL.Activado = activado;
        }

        private void ActivarTF(bool activado)
        {
            etiquetaTF.Activado = activado;
        }

        private void ActivarUAL(bool activado)
        {
            etiquetaUAL.Activado = activado;
        }

        private void ActivarLUAL(bool activado)
        {
            etiquetaLUAL.Activado = activado;
        }

        private void ActivarLF(bool activado)
        {
            etiquetaLF.Activado = activado;
            if (activado)
                ActivarFCFZ();
        }

        private void ActivarLCP(bool activado)
        {
            etiquetaLCP.Activado = activado;
        }

        private void ActivarLH(bool activado)
        {
            etiquetaLH.Activado = activado;
        }

        private void ActivarLL(bool activado)
        {
            etiquetaLL.Activado = activado;
        }

        private void ActivarMEM(bool activado)
        {
            etiquetaMEM.Activado = activado;
        }

        private void ActivarLRI(bool activado, bool evaluacion = false)
        {
            etiquetaLRI.Activado = activado;
            etiquetaCR.Activado = evaluacion;
            etiquetaCO.Activado = evaluacion;
        }

        private void ActivarLE(bool activado)
        {
            etiquetaLE.Activado = activado;
        }

        private void ActivarTMEM(bool activado)
        {
            etiquetaTMEM.Activado = activado;
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
            bool evaluacion = false;
            Properties.Resources.ResourceManager.ReleaseAllResources();
            switch (señalesTexto)
            {
                case "1100000000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S0;
                    break;
                case "0000000000000-1-1-100":
                    evaluacion = true;
                    pictureBox1.Image = Properties.Resources.S1;
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
                    pictureBox1.Image = Properties.Resources.S5_24;
                    break;
                case "0100010000001-1-1-100":
                    pictureBox1.Image = Properties.Resources.S6_25;
                    break;
                case "0001000000011-1-1-100":
                case "0001000000011-2-2-200":
                    pictureBox1.Image = Properties.Resources.S7;
                    break;
                case "0010000000110-1-1-100":
                case "0010000000110-2-2-200":
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

                case "0010100000000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S30_33;
                    break;
                case "0000010001000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S31;
                    break;
                case "0000000010011-1-1-100":
                    pictureBox1.Image = Properties.Resources.S32;
                    break;
                case "0010010000000-1-1-100":
                    pictureBox1.Image = Properties.Resources.S30_33;
                    break;
                case "0000000001110-1-1-100":
                    pictureBox1.Image = Properties.Resources.S34;
                    break;
            }

            Invoke(new Action(() => pictureBox1.Refresh()));

            ActivarCR01();

            ActivarLRI(ConvertirEnBool(valores[0]), evaluacion);
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
            Properties.Resources.ResourceManager.ReleaseAllResources();

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

            Invoke(new Action(() => pictureBox1.Refresh()));
        }

        private bool ConvertirEnBool(sbyte valor, bool ual = false)
        {
            return ual ? valor != - 1 : valor == 1;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ActualizarBotonMapaProcesadorActivo();
            visor.Owner = ParentForm;
            visor.StartPosition = FormStartPosition.Manual;
            visor.Location = new Point(MousePosition.X, MousePosition.Y);
            visor.Visible = true;
            ParentForm.Activate();
        }

        private void Button_RegistroB(object sender, EventArgs e)
        {
            Indice = 0;
            visor.ModificarVista("Registro B", Main.ObtenerRegistro(0).Contenido);
            Button_Click(sender, e);
        }

        private void Button_RegistroC(object sender, EventArgs e)
        {
            Indice = 1;
            visor.ModificarVista("Registro C", Main.ObtenerRegistro(1).Contenido);
            Button_Click(sender, e);
        }

        private void Button_RegistroD(object sender, EventArgs e)
        {
            Indice = 2;
            visor.ModificarVista("Registro D", Main.ObtenerRegistro(2).Contenido);
            Button_Click(sender, e);
        }

        private void Button_RegistroE(object sender, EventArgs e)
        {
            Indice = 3;
            visor.ModificarVista("Registro E", Main.ObtenerRegistro(3).Contenido);
            Button_Click(sender, e);
        }

        private void Button_Acumulador(object sender, EventArgs e)
        {
            Indice = 4;
            visor.ModificarVista("Acumulador", Main.ObtenerRegistro(4).Contenido);
            Button_Click(sender, e);
        }

        private void Button_RegistroInstrucciones(object sender, EventArgs e)
        {
            Indice = 5;
            visor.ModificarVista("Registro Instrucciones", Main.RegistroInstruccion.Contenido);
            Button_Click(sender, e);
        }

        private void Button_ContadorPrograma(object sender, EventArgs e)
        {
            Indice = 6;
            visor.ModificarVista("Registro ContadorPrograma", Main.ContadorPrograma, true);
            Button_Click(sender, e);
        }

        private void Button_RegistroDirecciones(object sender, EventArgs e)
        {
            Indice = 7;
            visor.ModificarVista("Registro Direcciones", (Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido, true);
            Button_Click(sender, e);
        }

        private void Button_RegistroFlags(object sender, EventArgs e)
        {
            Indice = 8;
            visor.ModificarVistaFlags("Registro Flags", Main.FlagCarry, Main.FlagZero);
            Button_Click(sender, e);
        }

        private void Button_RegistroUAL(object sender, EventArgs e)
        {
            Indice = 9;
            visor.ModificarVista("Registro UAL", Main.UnidadAritmeticoLogica.Resultado);
            Button_Click(sender, e);
        }

        private void Button_BusDatos(object sender, EventArgs e)
        {
            Indice = 10;
            visor.ModificarVista("Bus de Datos", Main.BusesDatosYDireccion.Contenido);
            Button_Click(sender, e);
        }

        private void Button_BusDirecciones(object sender, EventArgs e)
        {
            Indice = 11;
            visor.ModificarVista("Bus de Direcciones", Main.BusesDatosYDireccion.ContenidoDireccion, true);
            Button_Click(sender, e);
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

        private void MapaProcesador_Resize(object sender, EventArgs e)
        {
            //Estaría bien implementar que el mapa se pudiera redimensionar
        }
    }
}
