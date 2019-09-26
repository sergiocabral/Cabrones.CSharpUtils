using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Agrupa critérios através de operadores boleanos.</para>
    /// <para>Utiliza o operador de agrupamento: OR</para>
    /// </summary>
    public class CriterioFiltroAgrupamentoOu : CriterioFiltroAgrupamento
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="criterio1">
        /// <para>Critério 1</para>
        /// </param>
        /// <param name="criterio2">
        /// <para>Critério 2</para>
        /// </param>
        public CriterioFiltroAgrupamentoOu(ICriterioFiltro criterio1, ICriterioFiltro criterio2) : base("OR", criterio1, criterio2) { }
    }
}
