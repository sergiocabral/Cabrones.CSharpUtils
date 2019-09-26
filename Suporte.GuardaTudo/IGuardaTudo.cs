using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>São flags que definem como um objeto que implemente 
    /// a interface <see cref="IGuardaTudo"/> vai funcionar.</para>
    /// </summary>
    public enum ComportamentoParaGuardaTudo
    {
        /// <summary>
        /// <para>Quando uma informacao é apagada, todos os seus filhos são apagados.</para>
        /// <para>Quando não está defindo, os filhos ficam órfãos sendo
        /// visíveis na raiz dos dados.</para>
        /// </summary>
        RemoverFilhosQuandoPaiERemovido = 1,

        /// <summary>
        /// <para>Faz comparações de texto (<c>string</c>) sem 
        /// levar em conta se é maiúscula, mínúscula.</para>
        /// </summary>
        IgnorarMaiusculasEMinusculasAoComparar = 2,

        /// <summary>
        /// <para>Faz comparações de texto (<c>string</c>) sem 
        /// levar em conta a acentuação.</para>
        /// </summary>
        IgnorarAcentuacaoAoComparar = 4
    }

    /// <summary>
    /// <para>Interface para classe capaz de manipular <see cref="Informacao"/> e
    /// <see cref="Valor"/> na base de dados, seja ela qual for.</para>
    /// </summary>
    public interface IGuardaTudo
    {
        /// <summary>
        /// <para>Indica o modo de comportamento do objeto.</para>
        /// </summary>
        ComportamentoParaGuardaTudo Comportamento { set; get; }

        /// <summary>
        /// <para>Grava uma <see cref="Informacao"/> na base de dados.</para>
        /// </summary>
        /// <param name="informacao">
        /// <para><see cref="Informacao"/>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a <see cref="Informacao"/> que acabou de ser gravada.</para>
        /// </returns>
        Informacao Gravar(Informacao informacao);

        /// <summary>
        /// <para>Remove uma <see cref="Informacao"/> da base de dados.</para>
        /// </summary>
        /// <param name="Informacao">
        /// <para><see cref="Informacao"/>.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a <see cref="Informacao"/> que foi removida.</para>
        /// </returns>
        Informacao Remover(Informacao informacao);

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
        IList<Informacao> Remover(Criterio criterio);

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
        IList<Informacao> Consultar(Criterio criterio);
    }
}
