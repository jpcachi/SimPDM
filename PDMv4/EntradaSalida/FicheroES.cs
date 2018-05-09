using System;
using System.IO;

namespace PDMv4.EntradaSalida
{
    internal class FicheroES : IDisposable
    {
        //cursor entrada
        private ushort refAddressEntrada = 0xFFFF;
        private ushort refAddressSalida = 0xFFFF;

        private string rutaSalida;
        private string rutaEntrada;
        public string RutaEntrada
        {
            set
            {
                rutaEntrada = value;
            }
        }

        private StreamWriter archivoSalida;
        private StreamReader archivoEntrada;

        public FicheroES(ushort refAddressEntrada, string rutaEntrada, ushort refAddressSalida, string rutaSalida)
        {
            this.refAddressEntrada = refAddressEntrada;
            this.refAddressSalida = refAddressSalida;
            this.rutaEntrada = rutaEntrada;
            this.rutaSalida = rutaSalida;

            try
            {
                archivoSalida = new StreamWriter(rutaSalida, true);
                archivoEntrada = new StreamReader(rutaEntrada);
            }
            catch
            {
                throw new Exception("El archivo de entrada no es válido. Por favor, revise la ruta especificada.");
            }
        }

        public void ActualizarFicheroEntrada(ushort refAddressEntrada, string rutaEntrada, ushort refAddressSalida, string rutaSalida)
        {
            this.refAddressEntrada = refAddressEntrada;
            this.refAddressSalida = refAddressSalida;
            this.rutaEntrada = rutaEntrada;
            this.rutaSalida = rutaSalida;

            try
            {
                archivoSalida.Close();
                archivoEntrada.Close();
                archivoSalida = new StreamWriter(rutaSalida, true);
                archivoEntrada = new StreamReader(rutaEntrada);
            }
            catch
            {
                throw new Exception("El archivo de entrada no es válido. Por favor, revise la ruta especificada.");
            }
        }


        public void EscribirEnFichero(byte value)
        {
            archivoSalida.Write(value.ToString("X2"));
            archivoSalida.Flush();
        }


        public int LeerFichero()
        {
            char[] siguienteValor = new char[2];
            int valorEntero = 0;

            try
            {
                archivoEntrada.Read(siguienteValor, 0, siguienteValor.Length);
                valorEntero = int.Parse(new string(siguienteValor), System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                valorEntero = 0;
            }

            return valorEntero;
        }

        public void CerrarFichero()
        {
            archivoEntrada.Close();
            archivoSalida.Close();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    rutaEntrada = null;
                    rutaSalida = null;
                }

                archivoSalida.Close();
                archivoEntrada.Close();
                archivoEntrada = null;
                archivoSalida = null;

                disposedValue = true;
            }
        }

         ~FicheroES() {
           Dispose(false);
         }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
