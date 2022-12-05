using PDMv4.Argumentos;
using PDMv4.Controles;
using PDMv4.Instrucciones;
using PDMv4.Interfaces;
using PDMv4.Memoria;
using PDMv4.Procesador;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                if(texto.StartsWith("@") && texto.Length > 1)
                    texto = texto.Substring(1);

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
                string instruccion1 = texto.Trim().Substring(0, 3);
                string instruccion2 = texto.Trim().Substring(0, 2);


                bool resul;
                switch (instruccion1.ToUpperInvariant())
                {
                    case "ORA":
                    case "ADD":
                    case "CMP":
                    case "ANA":
                    case "SUB":
                    case "XRA":

                    case "LMR":
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
                string instruccion = texto.Trim().Substring(0, 3).ToUpperInvariant();
                instruccion = instruccion.Trim();
                string[] parametros = texto.Trim().Substring(3).Trim().Split(',');

                if (!(parametros.Length == 2 && parametros[0].Length > 0 && parametros[1].Length > 0)) return false;

                bool resul;
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
                    case "SMR":
                        resul = parametros[0].Trim().Length == 1 && registro.Contains(parametros[0].Trim()) && parametros[1].Trim().Length == 1 && registro.Contains(parametros[1].Trim());
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


            public static string[] ComprobarYExtraer(string texto, out int numLinea, out string lineaError)
            {

                bool resul = true;
                texto = texto.Replace('\t', ' ');
                string textoLimpio = Regex.Replace(texto, @"\s*;[^\n]*\n", "\n").Trim().Replace("\r", null);
                numLinea = -1;
                lineaError = null;
                int cont = 1;
                string lineaActual = null;

                string[] lineas = textoLimpio.Trim().Split('\n');
                try
                {
                    AñadirEtiquetas(lineas);
                    foreach (string linea in lineas)
                    {
                        lineaActual = linea;
                        if (linea.Trim().Length > 0)
                            resul = ComprobarLinea(linea.Trim());

                        if (!resul)
                        {
                            if (!string.IsNullOrWhiteSpace(linea))
                            {
                                lineaError = linea;
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
                    lineaError = lineaActual;
                    resul = false;
                }
                return resul ? lineas : null;
            }

            public static bool Comprobar(string texto, out int numLinea, out string lineaError)
            {

                bool resul = true;
                texto = texto.Replace('\t', ' ');
                string textoLimpio = Regex.Replace(texto, @"\s*;[^\n]*\n", "\n").Trim().Replace("\r", null);
                numLinea = -1;
                lineaError = null;
                int cont = 1;
                string lineaActual = null;

                string[] lineas = textoLimpio.Trim().Split('\n');
                try
                {
                    AñadirEtiquetas(lineas);
                    foreach (string linea in lineas)
                    {
                        lineaActual = linea;
                        if (linea.Trim().Length > 0)
                            resul = ComprobarLinea(linea.Trim());
                        if (!resul)
                        {
                            if (!string.IsNullOrWhiteSpace(linea))
                            {
                                lineaError = linea;
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
                    lineaError = lineaActual;
                    resul = false;
                }
                return resul;
            }
        }

        private string ruta;
        private string programa;
        private List<string[]> lineas;
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

        public string ObtenerPrograma
        {
            get
            {
                return programa;
            }
        }

        public void Restablecer()
        {
            lineas.Clear();
            programa = null;
            ruta = null;
        }

        public Fichero()
        {
            lineas = new List<string[]>();
            ruta = null;
        }

        public void ResetearRuta()
        {
            ruta = null;
        }

        public void LeerLinea(string linea, ref ushort posicion)
        {
            string[] instruccionArgumentos = UtilidadesInstruccion.ExtraerInstruccionArgumentos(linea);

            Instruccion instruccion;
            if (UtilidadesInstruccion.esInstruccion(instruccionArgumentos[0]))
            {
                instruccion = Instruccion.ConvertirEnInstruccion(instruccionArgumentos);
                lineas.Add(new string[] { string.Empty, linea });
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
                    else if (instruccion.ObtenerArgumento(0).TipoArgumento() == Argumento.Tipo.Registro && instruccion.ObtenerArgumento(1).TipoArgumento() == Argumento.Tipo.Registro)
                    {
                        posicion++;
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
                    Main.AñadirInstruccionListaInstrucciones(UtilidadesInstruccion.DecodificarInstruccion(Main.ObtenerMemoria.ObtenerDireccion(pos).Contenido, pos), pos);

                }
            }
        }
        public bool GuardarPrograma()
        {
            bool resul;
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta))
                {
                    writer.Write(programa);
                }
                resul = true;
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return resul;
        }

        public bool GuardarProgramaComo(string ruta)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta))
                {
                    writer.Write(programa);
                }
                this.ruta = ruta;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool LeerPrograma(string programa, string ruta, bool avisarMemoriaPreviamenteEditada = true)
        {
            ushort pos = OpcionesPrograma.DireccionMemoriaComienzoPrograma;

            string[] lineasTexto = ComprobarProgramaCorrecto.ComprobarYExtraer(programa, out int numLinea, out string lineaError);
            if (lineasTexto == null)
            {
                MessageBox.Show("Error en la línea " + numLinea + ": El formato de la instrucción \"" + lineaError + "\" no es el correcto. Por favor, compruebe que la instrucción y/o la etiqueta estén correctamente escritas.\r\n\r\nSi necesita ayuda sobre la sintaxis de las instrucciones puede acceder al fichero de ayuda desde el menú principal o la barra de herramientas.", "No se ha podido cargar el programa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                
                bool restablecer = false;
                if (Main.EditadaMemoriaManualmente && avisarMemoriaPreviamenteEditada)
                {
                    if (MessageBox.Show("¿Desea restablecer el contenido modificado de la memoria principal antes de cargar el programa?", "Restablecer memoria principal y registros", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        restablecer = true;
                }
                lineas.Clear();
                Main.Restablecer(restablecerMemoria: restablecer);
                foreach (string linea in lineasTexto)
                {
                    if (linea != string.Empty)
                        LeerLinea(linea.Trim(), ref pos);
                }
                pos = OpcionesPrograma.DireccionMemoriaComienzoPrograma;
                EscribirLinea(ref pos);
                Main.EstablecerUltimaDireccionPrograma(pos);

                if (ruta != null)
                    this.ruta = ruta;

                this.programa = programa;
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool LeerProgramaSinFichero(string programa, bool avisarMemoriaPreviamenteEditada = true)
        {
            return LeerPrograma(programa, null, avisarMemoriaPreviamenteEditada);
        }

        public bool LeerPrograma(string ruta)
        {
            if (ruta != null)
            {
                if(ruta == OpcionesPrograma.FicheroEntrada || ruta == OpcionesPrograma.FicheroSalida)
                {
                    MessageBox.Show("No se puede leer el archivo seleccionado porque actualmente se encuentra configurado como entrada o salida para las instrucciones IN y OUT del simulador.\r\n\r\nPor favor, revise las opciones de entrada y salida de SimPDM y vuelva a intentarlo de nuevo.", "No se puede leer el archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                StreamReader sr = new StreamReader(ruta);
                string programa = sr.ReadToEnd();
                sr.Close();
                return LeerPrograma(programa, ruta, Main.EditadaMemoriaManualmente);

            }
            return false;
        }
    }
}