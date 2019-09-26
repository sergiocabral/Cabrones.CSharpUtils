using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// <para>Utiliza o operador de comparação: &lt; (menor que)</para>
    /// </summary>
    public class CriterioFiltroComparacaoMenor : CriterioFiltroComparacao
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
        public CriterioFiltroComparacaoMenor(ICampoDeBancoDeDados campo, object valor) : base("<", campo, valor) { }
    }
}
