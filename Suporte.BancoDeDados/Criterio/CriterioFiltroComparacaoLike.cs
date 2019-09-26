using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de filtro para consulta num comando de banco de dados SQL.</para>
    /// <para>Armazena o vampo e seu valor.</para>
    /// <para>Utiliza o operador de comparação: like (like)</para>
    /// </summary>
    public class CriterioFiltroComparacaoLike : CriterioFiltroComparacao
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
        public CriterioFiltroComparacaoLike(ICampoDeBancoDeDados campo, object valor) : this(campo, valor, null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor do campo.</para>
        /// </param>
        /// <param name="escape">
        /// <para>Caracter para escapar os caracteres curingas % e _.</para>
        /// </param>
        public CriterioFiltroComparacaoLike(ICampoDeBancoDeDados campo, object valor, char? escape)
            : base("like", campo, valor)
        {
            if (escape==null)
            {
                Escape = string.Empty;
            }
            else
            {
                Escape = string.Format(" ESCAPE '{0}' ", escape);
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Código SQL para escapar os caracteres curingas % e _.</para>
        /// </summary>
        protected string Escape
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
                return base.Sql + Escape;
            }
        }

        #endregion

    }
}
