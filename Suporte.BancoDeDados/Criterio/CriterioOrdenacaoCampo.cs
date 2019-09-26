using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Critério de ordenação para consulta num comando de banco de dados SQL.</para>
    /// </summary>
    public class CriterioOrdenacao : ICriterioOrdenacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        /// <param name="ascendente">
        /// <para>Quando <c>true</c> indica ordenação crescente,
        /// se <c>false</c> indica ordenação decrescente.</para>
        /// </param>
        public CriterioOrdenacao(ICampoDeBancoDeDados campo, bool ascendente) : this(campo, ascendente, null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="ordenacao">
        /// <para>Ordenacao posterior a este campo.</para>
        /// </param>
        /// <param name="campo">
        /// <para>Informações sobre o campo do banco de dados.</para>
        /// </param>
        /// <param name="ascendente">
        /// <para>Quando <c>true</c> indica ordenação crescente,
        /// se <c>false</c> indica ordenação decrescente.</para>
        /// </param>
        public CriterioOrdenacao(ICampoDeBancoDeDados campo, bool ascendente, ICriterioOrdenacao ordenacao)
        {
            Campos = new List<ICampoDeBancoDeDados>();
            Campos.Add(campo);
            Ascendente = ascendente;
            Ordenacao = ordenacao;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Ordenacao posterior a este campo.</para>
        /// </summary>
        public ICriterioOrdenacao Ordenacao
        {
            get;
            private set;
        }

        #region ICriterioOrdenacao Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL para usar como critério numa consulta SQL.</para>
        /// </summary>
        public string Sql
        {
            get
            {
                return Campos[0].Nome + (Ascendente ? " ASC" : " DESC") + (Ordenacao != null ? ", " + Ordenacao.Sql : string.Empty);
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre os campo do banco de dados.</para>
        /// </summary>
        public IList<ICampoDeBancoDeDados> Campos
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Quando <c>true</c> indica ordenação crescente,
        /// se <c>false</c> indica ordenação decrescente.</para>
        /// </summary>
        public bool Ascendente
        {
            get;
            private set;
        }

        #endregion
    }
}
