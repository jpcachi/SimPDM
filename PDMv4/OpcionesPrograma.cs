namespace PDMv4
{
    static class OpcionesPrograma
    {
        private static bool entradaSalida;
        private static string ficheroEntrada;
        private static string ficheroSalida;

        private static ushort direccionMemoriaEntrada;
        private static ushort direccionMemoriaSalida;

        private static ushort direccionMemoriaComienzoPrograma = 0;

        public static string FicheroEntrada { get => ficheroEntrada; set => ficheroEntrada = value; }
        public static string FicheroSalida { get => ficheroSalida; set => ficheroSalida = value; }
        public static ushort DireccionMemoriaEntrada { get => direccionMemoriaEntrada; set => direccionMemoriaEntrada = value; }
        public static ushort DireccionMemoriaSalida { get => direccionMemoriaSalida; set => direccionMemoriaSalida = value; }
        public static ushort DireccionMemoriaComienzoPrograma { get => direccionMemoriaComienzoPrograma; set => direccionMemoriaComienzoPrograma = value; }
        public static bool EntradaSalida { get => entradaSalida; set => entradaSalida = value; }
    }
}
