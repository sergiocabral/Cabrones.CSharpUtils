using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critérios para consulta num comando de banco de dados SQL.</para>
    /// </summary>
    public class Criterios : ICriterios
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public Criterios() : this(null, null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="filtro">
        /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
        /// </param>
        public Criterios(ICriterioFiltro filtro) : this(filtro, null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="ordenacao">
        /// <para>Critério de ordenação para consulta num comando de banco de dados SQL.</para>
        /// </param>

        public Criterios(ICriterioOrdenacao ordenacao) : this(null, ordenacao) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="filtro">
        /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
        /// </param>
        /// <param name="ordenacao">
        /// <para>Critério de ordenação para consulta num comando de banco de dados SQL.</para>
        /// </param>
        public Criterios(ICriterioFiltro filtro, ICriterioOrdenacao ordenacao)
        {
            Filtro = filtro;
            Ordenacao = ordenacao;
        }

        #region ICriterios Members

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
        /// </summary>
        public ICriterioFiltro Filtro
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério de ordenação para consulta num comando de banco de dados SQL.</para>
        /// </summary>
        public ICriterioOrdenacao Ordenacao
        {
            get;
            set;
        }

        #endregion
    }
}
