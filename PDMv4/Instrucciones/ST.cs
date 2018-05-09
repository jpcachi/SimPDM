using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class ST : Instruccion1Arg
    {
        public ST(params Argumento[] args) : base(args)
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
                return (byte)(8 + (argumento as ArgRegistro).NumeroRegistro);
            }
        }

        public override int NumMicroinstrucciones => 3;

        public override string ConvertirEnLinea()
        {
            return "ST " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return 4; //4 es Acumulador
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return (argumento as ArgRegistro).NumeroRegistro;
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