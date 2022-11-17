using PDMv4.Argumentos;
using PDMv4.Interfaces;
using PDMv4.Procesador;
using System;
using static PDMv4.Argumentos.Argumento;

namespace PDMv4.Instrucciones.Personalizadas
{
    /**
     * EXPERIMENTAL
     * SMR <Registro 1>, <Registro 2>: Instruccion personalizada. 
     * Almacena en una direccion de memoria 0xRRSS el contenido del acumulador.
     * donde RR es el byte del Registro 1 y SS el byte del registro 2
     * 
     * El editor integrado no admite soporte para esta instrucción.
     */
    class SMR : Instruccion2Arg, IModificaDireccionMemoria
    {
        public override byte Codigo
        {
            get
            {
                byte resul = (byte)(12 + (argumento1 as ArgRegistro).NumeroRegistro);
                if ((argumento2 as ArgRegistro).NumeroRegistro == 1)
                {
                    resul = (byte)(20 + (argumento1 as ArgRegistro).NumeroRegistro);
                }
                else if ((argumento2 as ArgRegistro).NumeroRegistro == 2)
                {
                    resul = (byte)(28 + (argumento1 as ArgRegistro).NumeroRegistro);
                }
                else if ((argumento2 as ArgRegistro).NumeroRegistro == 3)
                {
                    resul = (byte)(60 + (argumento1 as ArgRegistro).NumeroRegistro);
                }

                return resul;
            }
        }

        public override int NumMicroinstrucciones => 5;
        public SMR(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Tipo.Registro || args[1].TipoArgumento() != Tipo.Registro)
                throw new ArgumentException();
        }

        public override string ConvertirEnLinea()
        {
            return "STR " + Main.ObtenerNombreRegistro((argumento1 as ArgRegistro).NumeroRegistro) + ", " + Main.ObtenerNombreRegistro((argumento2 as ArgRegistro).NumeroRegistro);
        }

        public override Argumento ObtenerArgumento(int indice)
        {
            if (indice > 1 || indice < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return indice == 0 ? argumento1 : argumento2;
        }

        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = true;
            return Main.ObtenerRegistro((argumento1 as ArgRegistro).NumeroRegistro).Contenido * 256 + Main.ObtenerRegistro((argumento2 as ArgRegistro).NumeroRegistro).Contenido;
        }

        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return null;
        }

        public override int ObtenerNumRegistroEscrito()
        {
            return -1;
        }

        public override int ObtenerNumRegistroLeido()
        {
            return (argumento1 as ArgRegistro).NumeroRegistro;
        }

        public override int ObtenerNumSegundoRegistroLeido()
        {
            return (argumento2 as ArgRegistro).NumeroRegistro;
        }

        public int ObtenerDirMemoriaModificada()
        {
            return Main.ObtenerRegistro((argumento1 as ArgRegistro).NumeroRegistro).Contenido * 256 + Main.ObtenerRegistro((argumento2 as ArgRegistro).NumeroRegistro).Contenido;
        }
    }
}
