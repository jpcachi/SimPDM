namespace PDMv4
{
    class Etiqueta
    {
        private ushort direccionMemoria;
        private string etiqueta;

        public string ObtenerEtiqueta
        {
            get
            {
                return etiqueta;
            }
        }
        public ushort ObtenerDireccionMemoria
        {
            get
            {
                return direccionMemoria;
            }
        }

        public Etiqueta(ushort direccion, string etiqueta)
        {
            direccionMemoria = direccion;
            this.etiqueta = etiqueta;
        }
    }
}
