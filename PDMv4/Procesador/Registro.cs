namespace PDMv4.Procesador
{
    class Registro : Interfaces.IAlmacenaDato
    {
        private byte contenido;
        public byte Contenido { get => contenido; set => contenido = value; }

        public Registro()
        {
            contenido = 0;
        }
    }
}
