using System.Data.Common;
using System.Data;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Interface para classe que implementa conexão com um banco de dados.</para>
    /// </summary>
    public interface IConexaoComBancoDeDados
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações para conexão com o banco de dados.</para>
        /// </summary>
        IInformacoesParaConexao InformacoesParaConexao { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Instância da conexão estabelecida.</para>
        /// </summary>
        DbConnection Conexao { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Informação sobre se a conexão está ativa ou não.</para>
        /// <para>Pode se estabelecer ou encerrar uma conexão por atribuir um valor.</para>
        /// </summary>
        bool Conectado { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Manipulador da string de conexão com o banco de dados.</para>
        /// </summary>
        IStringDeConexao StringDeConexao { get; }

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o status da operação. 
        /// Não retorna dados.</para></returns>
        int ExecuteNonQuery(string commandText, params DbParameter[] parametros);

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="transacao"><para>Transação ativa.</para></param>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o status da operação. 
        /// Não retorna dados.</para></returns>
        int ExecuteNonQuery(DbTransaction transacao, string commandText, params DbParameter[] parametros);

        /// <summary>
        /// <para>Inicia uma transação.</para>
        /// </summary>
        /// <returns><para>Objeto representando uma transação.</para></returns>
        DbTransaction BeginTransaction();

        /// <summary>
        /// <para>Inicia uma transação.</para>
        /// </summary>
        /// <param name="isolationLevel"><para>Nível de isolamento.</para></param>
        /// <returns><para>Objeto representando uma transação.</para></returns>
        DbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna as linhas selecionadas.</para></returns>
        DbDataReader ExecuteReader(string commandText, params DbParameter[] parametros);

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o valor escalar selecionado.</para></returns>
        object ExecuteScalar(string commandText, params DbParameter[] parametros);

        /// <summary>
        /// <para>Configura um parâmetro para ser usado em conjunto
        /// com um comando SQL.</para>
        /// </summary>
        /// <param name="nome"><para>Nome do parâmetro no comando SQL.</para></param>
        /// <param name="valor"><para>Valor que será aplicado.</para></param>
        /// <param name="tipo"><para>Tipo do valor.</para></param>
        /// <returns><para><see cref="DbParameter"/> configurado.</para></returns>
        DbParameter ConfigurarParametro(string nome, object valor, DbType tipo);
    }
}
