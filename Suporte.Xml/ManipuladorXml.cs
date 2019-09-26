using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Classe capaz de manipular documentos XML.</para>
    /// </summary>
    /// <typeparam name="TXmlTagInfo">
    /// <para>Precisa ser um tipo de classe não abstrata com as regras que regem a escrita da referência Xml.</para>
    /// </typeparam>
    public class ManipuladorXml<TXmlTagInfo> : ManipuladorXmlAbstrato where TXmlTagInfo : IXmlTagInfo
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ManipuladorXml()
        {
            if (typeof(TXmlTagInfo).IsAbstract || !typeof(TXmlTagInfo).IsClass)
            {
                throw new XmlException("O tipo genérico TXmlTagInfo precisa ser um tipo de classe não abstrata.");
            }
            else if (ObterInstanciaDeIXmlTagInfo() == null)
            {
                throw new XmlException("O tipo genérico TXmlTagInfo precisa ser um tipo de classe com construtor simples, sem parâmetros.");
            }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ManipuladorXml(IFormatador<string> formatador)
            : this(formatador, new ComparadorGenerico<string>()) { }


        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="comparador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ManipuladorXml(IComparador<string> comparador)
            : this(new FormatadorGenerico<string>(), comparador) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        /// <param name="comparador">
        /// <para>Interface para o comparador que rege o modo como o texto comparado.</para>
        /// </param>
        public ManipuladorXml(IFormatador<string> formatador, IComparador<string> comparador)
            : this()
        {
            this.formatador = formatador;
            this.comparador = comparador;
        }

        /// <summary>
        /// <para>Cria a estrutura de tags informadas no caminho de tags.</para>
        /// <para>Depois da execução deste método todas as tags já estarão acessíveis.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o caminho de tags para o documento Xml.</para>
        /// <para>Caso existam tags com índices novos, estes serão substituidos pelo índice real.</para>
        /// </returns>
        public override string CriarTags(string caminhoDeTags)
        {
            IXmlTagInfo xmlTagInfo = ObterInstanciaDeIXmlTagInfo();
            xmlTagInfo.Carregar(caminhoDeTags, EnumXmlTagModo.IndiceUnico | EnumXmlTagModo.IndiceMultiplo | EnumXmlTagModo.IndiceNovo);
            CriarTags(Xml, xmlTagInfo, true);
            DispararEventoQuandoAlterar();
            return xmlTagInfo.ReferenciaDasTagsComIndicesCompleto;
        }

        /// <summary>
        /// <para>Obtem uma lista de <see cref="XmlNode"/> a partir de um caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Lista de <see cref="XmlNode"/>.</para>
        /// </returns>
        public override IList<XmlNode> ObterTags(string caminhoDeTags, IXmlCriterios criterios)
        {
            IXmlTagInfo xmlTagInfo = ObterInstanciaDeIXmlTagInfo();
            xmlTagInfo.Carregar(caminhoDeTags, EnumXmlTagModo.IndiceUnico | EnumXmlTagModo.IndiceMultiplo | EnumXmlTagModo.IndiceNaoInformado);
            return ObterTags(Xml, xmlTagInfo, criterios);            
        }

        /// <summary>
        /// <para>Constrói a classe do tipo TXmlTagInfo.</para>
        /// </summary>
        /// <returns>
        /// <para>Instância do tipo TXmlTagInfo que implementa a interface <see cref="IXmlTagInfo"/>.</para>
        /// </returns>
        protected IXmlTagInfo ObterInstanciaDeIXmlTagInfo()
        {
            ConstructorInfo constructorInfo = typeof(TXmlTagInfo).GetConstructor(new Type[] { });
            if (constructorInfo != null)
            {
                return (IXmlTagInfo)constructorInfo.Invoke(new Type[] { });
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// <para>Cria a estrutura de tags informadas no parâmetro <paramref name="xmlTagInfo"/>.</para>
        /// <para>Depois da execução deste método todas as tags já estarão acessíveis.</para>
        /// </summary>
        /// <param name="xmlNodeBase">
        /// <para><see cref="XmlNode"/> base de onde será iniciada a execução do método.</para>
        /// </param>
        /// <param name="xmlTagInfo">
        /// <para>Referência para uma tag num documento Xml.</para>
        /// </param>
        /// <param name="substituirIndiceNovo">
        /// <para>Quando igual a <c>true</c> substitui o índice novo pelo índice 
        /// real da tag que acabou de ser criada.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o caminho de tags para o documento Xml.</para>
        /// <para>Caso existam tags com índices novos, estes serão substituidos pelo índice real.</para>
        /// </returns>
        private void CriarTags(XmlNode xmlNodeBase, IXmlTagInfo xmlTagInfo, bool substituirIndiceNovo)
        {
            if (xmlTagInfo.Indices == null)
            {
                IXmlTagInfo xmlTagInfoLocal = ObterInstanciaDeIXmlTagInfo();
                xmlTagInfoLocal.NomeDaTag = xmlTagInfo.NomeDaTag;
                IList<XmlNode> totalDeTags = ObterTags(xmlNodeBase, xmlTagInfoLocal, null);
                if (substituirIndiceNovo)
                {
                    xmlTagInfo.Indices = new uint[] { (uint)totalDeTags.Count };
                }
                XmlNode node = xmlNodeBase.AppendChild(Xml.CreateElement(xmlTagInfo.NomeDaTag));
                if (xmlTagInfo.ProximoXmlTagInfo != null)
                {
                    CriarTags(node, xmlTagInfo.ProximoXmlTagInfo, substituirIndiceNovo);
                }
            }
            else
            {
                foreach (uint indice in xmlTagInfo.Indices)
                {
                    XmlNode node;
                    checked
                    {
                        node = ObterTagPorOcorrencia(xmlNodeBase, xmlTagInfo.NomeDaTag, (int)indice, true);
                    }
                    if (xmlTagInfo.ProximoXmlTagInfo != null)
                    {
                        CriarTags(node, xmlTagInfo.ProximoXmlTagInfo, substituirIndiceNovo && xmlTagInfo.Indices.Length == 1);
                    }
                }
            }
        }

        /// <summary>
        /// <para>Obtem uma lista de <see cref="XmlNode"/> a partir 
        /// do parâmetro <paramref name="xmlTagInfo"/>.</para>
        /// </summary>
        /// <param name="xmlNodeBase">
        /// <para><see cref="XmlNode"/> base de onde será iniciada a execução do método.</para>
        /// </param>
        /// <param name="xmlTagInfo">
        /// <para>Referência para uma tag num documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Lista de <see cref="XmlNode"/>.</para>
        /// </returns>
        private IList<XmlNode> ObterTags(XmlNode xmlNodeBase, IXmlTagInfo xmlTagInfo, IXmlCriterios criterios)
        {
            List<XmlNode> listaDeTags = new List<XmlNode>();

            if (xmlTagInfo.Indices.Length > 0)
            {
                foreach (uint indice in xmlTagInfo.Indices)
                {
                    XmlNode node;
                    checked
                    {
                        node = ObterTagPorOcorrencia(xmlNodeBase, xmlTagInfo.NomeDaTag, (int)indice, false);
                    }
                    if (node != null && (criterios == null || criterios.CriteriosSaoAtendidos(node, Formatador, Comparador)))
                    {
                        if (xmlTagInfo.ProximoXmlTagInfo == null)
                        {
                            listaDeTags.Add(node);
                        }
                        else
                        {
                            //Recursivo
                            listaDeTags.AddRange(ObterTags(node, xmlTagInfo.ProximoXmlTagInfo, criterios));
                        }
                    }
                }
            }
            else
            {
                foreach (XmlNode node in xmlNodeBase.ChildNodes)
                {
                    if (Comparador.SaoIguais(Formatador, node.Name, xmlTagInfo.NomeDaTag))
                    {
                        if (node != null && (criterios == null || criterios.CriteriosSaoAtendidos(node, Formatador, Comparador)))
                        {
                            if (xmlTagInfo.ProximoXmlTagInfo == null)
                            {
                                listaDeTags.Add(node);
                            }
                            else
                            {
                                //Recursivo
                                listaDeTags.AddRange(ObterTags(node, xmlTagInfo.ProximoXmlTagInfo, criterios));
                            }
                        }
                    }
                }
            }

            return listaDeTags;
        }

        /// <summary>
        /// <para>Obtem uma tag dentro de outra numa dada ocorrência.</para>
        /// </summary>
        /// <param name="xmlNodeBase">
        /// <para><see cref="XmlNode"/> base de onde será iniciada a execução do método.</para>
        /// </param>
        /// <param name="nomeDaTag">
        /// <para>Nome da tag que será verificada.</para>
        /// </param>
        /// <param name="ocorrencia">
        /// <para>Refere-se a ocorrência da tag após tags com o mesmo nome.</para>
        /// <para>Primeira ocorrência deve ser 0 (zero).</para>
        /// </param>
        /// <param name="forcar">
        /// <para>Quando iguai a <c>true</c> força a criação da tag caso não exista.</para>
        /// </param>
        /// <returns>
        /// <para>Tag localizada.</para>
        /// <para>Se não for localizada retorna <c>nulll</c>.</para>
        /// </returns>
        private XmlNode ObterTagPorOcorrencia(XmlNode xmlNodeBase, string nomeDaTag, int ocorrencia, bool forcar)
        {
            foreach (XmlNode node in xmlNodeBase.ChildNodes)
            {
                if (Comparador.SaoIguais(Formatador, node.Name, nomeDaTag))
                {
                    if (ocorrencia == 0)
                    {
                        return node;
                    }
                    ocorrencia--;
                }
            }
            XmlNode nodeNode = null;
            while (forcar && ocorrencia >= 0)
            {
                nodeNode = xmlNodeBase.AppendChild(Xml.CreateElement(nomeDaTag));
                ocorrencia--;
            }
            return nodeNode;
        }

    }
}
