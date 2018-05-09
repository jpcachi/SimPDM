﻿using PDMv4.Argumentos;
using System;
using PDMv4.Procesador;

namespace PDMv4.Instrucciones
{
    class OUT : Instruccion2Arg
    {
        private byte codigo;
        public OUT(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Registro || args[1].TipoArgumento() != Argumento.Tipo.Memoria)
                throw new ArgumentException();

            codigo = (byte)(248 + (args[0] as ArgRegistro).NumeroRegistro);
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
                return codigo;
            }
        }

        public override int NumMicroinstrucciones => 5;
        public override string ConvertirEnLinea()
        {
            return "OUT " + Main.ObtenerNombreRegistro((argumento1 as ArgRegistro).NumeroRegistro) + ", " + (argumento2 as ArgMemoria).Etiqueta;
        }

        public override int ObtenerNumRegistroLeido()
        {
            return (argumento1 as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return -1;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = true;
            return (argumento2 as ArgMemoria).DireccionMemoria;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = false;
            return null;
        }
    }
}