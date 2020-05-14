namespace Cabrones.Utils.Security
{
    /// <summary>
    ///     Modos possíveis do texto puro que representa o mapa de permissões.
    /// </summary>
    public enum PermissionMapCharset
    {
        /// <summary>
        ///     Especifica um texto customizado.
        /// </summary>
        Custom,

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
        ///     Texto com caracteres ASCII visíveis.
        /// </summary>
        VisibleAscii,

        /// <summary>
        ///     Texto como caracteres Unicode até 1 byte de tamanho.
        /// </summary>
        UnicodeUpTo1BytesInSize,

        /// <summary>
        ///     Texto como caracteres Unicode até 2 bytes de tamanho.
        /// </summary>
        UnicodeUpTo2BytesInSize,

        /// <summary>
        ///     Texto como caracteres Unicode até 3 bytes de tamanho.
        /// </summary>
        UnicodeUpTo3BytesInSize,

        /// <summary>
        ///     Texto como caracteres Unicode até 4 bytes de tamanho.
        /// </summary>
        UnicodeUpTo4BytesInSize
    }
}