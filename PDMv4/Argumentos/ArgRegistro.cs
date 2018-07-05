using System;

namespace PDMv4.Argumentos
{
    class ArgRegistro : Argumento
    {

        public byte NumeroRegistro { get; }

        public ArgRegistro(byte numRegistro)
        {
            if (numRegistro > 3 || numRegistro < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.NumeroRegistro = numRegistro;
        }

        public override Tipo TipoArgumento()
        {
            return Tipo.Registro;
        }
    }
}
