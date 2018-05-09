using PDMv4.Utilidades;
using System;
using System.Globalization;

namespace PDMv4.Argumentos
{
    abstract class Argumento
    {
        public enum Tipo
        {
            Registro, Literal, Memoria
        }

        public abstract Tipo TipoArgumento();

        public static Argumento ConvertirEnArgumento(string argumento, bool instruccionMemoria, bool forzarLiteral = false, bool forzarMemoria = false)
        {
            Argumento _argumento = null;
            switch (argumento.ToUpperInvariant())
            {
                case "B": if (forzarLiteral) _argumento = new ArgLiteral((byte)(0xB).ToDecimal()); else if (forzarMemoria) _argumento = new ArgMemoria(argumento); else _argumento = new ArgRegistro(0); break;
                case "C": if (forzarLiteral) _argumento = new ArgLiteral((byte)(0xC).ToDecimal()); else if (forzarMemoria) _argumento = new ArgMemoria(argumento); else _argumento = new ArgRegistro(1); break;
                case "D": if (forzarLiteral) _argumento = new ArgLiteral((byte)(0xD).ToDecimal()); else if (forzarMemoria) _argumento = new ArgMemoria(argumento); else _argumento = new ArgRegistro(2); break;
                case "E": if (forzarLiteral) _argumento = new ArgLiteral((byte)(0xE).ToDecimal()); else if (forzarMemoria) _argumento = new ArgMemoria(argumento); else _argumento = new ArgRegistro(3); break;
                default:
                    if (instruccionMemoria)
                    {
                        _argumento = new ArgMemoria(argumento);
                    }
                    else
                    {
                        bool hex = argumento.Substring(argumento.Length - 1).Trim().ToUpperInvariant() == "H";
                        int num;
                        if (hex)
                        {
                            if (int.TryParse(argumento.Substring(0, argumento.Length - 1), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num) && num <= 65535)
                            {
                                _argumento = new ArgLiteral((byte)(num.ToDecimal()));
                            }
                        }
                        else
                        {
                            if (int.TryParse(argumento.Trim(), out num) || (int.TryParse(argumento.Trim(), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num) && num <= 65535))
                            {
                                _argumento = new ArgLiteral((byte)(num.ToDecimal()));
                            }
                        }
                    }
                    break;
            }

            return _argumento;
        }

        public static Argumento[] ConvertirEnArgumento(string[] argumentos, bool instruccionMemoria,  int indiceLiteral = -1, int indiceMemoria = -1)
        {
            if (argumentos.Length > 2)
            {
                throw new ArgumentOutOfRangeException();
            }

            Argumento[] _argumentos = new Argumento[argumentos.Length];
            for (int i = 0; i < argumentos.Length; i++)
            {
                bool forzarLiteral = indiceLiteral == i;
                bool forzarMemoria = indiceMemoria == i;
                _argumentos[i] = ConvertirEnArgumento(argumentos[i], instruccionMemoria, forzarLiteral, forzarMemoria);
            }

            return _argumentos;
        }
    }
}
