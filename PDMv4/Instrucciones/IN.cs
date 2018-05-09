using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class IN : Instruccion2Arg
    {
        public IN(params Argumento[] args) : base(args)
        {
            if (args[1] == null || args[0] == null) throw new ArgumentException();
            if (args[1].TipoArgumento() != Argumento.Tipo.Registro || args[0].TipoArgumento() != Argumento.Tipo.Memoria)
                throw new ArgumentException();

        }

        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice > 1 || indice < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return indice == 0 ? argumento1 : argumento2;
        }

        public override byte Codigo
        {
            get
            {
                return (byte)(240 + (argumento2 as ArgRegistro).NumeroRegistro);
            }
        }

        public override int NumMicroinstrucciones => 5;
        public override string ConvertirEnLinea()
        {
            return "IN " + (argumento1 as ArgMemoria).Etiqueta + ", " + Main.ObtenerNombreRegistro((argumento2 as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return -1;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return (argumento2 as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;
            return (argumento1 as ArgMemoria).DireccionMemoria;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return null;
        }
    }
}
