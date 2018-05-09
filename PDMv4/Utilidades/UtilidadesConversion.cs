using System;

namespace PDMv4.Utilidades
{
    static class UtilidadesConversion
    {
        public static string ToBin(this int value, int len)
        {
            return (len > 1 ? ToBin(value >> 1, len - 1) : null) + "01"[value & 1];
        }

        public static int ToCa2(this int value)
        {
            string _binary = ToBin(value, 8);

            if (_binary[0] == '0')
                return value;
            else
            {
                _binary = ToBin(value - 1, 8);
                char[] binary = new char[_binary.Length];
                for (int i = 0; i < binary.Length; i++)
                {
                    binary[i] = _binary[i] == '0' ? '1' : '0';
                }
                return -Convert.ToInt32(new string(binary), 2);
            }
        }

        public static int ToDecimal(this int Ca2Value)
        {
            return Ca2Value >= 0 ? Ca2Value : 256 + Ca2Value;
        }

        public static int FromBinary(string binary, int len)
        {

            return Convert.ToInt32(binary, 2);
        }
    }
}
