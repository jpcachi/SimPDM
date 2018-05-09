using PDMv4.Argumentos;
using System;

namespace PDMv4.Instrucciones
{
    abstract class Instruccion2Arg : Instruccion
    {
        protected Argumento argumento1;
        protected Argumento argumento2;
        public Instruccion2Arg(params Argumento[] argumentos)
        {
            if (argumentos.Length != 2)
                throw new ArgumentOutOfRangeException();

            argumento1 = argumentos[0];
            argumento2 = argumentos[1];
        }

        public override int NumArgumentos
        {
            get
            {
                return 2;
            }
        }
    }
}
