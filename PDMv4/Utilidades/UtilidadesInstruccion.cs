using PDMv4.Argumentos;
using PDMv4.Instrucciones;
using PDMv4.Procesador;
using System.Collections.Generic;

namespace PDMv4.Utilidades
{
    static class UtilidadesInstruccion
    {
        private readonly static List<string> instruccionesTexto = new List<string>
        {
            "LD",
            "ST",
            "LDM",
            "STM",
            "LDI",
            "ADD",
            "SUB",
            "CMP",
            "INC",
            "ADI",
            "SUI",
            "CMI",
            "ANA",
            "ORA",
            "XRA",
            "CMA",
            "ANI",
            "ORI",
            "XRI",
            "JMP",
            "BEQ",
            "BC",
            "LF",
            "IN",
            "OUT"
        };

        private readonly static List<string> instruccionesDescripcion = new List<string>
        {
            "Instrucción LOAD:\n\nFormato: LD <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Carga el contenido del registro <Letra_Registro> en el acumulador.\n\nEjemplo de uso: LD B (Carga el contenido del registro B en el acumulador).",
            "Instrucción STORE:\n\nFormato: ST <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Almacena el contenido actual del acumulador en el registro <Letra_Registro>.\n\nEjemplo de uso: ST B (Almacena en B el contenido del acumulador).",
            "Instrucción LOAD FROM MEMORY:\n\nFormato: LDM <Parametro1>, <Parametro2>\n\tParametro1: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t      <Etiqueta>\n\tParametro2: <Letra_Registro: (B, C, D, E)>\n\nFunción: Carga el contenido de <Dir_Mem> || <Etiqueta> en el registro <Letra_Registro>.\n\nEjemplos de uso:\n\tLDM 0010, C (Carga el contenido de la dirección de memoria 0010 en el registro C).\n\n\tLDM inicio, B (Carga el contenido de la dirección de memoria con etiqueta inicio en el registro B).",
            "Instrucción STORE IN MEMORY:\n\nFormato: STM <Parametro1>, <Parametro2>\n\tParametro1: <Letra_Registro: (B, C, D, E)>\n\tParametro2: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t      <Etiqueta>\n\nFunción: Guarda el contenido del registro <Letra_Registro> en la dirección de memoria <Dir_Mem> || <Etiqueta>.\n\nEjemplos de uso:\n\tSTM C, 0010 (Guarda el contenido del registro C en la dirección de memoria 0010).\n\n\tSTM B, inicio (Guarda el contenido del registro B en la dirección de memoria con etiqueta inicio).",
            "Instrucción LOAD INT:\n\nFormato: LDI <Parametro1>, <Parametro2>\n\tParametro1: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\tParametro2: <Letra_Registro: (B, C, D, E)>\n\nFunción: Carga el valor <Num_Entero> en el registro <Letra_Registro>.\n\nEjemplos de uso:\n\tLDI 10, C (Carga el valor 10 en el registro C).\n\n\tLDI 10h, B (Carga el valor 10 [hexadecimal] en el registro B).",
            "Instrucción ADDITION:\n\nFormato: ADD <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Suma el contenido del registro <Letra_Registro> al acumulador y actualiza el contenido de los flags\n(Ac <- Ac + <Letra_Registro>; FZ, FC).\n\nEjemplo de uso: ADD B (Suma el contenido del registro B al contenido del acumulador y actualiza los flags).",
            "Instrucción SUBSTRACTION:\n\nFormato: SUB <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Resta el contenido del registro <Letra_Registro> al acumulador y actualiza el contenido de los flags\n(Ac <- Ac - <Letra_Registro>; FZ, FC).\n\nEjemplo de uso: SUB D (Resta el contenido del registro D al contenido del acumulador y actualiza los flags).",
            "Instrucción COMPARE:\n\nFormato: CMP <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Realiza la resta del acumulador menos el contenido del registro <Letra_Registro>, sin almacenar el resultado y actualizando el contenido de los flags.\n\nSi Ac = <Letra_Registro>, FZ tendrá valor 1; por el contrario, si Ac > <Letra_Registro>, entonces FC valdrá 1.\n(Ac - <Letra_Registro>; FZ, FC).\n\nEjemplo de uso: CMP C (Compara el contenido del registro C con el contenido del acumulador y actualiza los flags).",
            "Instrucción INCREMENT:\n\nFormato: INC\n\nFunción: Incrementa el valor del acumulador en uno y actualiza el valor de los flags.\n(Ac <- Ac + 1; FZ, FC).\n\nEjemplo de uso: INC (No recibe parámetros).",
            "Instrucción ADDITION INT:\n\nFormato: ADI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Suma <Num_Entero> al contenido del acumulador y actualiza el valor de los flags.\n(Ac <- Ac + <Num_Entero>; FZ, FC).\n\nEjemplos de uso:\n\tADI 10 (Suma 10 al contenido del acumulador y actualiza los flags).\n\n\tADI 10h (Suma 10 [hexadecimal] al contenido del acumulador y actualiza los flags).",
            "Instrucción SUBSTRACTION INT:\n\nFormato: SUI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Resta <Num_Entero> al contenido del acumulador y actualiza el valor de los flags.\n(Ac <- Ac - <Num_Entero>; FZ, FC).\n\nEjemplos de uso:\n\tSUI 10 (Resta 10 al contenido del acumulador y actualiza los flags).\n\n\tSUI 10h (Resta 10 [hexadecimal] al contenido del acumulador y actualiza los flags).",
            "Instrucción COMPARE INT:\n\nFormato: CMI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Realiza la resta del contenido del acumulador menos <Num_Entero>, sin almacenar el resultado y actualizando el contenido de los flags.\n\nSi Ac = <Num_Entero>, FZ tendrá valor 1; por el contrario, si Ac > <Num_Entero>, entonces FC valdrá 1.\n(Ac - <Num_Entero>; FZ, FC).\n\nEjemplos de uso:\n\tCMI 10 (Compara el número entero 10 con el contenido del acumulador y actualiza los flags).\n\n\tCMI 10h (Compara el número entero 10 [hexadecimal] con el contenido del acumulador y actualiza los flags).",
            "Instrucción AND REG:\n\nFormato: AND <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Guarda en el acumulador el resultado de la operación lógica <Letra_Registro> AND acumulador.\n(Ac <- Ac AND <Letra_Registro>).\n\nEjemplo de uso: ANA B (Realiza la opearación AND entre el contenido del registro B y el contenido del acumulador, guardando el resultado en este último).",
            "Instrucción OR REG:\n\nFormato: ORA <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Guarda en el acumulador el resultado de la operación lógica <Letra_Registro> OR acumulador.\n(Ac <- Ac OR <Letra_Registro>).\n\nEjemplo de uso: ORA B (Realiza la opearación OR entre el contenido del registro B y el contenido del acumulador, guardando el resultado en este último).",
            "Instrucción XOR REG:\n\nFormato: XRA <Parametro>\n\tParametro: <Letra_Registro: (B, C, D, E)>\n\nFunción: Guarda en el acumulador el resultado de la operación lógica <Letra_Registro> XOR acumulador.\n(Ac <- Ac XOR <Letra_Registro>).\n\nEjemplo de uso: XRA B (Realiza la opearación XOR entre el contenido del registro B y el contenido del acumulador, guardando el resultado en este último).",
            "Instrucción COMPLEMENT Ac:\n\nFormato: CMA\n\nFunción: Invierte el valor de los bits del contenido del acumulador, convirtiendo los 1 en 0 y viceversa.\n(Ac <- NOT Ac).\n\nEjemplo de uso: CMA (No recibe parámetros).",
            "Instrucción AND INT:\n\nFormato: ANI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Realiza la operación lógica <Num_Entero> AND acumulador, almacenando el resultado en este último.\n(Ac <- Ac AND <Num_Entero>).\n\nEjemplos de uso:\n\tANI 10 (Realiza la operación lógica AND 10 (0A en hexadecimal) con el contenido del acumulador).\n\n\tANI 10h (Realiza la operación lógica AND 10 [hexadecimal] con el contenido del acumulador).",
            "Instrucción OR INT:\n\nFormato: ORI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Realiza la operación lógica <Num_Entero> OR acumulador, almacenando el resultado en este último.\n(Ac <- Ac OR <Num_Entero>).\n\nEjemplos de uso:\n\tOR 10 (Realiza la operación lógica OR 10 (0A en hexadecimal) con el contenido del acumulador).\n\n\tOR 10h (Realiza la operación lógica OR 10 [hexadecimal] con el contenido del acumulador).",
            "Instrucción XOR INT:\n\nFormato: XRI <Parametro>\n\tParametro: <Num_Entero: [0 ~ 255]    ||\n\t\t      [00h ~ FFh]>\n\nFunción: Realiza la operación lógica <Num_Entero> XOR acumulador, almacenando el resultado en este último.\n(Ac <- Ac XOR <Num_Entero>).\n\nEjemplos de uso:\n\tXOR 10 (Realiza la operación lógica XOR 10 (0A en hexadecimal) con el contenido del acumulador).\n\n\tXOR 10h (Realiza la operación lógica XOR 10 [hexadecimal] con el contenido del acumulador).",
            "Instrucción JUMP:\n\nFormato: JMP <Parametro>\n\tParametro: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t    <Etiqueta>\n\nFunción: Salto incondicional a la dirección de memoria principal <Dir_Mem>.\n\nEjemplos de uso:\n\tJMP 0010 (Salta a la dirección de memoria 0010).\n\n\tJMP inicio (Salta a la dirección de memoria con etiqueta inicio).",
            "Instrucción BRANCH IF ZERO:\n\nFormato: BEQ <Parametro>\n\tParametro: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t    <Etiqueta>\n\nFunción: Salto a la dirección de memoria principal <Dir_Mem> cuando se cumple la condición FZ = 1.\n\nEjemplos de uso:\n\tBEQ 0010 (Salta a la dirección de memoria 0010 si FZ = 1).\n\n\tBEQ inicio (Salta a la dirección de memoria con etiqueta inicio si FZ = 1).",
            "Instrucción BRANCH IF CARRY:\n\nFormato: BC <Parametro>\n\tParametro: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t    <Etiqueta>\n\nFunción: Salto a la dirección de memoria principal <Dir_Mem> cuando se cumple la condición FC = 1.\n\nEjemplos de uso:\n\tBC 0010 (Salta a la dirección de memoria 0010 si FC = 1).\n\n\tBC inicio (Salta a la dirección de memoria con etiqueta inicio si FC = 1).",
            "Instrucción LOAD FLAGS:\n\nFormato: LF\n\nFunción: Modifica el primer y último bit del acumulador con los valores de los flags FC y FZ respectivamente.\n\nEjemplo de uso: LF (Sin parámetros).\nSi el acumulador vale 0100 0010 en binario (66 en decimal)\n\tSi FZ = 1, entonces Ac = 0100 0011 (67).\n\n\tSi FC = 1, entonces Ac = 1100 0010 (194).",
            "Instrucción INPUT:\n\nFormato: IN <Parametro1>, <Parametro2>\n\tParametro1: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t      <Etiqueta>\n\tParametro2: <Letra_Registro: (B, C, D, E)>\n\nFunción: Carga el contenido del periférico de entrada conectado a <Dir_Mem> || <Etiqueta> en el registro <Letra_Registro>.\n\nEjemplos de uso:\n\tIN 0010, C (Carga el contenido corredpondiente del periférico conectado a la dirección de memoria 0010 en el registro C).\n\n\tIN inicio, B (Carga el contenido correpondiente del periférico conectado a la dirección de memoria con etiqueta inicio en el registro B).",
            "Instrucción OUTPUT:\n\nFormato: OUT <Parametro1>, <Parametro2>\n\tParametro1: <Letra_Registro: (B, C, D, E)>\n\tParametro2: <Dir_Mem: [0x0000 ~ 0xFFFF]>    ||\n\t\t      <Etiqueta>\n\nFunción: Guarda el contenido del registro <Letra_Registro> en el periférico de salida conectado a la dirección de memoria <Dir_Mem> || <Etiqueta>.\n\nEjemplos de uso:\n\tOUT C, 0010 (Guarda el contenido del registro C en el periférico de salida conectado a la dirección de memoria 0010).\n\n\tOUT B, inicio (Guarda el contenido del registro B en el periférico de salida conectado a la dirección de memoria con etiqueta inicio)."
        };

        public static List<string> InstruccionesTexto => instruccionesTexto; 

        public static List<string> DescripcionesInstruccion => instruccionesDescripcion;

        public static string[] ExtraerInstruccionArgumentos(string linea)
        {
            List<string> elementos = new List<string>();
            string[] argumentos = linea.Split(',');
            string[] etiquetaInstruccionArgumento1 = argumentos[0].TrimStart().TrimEnd().Split(' ');

            for (int i = 0; i < etiquetaInstruccionArgumento1.Length; i++)
            {
                if (etiquetaInstruccionArgumento1[i] != string.Empty)
                    elementos.Add(etiquetaInstruccionArgumento1[i].Trim());
            }

            for (int i = 1; i < argumentos.Length; i++)
            {
                if (argumentos[i] != string.Empty)
                    elementos.Add(argumentos[i].Trim());
            }

            return elementos.ToArray();
        }

        public static bool esInstruccion(string linea)
        {
            bool instruccion;
            switch (linea)
            {
                case "LD":
                case "ST":
                case "LDM":
                case "STM":
                case "LDI":
                case "ADD":
                case "SUB":
                case "CMP":
                case "INC":
                case "ADI":
                case "SUI":
                case "CMI":
                case "ANA":
                case "ORA":
                case "XRA":
                case "CMA":
                case "ANI":
                case "ORI":
                case "XRI":
                case "JMP":
                case "BEQ":
                case "BC":
                case "LF":
                case "IN":
                case "OUT":
                    instruccion = true;
                    break;
                default:
                    instruccion = false;
                    break;
            }

            return instruccion;
        }

        public static Instruccion DescodificarInstruccion(byte codigo, ushort pos)
        {
            Instruccion instruccion = null;
            ArgMemoria argumentoMemoria = Argumento.ConvertirEnArgumento((Main.ObtenerMemoria.ObtenerDireccion((ushort)(pos + 1)).Contenido * 256 + Main.ObtenerMemoria.ObtenerDireccion((ushort)(pos + 2)).Contenido).ToString("X4"), true) as ArgMemoria;
            ArgRegistro argumentoRegistro = Argumento.ConvertirEnArgumento(Main.ObtenerNombreRegistro(codigo % 4), false) as ArgRegistro;
            ArgLiteral argumentoLiteral = Argumento.ConvertirEnArgumento(Main.ObtenerMemoria.ObtenerDireccion((ushort)(pos + 1)).Contenido.ToString(), false) as ArgLiteral;

            switch (codigo / 8)
            {
                case 0:
                    instruccion = new LD(argumentoRegistro);
                    break;
                case 1:
                    instruccion = new ST(argumentoRegistro);
                    break;
                case 4:
                case 5:
                    instruccion = new LDM(argumentoMemoria, argumentoRegistro);
                    break;
                case 6:
                case 7:
                    instruccion = new STM(argumentoRegistro, argumentoMemoria);
                    break;
                case 2:
                case 3:
                    instruccion = new LDI(argumentoLiteral, argumentoRegistro);
                    break;
                case 8:
                    instruccion = new ADD(argumentoRegistro);
                    break;
                case 9:
                    instruccion = new SUB(argumentoRegistro);
                    break;
                case 10:
                    instruccion = new CMP(argumentoRegistro);
                    break;


                case 11:
                    instruccion = new INC();
                    break;
                case 12:
                    instruccion = new ADI(argumentoLiteral);
                    break;
                case 13:
                    instruccion = new SUI(argumentoLiteral);
                    break;
                case 14:
                case 15:
                    instruccion = new CMI(argumentoLiteral);
                    break;


                case 16:
                    instruccion = new ANA(argumentoRegistro);
                    break;
                case 17:
                    instruccion = new ORA(argumentoRegistro);
                    break;
                case 18:
                    instruccion = new XRA(argumentoRegistro);
                    break;


                case 19:
                    instruccion = new CMA();
                    break;
                case 20:
                    instruccion = new ANI(argumentoLiteral);
                    break;
                case 21:
                    instruccion = new ORI(argumentoLiteral);
                    break;
                case 22:
                case 23:
                    instruccion = new XRI(argumentoLiteral);
                    break;
                case 24:
                case 25:
                    instruccion = new JMP(argumentoMemoria);
                    break;
                case 26:
                    instruccion = new BEQ(argumentoMemoria);
                    break;
                case 27:
                    instruccion = new BC(argumentoMemoria);
                    break;
                case 28:
                case 29:
                    instruccion = new LF();
                    break;
                case 30:
                    instruccion = new IN(argumentoMemoria, argumentoRegistro);
                    break;
                case 31:
                    instruccion = new OUT(argumentoRegistro, argumentoMemoria);
                    break;
            }
            return instruccion;
        }
    }
}
