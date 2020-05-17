using System.Collections;
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
        ///     Expressão regular para QueryString
        ///     Localiza trechos como {0} ou {nome}. Mas rejeita {{0}}, {{0}, {0}}, {}, {{}}
        /// </summary>
        private const string RegexForQueryString = @"(?<!\{)\{[^\{\}]+\}(?!\})";

        /// <summary>
        ///     Converte um texto para o formato slug: minúscula, sem acento, espaço e caracteres especiais.
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>Texto como slug.</returns>
        public static string ToSlug(this string text)
        {
            if (text == null) return null!;

            // Remove acento.
            text = text.RemoveAccent();

            var dontHaveSpace = !text.Contains(" ");
            if (dontHaveSpace)
            {
                var hasLowers = text.Any(a => a >= 'a' && a <= 'z');
                var hasUppers = text.Any(a => a >= 'A' && a <= 'Z');
                var hasNumbers = text.Any(a => a >= '0' && a <= '9');
                if (hasLowers && (hasUppers || hasNumbers))
                {
                    var positionsToSpace = text
                        .Select((letter, position) => new {letter, position})
                        .Where(a => a.letter >= 'A' && a.letter <= 'Z' || a.letter >= '0' && a.letter <= '9')
                        .Select(a => a.position)
                        .Reverse()
                        .ToArray();

                    var stringBuilder = new StringBuilder(text);
                    foreach (var positionToSpace in positionsToSpace) stringBuilder.Insert(positionToSpace, ' ');
                    text = stringBuilder.ToString();
                }
            }

            // Minúsculo
            text = text.ToLower();

            // Tudo que não for letras, números e traços é substituído por traço.   
            text = Regex.Replace(text, @"[^a-z0-9]+", "-"); // hyphens   

            // Remove traços das extremidades.           
            text = Regex.Replace(text, @"(^-+|-+$)", string.Empty);

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
        public static string QueryString(this string text, params object?[] args)
        {
            if (string.IsNullOrWhiteSpace(text) || args == null || args.Length == 0) return text;

            var matches = Regex.Matches(text, RegexForQueryString, RegexOptions.Singleline);
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

        /// <summary>
        ///     Substitui argumentos em uma string baseado em nomes de dicionário.
        /// </summary>
        /// <param name="text">Texto.</param>
        /// <param name="args">Argumentos.</param>
        /// <returns>Texto com argumentos substituidos.</returns>
        public static string QueryString(this string text, IDictionary args)
        {
            if (string.IsNullOrWhiteSpace(text) || args == null || args.Count == 0) return text;

            var matches = Regex.Matches(text, RegexForQueryString, RegexOptions.Singleline);
            var result = new StringBuilder(text);
            for (var i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var key = match.Value.Substring(1, match.Value.Length - 2);

                if (!args.Contains(key)) continue;

                result.Remove(match.Index, match.Length);
                result.Insert(match.Index, $"{args[key]}");
            }

            return result.ToString();
        }
    }
}