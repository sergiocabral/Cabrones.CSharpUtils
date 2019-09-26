using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Interface para classe com função de validar se um <see cref="XmlNode"/>
    /// atende determinado critério de pesquisa.</para>
    /// </summary>
    public interface IXmlCriterio
    {
        /// <summary>
        /// <para>Responsável por informar se um <see cref="XmlNode"/>
        /// atende determinado critério de pesquisa.</para>
        /// </summary>
        /// <param name="node">
        /// <para><see cref="XmlNode"/> que será verificado.</para>
        /// </param>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        /// <param name="comparador">
        /// <para>Interface para o comparador que rege o modo como o texto comparado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando o critério é atendido.</para>
        /// </returns>
        bool CriterioEAtendido(XmlNode node, IFormatador<string> formatador, IComparador<string> comparador);
    }
}
