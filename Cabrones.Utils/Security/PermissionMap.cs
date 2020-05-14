using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabrones.Utils.Math;
using Cabrones.Utils.Text;

namespace Cabrones.Utils.Security
{
    /// <summary>
    ///     Faz um mapeamento de permissões e securables para string, e vice-versa
    /// </summary>
    public class PermissionMap<TSecurable, TPermission>
        where TSecurable : notnull
        where TPermission : notnull
    {
        /// <summary>
        ///     Tamanho máximo do bloco binário.
        /// </summary>
        private const int ChunkBinarySize = sizeof(long) * 8;

        /// <summary>
        ///     Comprimento máximo do bloco.
        /// </summary>
        private readonly int _chunkSize;

        /// <summary>
        ///     Comprimento máximo do mapa.
        /// </summary>
        private readonly int _mapSize;

        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        public PermissionMap(
            IEnumerable<TSecurable> securables,
            IEnumerable<TPermission> permissions,
            PermissionMapCharset charset) :
            this(securables, permissions, charset, null)
        {
        }

        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="charset">Texto customizado para o mapa de permissões.</param>
        public PermissionMap(
            IEnumerable<TSecurable> securables,
            IEnumerable<TPermission> permissions,
            string charset) :
            this(securables, permissions, PermissionMapCharset.Custom, charset)
        {
        }

        /// <summary>
        ///     Construtor privado.
        /// </summary>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        /// <param name="charsetValue">Texto customizado para o mapa de permissões.</param>
        private PermissionMap(
            IEnumerable<TSecurable> securables,
            IEnumerable<TPermission> permissions,
            PermissionMapCharset charset,
            string? charsetValue)
        {
            Permissions = permissions
                .OrderBy(a => $"{a}")
                .ToArray();

            Securables = securables
                .OrderBy(a => $"{a}")
                .ToArray();

            Charset = charset;

            var unicodeUpToBytesInSize = 1;

            switch (charset)
            {
                case PermissionMapCharset.Binary:
                    CharsetValue = "01".ToCharArray();
                    break;
                case PermissionMapCharset.Octal:
                    CharsetValue = "01234567".ToCharArray();
                    break;
                case PermissionMapCharset.Decimal:
                    CharsetValue = "0123456789".ToCharArray();
                    break;
                case PermissionMapCharset.Hexadecimal:
                    CharsetValue = "0123456789abcdef".ToCharArray();
                    break;
                case PermissionMapCharset.Letters:
                    CharsetValue = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
                    break;
                case PermissionMapCharset.NumbersAndLetters:
                    CharsetValue = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
                    break;
                case PermissionMapCharset.LettersCaseSensitive:
                    CharsetValue = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    break;
                case PermissionMapCharset.NumbersAndLettersCaseSensitive:
                    CharsetValue = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    break;
                case PermissionMapCharset.VisibleAscii:
                    CharsetValue = Encoding
                        .ASCII
                        .GetAllEncodedStrings()
                        .Where(a =>
                            a.Value.Code > 32 &&
                            a.Value.Code < 127)
                        .Select(a => a.Value.Text[0])
                        .ToArray();
                    break;
                case PermissionMapCharset.UnicodeUpTo4BytesInSize:
                    unicodeUpToBytesInSize = 4;
                    goto case PermissionMapCharset.UnicodeUpTo1BytesInSize;
                case PermissionMapCharset.UnicodeUpTo3BytesInSize:
                    unicodeUpToBytesInSize = 3;
                    goto case PermissionMapCharset.UnicodeUpTo1BytesInSize;
                case PermissionMapCharset.UnicodeUpTo2BytesInSize:
                    unicodeUpToBytesInSize = 2;
                    goto case PermissionMapCharset.UnicodeUpTo1BytesInSize;
                case PermissionMapCharset.UnicodeUpTo1BytesInSize:
                    CharsetValue = Encoding
                        .UTF8
                        .GetAllEncodedStrings()
                        .Where(a =>
                            a.Value.Code >= 32 &&
                            a.Value.Size >= 1 &&
                            a.Value.Size <= unicodeUpToBytesInSize)
                        .Select(a => a.Value.Text[0])
                        .ToArray();
                    break;
                case PermissionMapCharset.Custom:
                    if (charsetValue == null || charsetValue.Length < 2)
                        throw new ArgumentNullException(nameof(charset));

                    var charsetValueArray = charsetValue.ToCharArray();

                    if (charsetValueArray.Distinct().Count() != charsetValue.Length)
                        throw new ArgumentException($"Duplications found in the {nameof(charset)}.");

                    CharsetValue = charsetValueArray;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(charset), charset, null);
            }

            var maxValueBinary = new string('1', ChunkBinarySize);
            var maxValueInteger = Convert.ToUInt64(maxValueBinary, 2);
            var maxValue = maxValueInteger.ConvertToNumericBase(CharsetValue);
            _chunkSize = maxValue.Length;
            _mapSize = maxValue.Length * (int) System.Math.Ceiling(
                Permissions.Length * Securables.Length / (double) maxValueBinary.Length);
        }

        /// <summary>
        ///     Modo do texto puro que representa o mapa de permissões.
        /// </summary>
        public PermissionMapCharset Charset { get; }

        /// <summary>
        ///     Texto que representa o mapa de permissões.
        /// </summary>
        public char[] CharsetValue { get; }

        /// <summary>
        ///     Lista de todos os itens do sistema que tem permissão associada.
        /// </summary>
        public TSecurable[] Securables { get; }

        /// <summary>
        ///     Lista de todas as permissões possíveis.
        /// </summary>
        public TPermission[] Permissions { get; }

        /// <summary>
        ///     Gera o mapa.
        /// </summary>
        /// <returns>Mapa de bits.</returns>
        public string Generate(IDictionary<TSecurable, IEnumerable<TPermission>> securableAndPermissions)
        {
            var bits = new char[Permissions.Length * Securables.Length];

            var index = 0;
            foreach (var securable in Securables)
            {
                var securableIsPresent = securableAndPermissions.ContainsKey(securable);
                foreach (var permission in Permissions)
                {
                    var permissionIsPresent =
                        securableIsPresent &&
                        securableAndPermissions[securable].Contains(permission);

                    bits[index++] = permissionIsPresent ? '1' : '0';
                }
            }

            return ConvertFromBinary(bits);
        }

        /// <summary>
        ///     Restaura as permissões a partir do mapa.
        /// </summary>
        /// <param name="map">Mapa de bits.</param>
        /// <returns>Estrutura de permissões.</returns>
        public IDictionary<TSecurable, IEnumerable<TPermission>> Restore(string map)
        {
            var securableAndPermissions = new Dictionary<TSecurable, IEnumerable<TPermission>>();

            var bits = ConvertToBinary(map);

            var index = 0;
            foreach (var securable in Securables)
            foreach (var permission in Permissions)
            {
                if (bits[index++] != '1') continue;

                if (!securableAndPermissions.ContainsKey(securable))
                    securableAndPermissions[securable] = new List<TPermission>();

                ((List<TPermission>) securableAndPermissions[securable]).Add(permission);
            }


            return securableAndPermissions;
        }

        /// <summary>
        ///     Converte o mapa de saída para o mapa binário.
        /// </summary>
        /// <param name="map">Mapa de saída.</param>
        /// <returns>Mapa de bits.</returns>
        private char[] ConvertToBinary(string map)
        {
            var missingPadding = _mapSize - map.Length;
            map = new string(CharsetValue[0], missingPadding) + map;

            var chunks = Enumerable
                .Range(0, map.Length / _chunkSize)
                .Select(i => map.Substring(i * _chunkSize, _chunkSize))
                .ToArray();

            var chunksDecimal = chunks
                .Select(a => a.ConvertFromNumericBase(CharsetValue))
                .ToArray();

            var chunksBinary = chunksDecimal
                .Select(a => Convert
                    .ToString((long) a, 2)
                    .PadLeft(ChunkBinarySize, '0'))
                .ToArray();

            var bitsAsText = string.Join(string.Empty, chunksBinary);

            var extraPadding = bitsAsText.Length - Permissions.Length * Securables.Length;
            bitsAsText = bitsAsText.Substring(extraPadding);

            var bits = bitsAsText.ToCharArray();

            return bits;
        }

        /// <summary>
        ///     Converte o mapa binário para o mapa de saída.
        /// </summary>
        /// <param name="bits">Mapa de bits.</param>
        /// <returns>Mapa de saída.</returns>
        private string ConvertFromBinary(char[] bits)
        {
            var bitAsText = new string(bits);

            var missingPadding = ChunkBinarySize - bitAsText.Length % ChunkBinarySize;
            bitAsText = new string('0', missingPadding) + bitAsText;

            var chunksBinary = Enumerable
                .Range(0, (int) System.Math.Ceiling((double) bitAsText.Length / ChunkBinarySize))
                .Select(a => bitAsText.Substring(a * ChunkBinarySize, ChunkBinarySize))
                .ToArray();

            var chunksDecimal = chunksBinary
                .Select(a => (ulong) Convert.ToInt64(a, 2))
                .ToArray();

            var chunks = chunksDecimal
                .Select(a => a
                    .ConvertToNumericBase(CharsetValue)
                    .PadLeft(_chunkSize, CharsetValue[0]))
                .ToArray();

            var map = string.Join(string.Empty, chunks);

            var i = 0;
            while (i < map.Length && map[i] == CharsetValue[0]) i++;

            map = map.Substring(i);

            return map;
        }
    }
}