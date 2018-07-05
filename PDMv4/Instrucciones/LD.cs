using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;
using static PDMv4.Argumentos.Argumento;

namespace PDMv4.Instrucciones
{
    class LD : Instruccion1Arg
    {
        private readonly byte codigo;

        public LD(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Tipo.Registro)
                throw new ArgumentException();

            codigo = (args[0] as ArgRegistro).NumeroRegistro;
        }

        public override int NumMicroinstrucciones => 3;

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

        public override string ConvertirEnLinea()
        {
            return "LD " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return (argumento as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return 4; //4 es Acumulador
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
