using PDMv4.Utilidades;

namespace PDMv4.Procesador
{
    class UAL
    {
        private byte entradaA;
        private byte entradaB;
        private byte resultado;
        private bool _FZ;
        private bool _FC;

        public byte Resultado { get => resultado; set => resultado = value; }
        public bool FZ { get => _FZ; set => _FZ = value; }
        public bool FC { get => _FC; set => _FC = value; }

        public UAL()
        {
            Cargar();
        }

        public void Cargar()
        {
            entradaA = Main.Acumulador.Contenido;
            entradaB = Main.BusesDatosYDireccion.Contenido;
        }

        public void Suma()
        {
            //primero convertimos ambas entradas de Ca2 a decimal
            int _entradaA = ((int)entradaA).ToDecimal();
            int _entradaB = ((int)entradaB).ToDecimal();
            int resultado_int = (_entradaA + _entradaB);

            resultado = (byte)(resultado_int);
            _FZ = resultado == 0;
            _FC = (resultado_int > byte.MaxValue);
        }

        public void Resta()
        {
            //primero convertimos ambas entradas de Ca2 a decimal
            int _entradaA = ((int)entradaA).ToDecimal();
            int _entradaB = (-entradaB).ToDecimal();
            int resultado_int = (_entradaA + _entradaB);

            resultado = (byte)(resultado_int);
            _FZ = resultado == 0;
            _FC = (resultado_int > byte.MaxValue);

        }

        public void Comparar()
        {
            //primero convertimos ambas entradas de Ca2 a decimal
            int _entradaA = ((int)entradaA).ToDecimal();
            int _entradaB = (-entradaB).ToDecimal();
            int resultado_int = (_entradaA + _entradaB);

            _FZ = (byte)resultado_int == 0;
            _FC = (resultado_int > byte.MaxValue);

        }

        public void AND()
        {
            resultado = (byte)(entradaA & entradaB);
        }

        public void OR()
        {
            resultado = (byte)(entradaA | entradaB);
        }

        public void XOR()
        {
            resultado = (byte)(entradaA ^ entradaB);
        }

        public void NOT()
        {
            resultado = (byte)~entradaA;
        }

        public void Ac()
        {
            resultado = entradaA;
        }

        public void Incremento()
        {
            int _entradaA = ((int)entradaA).ToDecimal();
            int resultado_int = _entradaA + 1;
            resultado = (byte)(resultado_int);
            _FZ = resultado == 0;
            _FC = (resultado_int > byte.MaxValue);
        }

        public void Restablecer()
        {
            entradaA = 0;
            entradaB = 0;
            resultado = 0;
            _FZ = false;
            _FC = false;
        }
    }
}
