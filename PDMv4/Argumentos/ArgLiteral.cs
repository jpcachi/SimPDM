namespace PDMv4.Argumentos
{
    class ArgLiteral : Argumento
    {
        private byte valor;

        public byte Valor { get => valor; }

        public ArgLiteral(byte num)
        {
            valor = num;
        }

        public override Tipo TipoArgumento()
        {
            return Tipo.Literal;
        }
    }
}
