using System.Collections.Generic;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Interface para classe que implemente um
    /// critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// </summary>
    public interface ICriterioFiltro
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL para usar como critério numa consulta SQL.</para>
        /// </summary>
        string Sql { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista dos parâmetros usados no trecho de 
        /// código sql <see cref="Sql"/> e seus respectivos valores.</para>
        /// </summary>
        IDictionary<string, object> Parametros { get; }
    }
}
