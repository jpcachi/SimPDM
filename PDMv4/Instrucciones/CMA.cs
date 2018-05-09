using PDMv4.Argumentos;

namespace PDMv4.Instrucciones
{
    class CMA: Instruccion0Arg
    {
        private byte codigo;

        public CMA(params Argumento[] args) : base(args)
        {
            codigo = 152;
        }

        public override Argumento ObtenerArgumento(int indice)
        {
            return null;
        }

        public override byte Codigo
        {
            get
            {
                return codigo;
            }
        }

        //public int NumMicroinstrucciones => 4;

        public override string ConvertirEnLinea()
        {
            return "CMA";
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
            return null;
        }
    }
}