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

        /// <summary>
        ///     Substitui argumentos em uma string.
        /// </summary>
        /// <param name="text">Texto.</param>
        /// <param name="args">Argumentos.</param>
        /// <returns>Texto com argumentos substituidos.</returns>
        public static string QueryString(this string text, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(text) || args == null || args.Length == 0) return text;

            // Localiza trechos como {0} ou {nome}. Mas rejeita {{0}}, {{0}, {0}}, {}, {{}}  
            const string regex = @"(?<!\{)\{[^\{\}]+\}(?!\})";

            var matches = Regex.Matches(text, regex, RegexOptions.Singleline);
            var result = new StringBuilder(text);
            for (var i = matches.Count - 1; i >= 0; i--)
            {
                if (i >= args.Length) continue;

                var match = matches[i];
                result.Remove(match.Index, match.Length);
                result.Insert(match.Index, $"{args[i]}");
            }

            return result.ToString();
        }
    }
}