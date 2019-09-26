using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// <para>Utiliza o operador de comparação: %gt;= (maior igual que)</para>
    /// </summary>
    public class CriterioFiltroComparacaoMaiorIgual : CriterioFiltroComparacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor do campo.</para>
        /// </param>
        public CriterioFiltroComparacaoMaiorIgual(ICampoDeBancoDeDados campo, object valor) : base(">=", campo, valor) { }
    }
}
