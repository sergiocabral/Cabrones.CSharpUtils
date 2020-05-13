using System;

namespace Cabrones.Utils.Math
{
    /// <summary>
    ///     Funcionalidades para cálculos matemáticos.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertTo(this byte number, params char[] baseChars)
        {
            return ConvertTo((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertTo(this ushort number, params char[] baseChars)
        {
            return ConvertTo((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertTo(this uint number, params char[] baseChars)
        {
            return ConvertTo((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertTo(this ulong number, params char[] baseChars)
        {
            const int bits = sizeof(ulong) * 8;

            var buffer = new char[bits];
            var baseDigits = (uint) baseChars.Length;

            var i = buffer.Length;
            do
            {
                buffer[--i] = baseChars[number % baseDigits];
                number /= baseDigits;
            } while (number > 0);

            var result = new char[bits - i];
            Array.Copy(buffer, i, result, 0, bits - i);

            return new string(result);
        }
    }
}