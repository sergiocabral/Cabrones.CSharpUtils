using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabrones.Utils.Text;

namespace Cabrones.Utils.Security
{
    /// <summary>
    ///     Faz um mapeamento de permissões e securables para string, e vice-versa
    /// </summary>
    public class PermissionsMap<TPermission, TSecurable>
    {
        /// <summary>
        ///     Modos possíveis do texto puro que representa o mapa de permissões.
        /// </summary>
        public enum Charset
        {
            /// <summary>
            ///     Texto como número binário.
            /// </summary>
            Binary,

            /// <summary>
            ///     Texto como número octal.
            /// </summary>
            Octal,

            /// <summary>
            ///     Texto como número decimal.
            /// </summary>
            Decimal,

            /// <summary>
            ///     Texto como número hexadecimal.
            /// </summary>
            Hexadecimal,

            /// <summary>
            ///     Texto como caracteres de A-Z.
            /// </summary>
            Letters,

            /// <summary>
            ///     Texto como caracteres de 0-9 e A-Z.
            /// </summary>
            NumbersAndLetters,

            /// <summary>
            ///     Texto como caracteres de A-Z e a-z.
            /// </summary>
            LettersCaseSensitive,

            /// <summary>
            ///     Texto como caracteres de 0-9, A-Z e a-z.
            /// </summary>
            NumbersAndLettersCaseSensitive,

            /// <summary>
            ///     Texto como caracteres ASCII.
            /// </summary>
            Ascii,

            /// <summary>
            ///     Texto como caracteres Unicode visíveis.
            /// </summary>
            Unicode,

            /// <summary>
            ///     Especifica um texto customizado.
            /// </summary>
            Custom
        }

        /// <summary>
        ///     Modo do texto puro que representa o mapa de permissões.
        /// </summary>
        private readonly Charset _charset;

        /// <summary>
        ///     Texto que representa o mapa de permissões.
        /// </summary>
        private readonly string _charsetValue;

        /// <summary>
        ///     Lista de todas as permissões possíveis.
        /// </summary>
        private readonly IEnumerable<TPermission> _permissions;

        /// <summary>
        ///     Lista de todos os itens do sistema que tem permissão associada.
        /// </summary>
        private readonly IEnumerable<TSecurable> _securables;

        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        public PermissionsMap(
            IEnumerable<TPermission> permissions,
            IEnumerable<TSecurable> securables,
            Charset charset = Charset.NumbersAndLettersCaseSensitive) :
            this(permissions, securables, charset, null)
        {
        }

        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Texto customizado para o mapa de permissões.</param>
        public PermissionsMap(
            IEnumerable<TPermission> permissions,
            IEnumerable<TSecurable> securables,
            string charset) :
            this(permissions, securables, Charset.Custom, charset)
        {
        }

        /// <summary>
        ///     Construtor privado.
        /// </summary>
        /// <param name="permissions">Lista de todas as permissões possíveis.</param>
        /// <param name="securables">Lista de todos os itens do sistema que tem permissão associada.</param>
        /// <param name="charset">Modos possíveis do texto puro que representa o mapa de permissões.</param>
        /// <param name="charsetValue">Texto customizado para o mapa de permissões.</param>
        private PermissionsMap(
            IEnumerable<TPermission> permissions,
            IEnumerable<TSecurable> securables,
            Charset charset,
            string? charsetValue)
        {
            _permissions = permissions;
            _securables = securables;
            _charset = charset;

            switch (charset)
            {
                case Charset.Binary:
                    _charsetValue = "01";
                    break;
                case Charset.Octal:
                    _charsetValue = "01234567";
                    break;
                case Charset.Decimal:
                    _charsetValue = "0123456789";
                    break;
                case Charset.Hexadecimal:
                    _charsetValue = "0123456789abcdef";
                    break;
                case Charset.Letters:
                    _charsetValue = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case Charset.NumbersAndLetters:
                    _charsetValue = "0123456789abcdefghijklmnopqrstuvwxyz";
                    break;
                case Charset.LettersCaseSensitive:
                    _charsetValue = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case Charset.NumbersAndLettersCaseSensitive:
                    _charsetValue = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case Charset.Ascii:
                    _charsetValue =
                        @"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
                    break;
                case Charset.Unicode:
                    _charsetValue = new string(Encoding
                        .UTF8
                        .GetAllEncodedStrings()
                        .Where(a => a.Value.Length == 1)
                        .Select(a => a.Value[0])
                        .ToArray());
                    break;
                case Charset.Custom:
                    if (charsetValue == null || charsetValue.Length < 2)
                        throw new ArgumentNullException(nameof(charset));
                    _charsetValue = charsetValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(charset), charset, null);
            }
        }
    }
}