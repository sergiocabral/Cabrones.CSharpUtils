using System;
using System.Collections.Generic;
using System.Text;
using Suporte.Texto;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Agrupa critérios através de operadores boleanos.</para>
    /// </summary>
    public abstract class CriterioFiltroAgrupamento : ICriterioFiltroAgrupamento
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="operadorDeAgrupamento">
        /// <para>Código SQL para o operador de agrupamento.</para>
        /// </param>
        /// <param name="criterio1">
        /// <para>Critério 1</para>
        /// </param>
        /// <param name="criterio2">
        /// <para>Critério 2</para>
        /// </param>
        public CriterioFiltroAgrupamento(string operadorDeAgrupamento, ICriterioFiltro criterio1, ICriterioFiltro criterio2)
        {
            OperadorDeAgrupamento = operadorDeAgrupamento;
            Criterio1 = criterio1;
            Criterio2 = criterio2;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Código SQL para o operador de agrupamento.</para>
        /// </summary>
        public string OperadorDeAgrupamento
        {
            get;
            private set;
        }

        #region ICriterioAgrupamento Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério 1</para>
        /// </summary>
        public virtual ICriterioFiltro Criterio1
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Critério 2</para>
        /// </summary>
        public virtual ICriterioFiltro Criterio2
        {
            get;
            private set;
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
                return "(" + Criterio1.Sql + ") " + OperadorDeAgrupamento + " (" + Criterio2.Sql + ")";
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
                IDictionary<string, object> parametros = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> parametro in Criterio1.Parametros)
                {
                    parametros.Add(parametro);
                }
                foreach (KeyValuePair<string, object> parametro in Criterio2.Parametros)
                {
                    parametros.Add(parametro);
                }
                return parametros;
            }
        }

        #endregion
    }
}
