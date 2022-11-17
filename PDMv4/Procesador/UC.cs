using PDMv4.EntradaSalida;
using PDMv4.Instrucciones;
using System.Collections.Generic;
using System.Linq;

namespace PDMv4.Procesador
{
    static class UC
    {
        private static FicheroES archivoES;
        internal static FicheroES ArchivoES { get => archivoES; set => archivoES = value; }
        public static string Extraccion()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.ContadorProgramaH.Contenido << 8) + Main.ContadorProgramaL.Contenido);
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.RegistroInstruccion.Contenido = Main.BusesDatosYDireccion.Contenido;
            Main.ContadorPrograma = ((ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1));

            return "RI <- (CP); CP <- CP + 1";
        }

        public static string Decodificacion()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = Main.ContadorPrograma;
            return "Evaluación del RI";
        }

        public static string S2()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.Acumulador.Contenido = Main.BusesDatosYDireccion.Contenido;
            return "Ac <- " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S3()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.Acumulador.Contenido;
            Main.ObtenerRegistro(numRegistro).Contenido = Main.BusesDatosYDireccion.Contenido;

            return Main.ObtenerNombreRegistro(numRegistro) + " <- Ac";
        }

        public static string S4()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.ObtenerRegistro(numRegistro).Contenido = Main.BusesDatosYDireccion.Contenido;

            return Main.ObtenerNombreRegistro(numRegistro) + " <- (CP); CP <- CP + 1";
        }

        public static string S5()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.RegistroDireccionesH.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "LH <- (CP); CP <- CP + 1";
        }

        public static string S6()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)Main.ContadorPrograma;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.RegistroDireccionesL.Contenido = Main.BusesDatosYDireccion.Contenido;

            return " LL <- (CP); CP <- CP + 1";
        }

        public static string S7(bool input = false)
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ObtenerRegistro(numRegistro).Contenido = Main.BusesDatosYDireccion.Contenido;



            return Main.ObtenerNombreRegistro(numRegistro) + " <- (RDD)";
        }

        public static string S8()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido = Main.BusesDatosYDireccion.Contenido;
            Main.EditadaMemoriaManualmente = true;
            
            return Main.BusesDatosYDireccion.ContenidoDireccion + " <- " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S9()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;

            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Suma();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            return "R_UAL <- Ac + " + Main.ObtenerNombreRegistro(numRegistro) + "; FZ; FC";
        }

        public static string S10()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = Main.ContadorPrograma;
            Main.BusesDatosYDireccion.Contenido = Main.UnidadAritmeticoLogica.Resultado;
            Main.Acumulador.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "Ac <- R_UAL";
        }


        public static string S11()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;

            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Resta();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            return "R_UAL <- Ac - " + Main.ObtenerNombreRegistro(numRegistro) + "; FZ; FC";
        }

        public static string S12()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Comparar();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            return "Ac - " + Main.ObtenerNombreRegistro(numRegistro) + "; FZ; FC";
        }

        public static string S13()
        {
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Incremento();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            return "R_UAL <- Ac + 1; FZ; FC";
        }
        public static string S14()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Suma();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);

            return "R_UAL <- Ac + (CP); FZ; FC; CP <- CP + 1";
        }

        public static string S15()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Resta();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);

            return "R_UAL <- Ac - (CP); FZ; FC; CP <- CP + 1";
        }

        public static string S16()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.Comparar();
            Main.FlagCarry = Main.UnidadAritmeticoLogica.FC;
            Main.FlagZero = Main.UnidadAritmeticoLogica.FZ;

            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            return "Ac - (CP); FZ; FC; CP <- CP + 1";
        }

        public static string S17()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.AND();

            return "R_UAL <- Ac AND " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S18()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.OR();

            return "R_UAL <- Ac OR " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S19()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.XOR();

            return "R_UAL <- Ac XOR " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S20()
        {
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.NOT();

            return "R_UAL <- NOT Ac";
        }

        public static string S21()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.AND();

            return "R_UAL <- Ac AND (CP)";
        }

        public static string S22()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.OR();

            return "R_UAL <- Ac OR (CP)";
        }

        public static string S23()
        {
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);
            Main.UnidadAritmeticoLogica.Cargar();
            Main.UnidadAritmeticoLogica.XOR();

            return "R_UAL <- Ac XOR (CP)";
        }

        public static string S26()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.RegistroInstruccion.Contenido = Main.BusesDatosYDireccion.Contenido;
            Main.ContadorPrograma = (ushort)(Main.BusesDatosYDireccion.ContenidoDireccion + 1);

            return "RI <- (RDD); CP <- RDD + 1";
        }

        public static string S27()
        {
            //ponemos primer y último bytes a 0 -> 0xxxxxxxx0
            byte nuevoContenido = (byte)((byte)(Main.BusesDatosYDireccion.Contenido << 1) >> 1);

            //Máscara FCxxxxxxFZ
            byte contenidoRF = (byte)((Main.FlagCarry ? 128 : 0) + (Main.FlagZero ? 1 : 0));

            Main.BusesDatosYDireccion.Contenido = (byte)(nuevoContenido | contenidoRF);
            Main.Acumulador.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "AC <- FC & xxxxxx & FZ";
        }

        public static string S28()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            ushort direccionMemoria = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            
            if (OpcionesPrograma.EntradaSalida && direccionMemoria == OpcionesPrograma.DireccionMemoriaEntrada)
            {
                Main.BusesDatosYDireccion.ContenidoDireccion = direccionMemoria;
                Main.BusesDatosYDireccion.Contenido = (byte)archivoES.LeerFichero();
                Main.ObtenerRegistro(numRegistro).Contenido = Main.BusesDatosYDireccion.Contenido;

                return Main.ObtenerNombreRegistro(numRegistro) + " <- input: @(0x" + direccionMemoria.ToString("X4") + ")";
            }

            return "Instrucción entrada no disponible.";
        }

        public static string S29()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            ushort direccionMemoria = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);

            if (OpcionesPrograma.EntradaSalida && direccionMemoria == OpcionesPrograma.DireccionMemoriaSalida)
            {
                Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
                Main.BusesDatosYDireccion.ContenidoDireccion = direccionMemoria;

                archivoES.EscribirEnFichero(Main.BusesDatosYDireccion.Contenido);

                return "output: @(0x" + Main.BusesDatosYDireccion.ContenidoDireccion.ToString("X4") + ") <- " + Main.ObtenerNombreRegistro(numRegistro);
            }
            return "Instrucción salida no disponible.";
        }

        public static string S30()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.RegistroDireccionesH.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "LH <- " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S31()
        {
            Main.BusesDatosYDireccion.Contenido = Main.Acumulador.Contenido;
            Main.RegistroDireccionesL.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "LL <- Ac";
        }

        public static string S32()
        {
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            Main.BusesDatosYDireccion.Contenido = Main.ObtenerMemoria.ObtenerDireccion(Main.BusesDatosYDireccion.ContenidoDireccion).Contenido;
            Main.Acumulador.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "Ac <- (RDD)";
        }

        public static string S33()
        {
            int numRegistro = Main.RegistroInstruccion.Contenido % 4;

            switch(Main.RegistroInstruccion.Contenido >> 2)
            {
                case 3:
                    numRegistro = 0; break;
                case 5:
                    numRegistro = 1; break;
                case 7:
                    numRegistro = 2; break;
                case 15:
                    numRegistro = 3; break;
            }

            Main.BusesDatosYDireccion.Contenido = Main.ObtenerRegistro(numRegistro).Contenido;
            Main.RegistroDireccionesL.Contenido = Main.BusesDatosYDireccion.Contenido;

            return "LL <- " + Main.ObtenerNombreRegistro(numRegistro);
        }

        public static string S34()
        {
            Main.BusesDatosYDireccion.Contenido = Main.Acumulador.Contenido;
            Main.BusesDatosYDireccion.ContenidoDireccion = (ushort)((Main.RegistroDireccionesH.Contenido << 8) + Main.RegistroDireccionesL.Contenido);
            Main.ObtenerMemoria.EscribirMemoria(Main.BusesDatosYDireccion.Contenido, Main.BusesDatosYDireccion.ContenidoDireccion);

            return Main.BusesDatosYDireccion.ContenidoDireccion + " <- Ac";
        }


        public static string ObtenerMicroInstruccionAPartirDeSeñales(sbyte[] señales)
        {
            bool escritura = false;
            string señalesTexto = string.Empty;
            foreach (sbyte señal in señales)
                señalesTexto += señal.ToString();
            switch (señalesTexto)
            {
                case "1100000000001-1-1-100":
                    señalesTexto = "RI <- (CP); CP <- CP + 1";
                    break;
                case "0000000000000-1-1-100":
                    señalesTexto = "Evaluación del RI";
                    break;
                case "0010000010000-1-1-100":
                    señalesTexto = "Ac <- " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "0001000001000-1-1-100":
                    señalesTexto = Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroEscrito()) + " <- Ac";
                    break;
                case "0101000000001-1-1-100":
                    señalesTexto = Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroEscrito()) + " <- (CP); CP <- CP + 1";
                    break;
                case "0100100000001-1-1-100":
                    señalesTexto = "LH <- (CP); CP <- CP + 1";
                    break;
                case "0100010000001-1-1-100":
                    señalesTexto = "LL <- (CP); CP <- CP + 1";
                    break;
                case "0001000000011-1-1-100":
                    señalesTexto = Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroEscrito()) + " <- (RDD)";
                    break;
                case "0010000000110-1-1-100":
                    señalesTexto = "0x" + Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerDirMemoria(out escritura).ToString("X4") + " <- " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "001000100000000001":
                    señalesTexto = "R_UAL <- Ac + " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido()) + "; FZ; FC";
                    break;
                case "0000000010000-1-1-110":
                    señalesTexto = "Ac <- R_UAL";
                    break;
                case "001000100000000101":
                    señalesTexto = "R_UAL <- Ac - " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido()) + "; FZ; FC";
                    break;
                case "001000100000000100":
                    señalesTexto = "Ac - " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido()) + "; FZ; FC";
                    break;
                case "000000100000011101":
                    señalesTexto = "R_UAL <- Ac + 1; FZ; FC; CP <- CP + 1";
                    break;
                case "010000100000100001":
                    señalesTexto = "R_UAL <- Ac + (CP); FZ; FC; CP <- CP + 1";
                    break;
                case "010000100000100101":
                    señalesTexto = "R_UAL <- Ac - (CP); FZ; FC; CP <- CP + 1";
                    break;
                case "010000100000100100":
                    señalesTexto = "Ac - (CP); FZ; FC; CP <- CP + 1";
                    break;
                case "001000000000001001":
                    señalesTexto = "R_UAL <- Ac AND " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "001000000000001101":
                    señalesTexto = "R_UAL <- Ac OR " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "001000000000010001":
                    señalesTexto = "R_UAL <- Ac XOR " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "000000000000010101":
                    señalesTexto = "R_UAL <- NOT Ac";
                    break;
                case "010000000000101001":
                    señalesTexto = "R_UAL <- Ac AND (CP); CP <- CP + 1";
                    break;
                case "010000000000101101":
                    señalesTexto = "R_UAL <- Ac OR (CP); CP <- CP + 1";
                    break;
                case "010000000000110001":
                    señalesTexto = "R_UAL <- Ac XOR (CP); CP <- CP + 1";
                    break;
                case "1100000000011-1-1-100":
                    señalesTexto = "RI <- (RDD); CP <- RDD + 1";
                    break;
                case "0000000110000-1-1-100":
                    señalesTexto = "AC <- FC && xxxxxx && FZ";
                    break;
                case "0001000000011-2-2-200":
                    señalesTexto = OpcionesPrograma.EntradaSalida ? "input: @(0x" + Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerDirMemoria(out escritura).ToString("X4") + 
                        ")" : "Instrucción entrada no disponible.";
                    break;
                case "0010000000110-2-2-200":
                    señalesTexto = OpcionesPrograma.EntradaSalida ? "output: @(0x" + OpcionesPrograma.DireccionMemoriaSalida.ToString("X4") + ")" : "Instrucción salida no disponible.";
                    break;

                case "0010100000000-1-1-100":
                    señalesTexto = "LH <- " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumRegistroLeido());
                    break;
                case "0000010001000-1-1-100":
                    señalesTexto = "LL <- AC";
                    break;
                case "0000000010011-1-1-100":
                    señalesTexto = "AC <- (RDD)";
                    break;
                case "0010010000000-1-1-100":
                    señalesTexto = "LL <- " + Main.ObtenerNombreRegistro(Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerNumSegundoRegistroLeido());
                    break;
                case "0000000001110-1-1-100":
                    señalesTexto = "0x" + (Main.ListaInstrucciones[Main.IndiceInstruccionActual].instruccion.ObtenerDirMemoria(out escritura).ToString("X4")) + " <- AC";
                    break;
                default:
                    señalesTexto = string.Empty;
                    break;
            }

            return señalesTexto;
        }

        public static int EjecutarMicroInstruccionAPartirDeSeñales(sbyte[] señales)
        {
            int microinstruccion = 1;
            string señalesTexto = string.Empty;
            foreach (sbyte señal in señales)
                señalesTexto += señal.ToString();
            switch (señalesTexto)
            {
                case "1100000000001-1-1-100":
                    Extraccion();
                    microinstruccion = 0;
                    break;
                case "0000000000000-1-1-100":
                    Decodificacion();
                    microinstruccion = 1;
                    break;
                case "0010000010000-1-1-100":
                    S2();
                    microinstruccion = 2;
                    break;
                case "0001000001000-1-1-100":
                    S3();
                    microinstruccion = 3;
                    break;
                case "0101000000001-1-1-100":
                    S4();
                    microinstruccion = 4;
                    break;
                case "0100100000001-1-1-100":
                    S5();
                    microinstruccion = 5;
                    break;
                case "0100010000001-1-1-100":
                    S6();
                    microinstruccion = 6;
                    break;
                case "0001000000011-1-1-100":
                    S7();
                    microinstruccion = 7;
                    break;
                case "0010000000110-1-1-100":
                    S8();
                    microinstruccion = 8;
                    break;
                case "001000100000000001":
                    S9();
                    microinstruccion = 9;
                    break;
                case "0000000010000-1-1-110":
                    S10();
                    microinstruccion = 10;
                    break;
                case "001000100000000101":
                    S11();
                    microinstruccion = 11;
                    break;
                case "001000100000000100":
                    S12();
                    microinstruccion = 12;
                    break;
                case "000000100000011101":
                    S13();
                    microinstruccion = 13;
                    break;
                case "010000100000100001":
                    S14();
                    microinstruccion = 14;
                    break;
                case "010000100000100101":
                    S15();
                    microinstruccion = 15;
                    break;
                case "010000100000100100":
                    S16();
                    microinstruccion = 16;
                    break;
                case "001000000000001001":
                    S17();
                    microinstruccion = 17;
                    break;
                case "001000000000001101":
                    S18();
                    microinstruccion = 18;
                    break;
                case "001000000000010001":
                    S19();
                    microinstruccion = 19;
                    break;
                case "000000000000010101":
                    S20();
                    microinstruccion = 20;
                    break;
                case "010000000000101001":
                    S21();
                    microinstruccion = 21;
                    break;
                case "010000000000101101":
                    S22();
                    microinstruccion = 22;
                    break;
                case "010000000000110001":
                    S23();
                    microinstruccion = 23;
                    break;
                case "1100000000011-1-1-100":
                    S26();
                    microinstruccion = 26;
                    break;
                case "0000000110000-1-1-100":
                    S27();
                    microinstruccion = 27;
                    break;
                case "0001000000011-2-2-200":
                    S28();
                    microinstruccion = 28;
                    break;
                case "0010000000110-2-2-200":
                    S29();
                    microinstruccion = 29;
                    break;

                case "0010100000000-1-1-100":
                    S30();
                    microinstruccion = 30;
                    break;
                case "0000010001000-1-1-100":
                    S31();
                    microinstruccion = 31;
                    break;
                case "0000000010011-1-1-100":
                    S32();
                    microinstruccion = 32;
                    break;
                case "0010010000000-1-1-100":
                    S33();
                    microinstruccion = 33;
                    break;
                case "0000000001110-1-1-100":
                    S34();
                    microinstruccion = 34;
                    break;
            }

            return microinstruccion;
        }

        public static List<sbyte[]> ObtenerSeñalesControl(int indice)
        {
            List<sbyte[]> resul = new List<sbyte[]>
            {
                new sbyte[18] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 },
                new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0 }
            };

            int codigoInstruccion = (Main.ListaInstrucciones[indice].instruccion.Codigo >> 3) * 4 + 2 * (Main.FlagZero ? 1 : 0) + (Main.FlagCarry ? 1 : 0);
            int[] instruccionSTR = new int[] { 3, 5, 7, 15};
            if (instruccionSTR.Contains(Main.ListaInstrucciones[indice].instruccion.Codigo >> 2))
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 1)
            {
                resul.Add(new sbyte[18] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 0)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 3 == 1)
                resul.Add(new sbyte[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
            else if (codigoInstruccion >> 3 == 2)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 3 == 3)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 8)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 9)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 10)
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 });
            else if (codigoInstruccion >> 2 == 11)
            {
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 12)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 13)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 3 == 7)
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0 });
            else if (codigoInstruccion >> 2 == 16)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 17)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 18)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 19)
            {
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 20)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 2 == 21)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 3 == 11)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, -1, -1, 1, 0 });
            }
            else if (codigoInstruccion >> 3 == 12)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 26 || codigoInstruccion >> 2 == 27)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 3 == 14)
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, -1, -1, -1, 0, 0 });
            else if (Main.ListaInstrucciones[indice].instruccion.Codigo >> 2 == 63)
            {
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, -1, -1, -1, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 30)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, -2, -2, -2, 0, 0 });
            }
            else if (codigoInstruccion >> 2 == 31)
            {
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 0, 0 });
                resul.Add(new sbyte[18] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, -2, -2, -2, 0, 0 });
            }
            

            return resul;
        }
    }
}
