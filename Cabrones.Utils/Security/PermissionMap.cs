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
        ///     Construtor.
        /// </summary>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        public PermissionMap(
            IEnumerable<TSecurable> securables,
            IEnumerable<TPermission> permissions,
            PermissionMapCharset charset = PermissionMapCharset.NumbersAndLettersCaseSensitive) :
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

            switch (charset)
            {
                case PermissionMapCharset.Binary:
                    CharsetValue = "01";
                    break;
                case PermissionMapCharset.Octal:
                    CharsetValue = "01234567";
                    break;
                case PermissionMapCharset.Decimal:
                    CharsetValue = "0123456789";
                    break;
                case PermissionMapCharset.Hexadecimal:
                    CharsetValue = "0123456789abcdef";
                    break;
                case PermissionMapCharset.Letters:
                    CharsetValue = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case PermissionMapCharset.NumbersAndLetters:
                    CharsetValue = "0123456789abcdefghijklmnopqrstuvwxyz";
                    break;
                case PermissionMapCharset.LettersCaseSensitive:
                    CharsetValue = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case PermissionMapCharset.NumbersAndLettersCaseSensitive:
                    CharsetValue = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case PermissionMapCharset.Ascii:
                    CharsetValue = new string(Encoding
                        .ASCII
                        .GetAllEncodedStrings()
                        .Where(a =>
                            a.Key > 32 &&
                            a.Key < 127 &&
                            a.Value.Length == 1 &&
                            !string.IsNullOrWhiteSpace(a.Value))
                        .Select(a => a.Value[0])
                        .ToArray());
                    break;
                case PermissionMapCharset.Unicode:
                    CharsetValue = new string(Encoding
                        .UTF8
                        .GetAllEncodedStrings()
                        .Where(a => a.Key > 32 && a.Value.Length == 1)
                        .Select(a => a.Value[0])
                        .ToArray());
                    break;
                case PermissionMapCharset.Custom:
                    if (charsetValue == null || charsetValue.Length < 2)
                        throw new ArgumentNullException(nameof(charset));

                    if (charsetValue.ToCharArray().Distinct().Count() != charsetValue.Length)
                        throw new ArgumentException($"Duplications found in the {nameof(charset)}.");

                    CharsetValue = charsetValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(charset), charset, null);
            }

            var maxValueBinary = new string('1', ChunkBinarySize);
            var maxValueInteger = Convert.ToUInt64(maxValueBinary, 2);
            _chunkSize = maxValueInteger.ConvertToNumericBase(CharsetValue.ToCharArray()).Length;
        }

        /// <summary>
        ///     Modo do texto puro que representa o mapa de permissões.
        /// </summary>
        public PermissionMapCharset Charset { get; }

        /// <summary>
        ///     Texto que representa o mapa de permissões.
        /// </summary>
        public string CharsetValue { get; }

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
            var bits = new byte[Permissions.Length * Securables.Length];

            var index = 0;
            foreach (var securable in Securables)
            {
                var securableIsPresent = securableAndPermissions.ContainsKey(securable);
                foreach (var permission in Permissions)
                {
                    var permissionIsPresent =
                        securableIsPresent &&
                        securableAndPermissions[securable].Contains(permission);
                    bits[index] = permissionIsPresent ? (byte) 1 : (byte) 0;

                    index++;
                }
            }

            var bitsAsText = new string(bits.Select(a => $"{a}"[0]).ToArray());

            return ConvertFromBinary(bitsAsText);
        }


        /// <summary>
        ///     Converte o texto de binário para o formato de saída.
        /// </summary>
        /// <param name="bits">Mapa de bits.</param>
        /// <returns>Mapa de bits no formato esperado.</returns>
        private string ConvertFromBinary(string bits)
        {
            var missingPadding = ChunkBinarySize - bits.Length % ChunkBinarySize;
            bits = new string('0', missingPadding) + bits;

            var chunksBinary = Enumerable
                .Range(0, (int) System.Math.Ceiling((double) bits.Length / ChunkBinarySize))
                .Select(i => bits.Substring(i * ChunkBinarySize, ChunkBinarySize))
                .ToArray();

            var chunksDecimal = chunksBinary
                .Select(a => (ulong) Convert.ToInt64(a, 2))
                .ToArray();

            var chunks = chunksDecimal
                .Select(a => a
                    .ConvertToNumericBase(CharsetValue.ToCharArray())
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