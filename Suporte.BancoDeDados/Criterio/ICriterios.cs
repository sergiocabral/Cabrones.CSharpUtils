using System.Collections.Generic;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Interface para classe que implemente um
    /// critérios para consulta num comando de banco de dados SQL.</para>
    /// </summary>
    public interface ICriterios
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
        /// </summary>
        ICriterioFiltro Filtro { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério de ordenação para consulta num comando de banco de dados SQL.</para>
        /// </summary>
        ICriterioOrdenacao Ordenacao { get; }
    }
}
