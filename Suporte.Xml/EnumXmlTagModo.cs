namespace Suporte.Xml
{
    /// <summary>
    /// <para>Descreve os modos permitidos para um caminho de tags.</para>
    /// </summary>
    public enum EnumXmlTagModo
    {
        /// <summary>
        /// <para>Permite referenciar tags únicas.</para>
        /// </summary>
        IndiceUnico = 1,
        /// <summary>
        /// <para>Permite referenciar múltiplas tags.</para>
        /// </summary>
        IndiceMultiplo = 2,
        /// <summary>
        /// <para>Permite referenciar todas as tags.</para>
        /// </summary>
        IndiceNaoInformado = 4,
        /// <summary>
        /// <para>Permite referenciar uma nova tag.</para>
        /// </summary>
        IndiceNovo = 8
    }
}
