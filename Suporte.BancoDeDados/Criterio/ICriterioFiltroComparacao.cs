namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Interface para classe que implemente um
    /// critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// </summary>
    public interface ICriterioFiltroComparacao : ICriterioFiltro
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </summary>
        ICampoDeBancoDeDados Campo { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor do campo.</para>
        /// </summary>
        object Valor { get; }
    }
}
