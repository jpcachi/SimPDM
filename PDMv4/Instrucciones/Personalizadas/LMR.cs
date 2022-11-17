using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;
using static PDMv4.Argumentos.Argumento;

namespace PDMv4.Instrucciones.Personalizadas
{
    /**
     * EXPERIMENTAL
     * LMR <Registro>: Instruccion personalizada. 
     * Almacena en el acumulador el contenido de una direccion de memoria 0xRRAA;
     * donde RR es el byte del registro y AA el byte del acumulador
     * 
     * El editor integrado no admite soporte para esta instrucción.
     */
    class LMR : Instruccion1Arg
    {

        public LMR(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Tipo.Registro)
                throw new ArgumentException();
        }

        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return argumento;
        }


        public override byte Codigo
        {
            get
            {
                return (byte)(252 + (argumento as ArgRegistro).NumeroRegistro);
            }
        }

        public override int NumMicroinstrucciones => 5;
        public override string ConvertirEnLinea()
        {
            return "LMR " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return (argumento as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return 4;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;
            return -1;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return null;
        }
    }
}
