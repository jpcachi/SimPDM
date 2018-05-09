using PDMv4.Argumentos;
using System;

namespace PDMv4.Instrucciones
{
    abstract class Instruccion0Arg : Instruccion
    {
        protected bool destinoSalto = false;
        public Instruccion0Arg(params Argumento[] argumentos)
        {
            if (argumentos.Length != 0)
                throw new ArgumentOutOfRangeException();
        }


        public override int NumArgumentos
        {
            get
            {
                return 0;
            }
        }
    }
}
