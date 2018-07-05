using PDMv4.Argumentos;

namespace PDMv4.Instrucciones
{
    class LF : Instruccion0Arg
    {
        public LF(params Argumento[] args) : base(args) { }

        public override Argumento ObtenerArgumento(int indice)
        {
            return null;
        }

        public override byte Codigo
        {
            get
            {
                return 224;
            }
        }

        public override int NumMicroinstrucciones => 3;

        public override string ConvertirEnLinea()
        {
            return "LF";
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
            escritura = false;
            return new int[] { 0, 1 };
        }
    }
}
