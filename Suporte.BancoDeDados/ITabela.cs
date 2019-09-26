using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Interface para classe que disponibiliza seus dados em
    /// forma de tabela, isto é, <see cref="DataSet"/>.</para>
    /// </summary>
    public interface ITabela
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Dados da tabela.</para>
        /// </summary>
        DataTable Dados { get; }
    }
}
