namespace PDMv4.Procesador
{
    class Buses : Interfaces.IAlmacenaDato
    {
        private byte contenido;
        private ushort direccion;
        public byte Contenido { get => contenido; set => contenido = value; }
        public ushort ContenidoDireccion { get => direccion; set => direccion = value; }
        public Buses()
        {
            contenido = 0;
            direccion = 0;
        }
    }
}
