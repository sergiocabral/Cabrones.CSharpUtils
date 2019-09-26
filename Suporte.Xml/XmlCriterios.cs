using System.Collections.Generic;
using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Classe capaz de agrupar diversos critérios (<see cref="IXmlCriterio"/>)
    /// para serem aplicados a um <see cref="XmlNode"/>.</para>
    /// </summary>
    public class XmlCriterios : IXmlCriterios
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="criterios">
        /// <para>Lista de critérios.</para>
        /// </param>
        public XmlCriterios(params IXmlCriterio[] criterios)
        {
            ((List<IXmlCriterio>)Criterios).AddRange(criterios);
        }

        #region IXmlCriterios Members

        private IList<IXmlCriterio> criterios = new List<IXmlCriterio>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista de critérios.</para>
        /// </summary>
        public IList<IXmlCriterio> Criterios
        {
            get
            {
                return criterios;
            }
        }

        /// <summary>
        /// <para>Responsável por informar se um <see cref="XmlNode"/>
        /// atende a todos os critério contidos na lista <see cref="Criterios"/>.</para>
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
        public bool CriteriosSaoAtendidos(XmlNode node, IFormatador<string> formatador, IComparador<string> comparador)
        {
            foreach (IXmlCriterio criterio in Criterios)
            {
                if (!criterio.CriterioEAtendido(node, formatador, comparador))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
