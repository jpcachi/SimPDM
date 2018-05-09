using PDMv4.Argumentos;
using PDMv4.Interfaces;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class BC : Instruccion1Arg, IInstruccionSalto
    {
        private byte codigo;
        private ushort IndiceMemoriaSalto;
        private int numMicroInstrucciones = 5;

        public BC(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Memoria)
                throw new ArgumentException();

            codigo = 216;
        }


        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return argumento;
        }

        public int ObtenerSalto()
        {
            int CP = Main.IndiceInstruccionActual + 1;
            IndiceMemoriaSalto = (argumento as ArgMemoria).DireccionMemoria;
            if (Main.FlagCarry)
            {
                for (int i = 0; i < Main.ListaInstrucciones.Count; i++)
                {
                    if (Main.ListaInstrucciones[i].direccion == IndiceMemoriaSalto)
                    {
                        CP = i;
                        break;
                    }
                }
            }
            return CP;
        }

        public override byte Codigo
        {
            get
            {
                return codigo;
            }
        }

        public override int NumMicroinstrucciones => numMicroInstrucciones;
        public override string ConvertirEnLinea()
        {
            return "BC " + (argumento as ArgMemoria).Etiqueta;
        }

        public override int ObtenerNumRegistroLeido()
        {
            return -1;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return -1;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;
            return (argumento as ArgMemoria).DireccionMemoria;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return new int[] { 1 };
        }
    }
}