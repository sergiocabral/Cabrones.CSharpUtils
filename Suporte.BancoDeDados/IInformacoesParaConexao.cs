namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Interface para classe capaz de agrupar informações necessárias
    /// para realizar uma conexão com banco de dados.</para>
    /// </summary>
    public interface IInformacoesParaConexao
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome do usuário.</para>
        /// </summary>
        string NomeDeUsuario { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Senha do usuário.</para>
        /// </summary>
        string SenhaDoUsuario { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Endereço do servidor.</para>
        /// </summary>
        string Endereco { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome da fonte dos dados que serão utilizados.</para>
        /// </summary>
        string FonteDeDados { get; set; }
    }
}
