using System.Collections.Generic;
using System.Xml;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Interface para classe capaz de manipular arquivos XML.</para>
    /// </summary>
    public interface IManipuladorXml
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência ao documento Xml que está sendo manipulado.</para>
        /// </summary>
        XmlDocument Xml { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para o formatador que rege o modo como o texto deve ser formatado.</para>
        /// </summary>
        IFormatador<string> Formatador { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para o comparador que rege o modo como o texto deve ser comparado.</para>
        /// </summary>
        IComparador<string> Comparador { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> faz com que o formatador seja aplicado 
        /// sempre que algum valor for lido.</para>
        /// </summary>
        bool AoLerValorFormatarAutomaticamente { get; set; }

        /// <summary>
        /// <para>Cria a estrutura de tags informadas no caminho de tags.</para>
        /// <para>Depois da execução deste método todas as tags já estarão acessíveis.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        string CriarTags(string caminhoDeTags);

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
        IList<XmlNode> ObterTags(string caminhoDeTags, IXmlCriterios criterios);

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
        IList<XmlAttribute> ObterAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios);

        /// <summary>
        /// <para>Obtem o <see cref="XmlNode"/> a partir de um caminho de tags.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o <see cref="XmlNode"/>.</para>
        /// </returns>
        XmlNode ObterTag(string caminhoDeTags);

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
        XmlAttribute ObterAtributo(string atributo, string caminhoDeTags);

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
        bool EscreverValores(string valor, bool adicionarAoFinal, string caminhoDeTags, IXmlCriterios criterios);

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
        /// <returns>
        /// <para>Retorna <c>true</c> quando alguma alteração é feita.</para>
        /// </returns>
        bool EscreverAtributos(string valor, string atributo, string caminhoDeTags, IXmlCriterios criterios);

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
        bool ApagarTags(string caminhoDeTags, IXmlCriterios criterios);

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
        bool ApagarAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios);

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
        IList<string> LerValores(string caminhoDeTags, IXmlCriterios criterios);

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
        IList<string> LerAtributos(string atributo, string caminhoDeTags, IXmlCriterios criterios);

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
        string LerValor(string caminhoDeTags, IXmlCriterios criterios);

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
        string LerAtributo(string atributo, string caminhoDeTags, IXmlCriterios criterios);

    }
}
