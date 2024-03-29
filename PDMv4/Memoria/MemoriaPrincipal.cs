﻿using PDMv4.Argumentos;
using PDMv4.Instrucciones;
using PDMv4.Procesador;
using System;
using System.Collections.Generic;
using static PDMv4.Argumentos.Argumento;

namespace PDMv4.Memoria
{
    class MemoriaPrincipal
    {
        private readonly DireccionMemoria[] memoria;

        private MemoriaPrincipal(int tamaño)
        {
            this.Tamaño = tamaño;
            memoria = new DireccionMemoria[tamaño];
            Etiquetas = new List<Etiqueta>();
        }

        public static MemoriaPrincipal ObtenerMemoria(int tamaño)
        {
            return new MemoriaPrincipal(tamaño);
        }

        public int Tamaño { get; }

        public List<Etiqueta> Etiquetas { get; }

        public void InicializarMemoria()
        {
            for (int i = 0; i < Tamaño; i++)
            {
                memoria[i] = new DireccionMemoria();
            }
        }

        public void RestablecerMemoria()
        {
            for (int i = 0; i < Tamaño; i++)
            {
                memoria[i].Contenido = 0;
            }
            Etiquetas.Clear();
        }

        public void RestablecerMemoria(ushort start, ushort end)
        {
            for (int i = start; i < end; i++)
            {
                memoria[i].Contenido = 0;
            }
            Etiquetas.Clear();
        }

        public void RestablecerMemoria(IEnumerable<int> indices)
        {
            foreach(int i in indices)
                memoria[i].Contenido = 0;
        }

        public void EscribirMemoria(byte contenido, int posicion)
        {
            Main.EditadaMemoriaManualmente = true;
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
            else if (instruccion.NumArgumentos == 2 && (instruccion.ObtenerArgumento(0).TipoArgumento() != Tipo.Registro || 
                     instruccion.ObtenerArgumento(1).TipoArgumento() != Tipo.Registro))
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
                else if (instruccion.ObtenerArgumento(0).TipoArgumento() != Tipo.Registro || instruccion.ObtenerArgumento(1).TipoArgumento() != Tipo.Registro)
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
