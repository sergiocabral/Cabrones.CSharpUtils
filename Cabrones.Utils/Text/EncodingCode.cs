using System.Text;

namespace Cabrones.Utils.Text
{
    /// <summary>
    ///     Agrupa informações sobre um código de um Encoding
    /// </summary>
    public class EncodingCode
    {
        /// <summary>
        ///     Construtor.
        /// </summary>
        /// <param name="encoding">Encoding.</param>
        /// <param name="code">Código.</param>
        public EncodingCode(Encoding encoding, int code)
        {
            Code = code;
            EncodingName = encoding.WebName;
            Text = char.ConvertFromUtf32(code);
            Size = encoding.GetByteCount(Text);
        }

        /// <summary>
        ///     Código numérico.
        /// </summary>
        public int Code { get; }

        /// <summary>
        ///     Nome do Encoding.
        /// </summary>
        public string EncodingName { get; }

        /// <summary>
        ///     Texto.
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     Tamanho.
        /// </summary>
        public int Size { get; }
    }
}