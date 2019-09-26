namespace Suporte.Xml
{
    /// <summary>
    /// <para>Interface para classe que armazena a referência para uma tag
    /// num documento Xml.</para>
    /// </summary>
    public interface IXmlTagInfo
    {
        /// <summary>
        /// <para>Lê o caminho de tags e define as propriedades desta classe.</para>
        /// </summary>
        /// <param name="caminhoDeTags">
        /// <para>Caminho de tags para o documento Xml.</para>
        /// </param>
        /// <param name="modo">
        /// <para>Informa os modos permitidos para o caminho de tags a partir desta tag.</para>
        /// </param>
        void Carregar(string caminhoDeTags, EnumXmlTagModo modo);

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Descreve os modos permitidos para o caminho de tags a partir desta tag.</para>
        /// </summary>
        EnumXmlTagModo Modo { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome desta tag.</para>
        /// </summary>
        string NomeDaTag { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista dos índices (base zero) informando as posições da tag.</para>
        /// <para>Quando o valor é um array vazio (<c>new uint[] { }</c>) indica que todas as tags foram referenciadas.</para>
        /// <para>Quando o valor é <c>null</c> indica que a tag não existe e será criada.</para>
        /// </summary>
        uint[] Indices { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista dos índices (base zero) informando as posições da tag.</para>
        /// <para>Esta propriedade usa texto livre para receber os índices.</para>
        /// </summary>
        string IndicesTexto { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Com base nos atributos <see cref="NomeDaTag"/> e <see cref="Indices"/>
        /// monta a exibição da referência para as tags Xml.</para>
        /// </summary>
        string ReferenciaDasTagsComIndices { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Com base nos atributos <see cref="NomeDaTag"/> e <see cref="Indices"/>
        /// monta a exibição da referência para as tags Xml.</para>
        /// <para>Será concatenado com a referência de todas as sub tags acessadas
        /// pela propriedade <see cref="ProximoXmlTagInfo"/></para>
        /// </summary>
        string ReferenciaDasTagsComIndicesCompleto { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Permite armazenar uma referência para a próxima tag Xml.</para>
        /// </summary>
        IXmlTagInfo ProximoXmlTagInfo { get; set; }

    }
}
