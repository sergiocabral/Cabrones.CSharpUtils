using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Verefica se um <see cref="XmlNode"/> possui um atributo igual a determinado valor.</para>
    /// </summary>
    public class XmlCriterioAtributoIgual : IXmlCriterio
    {
        string tag;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome da tag.</para>
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        string atributo;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome do atributo.</para>
        /// </summary>
        public string Atributo
        {
            get { return atributo; }
            set { atributo = value; }
        }

        string valor;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor do atributo.</para>
        /// </summary>
        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// <para>O critério de pesquisa utilizado aqui é:
        /// Quando o Atributo <paramref name="atributo"/>
        /// da Tag <paramref name="tag"/> 
        /// possuir o Valor <paramref name="valor"/>.</para>
        /// </summary>
        /// <param name="tag"><para>Nome da tag.</para></param>
        /// <param name="atributo"><para>Nome do atributo.</para></param>
        /// <param name="valor"><para>Valor do atributo.</para></param>
        public XmlCriterioAtributoIgual(string tag, string atributo, string valor)
        {
            Tag = tag;
            Atributo = atributo;
            Valor = valor;
        }

        #region IXmlCriterio Members

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
        public bool CriterioEAtendido(XmlNode node, IFormatador<string> formatador, IComparador<string> comparador)
        {
            if (comparador.SaoIguais(formatador, node.Name, Tag))
            {
                foreach (XmlAttribute xmlAttribute in node.Attributes)
                {
                    if (comparador.SaoIguais(formatador, xmlAttribute.Name, Atributo))
                    {
                        if (comparador.SaoIguais(formatador, xmlAttribute.Value, Valor))
                        {
                            return true;
                        }
                        break;
                    }
                }
                return false;
            }
            return true;
        }

        #endregion

    }
}
