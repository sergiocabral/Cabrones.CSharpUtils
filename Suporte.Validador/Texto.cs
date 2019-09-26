using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades para validar formatos de textos.</para>
    /// </summary>
    public static class Texto
    {
        /// <summary>
        /// <para>Verifica se o texto é um e-mail válido.</para>
        /// </summary>
        /// <param name="texto">
        /// <para>Texto a ser verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna true quando o texto é válido.</para>
        /// </returns>
        public static bool EmailValido(string texto)
        {
            if (texto == null) { return false; }
            if (texto.Contains(" ")) { return false; }
            if (texto.Contains(";")) { return false; }
            if (texto.Contains(",")) { return false; }
            if (!texto.Contains("@")) { return false; }
            if (!texto.Contains(".")) { return false; }
            if (!texto.Substring(texto.IndexOf("@")).Contains(".")) { return false; }
            if (texto.Substring(texto.IndexOf("@") + 1).Contains("@")) { return false; }
            return true;
        }

        /// <summary>
        /// <para>Verifica se o texto é um CNPJ válido.</para>
        /// </summary>
        /// <param name="texto">
        /// <para>Texto a ser verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna true quando o texto é válido.</para>
        /// </returns>
        public static bool CnpjValido(string texto)
        {
            if (texto == null) { return false; }
            if (Regex.Replace(texto, @"([0-9]{3}|[0-9]{2})\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}-[0-9]{2}", string.Empty) != string.Empty) { return false; }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            texto = Regex.Replace(texto, @"(\.|\/|-| )", string.Empty);

            if (texto.Length != 14) { return false; }

            tempCnpj = texto.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }
            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }
            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();

            return texto.EndsWith(digito);
        }

        /// <summary>
        /// <para>Verifica se o texto é numérico, independente do tamanho (Length) do texto.</para>
        /// </summary>
        /// <param name="texto">
        /// <para>Texto a ser verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna true quando o texto é válido.</para>
        /// </returns>
        public static bool Numerico(string texto)
        {
            if (texto == null) { return false; }
            foreach (char c in texto)
            {
                if (!char.IsNumber(c)) { return false; }
            }
            return true;
        }
    }
}
