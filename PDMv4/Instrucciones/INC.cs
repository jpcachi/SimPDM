using PDMv4.Argumentos;

namespace PDMv4.Instrucciones
{
    class INC : Instruccion0Arg
    {
        private byte codigo;
        public INC(params Argumento[] args) : base(args)
        {
            codigo = 88;
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

        public override int NumMicroinstrucciones => 4;
        public override string ConvertirEnLinea()
        {
            return "INC";
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
