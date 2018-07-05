namespace PDMv4.Argumentos
{
    class ArgLiteral : Argumento
    {

        public byte Valor { get; }

        public ArgLiteral(byte num)
        {
            Valor = num;
        }

        public override Tipo TipoArgumento()
        {
            return Tipo.Literal;
        }
    }
}
