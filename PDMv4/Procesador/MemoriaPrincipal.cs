using PDMv4.Argumentos;
using PDMv4.Instrucciones;
using System;
using System.Collections.Generic;
using static PDMv4.Argumentos.Argumento;

namespace PDMv4.Procesador
{
    class MemoriaPrincipal
    {
        private DireccionMemoria[] memoria;
        private List<Etiqueta> etiquetas;
        private int tamaño;

        private MemoriaPrincipal(int tamaño)
        {
            this.tamaño = tamaño;
            memoria = new DireccionMemoria[tamaño];
            etiquetas = new List<Etiqueta>();
        }

        public static MemoriaPrincipal ObtenerMemoria(int tamaño)
        {
            return new MemoriaPrincipal(tamaño);
        }

        public int Tamaño
        {
            get
            {
                return tamaño;
            }
        }

        public List<Etiqueta> Etiquetas
        {
            get
            {
                return etiquetas;
            }
        }

        public void InicializarMemoria()
        {
            for (int i = 0; i < tamaño; i++)
            {
                memoria[i] = new DireccionMemoria();
            }
        }

        public void RestablecerMemoria()
        {
            for (int i = 0; i < tamaño; i++)
            {
                memoria[i].Contenido = 0;
            }
            etiquetas.Clear();
        }

        public void EscribirMemoria(byte contenido, int posicion)
        {
            memoria[posicion].Contenido = contenido;
        }

        public void EscribirInstruccionMemoria(Instruccion instruccion, ref ushort posicion)
        {
            memoria[posicion].Contenido = instruccion.Codigo;
            if (instruccion.NumArgumentos == 1 && instruccion.ObtenerArgumento(0).TipoArgumento() != Tipo.Registro)
            {
                if (instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria)
                {
                    memoria[++posicion].Contenido = BitConverter.GetBytes((instruccion.ObtenerArgumento(0) as ArgMemoria).DireccionMemoria)[1];
                    memoria[++posicion].Contenido = BitConverter.GetBytes((instruccion.ObtenerArgumento(0) as ArgMemoria).DireccionMemoria)[0];
                }
                else
                {
                    memoria[++posicion].Contenido = (instruccion.ObtenerArgumento(0) as ArgLiteral).Valor;
                }
            }
            else if (instruccion.NumArgumentos == 2)
            {
                if (instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria || instruccion.ObtenerArgumento(1).TipoArgumento() == Tipo.Memoria)
                {
                    int n = instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria ? 0 : 1;
                    memoria[++posicion].Contenido = BitConverter.GetBytes((instruccion.ObtenerArgumento(n) as ArgMemoria).DireccionMemoria)[1];
                    memoria[++posicion].Contenido = BitConverter.GetBytes((instruccion.ObtenerArgumento(n) as ArgMemoria).DireccionMemoria)[0];
                }
                else
                {
                    int n = instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Literal ? 0 : 1;
                    memoria[++posicion].Contenido = (instruccion.ObtenerArgumento(n) as ArgLiteral).Valor;
                }
            }

            posicion++;
        }

        public void ProbarInstruccionMemoria(Instruccion instruccion, ref ushort posicion)
        {
            byte prueba;
            memoria[posicion].Contenido = instruccion.Codigo;
            if (instruccion.NumArgumentos == 1 && instruccion.ObtenerArgumento(0).TipoArgumento() != Tipo.Registro)
            {
                if (instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria)
                {
                    ++posicion;
                    prueba = BitConverter.GetBytes((instruccion.ObtenerArgumento(0) as ArgMemoria).DireccionMemoria)[1];
                    ++posicion;
                    prueba = BitConverter.GetBytes((instruccion.ObtenerArgumento(0) as ArgMemoria).DireccionMemoria)[0];
                }
                else
                {
                    ++posicion;
                    prueba = (instruccion.ObtenerArgumento(0) as ArgLiteral).Valor;
                }
            }
            else if (instruccion.NumArgumentos == 2)
            {
                if (instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria || instruccion.ObtenerArgumento(1).TipoArgumento() == Tipo.Memoria)
                {
                    int n = instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Memoria ? 0 : 1;
                    ++posicion;
                    prueba = BitConverter.GetBytes((instruccion.ObtenerArgumento(n) as ArgMemoria).DireccionMemoria)[1];
                    ++posicion;
                    prueba = BitConverter.GetBytes((instruccion.ObtenerArgumento(n) as ArgMemoria).DireccionMemoria)[0];
                }
                else
                {
                    int n = instruccion.ObtenerArgumento(0).TipoArgumento() == Tipo.Literal ? 0 : 1;
                    ++posicion;
                    prueba = (instruccion.ObtenerArgumento(n) as ArgLiteral).Valor;
                }
            }

            posicion++;
        }

        public DireccionMemoria ObtenerDireccion(ushort direccion)
        {
            return memoria[direccion];
        }
    }
}
