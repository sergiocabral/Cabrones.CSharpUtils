using System.Collections.Generic;

namespace Suporte.BancoDeDados.Criterio
{
    /// <summary>
    /// <para>Interface para classe que implemente um
    /// critério de ordenação para consulta num comando de banco de dados SQL.</para>
    /// </summary>
    public interface ICriterioOrdenacao
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Trecho de código SQL para usar como critério numa consulta SQL.</para>
        /// </summary>
        string Sql { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre os campo do banco de dados.</para>
        /// </summary>
        IList<ICampoDeBancoDeDados> Campos { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Quando <c>true</c> indica ordenação crescente,
        /// se <c>false</c> indica ordenação decrescente.</para>
        /// </summary>
        bool Ascendente { get; }
    }
}
