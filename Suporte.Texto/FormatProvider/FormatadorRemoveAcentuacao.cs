using System;
using System.Text;
using System.Web;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe formata uma sequência de texto de modo que os caracteres 
    /// acentuados sejam substituidos pelo caracter correspondente, porém, sem acento.</para>
    /// </summary>
    public class FormatadorRemoveAcentuacao : FormatadorTransparente
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorRemoveAcentuacao() : base() { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorRemoveAcentuacao(IFormatProvider formatProvider) : base(formatProvider) { }

        #region ICustomFormatter Members

        /// <summary>
        /// <para>Converte o valor de um objeto especificado para um representação 
        /// de seqüência equivalente usando o formato especificado e 
        /// informações de formatação da região (culture-specific).</para>
        /// </summary>
        /// <param name="format"><para>A seqüência de formato que contém 
        /// especificações de formatação.</para></param>
        /// <param name="arg"><para>O objeto a ser formatado.</para></param>
        /// <param name="formatProvider"><para>Um objeto que fornece informações sobre 
        /// o formato da instância atual.</para></param>
        /// <returns><para>A sequência de texto <paramref name="arg"/> formatada
        /// conforme especificado pelos parâmetros <paramref name="format"/> e 
        /// <paramref name="formatProvider"/>.</para></returns>
        public override string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return HttpUtility.UrlDecode(HttpUtility.UrlEncode(base.Format(format, arg, formatProvider), Encoding.GetEncoding(28597)));
        }

        #endregion
    }
}
