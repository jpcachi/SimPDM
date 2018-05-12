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

        public override int NumArgumentos => 1;
    }
}
