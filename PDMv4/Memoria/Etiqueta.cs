namespace PDMv4.Memoria
{
    class Etiqueta
    {
        public string ObtenerEtiqueta { get; }
        public ushort ObtenerDireccionMemoria { get; }

        public Etiqueta(ushort direccion, string etiqueta)
        {
            ObtenerDireccionMemoria = direccion;
            ObtenerEtiqueta = etiqueta;
        }
    }
}
