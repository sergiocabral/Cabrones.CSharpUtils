using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// <para>Utiliza o operador de comparação: IS NULL (é nulo?)</para>
    /// </summary>
    public class CriterioFiltroComparacaoENulo : ICriterioFiltroComparacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        public CriterioFiltroComparacaoENulo(ICampoDeBancoDeDados campo)
        {
            Campo = campo;
        }

        #region ICriterioComparacao Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </summary>
        public virtual ICampoDeBancoDeDados Campo
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor do campo.</para>
        /// </summary>
        public virtual object Valor
        {
            get { return null; }
        }

        #endregion

        #region ICriterio Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL para usar como critério numa consulta SQL.</para>
        /// </summary>
        public virtual string Sql
        {
            get
            {
                return Campo.Nome + " IS NULL";
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista dos parâmetros usados no trecho de 
        /// código sql <see cref="Sql"/> e seus respectivos valores.</para>
        /// </summary>
        public virtual IDictionary<string, object> Parametros
        {
            get
            {
                return new Dictionary<string, object>();
            }
        }

        #endregion
    }
}
