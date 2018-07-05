using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class XRA : Instruccion1Arg
    {
        private readonly byte codigo;
        public XRA(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Registro)
                throw new ArgumentException();

            codigo = (byte)(144 + (args[0] as ArgRegistro).NumeroRegistro);
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
                return codigo;
            }
        }

        public override int NumMicroinstrucciones => 4;
        public override string ConvertirEnLinea()
        {
            return "XRA " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return (argumento as ArgRegistro).NumeroRegistro;
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
            escritura = false;
            return null;
        }
    }
}
