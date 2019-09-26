using System;
using System.Web;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe extrai de um código HTML ou XML o texto real sem as tags
    /// e sem códigos para caracteres especiais.</para>
    /// </summary>
    public class FormatadorExtraiTextoDeHtml : FormatadorTransparente
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorExtraiTextoDeHtml() : base() { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorExtraiTextoDeHtml(IFormatProvider formatProvider) : base(formatProvider) { }

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
            string semHtmlTag = string.Format(new FormatadorRemoveTags(), "{0}", base.Format(format, arg, formatProvider));
            return HttpUtility.HtmlDecode(semHtmlTag);
        }

        #endregion
    }
}
