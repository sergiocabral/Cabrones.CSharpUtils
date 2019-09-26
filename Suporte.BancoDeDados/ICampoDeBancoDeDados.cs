using System;
namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Interface para classe que representa um 
    /// campo de banco de dados com valor atribuido.</para>
    /// <para>Agrupa atributos genéricos para qualquer tipo de campo.</para>
    /// </summary>
    public interface ICampoDeBancoDeDados
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome do campo.</para>
        /// </summary>
        string Nome { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </summary>
        bool NotNull { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </summary>
        bool ChavePrimaria { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </summary>
        bool ChaveUnica { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor puro do campo.</para>
        /// </summary>
        object ValorRaw { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tamanho do campo.</para>
        /// </summary>
        int Tamanho { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Precisão do campo.</para>
        /// </summary>
        int Precisao { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Tipo de dados deste campo.</para>
        /// </summary>
        Type Tipo { get; }
    }
}
