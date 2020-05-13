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
        public static string ConvertToNumericBase(this byte number, params char[] baseChars)
        {
            return ConvertToNumericBase((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertToNumericBase(this ushort number, params char[] baseChars)
        {
            return ConvertToNumericBase((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertToNumericBase(this uint number, params char[] baseChars)
        {
            return ConvertToNumericBase((ulong) number, baseChars);
        }

        /// <summary>
        ///     Converte um número decimal para outra base numérica.
        /// </summary>
        /// <param name="number">Valor na base decimal.</param>
        /// <param name="baseChars">Caracteres da outra base numérica.</param>
        /// <returns>Valor na outra base numérica.</returns>
        public static string ConvertToNumericBase(this ulong number, params char[] baseChars)
        {
            const int bits = sizeof(ulong) * 8;

            var buffer = new char[bits];
            var baseDigitCount = (uint) baseChars.Length;

            var i = buffer.Length;
            do
            {
                buffer[--i] = baseChars[number % baseDigitCount];
                number /= baseDigitCount;
            } while (number > 0);

            var result = new char[bits - i];
            Array.Copy(buffer, i, result, 0, bits - i);

            return new string(result);
        }

        /// <summary>
        ///     Converte um número numa base numérica para base decimal.
        /// </summary>
        /// <param name="number">Valor numa base numérica.</param>
        /// <param name="baseChars">Caracteres da base numérica.</param>
        /// <returns>Valor na base decimal.</returns>
        public static ulong ConvertFromNumericBase(this string number, char[] baseChars)
        {
            var result = (ulong) 0;
            var baseDigitCount = baseChars.Length;

            for (var currentChar = number.Length - 1; currentChar >= 0; currentChar--)
            {
                var next = number[currentChar];

                int nextCharIndex;
                for (nextCharIndex = 0; nextCharIndex < baseDigitCount; nextCharIndex++)
                    if (baseChars[nextCharIndex] == next)
                        break;

                result += (ulong) (System.Math.Pow(baseChars.Length, number.Length - 1 - currentChar) * nextCharIndex);
            }

            return result;
        }
    }
}