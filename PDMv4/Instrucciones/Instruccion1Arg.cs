using PDMv4.Argumentos;
using System;

namespace PDMv4.Instrucciones
{
    abstract class Instruccion1Arg : Instruccion
    {
        protected Argumento argumento;
        public Instruccion1Arg(params Argumento[] argumentos)
        {
            if (argumentos.Length != 1)
                throw new ArgumentOutOfRangeException();

            argumento = argumentos[0];
        }

        public override int NumArgumentos
        {
            get
            {
                return 1;
            }
        }


        /*public abstract void Ejecutar();
        public abstract Argumento ObtenerArgumento(int indice);
        public abstract string ConvertirEnLinea();
        public abstract byte Codigo { get; }

        public abstract int ObtenerNumRegistroLeido();
        public abstract int ObtenerNumRegistroEscrito();
        public abstract int ObtenerDirMemoria(out bool escritura);
        public abstract int[] ObtenerFlags(out bool escritura);
        public abstract bool Revertir();*/
    }
}
