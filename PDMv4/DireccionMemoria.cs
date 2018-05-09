using PDMv4.Interfaces;

namespace PDMv4
{
    class DireccionMemoria : IAlmacenaDato
    {
        private byte contenido;

        public byte Contenido { get => contenido; set => contenido = value; }

        public DireccionMemoria()
        {
            contenido = 0;
        }
    }
}
