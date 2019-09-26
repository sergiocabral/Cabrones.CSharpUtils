using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suporte.Texto.FormatProvider;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>Classe capaz de manipular <see cref="Informacao"/> e
    /// <see cref="Valor"/> e armazenar na memória.</para>
    /// </summary>
    public class GuardaTudoNaMemoria : GuardaTudo
    {
        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        public GuardaTudoNaMemoria()
        {
        }

        /// <summary>
        /// <para>Lista de <see cref="Informacao"/> armazenada, porém
        /// não é uma interface.</para>
        /// </summary>
        protected List<Informacao> dados = new List<Informacao>();

        /// <summary>
        /// <para>Lista de <see cref="Informacao"/> armazenada.</para>
        /// </summary>
        public IList<Informacao> Dados
        {
            get
            {
                return new List<Informacao>(dados);
            }
        }

        #region Abstract

        /// <summary>
        /// <para>Consulta uma ou mais <see cref="Informacao"/> da base de dados de acordo
        /// com os critérios de consultas.</para>
        /// </summary>
        /// <param name="criterio">
        /// <para>Critérios de consulta.</para>
        /// </param>
        /// <returns>
        /// <para>Lista com as <see cref="Informacao"/> encontradas.</para>
        /// </returns>
        public override IList<Informacao> Consultar(Criterio criterio)
        {
            List<Informacao> lista = new List<Informacao>();

            foreach (Informacao i in dados)
            {
                if (CriteriosSaoAplicaveis(i, criterio))
                {
                    lista.Add(i);
                }
            }

            return lista;
        }

        /// <summary>
        /// <para>Obtem a partir do identificador uma <see cref="Informacao"/> 
        /// cadastrada na base de dados.</para>
        /// </summary>
        /// <param name="informacao"><para><see cref="Informacao"/></para></param>
        /// <returns><para>Caso não exista retorna <c>null</c>.</para></returns>
        protected override Informacao ObterInformacao(Informacao informacao)
        {
            return dados.Find(
                delegate(Informacao i)
                {
                    return i.Id == informacao.Id;
                }
            );
        }

        /// <summary>
        /// <para>Valida os valores dos filhos para garantir
        /// a integridade dos dados.</para>
        /// </summary>
        /// <param name="informacao"><see cref="Informacao"/> na base de dados.</param>
        protected override void ValidarValoresDosFilhos(Informacao informacao)
        {
            informacao.Filhos = dados.FindAll(
                delegate(Informacao i)
                {
                    return i.Pai != null && i.Pai.Id == informacao.Id;
                }
            );
        }

        /// <summary>
        /// <para>
        /// Incluir uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected override void Incluir(Informacao informacao)
        {
            informacao.Id = (dados.Count == 0 ? 0 : dados.Max(i => i.Id)) + 1;
            dados.Add(informacao);
        }

        /// <summary>
        /// <para>
        /// Atualizar uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected override void Atualizar(Informacao informacao)
        {
            //Nada é necessário pois o armazenamento é na memória.
        }

        /// <summary>
        /// <para>Excluir uma informação da base de dados.</para>
        /// </summary>
        /// <param name="informacao">Informação para exclusão.</param>
        protected override void Excluir(Informacao informacao)
        {
                dados.Remove(informacao);
        }

        #endregion

    }
}
