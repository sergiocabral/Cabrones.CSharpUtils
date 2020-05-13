using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabrones.Utils.Text
{
    /// <summary>
    ///     Funcionalidades para encoding
    /// </summary>
    public static class EncodingExtensions
    {
        /// <summary>
        ///     Cache para resultados do método GetAllCodes.
        /// </summary>
        private static readonly Dictionary<string, IReadOnlyDictionary<int, string>> _cacheForGetAllCodes =
            new Dictionary<string, IReadOnlyDictionary<int, string>>();

        /// <summary>
        ///     Retorna todos os códigos e caracteres possíveis para um encoding.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        /// <param name="ranges">
        ///     Range de códigos para buscar.
        ///     Se não especificado usa o padrão para Unicode 0..0xD7FF e 0xE000..0x10FFFF.
        /// </param>
        /// <returns>Lista de caracteres.</returns>
        public static IReadOnlyDictionary<int, string> GetAllEncodedStrings(this Encoding encoding,
            params KeyValuePair<int, int>[] ranges)
        {
            encoding = Encoding.GetEncoding(
                encoding.WebName,
                new EncoderExceptionFallback(),
                new DecoderExceptionFallback());

            const int maxValue = 0x10ffff;

            if (ranges == null || ranges.Length == 0)
            {
                if (encoding.WebName == Encoding.ASCII.WebName)
                {
                    const int asciiValidLimit = 0x007F;
                    ranges = new[]
                    {
                        new KeyValuePair<int, int>(0, asciiValidLimit)
                    };
                }
                else
                {
                    const int unicodeInvalidStart = 0xD800;
                    const int unicodeInvalidEnd = 0xDFFF;
                    ranges = new[]
                    {
                        new KeyValuePair<int, int>(0, unicodeInvalidStart - 1),
                        new KeyValuePair<int, int>(unicodeInvalidEnd + 1, maxValue)
                    };
                }
            }

            var rangeHash = encoding.WebName + ranges
                .Select(a => $"{a.Key}-{a.Value}")
                .Aggregate(string.Empty, (acc, cur) => $"{acc},{cur}");

            if (_cacheForGetAllCodes.ContainsKey(rangeHash)) return _cacheForGetAllCodes[rangeHash];

            var codes = new Dictionary<int, string>();
            foreach (var range in ranges)
                for (var i = range.Key; i <= range.Value; i++)
                    codes[i] = char.ConvertFromUtf32(i);

            return _cacheForGetAllCodes[rangeHash] = codes;
        }
    }
}