using PDMv4.Instrucciones;
using PDMv4.Interfaces;
using PDMv4.Memoria;
using System;
using System.Collections.Generic;

namespace PDMv4.Procesador
{
    static class Main
    {
        private static MemoriaPrincipal memoria;
        private static UAL unidadAritmeticoLogica;
        private static Registro[] registros;
        private static Registro acumulador;
        private static Registro registroInstruccion;
        private static Registro registroDireccionesH;
        private static Registro registroDireccionesL;
        private static Registro contadorProgramaH;
        private static Registro contadorProgramaL;
        private static Buses busesDatosYDireccion;
        private static bool flagCarry;
        private static bool flagZero;

        private static List<sbyte[]> listaMicroinstrucciones;
        private static List<InstruccionDireccionMemoria> listaInstrucciones;
        private static int indiceInstruccionActual;
        private static int indiceMicroinstruccionActual;
        private static int codMicroinstruccion;
        private static int numRegistroSeleccionado;

        public static MemoriaPrincipal ObtenerMemoria{ get => memoria; }
        public static bool FlagCarry { get => flagCarry; set => flagCarry = value; }
        public static bool FlagZero { get => flagZero; set => flagZero = value; }
        internal static Registro Acumulador { get => acumulador; set => acumulador = value; }
        internal static Registro RegistroInstruccion { get => registroInstruccion; set => registroInstruccion = value; }
        internal static Registro RegistroDireccionesH { get => registroDireccionesH; set => registroDireccionesH = value; }
        internal static Registro RegistroDireccionesL { get => registroDireccionesL; set => registroDireccionesL = value; }
        internal static Buses BusesDatosYDireccion { get => busesDatosYDireccion; set => busesDatosYDireccion = value; }
        internal static Registro ContadorProgramaH { get => contadorProgramaH; set => contadorProgramaH = value; }
        internal static Registro ContadorProgramaL { get => contadorProgramaL; set => contadorProgramaL = value; }

        private static Stack<int[]> pilaProcesador;

        public static bool EditadaMemoriaManualmente { get; set; }

        static Main()
        {
            
            memoria = MemoriaPrincipal.ObtenerMemoria(65536);
            memoria.InicializarMemoria();

            acumulador = new Registro();
            registroInstruccion = new Registro();
            registroDireccionesH = new Registro();
            registroDireccionesL = new Registro();
            contadorProgramaH = new Registro();
            contadorProgramaL = new Registro();
            ContadorPrograma = OpcionesPrograma.DireccionMemoriaComienzoPrograma; 
            busesDatosYDireccion = new Buses();
            unidadAritmeticoLogica = new UAL();

            flagCarry = false;
            flagZero = false;

            registros = new Registro[4];
            acumulador = new Registro();
            for (int i = 0; i < registros.Length; i++)
            {
                registros[i] = new Registro();
            }

            listaInstrucciones = new List<InstruccionDireccionMemoria>();
            listaMicroinstrucciones = new List<sbyte[]>();

            indiceInstruccionActual = 0;
            indiceMicroinstruccionActual = 0;
            codMicroinstruccion = -1;
            numRegistroSeleccionado = 0;

            pilaProcesador = new Stack<int[]>();
        }

        internal static ushort ContadorPrograma
        {
            get
            {
                return (ushort)((contadorProgramaH.Contenido << 8) + contadorProgramaL.Contenido);
            }
            set
            {
                contadorProgramaL.Contenido = (byte)(value % 256);
                contadorProgramaH.Contenido = (byte)(value >> 8);
            }
        }

        internal static ushort RegistroDirecciones
        {
            get
            {
                return (ushort)((registroDireccionesH.Contenido << 8) + registroDireccionesL.Contenido);
            }
            set
            {
                registroDireccionesL.Contenido = (byte)(value % 256);
                registroDireccionesH.Contenido = (byte)(value >> 8);
            }
        }

        internal static UAL UnidadAritmeticoLogica { get => unidadAritmeticoLogica;}
        public static List<InstruccionDireccionMemoria> ListaInstrucciones => listaInstrucciones;

        public static int IndiceInstruccionActual { get => indiceInstruccionActual; set => indiceInstruccionActual = value; }
        public static List<sbyte[]> ListaMicroinstrucciones { get => listaMicroinstrucciones; set => listaMicroinstrucciones = value; }
        public static int IndiceMicroinstruccionActual { get => indiceMicroinstruccionActual; set => indiceMicroinstruccionActual = value; }
        private static bool PilaLlamadaVacias { get; set; }
        public static int NumRegistroSeleccionado { get => numRegistroSeleccionado; }

        public static string ObtenerNombreRegistro(int numero)
        {
            string nombreRegistro = null;
            if (numero > 4)
                numero -= 5;
            switch (numero)
            {
                case 0:
                    nombreRegistro = "B";
                    break;
                case 1:
                    nombreRegistro = "C";
                    break;
                case 2:
                    nombreRegistro = "D";
                    break;
                case 3:
                    nombreRegistro = "E";
                    break;
                case 4:
                    nombreRegistro = "Acum.";
                    break;
            }

            return nombreRegistro;
        }

        public static Registro ObtenerRegistro(int numRegistro)
        {
            if (numRegistro == 4)
                return acumulador;

            return registros[numRegistro];
        }

        private static ushort ultimaPosicionPrograma;
        public static void EstablecerUltimaDireccionPrograma(ushort pos)
        {
            ultimaPosicionPrograma = pos;
        }
        public static void Restablecer(bool completo = true, bool restablecerRegistrosYMemoria = true)
        {
            if (restablecerRegistrosYMemoria)
            {
                EditadaMemoriaManualmente = false;

                registros[0].Contenido = 0;
                registros[1].Contenido = 0;
                registros[2].Contenido = 0;
                registros[3].Contenido = 0;

                acumulador.Contenido = 0;
            }
            registroInstruccion.Contenido = 0;
            registroDireccionesH.Contenido = 0;
            registroDireccionesL.Contenido = 0;
            ContadorPrograma = OpcionesPrograma.DireccionMemoriaComienzoPrograma;
            busesDatosYDireccion.Contenido = 0;
            busesDatosYDireccion.ContenidoDireccion = 0;
            unidadAritmeticoLogica.Restablecer();
            flagCarry = false;
            flagZero = false;

            if (completo)
            {
                if(restablecerRegistrosYMemoria)
                    memoria.RestablecerMemoria();
                else
                    memoria.RestablecerMemoria(ContadorPrograma, ultimaPosicionPrograma);

                listaInstrucciones.Clear();
            } 
            listaMicroinstrucciones.Clear();

            indiceInstruccionActual = 0;
            indiceMicroinstruccionActual = 0;
            codMicroinstruccion = -1;
            numRegistroSeleccionado = 0;

            pilaProcesador.Clear();

            GC.Collect();
        }

        public static string ObtenerTextoInstruccion(ushort direccion)
        {
            foreach (InstruccionDireccionMemoria inst in listaInstrucciones)
            {
                if (inst.direccion == direccion)
                    return inst.instruccion.ConvertirEnLinea();
            }

            return string.Empty;
        }

        public static void InicializarRegistros()
        {
            for (int i = 0; i < registros.Length; i++)
            {
                registros[i] = new Registro();
            }
        }

        public static void ActualizarSeñalesControl()
        {
            listaMicroinstrucciones = UC.ObtenerSeñalesControl(indiceInstruccionActual);
        }

        public static void AñadirInstruccionListaInstrucciones(Instruccion instruccion, ushort direccion)
        {
            InstruccionDireccionMemoria _instruccion;
            _instruccion.direccion = direccion;
            _instruccion.instruccion = instruccion;
            listaInstrucciones.Add(_instruccion);
        }

        public static void GuardarEstadoActual(int direccionMemoriaModificada = -1)
        {
            int[] estadoActual = new int[21];
            estadoActual[0] = unidadAritmeticoLogica.Resultado;
            estadoActual[1] = registros[0].Contenido;
            estadoActual[2] = registros[1].Contenido;
            estadoActual[3] = registros[2].Contenido;
            estadoActual[4] = registros[3].Contenido;
            estadoActual[5] = acumulador.Contenido;
            estadoActual[6] = registroInstruccion.Contenido;
            estadoActual[7] = registroDireccionesH.Contenido;
            estadoActual[8] = registroDireccionesL.Contenido;
            estadoActual[9] = contadorProgramaH.Contenido;
            estadoActual[10] = contadorProgramaL.Contenido;
            estadoActual[11] = busesDatosYDireccion.Contenido;
            estadoActual[12] = busesDatosYDireccion.ContenidoDireccion;
            estadoActual[13] = flagCarry ? 1 : 0;
            estadoActual[14] = flagZero ? 1 : 0;
            estadoActual[15] = indiceInstruccionActual;
            estadoActual[16] = indiceMicroinstruccionActual;
            estadoActual[17] = codMicroinstruccion;
            estadoActual[18] = numRegistroSeleccionado;
            estadoActual[19] = direccionMemoriaModificada;
            estadoActual[20] = direccionMemoriaModificada == -1 ? 0 : memoria.ObtenerDireccion((ushort)direccionMemoriaModificada).Contenido;

            pilaProcesador.Push(estadoActual);
    }

        public static void RevertirEstadoAnterior()
        {
            int[] estadoAnterior = pilaProcesador.Pop();

            unidadAritmeticoLogica.Resultado = (byte)estadoAnterior[0];
            registros[0].Contenido = (byte)estadoAnterior[1];
            registros[1].Contenido = (byte)estadoAnterior[2];
            registros[2].Contenido = (byte)estadoAnterior[3];
            registros[3].Contenido = (byte)estadoAnterior[4];
            acumulador.Contenido = (byte)estadoAnterior[5];
            registroInstruccion.Contenido = (byte)estadoAnterior[6];
            registroDireccionesH.Contenido = (byte)estadoAnterior[7];
            registroDireccionesL.Contenido = (byte)estadoAnterior[8];
            contadorProgramaH.Contenido = (byte)estadoAnterior[9];
            contadorProgramaL.Contenido = (byte)estadoAnterior[10];
            busesDatosYDireccion.Contenido = (byte)estadoAnterior[11];
            busesDatosYDireccion.ContenidoDireccion = (byte)estadoAnterior[12];
            flagCarry = estadoAnterior[13] == 1;
            flagZero = estadoAnterior[14] == 1;
            indiceInstruccionActual = estadoAnterior[15];
            indiceMicroinstruccionActual = estadoAnterior[16];
            codMicroinstruccion = estadoAnterior[17];
            numRegistroSeleccionado = estadoAnterior[18];
            if (estadoAnterior[19] != -1)
                memoria.ObtenerDireccion((ushort)estadoAnterior[19]).Contenido = (byte)estadoAnterior[20];

        }

        public static void EjecutarMicroinstruccion()
        {
            int dirMemoriaModificada = -1;            
            int instruccionAnterior = IndiceInstruccionActual;
            PilaLlamadaVacias = pilaProcesador.Count == 0;

            if (listaInstrucciones[indiceInstruccionActual].instruccion is IModificaDireccionMemoria)
                dirMemoriaModificada = (listaInstrucciones[indiceInstruccionActual].instruccion as IModificaDireccionMemoria).ObtenerDirMemoriaModificada();

            GuardarEstadoActual(dirMemoriaModificada);
            indiceInstruccionActual = ObtenerSiguienteInstruccion();
            indiceMicroinstruccionActual = ObtenerSiguienteMicroinstruccion();

            if (PilaLlamadaVacias ||instruccionAnterior != indiceInstruccionActual)
                ActualizarSeñalesControl();

            codMicroinstruccion = UC.EjecutarMicroInstruccionAPartirDeSeñales(listaMicroinstrucciones[indiceMicroinstruccionActual]);

            if(codMicroinstruccion == 1)
                numRegistroSeleccionado = registroInstruccion.Contenido % 4;
        }

        public static void RevertirMicroinstruccion()
        {
            if (pilaProcesador.Count == 0)
                Restablecer(false);
            else
            {
                int instruccionAnterior = indiceInstruccionActual;
                RevertirEstadoAnterior();
                if (instruccionAnterior != IndiceInstruccionActual)
                {
                    ActualizarSeñalesControl();
                }
            }
        }

        public static void EjecutarInstruccion()
        {
            do
            {
                EjecutarMicroinstruccion();
            } while (indiceInstruccionActual == ObtenerSiguienteInstruccion());
        }

        public static void RevertirInstruccion()
        {
            do
            {
                RevertirMicroinstruccion();
            } while (indiceInstruccionActual == ObtenerSiguienteInstruccion());
        }

        private static int ObtenerSiguienteMicroinstruccion()
        {
            int microinstruccion = 0;
            if(PilaLlamadaVacias)
            {
                microinstruccion = 0;
            }
            else
            {
                int indiceInstruccionAnterior = pilaProcesador.Peek()[15];
                if (indiceMicroinstruccionActual < listaInstrucciones[indiceInstruccionAnterior].instruccion.NumMicroinstrucciones)
                {
                    if (indiceMicroinstruccionActual == listaInstrucciones[indiceInstruccionAnterior].instruccion.NumMicroinstrucciones - 2)
                    {
                        if (listaInstrucciones[indiceInstruccionAnterior].instruccion is BEQ)
                        {
                            if (!flagZero)
                                microinstruccion = 0;
                            else microinstruccion = indiceMicroinstruccionActual + 1;

                        }
                        else if (listaInstrucciones[indiceInstruccionAnterior].instruccion is BC)
                        {
                            if (!flagCarry)
                                microinstruccion = 0;
                            else microinstruccion = indiceMicroinstruccionActual + 1;
                        }
                        else microinstruccion = indiceMicroinstruccionActual + 1;
                    }
                    else if (indiceMicroinstruccionActual == listaInstrucciones[indiceInstruccionAnterior].instruccion.NumMicroinstrucciones - 1)
                    {
                        if (listaInstrucciones[indiceInstruccionAnterior].instruccion is IInstruccionSalto)
                            microinstruccion = 1;
                        else microinstruccion = 0;
                    }
                    else microinstruccion = indiceMicroinstruccionActual + 1;
                }
            }

            return microinstruccion;
        }

        private static int ObtenerSiguienteInstruccion()
        {
            int instruccion = indiceInstruccionActual;
            if (PilaLlamadaVacias)
            {
                instruccion = 0;
            }
            else if (indiceMicroinstruccionActual < listaInstrucciones[IndiceInstruccionActual].instruccion.NumMicroinstrucciones)
            {
                if (indiceMicroinstruccionActual == listaInstrucciones[IndiceInstruccionActual].instruccion.NumMicroinstrucciones - 2)
                {
                    if (listaInstrucciones[IndiceInstruccionActual].instruccion is BEQ && !flagZero || listaInstrucciones[IndiceInstruccionActual].instruccion is BC && !flagCarry)
                        instruccion = indiceInstruccionActual + 1;
                }
                else if (indiceMicroinstruccionActual == listaInstrucciones[IndiceInstruccionActual].instruccion.NumMicroinstrucciones - 1)
                {
                    if (listaInstrucciones[IndiceInstruccionActual].instruccion is IInstruccionSalto)
                         instruccion = (listaInstrucciones[IndiceInstruccionActual].instruccion as IInstruccionSalto).ObtenerSalto();
                    else instruccion = indiceInstruccionActual + 1;
                }
            }

            return instruccion;
        }

        public static int[] ObtenerRegistroLeidoAPartirMicroinstruccion()
        {
            int[] registroLeido = { -1 };
            switch(codMicroinstruccion)
            {
                case 2:
                case 3:
                case 8:
                case 9:
                case 11:
                case 12:
                case 16:
                case 17:
                case 18:
                case 19:
                case 30:
                    registroLeido[0] = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerNumRegistroLeido();
                    break;
                case 33:
                    registroLeido = new int[2];
                    registroLeido[0] = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerNumRegistroLeido();
                    registroLeido[1] = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerNumSegundoRegistroLeido();
                    break;
            }
            if(registroLeido[0] > 4)
            {
                int registro = registroLeido[0];
                registroLeido = new int[2]{ registro - 5, 4};
            }
            return registroLeido;
        }

        public static int ObtenerRegistroEscritoAPartirMicroinstruccion()
        {
            int registroEscrito = -1;
            switch (codMicroinstruccion)
            {
                case 2:
                case 3:
                case 4:
                case 7:
                case 10:
                case 27:
                case 32:
                    registroEscrito = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerNumRegistroEscrito();
                    break;
            }
            return registroEscrito;
        }

        public static int ObtenerDireccionMemoriaLEaPartirMicroinstruccion(out bool escritura)
        {
            int dirMem = -1;
            escritura = false;
            switch (codMicroinstruccion)
            {
                case 0:
                case 4:
                case 5:
                case 6:
                case 14:
                case 15:
                case 16:
                case 21:
                case 22:
                case 23:
                    dirMem = (pilaProcesador.Peek()[9] << 8) + pilaProcesador.Peek()[10];
                    break;
                case 7:
                case 8:
                case 26:
                case 34:
                    dirMem = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerDirMemoria(out escritura);
                    break;

            }
            return dirMem;
        }

        public static int[] ObtenerFlagsAPartirMicroinstruccion(out bool escritura)
        {
            int[] dirMem = { -1};
            escritura = false;
            switch (codMicroinstruccion)
            {
                //case 1:
                case 9:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 6:
                case 27:
                    dirMem = listaInstrucciones[indiceInstruccionActual].instruccion.ObtenerFlags(out escritura);
                    break;

            }
            return dirMem;
        }
    }
}
