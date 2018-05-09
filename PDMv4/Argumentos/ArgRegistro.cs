using System;

namespace PDMv4.Argumentos
{
    class ArgRegistro : Argumento
    {
        private byte numRegistro;

        public byte NumeroRegistro
        {
            get
            {
                return numRegistro;
            }
        }

        public ArgRegistro(byte numRegistro)
        {
            if (numRegistro > 3 || numRegistro < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.numRegistro = numRegistro;
        }

        public override Tipo TipoArgumento()
        {
            return Tipo.Registro;
        }
    }
}
