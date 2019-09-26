using System;
using System.Collections.Generic;
using System.Text;
using Suporte.Texto;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// </summary>
    public abstract class CriterioFiltroComparacao : ICriterioFiltroComparacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="operadorDeComparacao">
        /// <para>Código SQL para o operador de comparação.</para>
        /// </param>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor do campo.</para>
        /// </param>
        public CriterioFiltroComparacao(string operadorDeComparacao, ICampoDeBancoDeDados campo, object valor)
        {
            OperadorDeComparacao = operadorDeComparacao;
            Campo = campo;
            Valor = valor;
            Identificador = (new ValorUnico()).UnicoAbsoluto;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Código SQL para o operador de comparação.</para>
        /// </summary>
        protected string OperadorDeComparacao
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Código único para os parâmetros usados no trecho de 
        /// código sql <see cref="Sql"/>.</para>
        /// </summary>
        protected string Identificador
        {
            get;
            private set;
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
                return Campo.Nome + " " + OperadorDeComparacao + " @" + Campo.Nome + "_" + Identificador;
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
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add(Campo.Nome + "_" + Identificador, Valor);
                return parametros;
            }
        }

        #endregion
    }
}
