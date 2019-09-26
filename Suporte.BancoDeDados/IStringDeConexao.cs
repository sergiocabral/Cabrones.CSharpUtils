
namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Interface para classe capaz de manipular uma string de conexão com o banco de dados.</para>
    /// </summary>
    public interface IStringDeConexao
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista de atributos que compõe a string de conexão.</para>
        /// </summary>
        string this[string atributo] { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Limpar todos os atributos da string de conexão.</para>
        /// </summary>
        void Limpar();

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor da string de conexão com o banco de dados.</para>
        /// </summary>
        string Valor { get; set; }
    }
}
