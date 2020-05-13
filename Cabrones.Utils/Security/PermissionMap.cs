using System;
using System.Collections;
using System.Linq;
using System.Text;
using Cabrones.Utils.Text;

namespace Cabrones.Utils.Security
{
    /// <summary>
    ///     Faz um mapeamento de permissões e securables para string, e vice-versa
    /// </summary>
    public class PermissionMap
    {
        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        public PermissionMap(
            IEnumerable permissions,
            IEnumerable securables,
            PermissionMapCharset charset = PermissionMapCharset.NumbersAndLettersCaseSensitive) :
            this(permissions, securables, charset, null)
        {
        }

        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Texto customizado para o mapa de permissões.</param>
        public PermissionMap(
            IEnumerable permissions,
            IEnumerable securables,
            string charset) :
            this(permissions, securables, PermissionMapCharset.Custom, charset)
        {
        }

        /// <summary>
        ///     Construtor privado.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        /// <param name="charsetValue">Texto customizado para o mapa de permissões.</param>
        private PermissionMap(
            IEnumerable permissions,
            IEnumerable securables,
            PermissionMapCharset charset,
            string? charsetValue)
        {
            Permissions = permissions;
            Securables = securables;
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
        ///     Lista de todas as permissões possíveis.
        /// </summary>
        public IEnumerable Permissions { get; }

        /// <summary>
        ///     Lista de todos os itens do sistema que tem permissão associada.
        /// </summary>
        public IEnumerable Securables { get; }
    }
}