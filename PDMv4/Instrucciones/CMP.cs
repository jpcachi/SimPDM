﻿using PDMv4.Argumentos;
using PDMv4.Procesador;
using System;

namespace PDMv4.Instrucciones
{
    class CMP : Instruccion1Arg
    {
        private readonly byte codigo;

        public CMP(params Argumento[] args) : base(args)
        {
            if (args[0] == null) throw new ArgumentException();
            if (args[0].TipoArgumento() != Argumento.Tipo.Registro)
                throw new ArgumentException();

            codigo = (byte)(80 + (args[0] as ArgRegistro).NumeroRegistro);
        }

        public void ActualizarFlags(int result)
        {

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
                return codigo;
            }
        }

        public override int NumMicroinstrucciones => 3;
        public override string ConvertirEnLinea()
        {
            return "CMP " + Main.ObtenerNombreRegistro((argumento as ArgRegistro).NumeroRegistro);
        }

        public override int ObtenerNumRegistroLeido()
        {
            return 5 + (argumento as ArgRegistro).NumeroRegistro;
        }
        public override int ObtenerNumRegistroEscrito()
        {
            return -1;
        }
        public override int ObtenerDirMemoria(out bool escritura)
        {
            escritura = false;
            return -1;
        }
        public override int[] ObtenerFlags(out bool escritura)
        {
            escritura = true;
            return new int[2] { 0, 1 };
        }
    }
}