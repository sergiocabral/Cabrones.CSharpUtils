namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Interface para classe que implemente um
    /// critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Agrupa critérios através de operadores boleanos.</para>
    /// </summary>
    public interface ICriterioFiltroAgrupamento : ICriterioFiltro
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério 1</para>
        /// </summary>
        ICriterioFiltro Criterio1 { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério 2</para>
        /// </summary>
        ICriterioFiltro Criterio2 { get; }
    }
}
