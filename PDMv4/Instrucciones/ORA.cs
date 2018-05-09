using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class ORA : Instruccion1Arg
    {
        public ORA(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Registro)
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
                return (byte)(136 + (argumento as ArgRegistro).NumeroRegistro);
            }
        }

        public override int NumMicroinstrucciones => 4;

        public override string ConvertirEnLinea()
        {
            return "ORA " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
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
