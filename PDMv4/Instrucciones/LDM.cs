using System;
using PDMv4.Argumentos;
using PDMv4.Procesador;

namespace PDMv4.Instrucciones
{
    class LDM : Instruccion2Arg 
    {
        public LDM(params Argumento[] args) : base(args)
        {
            if (args[1] == null || args[0] == null) throw new ArgumentException();
            if (args[1].TipoArgumento() != Argumento.Tipo.Registro || args[0].TipoArgumento() != Argumento.Tipo.Memoria)
                throw new ArgumentException();

        }

        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice > 1 || indice < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return indice == 0 ? argumento1 : argumento2;
        }

        public override byte Codigo
        {
            get
            {
                return (byte)(32 + (argumento2 as ArgRegistro).NumeroRegistro);
            }
        }

        public override int NumMicroinstrucciones => 5;
        public override string ConvertirEnLinea()
        {
            return "LDM " + (argumento1 as ArgMemoria).Etiqueta + ", " + Main.ObtenerNombreRegistro((argumento2 as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return -1;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return (argumento2 as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;

            ushort LH = Main.ObtenerMemoria.ObtenerDireccion((ushort)(DireccionMemoriaDondeEsEscrita + 1)).Contenido;
            ushort LL = Main.ObtenerMemoria.ObtenerDireccion((ushort)(DireccionMemoriaDondeEsEscrita + 2)).Contenido;

            return LH * 256 + LL;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return null;
        }
    }
}
