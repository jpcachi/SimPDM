using PDMv4.Argumentos;
using PDMv4.Instrucciones;
using PDMv4.Interfaces;
using PDMv4.Procesador;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PDMv4.Utilidades
{
    class Fichero
    {
        public static class ComprobarProgramaCorrecto
        {
            private static string registro = "BCDEbcde";
            private static string digito = "0123456789";
            private static string letraHex = "ABCDEFabcdef";
            private static string letra = "qwertyuiopasdfghjklñzxcvbnm";
            private static string caractersinespacio = "0123456789qwertyuiopasdfghjklñzxcvbnm_-:";
            private static List<string> etiquetas = new List<string>();

            private static bool DireccionMemoria(string texto)
            {
                if (texto.Length > 5)
                    return false;
                else
                {
                    int len = texto.Length == 5 ? 4 : texto.Length;
                    foreach (char c in texto.Substring(0, len))
                    {
                        if (!letraHex.Contains(c) && !digito.Contains(c))
                            return false;
                    }
                    if (texto.Length == 5 && texto[4] != 'h' && texto[4] != 'H')
                        return false;
                }
                return true;
            }

            private static bool Etiqueta(string texto)
            {
                if (texto.Length == 0)
                    return false;
                else if (!letra.Contains(texto[0]))
                    return false;

                foreach (char c in texto.Substring(1))
                {
                    if (!caractersinespacio.Contains(c))
                    {
                        return false;
                    }
                }
                return true;
            }


            private static bool Instruccion0(string texto)
            {
                bool resul = texto.Trim().ToUpperInvariant() == "CMA" || texto.Trim().ToUpperInvariant() == "LF" || texto.Trim().ToUpperInvariant() == "INC";
                return resul;
            }

            private static bool Instruccion1(string texto)
            {
                bool resul = true;
                string instruccion1 = texto.Trim().Substring(0, 3);
                string instruccion2 = texto.Trim().Substring(0, 2);


                switch (instruccion1.ToUpperInvariant())
                {
                    case "ORA":
                    case "ADD":
                    case "CMP":
                    case "ANA":
                    case "SUB":
                    case "XRA":
                        resul = texto.Trim().Substring(3).Trim().Length == 1 && registro.Contains(texto.Trim().Substring(3).Trim());
                        break;
                    case "CMI":
                    case "ORI":
                    case "ADI":
                    case "ANI":
                    case "SUI":
                    case "XRI":
                        int aux;
                        string numHexParam = texto.Trim().Substring(3).Trim();
                        numHexParam = (numHexParam.Substring(numHexParam.Length - 1).ToUpperInvariant() == "H") ? numHexParam.Substring(0, numHexParam.Length - 1) : numHexParam;
                        resul = int.TryParse(numHexParam, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out aux) && aux <= 65535;
                        break;
                    case "JMP":
                    case "BEQ":
                        resul = DireccionMemoria(texto.Trim().Substring(3).Trim()) || (Etiqueta(texto.Trim().Substring(3).Trim()) && etiquetas.Contains(texto.Trim().Substring(3).Trim()));
                        break;
                    default:
                        switch (instruccion2.ToUpperInvariant())
                        {
                            case "ST":
                            case "LD":
                                resul = texto.Trim().Substring(2).Trim().Length == 1 && registro.Contains(texto.Trim().Substring(2).Trim());
                                break;
                            case "BC":
                                resul = DireccionMemoria(texto.Trim().Substring(2).Trim()) || (Etiqueta(texto.Trim().Substring(2).Trim()) && etiquetas.Contains(texto.Trim().Substring(2).Trim()));
                                break;
                            default:
                                resul = false;
                                break;
                        }
                        break;
                }
                return resul;
            }

            private static bool Instruccion2(string texto)
            {
                bool resul = true;
                string instruccion = texto.Trim().Substring(0, 3).ToUpperInvariant();
                instruccion = instruccion.Trim();
                string[] parametros = texto.Trim().Substring(3).Trim().Split(',');

                if (!(parametros.Length == 2 && parametros[0].Length > 0 && parametros[1].Length > 0)) return false;

                switch (instruccion)
                {
                    case "STM":
                    case "OUT":
                        resul = parametros[0].Trim().Length == 1 && registro.Contains(parametros[0].Trim()) && (DireccionMemoria(parametros[1].Trim()) || (Etiqueta(parametros[1].Trim()) && etiquetas.Contains(parametros[1].Trim())));
                        break;
                    case "LDI":
                        int aux;
                        string numHexParam = parametros[0].Trim();
                        numHexParam = (numHexParam.Substring(numHexParam.Length - 1).ToUpperInvariant() == "H") ? numHexParam.Substring(0, numHexParam.Length - 1) : numHexParam;
                        resul = int.TryParse(numHexParam, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out aux) && aux <= 65535 && parametros[1].Trim().Length == 1 && registro.Contains(parametros[1].Trim());
                        break;
                    case "LDM":
                    case "IN":
                        resul = parametros[1].Trim().Length == 1 && registro.Contains(parametros[1].Trim()) && (DireccionMemoria(parametros[0].Trim()) || (Etiqueta(parametros[0].Trim()) && etiquetas.Contains(parametros[0].Trim())));
                        break;
                    default:
                        resul = false;
                        break;
                }
                return resul;
            }

            private static void AñadirEtiqueta(string texto)
            {
                if (texto[texto.Length - 1] == '\r')
                    texto.Substring(0, texto.Length - 1);

                while (texto.Contains("  "))
                    texto = texto.Replace("  ", " ");

                string[] linea = texto.Trim().Split(' ');
                if (!UtilidadesInstruccion.esInstruccion(linea[0]) && Etiqueta(linea[0]))
                    etiquetas.Add(linea[0]);
            }

            private static void AñadirEtiquetas(string[] lineas)
            {
                etiquetas.Clear();
                foreach (string linea in lineas)
                    if (linea.Trim().Length > 0)
                        AñadirEtiqueta(linea);
            }

            private static bool ComprobarLinea(string texto)
            {
                if (texto[texto.Length - 1] == '\r')
                    texto.Substring(0, texto.Length - 1);

                while (texto.Contains("  "))
                    texto = texto.Replace("  ", " ");

                string[] linea = texto.Trim().Split(' ');
                string instruccion = string.Empty;
                if (!UtilidadesInstruccion.esInstruccion(linea[0]))
                {
                    if (Etiqueta(linea[0]))
                    {
                        for (int i = 1; i < linea.Length; i++)
                            instruccion += linea[i];
                    }
                    else return false;
                }
                else instruccion = texto;
                return Instruccion0(instruccion) || Instruccion2(instruccion) || Instruccion1(instruccion);
            }


            public static string[] Comprobar(string texto)
            {

                bool resul = true;
                texto = texto.Replace('\t', ' ');
                string textoLimpio = texto.Trim().Replace("\r", null);


                string[] lineas = textoLimpio.Trim().Split('\n');
                try
                {
                    AñadirEtiquetas(lineas);
                    foreach (string linea in lineas)
                    {
                        if (linea.Trim().Length > 0)
                            resul = ComprobarLinea(linea.Trim());
                        if (!resul) break;
                    }
                }
                catch
                {
                    resul = false;
                }
                return resul ? lineas : null;
            }

            public static bool Comprobar(string texto, out int numLinea)
            {

                bool resul = true;
                texto = texto.Replace('\t', ' ');
                string textoLimpio = texto.Trim().Replace("\r", null);
                numLinea = -1;
                int cont = 1;

                string[] lineas = textoLimpio.Trim().Split('\n');
                try
                {
                    AñadirEtiquetas(lineas);
                    foreach (string linea in lineas)
                    {
                        if (linea.Trim().Length > 0)
                            resul = ComprobarLinea(linea.Trim());
                        if (!resul)
                        {
                            if (!string.IsNullOrWhiteSpace(linea))
                            {
                                numLinea = cont;
                                cont = 1;
                            }
                            break;
                        }
                        cont++;
                    }
                }
                catch
                {
                    numLinea = cont;
                    cont = 1;
                    resul = false;
                }
                return resul;
            }
        }

        private string ruta;
        private List<string[]> lineas;
        //private BindingList<EtiquetaInstruccion> lineas2;
        public string Ruta
        {
            get
            {
                return ruta;
            }
        }

        public List<string[]> ObtenerLineasPrograma
        {
            get
            {
                return lineas;
            }
        }

        //public BindingList<EtiquetaInstruccion> ObtenerBindingSourceLineasPrograma{ get => lineas2; }

        public Fichero()
        {
            lineas = new List<string[]>();
            //lineas2 = new BindingList<EtiquetaInstruccion>();
            ruta = null;
        }

        public void ResetearRuta()
        {
            ruta = null;
        }

        public void LeerLinea(string linea, ref ushort posicion)
        {
            Instruccion instruccion = null;
            string[] instruccionArgumentos = UtilidadesInstruccion.ExtraerInstruccionArgumentos(linea);

            if (UtilidadesInstruccion.esInstruccion(instruccionArgumentos[0]))
            {
                instruccion = Instruccion.ConvertirEnInstruccion(instruccionArgumentos);
                lineas.Add(new string[] { string.Empty, linea });
                //--
                //lineas2.Add(new EtiquetaInstruccion(string.Empty, linea));
            }
            else
            {
                Main.ObtenerMemoria.Etiquetas.Add(new Etiqueta(posicion, instruccionArgumentos[0]));
                if (instruccionArgumentos.Length <= 0)
                    throw new ArgumentException();

                string[] _instruccionArgumentos = new string[instruccionArgumentos.Length - 1];
                for (int i = 0; i < _instruccionArgumentos.Length; i++)
                {
                    _instruccionArgumentos[i] = instruccionArgumentos[i + 1];
                }
                instruccion = Instruccion.ConvertirEnInstruccion(_instruccionArgumentos);
                lineas.Add(new string[] { instruccionArgumentos[0], linea.Substring(instruccionArgumentos[0].Length).TrimStart() });
                //lineas2.Add(new EtiquetaInstruccion(instruccionArgumentos[0], linea.Substring(instruccionArgumentos[0].Length).TrimStart()));
            }

            switch (instruccion.NumArgumentos)
            {
                case 0:
                    posicion++;
                    break;
                case 1:
                    if (instruccion is IInstruccionSalto)
                    {
                        posicion += 3;
                    }
                    else
                    {
                        if (instruccion.ObtenerArgumento(0).TipoArgumento() == Argumento.Tipo.Literal)
                        {
                            posicion += 2;
                        }
                        else
                        {
                            posicion++;
                        }
                    }
                    break;
                case 2:
                    if (instruccion.ObtenerArgumento(0).TipoArgumento() == Argumento.Tipo.Literal)
                    {
                        posicion += 2;
                    }
                    else
                    {
                        posicion += 3;
                    }
                    break;
            }
        }

        public void EscribirLinea(ref ushort posicion)
        {
            if (lineas.Count > 0)
            {
                foreach (string[] linea in lineas)
                {
                    //forma fácil: escribir en memoria y en registro de instrucciones al mismo tiempo
                    //Main.AñadirInstruccionRegistroInstrucciones(FInstruccion.ConvertirEnInstruccion(UtilidadesInstruccion.ExtraerInstruccionArgumentos(linea[1])), posicion);

                    ushort pos = posicion;
                    Main.ObtenerMemoria.EscribirInstruccionMemoria(Instruccion.ConvertirEnInstruccion(UtilidadesInstruccion.ExtraerInstruccionArgumentos(linea[1])), ref posicion);
                    Main.AñadirInstruccionListaInstrucciones(UtilidadesInstruccion.DescodificarInstruccion(Main.ObtenerMemoria.ObtenerDireccion(pos).Contenido, pos), pos);

                }
            }
        }

        public bool LeerPrograma(string ruta)
        {
            ushort pos = OpcionesPrograma.DireccionMemoriaComienzoPrograma;
            if (ruta != null)
            {
                if(ruta == OpcionesPrograma.FicheroEntrada || ruta == OpcionesPrograma.FicheroSalida)
                {
                    MessageBox.Show("El archivo no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                StreamReader sr = new StreamReader(ruta);
                string programa = sr.ReadToEnd();
                sr.Close();
                string[] lineasTexto = ComprobarProgramaCorrecto.Comprobar(programa);
                if (lineasTexto == null)
                {
                    MessageBox.Show("El archivo no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                try
                {
                    lineas.Clear();
                    Main.Restablecer();

                    foreach (string linea in lineasTexto)
                    {
                        if (linea != string.Empty)
                            LeerLinea(linea.Trim(), ref pos);
                    }
                    pos = OpcionesPrograma.DireccionMemoriaComienzoPrograma;
                    EscribirLinea(ref pos);
                    this.ruta = ruta;
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }
            return false;
        }
    }
}
