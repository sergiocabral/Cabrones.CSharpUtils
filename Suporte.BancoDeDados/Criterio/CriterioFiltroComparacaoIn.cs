using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// <para>Utiliza o operador de comparação: IN (dentro de uma lista)</para>
    /// </summary>
    public class CriterioFiltroComparacaoIn : CriterioFiltroComparacao
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
        public CriterioFiltroComparacaoIn(ICampoDeBancoDeDados campo, object valor)
            : base("IN", campo, valor)
        {
            SqlDaLista = MontarListaSQL(valor);
        }

        /// <summary>
        /// <para>Montar um código SQL com os valores em forma de lista.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valores.</para>
        /// </param>
        /// <returns>
        /// <para>Código SQL com os valores em forma de lista.</para>
        /// </returns>
        private string MontarListaSQL(object valor)
        {
            if (valor == null || !valor.GetType().IsArray)
            {
                throw new Exception("O valor precisa ser um array");
            }
            StringBuilder sql = new StringBuilder();
            IList lista = (IList)valor;
            foreach (object item in lista)
            {
                if (item != null)
                {
                    if (typeof(string).IsAssignableFrom(item.GetType()))
                    {
                        sql.AppendFormat(", '{0}'", item);
                    }
                    else
                    {
                        sql.AppendFormat(", {0}", item);
                    }
                }
            }
            if (sql.Length > 0)
            {
                sql.Remove(0, ", ".Length);
            }
            return "(" + sql.ToString() + ")";
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL com os valores em forma de lista.</para>
        /// </summary>
        protected string SqlDaLista
        {
            get;
            private set;
        }

        #region ICriterio Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL para usar como critério numa consulta SQL.</para>
        /// </summary>
        public override string Sql
        {
            get
            {
                return Campo.Nome + " " + OperadorDeComparacao + " " + SqlDaLista;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista dos parâmetros usados no trecho de 
        /// código sql <see cref="Sql"/> e seus respectivos valores.</para>
        /// </summary>
        public override IDictionary<string, object> Parametros
        {
            get
            {
                return new Dictionary<string, object>();
            }
        }
        
        #endregion
    }
}
