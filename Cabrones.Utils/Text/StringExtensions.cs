using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cabrones.Utils.Text
{
    /// <summary>
    ///     Extensões relacionadas com: string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converte um texto para o formato slug: minúscula, sem acento, espaço e caracteres especiais.
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>Texto como slug.</returns>
        public static string ToSlug(this string text)
        {
            // Remove acento.
            text = text.RemoveAccent();

            // Minúsculo
            text = text.ToLower();

            // Troca espaços por traço.   
            text = Regex.Replace(text, @"(\s+|-+)", "-"); // hyphens   

            // Mantem apenas: letras, números e traços. Remove traços iniciais e finais.           
            text = Regex.Replace(text, @"([^a-z0-9\s-]|^-|-$)", string.Empty);

            return text;
        }

        /// <summary>
        ///     Remove acentos de um texto.
        /// </summary>
        /// <param name="text">Texto.</param>
        /// <returns>Mesmo texto sem acentos.</returns>
        public static string RemoveAccent(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var character in
                from character in normalizedString
                let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character)
                where unicodeCategory != UnicodeCategory.NonSpacingMark
                select character)
                stringBuilder.Append(character);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}