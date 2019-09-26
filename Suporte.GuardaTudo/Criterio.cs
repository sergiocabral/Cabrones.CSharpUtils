using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>Lista dos campos de uma <see cref="Informacao"/> que
    /// podem ser usado na compração.</para>
    /// <para>Os itens desta lista (<c>enum</c>) não devem
    /// ser usados com comparadores bit-a-bit.</para>
    /// </summary>
    public enum CamposParaComparacao
    {
        /// <summary>
        /// <para>Identificador da <see cref="Informacao"/>.</para>
        /// </summary>
        Identificador,

        /// <summary>
        /// <para>Nome da <see cref="Informacao"/>.</para>
        /// </summary>
        Nome,

        /// <summary>
        /// <para>Valor da <see cref="Informacao"/>.</para>
        /// </summary>
        Valor
    }

    /// <summary>
    /// <para>Comparadores disponíveis.</para>
    /// <para>Os itens desta lista (<c>enum</c>) não devem
    /// ser usados com comparadores bit-a-bit.</para>
    /// </summary>
    public enum Comparadores
    {
        /// <summary>
        /// <para>Igual.</para>
        /// </summary>
        Igual,

        /// <summary>
        /// <para>Menor.</para>
        /// </summary>
        Menor,

        /// <summary>
        /// <para>Menor igual.</para>
        /// </summary>
        MenorIgual,

        /// <summary>
        /// <para>Maior.</para>
        /// </summary>
        Maior,

        /// <summary>
        /// <para>Maior igual.</para>
        /// </summary>
        MaiorIgual,

        /// <summary>
        /// <para>Diferente.</para>
        /// </summary>
        Diferente,

        /// <summary>
        /// <para>Contem a parte do texto.</para>
        /// </summary>
        Contem,

        /// <summary>
        /// <para>Inicia com a parte do texto.</para>
        /// </summary>
        IniciaCom,

        /// <summary>
        /// <para>Termina com a parte do texto.</para>
        /// </summary>
        TerminaCom
    }

    /// <summary>
    /// <para>Operadores de agrupamento de critérios.</para>
    /// <para>Os itens desta lista (<c>enum</c>) não devem
    /// ser usados com comparadores bit-a-bit.</para>
    /// </summary>
    public enum OperadorDeAgrupamento
    {
        /// <summary>
        /// <para>OU (OR).</para>
        /// </summary>
        OR,

        /// <summary>
        /// <para>E (AND).</para>
        /// </summary>
        AND,

        /// <summary>
        /// <para>OU exclusivo (XOR).</para>
        /// </summary>
        XOR,

        /// <summary>
        /// <para>Negação para OU (OR).</para>
        /// </summary>
        NOR,

        /// <summary>
        /// <para>Negação para E (AND).</para>
        /// </summary>
        NAND,

        /// <summary>
        /// <para>Negação para OU exclusivo (XOR).</para>
        /// </summary>
        NXOR
    }

    /// <summary>
    /// <para>Classe que representa um critério para consultar 
    /// uma ou mais <see cref="Informacao"/>.</para>
    /// </summary>
    public class Criterio
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Campo usado na compração.</para>
        /// </summary>
        public CamposParaComparacao CampoParaComparacao { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Comparação que será realizada com o valor do
        /// campo informado em <see cref="CampoParaComparacao"/>.</para>
        /// </summary>
        public Comparadores Comparador { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor que será comparado.</para>
        /// </summary>
        public object Valor { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Outro <see cref="Criterio"/> que será agregado na comparação.</para>
        /// </summary>
        public Criterio OutroCriterio { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Operador de agrupamento aplicado ao <see cref="OutroCriterio"/>.</para>
        /// </summary>
        public OperadorDeAgrupamento OperadorParaOutroCriterio { get; set; }
    }
}
