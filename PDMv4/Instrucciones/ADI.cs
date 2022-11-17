using PDMv4.Argumentos;
using System;

namespace PDMv4.Instrucciones
{
    class ADI : Instruccion1Arg
    {
        public ADI(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Literal)
                throw new ArgumentException();

        }

        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return argumento;
        }

        public override byte Codigo
        {
            get
            {
                return 96;
            }
        }

        public override string ConvertirEnLinea()
        {
            return "ADI " + (argumento as ArgLiteral).Valor;
        }

        public override int ObtenerNumRegistroLeido()
        {
            return -1;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return 4;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;
            return -1;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = true;
            return new int[2] { 0, 1 };
        }
    }
}