using PDMv4.Procesador;
using System;
using System.Globalization;

namespace PDMv4.Argumentos
{
    class ArgMemoria : Argumento
    {
        private string etiqueta;

        public string Etiqueta { get => etiqueta; }

        public ushort DireccionMemoria
        {
            get
            {
                foreach (Etiqueta mem in Main.ObtenerMemoria.Etiquetas)
                {
                    if (mem.ObtenerEtiqueta.ToUpperInvariant().Trim() == etiqueta.ToUpperInvariant().Trim())
                        return mem.ObtenerDireccionMemoria;
                }
                if ((ushort.TryParse(etiqueta, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out ushort n)) || (ushort.TryParse(etiqueta.Substring(0, etiqueta.Length - 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out n)))
                {
                    if (n <= Main.ObtenerMemoria.Tamaño)
                        return n;
                }
                throw new ArgumentException();
            }
        }

        public ArgMemoria(string arg)
        {
            etiqueta = arg;
        }
        public override Tipo TipoArgumento()
        {
            return Tipo.Memoria;
        }
    }
}
