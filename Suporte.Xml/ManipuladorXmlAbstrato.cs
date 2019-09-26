using System;
using System.Collections.Generic;
using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Classe abstrata capaz de manipular arquivos XML.</para>
    /// </summary>
    public abstract class ManipuladorXmlAbstrato : IManipuladorXml
    {
        /// <summary>
        /// <para>Evento disparado quando um valor do documento Xml 
        /// é formatador automaticamente.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoFormatarAutomaticamente;

        /// <summary>
        /// <para>Evento disparado quando ocorre alguma alteração no documento Xml.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoAlterar;

        /// <summary>
        /// <para>Dispara o evento <see cref="QuandoAlterar"/>.</para>
        /// <para>Trata-se de uma maneira de disparar um evento desta classe por classes filhas.</para>
        /// </summary>
        protected void DispararEventoQuandoAlterar()
        {
            if (QuandoAlterar != null)
            {
                QuandoAlterar(this, new EventArgs());
            }
        }

        /// <summary>
        /// <para>Esta variável armazena o valor fonte da propriedade <see cref="formatador"/>.
        /// Somente uma classe derivada pode modificar este valor.</para>
        /// </summary>
        protected IFormatador<string> formatador = new FormatadorGenerico<string>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para o formatador que rege o modo como o texto deve ser formatado.</para>
        /// </summary>
        public IFormatador<string> Formatador
        {
            get { return formatador; }
        }

        /// <summary>
        /// <para>Esta variável armazena o valor fonte da propriedade <see cref="comparador"/>.
        /// Somente uma classe derivada pode modificar este valor.</para>
        /// </summary>
        protected IComparador<string> comparador = new ComparadorGenerico<string>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para o comparador que rege o modo como o texto deve ser comparado.</para>
        /// </summary>
        public IComparador<string> Comparador
        {
            get { return comparador; }
        }

        private bool aoLerValorFormatarAutomaticamente = false;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> faz com que o formatador seja aplicado 
        /// sempre que algum valor for lido.</para>
        /// </summary>
        public bool AoLerValorFormatarAutomaticamente
        {
            get { return aoLerValorFormatarAutomaticamente; }
            set { aoLerValorFormatarAutomaticamente = value; }
        }

        /// <summary>
        /// <para>Método usado para ler e formatar um valor do documento Xml.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>O valor que será lido.</para>
        /// <para>Se a propriedade <see cref="AoLerValorFormatarAutomaticamente"/> for 
        /// igual a <c>true</c> o valor lido é automaticamente formatado.
        /// Neste momento é disparado os eventos <see cref="QuandoFormatarAutomaticamente"/>
        /// e <see cref="QuandoAlterar"/>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o valor lido sem formatação.</para>
        /// </returns>
        protected virtual string LerEFormatarValor(ref string valor)
        {
            if (AoLerValorFormatarAutomaticamente)
            {
                string valorOriginal = valor;
                valor = Formatador.Formatar(true, valor);
            }
            return Formatador.Formatar(false, valor);
        }

        #region IManipuladorXml Members

        /// <summary>
        /// <para>Esta variável armazena o valor fonte da propriedade <see cref="Xml"/>.
        /// Somente uma classe derivada pode modificar este valor.</para>
        /// </summary>
        protected XmlDocument xml = new XmlDocument();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência ao documento Xml que está sendo manipulado.</para>
        /// </summary>
        public XmlDocument Xml
        {
            get
            {
                return xml;
            }
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
        public abstract string CriarTags(string caminhoDeTags);

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
        public abstract IList<XmlNode> ObterTags(string caminhoDeTags, IXmlCriterios criterios);

        /// <summary>
        /// <para>Obtem uma lista de <see cref="XmlAttribute"/> a partir de um caminho de tags.</para>
        /// </summary>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Lista de <see cref="XmlAttribute"/>.</para>
        /// </returns>
        public virtual IList<XmlAttribute> ObterAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios)
        {
            IList<XmlAttribute> atributos = new List<XmlAttribute>();
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                atributos.Add(node.Attributes[atributo]);
            }
            return atributos;
        }

        /// <summary>
        /// <para>Obtem o <see cref="XmlNode"/> a partir de um caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o <see cref="XmlNode"/>.</para>
        /// </returns>
        public virtual XmlNode ObterTag(string caminhoDeTags)
        {
            IList<XmlNode> nodes = ObterTags(caminhoDeTags, null);
            if (nodes.Count == 1)
            {
                return nodes[0];
            }
            else
            {
                throw new XmlException("O caminho de tags deve referenciar uma e somente uma tag.");
            }
        }

        /// <summary>
        /// <para>Obtem o <see cref="XmlAttribute"/> a partir de um caminho de tags.</para>
        /// </summary>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o <see cref="XmlAttribute"/>.</para>
        /// </returns>
        public virtual XmlAttribute ObterAtributo(string atributo, string caminhoDeTags)
        {
            IList<XmlAttribute> atributos = ObterAtributos(atributo, caminhoDeTags, null);
            if (atributos.Count == 1)
            {
                return atributos[0];
            }
            else
            {
                throw new XmlException("O caminho de tags deve referenciar um e somente um atributo.");
            }
        }

        /// <summary>
        /// <para>Escreve um valor dentro de uma ou mais tags Xml.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor que será escrito.</para>
        /// </param>
        /// <param name="adicionarAoFinal">
        /// <para>Quando igual a <c>true</c>, não apaga o valor atual da tag, 
        /// adicionar o valor no final do conteúdo.</para>
        /// <para>Quando igual a <c>false</c>, substitui todo o conteúdo atual 
        /// pelo valor informado.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando alguma alteração é feita.</para>
        /// </returns>
        public virtual bool EscreverValores(string valor, bool adicionarAoFinal, string caminhoDeTags, IXmlCriterios criterios)
        {
            bool alteracao = false;
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                node.InnerXml = Formatador.Formatar(true, (adicionarAoFinal ? node.InnerXml : string.Empty) + valor);
                alteracao = true;
            }
            if (alteracao)
            {
                DispararEventoQuandoAlterar();
            }
            return alteracao;
        }

        /// <summary>
        /// <para>Escreve um valor dentro de uma ou mais atributos de uma ou mais tags Xml.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor que será escrito.</para>
        /// </param>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        public virtual bool EscreverAtributos(string valor, string atributo, string caminhoDeTags, IXmlCriterios criterios)
        {
            bool alteracao = false;
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                XmlAttribute xmlAttribute = node.Attributes[atributo];
                if (xmlAttribute == null)
                {
                    xmlAttribute = Xml.CreateAttribute(atributo);
                    node.Attributes.Append(xmlAttribute);
                }
                xmlAttribute.Value = Formatador.Formatar(true, valor);
                alteracao = true;
            }
            if (alteracao)
            {
                DispararEventoQuandoAlterar();
            }
            return alteracao;
        }

        /// <summary>
        /// <para>Apaga as tags Xml referenciadas no caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando alguma alteração é feita.</para>
        /// </returns>
        public virtual bool ApagarTags(string caminhoDeTags, IXmlCriterios criterios)
        {
            bool alteracao = false;
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                node.ParentNode.RemoveChild(node);
                alteracao = true;
            }
            if (alteracao)
            {
                DispararEventoQuandoAlterar();
            }
            return alteracao;
        }

        /// <summary>
        /// <para>Apaga os atributos das tags Xml referenciadas no caminho de tags.</para>
        /// </summary>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando alguma alteração é feita.</para>
        /// </returns>
        public virtual bool ApagarAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios)
        {
            bool alteracao = false;
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                XmlAttribute xmlAttribute = node.Attributes[atributo];
                if (xmlAttribute != null)
                {
                    node.Attributes.Remove(xmlAttribute);
                }
                alteracao = true;
            }
            if (alteracao)
            {
                DispararEventoQuandoAlterar();
            }
            return alteracao;
        }

        /// <summary>
        /// <para>Lê todos os valores contidos dentro das tags Xml referenciadas no caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Lista de string com os valores lidos.</para>
        /// </returns>
        public virtual IList<string> LerValores(string caminhoDeTags, IXmlCriterios criterios)
        {
            IList<string> valores = new List<string>();
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                string valor = node.InnerXml;
                valores.Add(LerEFormatarValor(ref valor));
                if (node.InnerXml != valor)
                {
                    node.InnerXml = valor;
                    DispararEventoQuandoAlterar();
                    if (QuandoFormatarAutomaticamente != null)
                    {
                        QuandoFormatarAutomaticamente(this, new EventArgs());
                    }
                }
            }
            return valores;
        }

        /// <summary>
        /// <para>Lê todos os valores dos atributos das tags Xml referenciadas no caminho de tags.</para>
        /// </summary>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Lista de string com os valores dos atributos lidos.</para>
        /// </returns>
        public virtual IList<string> LerAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios)
        {
            IList<string> atributos = new List<string>();
            foreach (XmlNode node in ObterTags(caminhoDeTags, criterios))
            {
                XmlAttribute xmlAttribute = node.Attributes[atributo];
                if (xmlAttribute == null)
                {
                    atributos.Add(null);
                }
                else
                {
                    string valor = xmlAttribute.Value;
                    atributos.Add(LerEFormatarValor(ref valor));
                    if (xmlAttribute.Value != valor)
                    {
                        xmlAttribute.Value = valor;
                        DispararEventoQuandoAlterar();
                        if (QuandoFormatarAutomaticamente != null)
                        {
                            QuandoFormatarAutomaticamente(this, new EventArgs());
                        }
                    }
                }
            }
            return atributos;
        }

        /// <summary>
        /// <para>Lê o valor contido dentro de uma tag Xml referenciada no caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>String com o valor lido.</para>
        /// </returns>
        public virtual string LerValor(string caminhoDeTags, IXmlCriterios criterios)
        {
            IList<XmlNode> nodes = ObterTags(caminhoDeTags, criterios);
            if (nodes.Count == 1)
            {
                string valor = nodes[0].InnerXml;
                string valorLido = LerEFormatarValor(ref valor);
                if (nodes[0].InnerXml != valor)
                {
                    nodes[0].InnerXml = valor;
                    DispararEventoQuandoAlterar();
                    if (QuandoFormatarAutomaticamente != null)
                    {
                        QuandoFormatarAutomaticamente(this, new EventArgs());
                    }
                }
                return valorLido;
            }
            else
            {
                throw new XmlException("O caminho de tags deve referenciar uma e somente uma tag.");
            }
        }

        /// <summary>
        /// <para>Lê o valor do atributo de uma tag Xml referenciada no caminho de tags.</para>
        /// </summary>
        /// <param name="atributo">
        /// <para>Nome do atributo.</para>
        /// </param>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="criterios">
        /// <para>Critérios a serem usados para filtrar o resultado de tags obtidas.</para>
        /// <para>Pode ser informado como <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>String com o valor do atributo lido.</para>
        /// </returns>
        public virtual string LerAtributo(string atributo, string caminhoDeTags, IXmlCriterios criterios)
        {
            IList<XmlAttribute> atributos = ObterAtributos(atributo, caminhoDeTags, criterios);
            if (atributos.Count == 1)
            {
                string valor = atributos[0].Value;
                string valorLido = LerEFormatarValor(ref valor);
                if (atributos[0].Value != valor)
                {
                    atributos[0].Value = valor;
                    DispararEventoQuandoAlterar();
                    if (QuandoFormatarAutomaticamente != null)
                    {
                        QuandoFormatarAutomaticamente(this, new EventArgs());
                    }
                }
                return valorLido;
            }
            else
            {
                throw new XmlException("O caminho de tags deve referenciar um e somente um atributo.");
            }
        }

        #endregion

    }
}
