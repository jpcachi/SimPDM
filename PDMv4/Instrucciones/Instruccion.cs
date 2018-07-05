using PDMv4.Argumentos;
using System;

namespace PDMv4.Instrucciones
{
    abstract class Instruccion
    {
        public abstract byte Codigo { get; }
        public virtual int NumMicroinstrucciones { get => 4; }
        public abstract int NumArgumentos { get ; }
        public abstract Argumento ObtenerArgumento(int indice);
        public abstract string ConvertirEnLinea();
        public abstract int ObtenerNumRegistroLeido();
        public abstract int ObtenerNumRegistroEscrito();
        public abstract int ObtenerDirMemoria(out bool escritura);
        public abstract int[] ObtenerFlags(out bool escritura);

        public static Instruccion ConvertirEnInstruccion(string[] instruccionArgumentos)
        {
            if (instruccionArgumentos.Length < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            Instruccion _instruccion = null;
            string instruccion = instruccionArgumentos[0].ToUpperInvariant();
            string[] argumentos = new string[instruccionArgumentos.Length - 1];
            for (int i = 1; i < instruccionArgumentos.Length; i++)
            {
                argumentos[i - 1] = instruccionArgumentos[i];
            }

            switch (instruccion)
            {
                case "STM":
                    _instruccion = new STM(Argumento.ConvertirEnArgumento(argumentos, true, -1, 1));
                    break;
                case "LDM":
                    _instruccion = new LDM(Argumento.ConvertirEnArgumento(argumentos, true, -1, 0));
                    break;
                case "LDI":
                    _instruccion = new LDI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "BEQ":
                    _instruccion = new BEQ(Argumento.ConvertirEnArgumento(argumentos, true, -1, 0));
                    break;
                case "BC":
                    _instruccion = new BC(Argumento.ConvertirEnArgumento(argumentos, true, -1, 0));
                    break;
                case "JMP":
                    _instruccion = new JMP(Argumento.ConvertirEnArgumento(argumentos, true, -1, 0));
                    break;
                case "LD":
                    _instruccion = new LD(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "ST":
                    _instruccion = new ST(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "ADD":
                    _instruccion = new ADD(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "SUB":
                    _instruccion = new SUB(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "CMP":
                    _instruccion = new CMP(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "INC":
                    _instruccion = new INC(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "ADI":
                    _instruccion = new ADI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "SUI":
                    _instruccion = new SUI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "CMI":
                    _instruccion = new CMI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "ANA":
                    _instruccion = new ANA(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "ORA":
                    _instruccion = new ORA(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "XRA":
                    _instruccion = new XRA(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "CMA":
                    _instruccion = new CMA(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "ANI":
                    _instruccion = new ANI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "ORI":
                    _instruccion = new ORI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "XRI":
                    _instruccion = new XRI(Argumento.ConvertirEnArgumento(argumentos, false, 0));
                    break;
                case "LF":
                    _instruccion = new LF(Argumento.ConvertirEnArgumento(argumentos, false));
                    break;
                case "IN":
                    _instruccion = new IN(Argumento.ConvertirEnArgumento(argumentos, true, -1, 0));
                    break;
                case "OUT":
                    _instruccion = new OUT(Argumento.ConvertirEnArgumento(argumentos, true, -1, 1));
                    break;
                 
                default:
                    throw new ArgumentException();
            }

            return _instruccion;
        }
    }
}
