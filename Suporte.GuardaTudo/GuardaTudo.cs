using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suporte.Texto.FormatProvider;

namespace Suporte.GuardaTudo
{
    ///TODO: Pendências no GuardaTudoNoBancoDeDados
    ///Problemas ao gravar valor decimal, por exemplo: 15.12
    ///Ao excluir um valor referência-pai, deve eleger outro valor para referência-pai.
    ///Ao alterar um valor referência-filho, deve replicar no valores referênc-pai e os referencia-irmãos(filhos)
    ///Testar cada tipo de dado, especialmente: DataHora, Binário

    /// <summary>
    /// <para>Classe base para classes capazes de manipular <see cref="Informacao"/> e
    /// <see cref="Valor"/> e armazenar.</para>
    /// </summary>
    public abstract class GuardaTudo : IGuardaTudo
    {

        /// <summary>
        /// <para>Valida os valores das propriedades para garantir
        /// a integridade dos dados.</para>
        /// </summary>
        /// <param name="informacao"><see cref="Informacao"/> na base de dados.</param>
        protected virtual void ValidarValores(Informacao informacao)
        {
            ValidarValores(informacao, null);
        }

        /// <summary>
        /// <para>Valida os valores das propriedades para garantir
        /// a integridade dos dados.</para>
        /// </summary>
        /// <param name="informacao"><see cref="Informacao"/> na base de dados.</param>
        /// <param name="objetosProcessados">
        /// <para>Objetos já processados pela função recursiva.</para>
        /// <para>Usado para evitar loops infinitos.</para>
        /// </param>
        private void ValidarValores(Informacao informacao, object[] objetosProcessados)
        {
            if (informacao == null || informacao.Id == 0)
            {
                return;
            }

            if (objetosProcessados == null || !objetosProcessados.Contains(informacao))
            {
                List<object> objetosProcessados_ = objetosProcessados == null ? new List<object>() : new List<object>(objetosProcessados);
                objetosProcessados_.Add(informacao);

                if (ObterInformacao(informacao) == null)
                {
                    if (informacao.Pai != null)
                    {
                        ValidarValores(informacao.Pai, objetosProcessados_.ToArray());
                    }
                }
                else
                {
                    if (informacao.Pai != null)
                    {
                        informacao.Pai = ObterInformacao(informacao.Pai);
                        if (informacao.Pai != null)
                        {                            
                            ValidarValores(informacao.Pai, objetosProcessados_.ToArray());
                        }
                    }
                }
            }

            ValidarValoresDosFilhos(informacao);
        }

        /// <summary>
        /// <para>Lista com os tipos de manipulação que 
        /// uma <see cref="Informacao"/> pode sofrer.</para>
        /// </summary>
        protected enum TipoDeManipulacaoDaInformacao
        {
            /// <summary>
            /// <para>Inclusão.</para>
            /// </summary>
            Inclusao = 1,

            /// <summary>
            /// <para>Alteração.</para>
            /// </summary>
            Alteracao = 2,

            /// <summary>
            /// <para>Exclusão.</para>
            /// </summary>
            Exclusao = 4
        }

        /// <summary>
        /// <para>Valida os valores das propriedades de uma <see cref=" Informacao"/>
        /// antes de ser manipulada na base de dados.</para>
        /// </summary>
        /// <param name="manipulacao"><para>Tipo de manipulacao.</para></param>
        /// <param name="informacao"><para><see cref="Informacao"/>.</para></param>
        protected virtual void ValidarInformacaoAntesDaGravacao(TipoDeManipulacaoDaInformacao manipulacao, Informacao informacao)
        {
            if (informacao == null)
            {
                throw new NullReferenceException("A Informacao não pode ser nula.");
            }
            else if (((manipulacao & TipoDeManipulacaoDaInformacao.Inclusao) == TipoDeManipulacaoDaInformacao.Inclusao) && 
                     (informacao.Id != 0))
            {
                throw new ArgumentException("O identificador não pode ser informado para uma inclusão.");
            }
            else if ((((manipulacao & TipoDeManipulacaoDaInformacao.Inclusao) == TipoDeManipulacaoDaInformacao.Inclusao) ||
                      ((manipulacao & TipoDeManipulacaoDaInformacao.Alteracao) == TipoDeManipulacaoDaInformacao.Alteracao)) && 
                     (string.IsNullOrWhiteSpace(informacao.Nome)))
            {
                throw new ArgumentException("O nome não pode ficar em branco.");
            }
        }

        /// <summary>
        /// <para>Verifica se uma informação passa com êxito pelos
        /// critérios informados.</para>
        /// </summary>
        /// <param name="informacao"><para>Informacao</para>.</param>
        /// <param name="criterio"><see cref="Criterio"/>.</param>
        /// <returns><para>Retorna <c>true</c> se os critérios forem aplicáveis.</para></returns>
        protected virtual bool CriteriosSaoAplicaveis(Informacao informacao, Criterio criterio)
        {
            bool criterio1 = CriterioEAplicavel(informacao, criterio);

            if (criterio.OutroCriterio != null)
            {
                switch (criterio.OperadorParaOutroCriterio)
                {
                    case OperadorDeAgrupamento.OR:
                        return criterio1 || CriterioEAplicavel(informacao, criterio.OutroCriterio);
                    case OperadorDeAgrupamento.AND:
                        return criterio1 && CriterioEAplicavel(informacao, criterio.OutroCriterio);
                    case OperadorDeAgrupamento.XOR:
                        return criterio1 ^ CriterioEAplicavel(informacao, criterio.OutroCriterio);
                    case OperadorDeAgrupamento.NOR:
                        return !(criterio1 || CriterioEAplicavel(informacao, criterio.OutroCriterio));
                    case OperadorDeAgrupamento.NAND:
                        return !(criterio1 && CriterioEAplicavel(informacao, criterio.OutroCriterio));
                    case OperadorDeAgrupamento.NXOR:
                        return !(criterio1 ^ CriterioEAplicavel(informacao, criterio.OutroCriterio));
                    default:
                        throw new NotImplementedException();
                }
            }

            return criterio1;
        }

        /// <summary>
        /// <para>Verifica se uma informação passa com êxito por um único critério informado.</para>
        /// </summary>
        /// <param name="informacao"><para>Informacao</para>.</param>
        /// <param name="criterio"><see cref="Criterio"/>.</param>
        /// <returns><para>Retorna <c>true</c> se o critério for aplicável.</para></returns>
        private bool CriterioEAplicavel(Informacao informacao, Criterio criterio)
        {
            if (informacao != null && criterio != null)
            {
                IFormatProvider formatProvider = new FormatadorTransparente();
                if ((Comportamento & ComportamentoParaGuardaTudo.IgnorarAcentuacaoAoComparar) == ComportamentoParaGuardaTudo.IgnorarAcentuacaoAoComparar)
                {
                    formatProvider = new FormatadorRemoveAcentuacao(formatProvider);
                }
                if ((Comportamento & ComportamentoParaGuardaTudo.IgnorarMaiusculasEMinusculasAoComparar) == ComportamentoParaGuardaTudo.IgnorarMaiusculasEMinusculasAoComparar)
                {
                    formatProvider = new FormatadorTransformaParaMinusculo(formatProvider);
                }
                switch (criterio.CampoParaComparacao)
                {
                    case CamposParaComparacao.Identificador:
                        switch (criterio.Comparador)
                        {
                            case Comparadores.Diferente:
                                return Convert.ToInt64(criterio.Valor) != informacao.Id;
                            case Comparadores.Igual:
                                return Convert.ToInt64(criterio.Valor) == informacao.Id;
                            case Comparadores.Maior:
                                return Convert.ToInt64(criterio.Valor) > informacao.Id;
                            case Comparadores.MaiorIgual:
                                return Convert.ToInt64(criterio.Valor) >= informacao.Id;
                            case Comparadores.Menor:
                                return Convert.ToInt64(criterio.Valor) < informacao.Id;
                            case Comparadores.MenorIgual:
                                return Convert.ToInt64(criterio.Valor) <= informacao.Id;
                            case Comparadores.Contem:
                                return Convert.ToString(informacao.Id).Contains(Convert.ToString(criterio.Valor));
                            case Comparadores.IniciaCom:
                                return Convert.ToString(informacao.Id).StartsWith(Convert.ToString(criterio.Valor));
                            case Comparadores.TerminaCom:
                                return Convert.ToString(informacao.Id).EndsWith(Convert.ToString(criterio.Valor));
                            default:
                                throw new NotImplementedException();
                        }
                    case CamposParaComparacao.Nome:
                        switch (criterio.Comparador)
                        {
                            case Comparadores.Diferente:
                                return string.Format(formatProvider, "{0}", criterio.Valor) != string.Format(formatProvider, "{0}", informacao.Nome);
                            case Comparadores.Igual:
                                return string.Format(formatProvider, "{0}", criterio.Valor) == string.Format(formatProvider, "{0}", informacao.Nome);
                            case Comparadores.Maior:
                                return string.Format(formatProvider, "{0}", criterio.Valor).CompareTo(string.Format(formatProvider, "{0}", informacao.Nome)) < 0;
                            case Comparadores.MaiorIgual:
                                return string.Format(formatProvider, "{0}", criterio.Valor).CompareTo(string.Format(formatProvider, "{0}", informacao.Nome)) <= 0;
                            case Comparadores.Menor:
                                return string.Format(formatProvider, "{0}", criterio.Valor).CompareTo(string.Format(formatProvider, "{0}", informacao.Nome)) > 0;
                            case Comparadores.MenorIgual:
                                return string.Format(formatProvider, "{0}", criterio.Valor).CompareTo(string.Format(formatProvider, "{0}", informacao.Nome)) >= 0;
                            case Comparadores.Contem:
                                return string.Format(formatProvider, "{0}", informacao.Nome).Contains(string.Format(formatProvider, "{0}", criterio.Valor));
                            case Comparadores.IniciaCom:
                                return string.Format(formatProvider, "{0}", informacao.Nome).StartsWith(string.Format(formatProvider, "{0}", criterio.Valor));
                            case Comparadores.TerminaCom:
                                return string.Format(formatProvider, "{0}", informacao.Nome).EndsWith(string.Format(formatProvider, "{0}", criterio.Valor));
                            default:
                                throw new NotImplementedException();
                        }
                    case CamposParaComparacao.Valor:
                        switch (criterio.Comparador)
                        {
                            case Comparadores.Diferente:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return !string.Format(formatProvider, "{0}", informacao.Valor).Equals(string.Format(formatProvider, "{0}", criterio.Valor));
                                }
                                else
                                {
                                    return !informacao.Valor.Equals(criterio.Valor);
                                }
                            case Comparadores.Igual:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return string.Format(formatProvider, "{0}", informacao.Valor).Equals(string.Format(formatProvider, "{0}", criterio.Valor));
                                }
                                else
                                {
                                    return informacao.Valor.Equals(criterio.Valor);
                                }
                            case Comparadores.Maior:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return string.Format(formatProvider, "{0}", informacao.Valor).CompareTo(string.Format(formatProvider, "{0}", criterio.Valor)) > 0;
                                }
                                else
                                {
                                    return informacao.Valor.CompareTo(criterio.Valor) > 0;
                                }
                            case Comparadores.MaiorIgual:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return string.Format(formatProvider, "{0}", informacao.Valor).CompareTo(string.Format(formatProvider, "{0}", criterio.Valor)) >= 0;
                                }
                                else
                                {
                                    return informacao.Valor.CompareTo(criterio.Valor) >= 0;
                                }
                            case Comparadores.Menor:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return string.Format(formatProvider, "{0}", informacao.Valor).CompareTo(string.Format(formatProvider, "{0}", criterio.Valor)) < 0;
                                }
                                else
                                {
                                    return informacao.Valor.CompareTo(criterio.Valor) < 0;
                                }
                            case Comparadores.MenorIgual:
                                if (informacao.Valor.Tipo == TipoDeValor.Texto)
                                {
                                    return string.Format(formatProvider, "{0}", informacao.Valor).CompareTo(string.Format(formatProvider, "{0}", criterio.Valor)) <= 0;
                                }
                                else
                                {
                                    return informacao.Valor.CompareTo(criterio.Valor) <= 0;
                                }
                            case Comparadores.Contem:
                                return string.Format(formatProvider, "{0}", informacao.Valor).Contains(string.Format(formatProvider, "{0}", criterio.Valor));
                            case Comparadores.IniciaCom:
                                return string.Format(formatProvider, "{0}", informacao.Valor).StartsWith(string.Format(formatProvider, "{0}", criterio.Valor));
                            case Comparadores.TerminaCom:
                                return string.Format(formatProvider, "{0}", informacao.Valor).EndsWith(string.Format(formatProvider, "{0}", criterio.Valor));
                            default:
                                throw new NotImplementedException();
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                return false;
            }
        }

        #region Interface IGuardaTudo

        /// <summary>
        /// <para>Indica o modo de comportamento do objeto.</para>
        /// </summary>
        public virtual ComportamentoParaGuardaTudo Comportamento { set; get; }

        /// <summary>
        /// <para>Grava uma <see cref="Informacao"/> na base de dados.</para>
        /// </summary>
        /// <param name="informacao">
        /// <para><see cref="Informacao"/> que será gravada.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a <see cref="Informacao"/> que acabou de ser gravada.</para>
        /// </returns>
        public virtual Informacao Gravar(Informacao informacao)
        {
            Informacao informacaoNaBase = ObterInformacao(informacao);
            if (informacaoNaBase == null)
            {
                ValidarInformacaoAntesDaGravacao(TipoDeManipulacaoDaInformacao.Inclusao, informacao);
                informacaoNaBase = informacao;
                Incluir(informacaoNaBase);
            }
            else
            {
                ValidarInformacaoAntesDaGravacao(TipoDeManipulacaoDaInformacao.Alteracao, informacao);
                informacaoNaBase.SubstituirConteudo(informacao);
                Atualizar(informacaoNaBase);
            }
            ValidarValores(informacaoNaBase);
            return informacaoNaBase;
        }

        /// <summary>
        /// <para>Remove uma <see cref="Informacao"/> da base de dados.</para>
        /// </summary>
        /// <param name="Informacao">
        /// <para><see cref="Informacao"/>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a <see cref="Informacao"/> que foi removida.</para>
        /// </returns>
        public virtual Informacao Remover(Informacao informacao)
        {
            Informacao informacaoNaBase = ObterInformacao(informacao);
            if (informacaoNaBase != null)
            {
                Excluir(informacaoNaBase);

                if ((Comportamento & ComportamentoParaGuardaTudo.RemoverFilhosQuandoPaiERemovido) == ComportamentoParaGuardaTudo.RemoverFilhosQuandoPaiERemovido)
                {
                    while (informacaoNaBase.Filhos.Count > 0)
                    {
                        Informacao filho = informacaoNaBase.Filhos[0];
                        Remover(filho);
                        informacaoNaBase.Filhos.Remove(filho);
                    }
                }
                else
                {
                    foreach (Informacao i in informacaoNaBase.Filhos)
                    {
                        i.Pai = null;
                    }
                }

                ValidarValores(informacaoNaBase);
            }
            return informacaoNaBase;
        }

        /// <summary>
        /// <para>Remove uma ou mais <see cref="Informacao"/> da base de dados de acordo
        /// com os critérios de consultas.</para>
        /// </summary>
        /// <param name="criterio">
        /// <para>Critérios de consulta.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna uma lista com as <see cref="Informacao"/> que foram removidas.</para>
        /// </returns>
        public virtual IList<Informacao> Remover(Criterio criterio)
        {
            IList<Informacao> listaParaApagar = Consultar(criterio);
            foreach (Informacao informacao in listaParaApagar)
            {
                Remover(informacao);
            }
            return listaParaApagar;
        }

        #endregion

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
        public abstract IList<Informacao> Consultar(Criterio criterio);

        /// <summary>
        /// <para>Obtem a partir do identificador uma <see cref="Informacao"/> 
        /// cadastrada na base de dados.</para>
        /// </summary>
        /// <param name="informacao"><para><see cref="Informacao"/></para></param>
        /// <returns><para>Caso não exista retorna <c>null</c>.</para></returns>
        protected abstract Informacao ObterInformacao(Informacao informacao);

        /// <summary>
        /// <para>Valida os valores dos filhos para garantir
        /// a integridade dos dados.</para>
        /// </summary>
        /// <param name="informacao"><see cref="Informacao"/> na base de dados.</param>
        protected abstract void ValidarValoresDosFilhos(Informacao informacao);

        /// <summary>
        /// <para>
        /// Incluir uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected abstract void Incluir(Informacao informacao);

        /// <summary>
        /// <para>
        /// Atualiza uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected abstract void Atualizar(Informacao informacao);

        /// <summary>
        /// <para>Excluir uma informação da base de dados.</para>
        /// </summary>
        /// <param name="informacao">Informação para exclusão.</param>
        protected abstract void Excluir(Informacao informacao);

        #endregion
    }
}
