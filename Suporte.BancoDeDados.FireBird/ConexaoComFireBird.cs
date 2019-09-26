using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using System.IO;

namespace Suporte.BancoDeDados.FireBird
{
    /// <summary>
    /// <para>Classe capaz de estabelecer uma conexão com um banco de dados FireBird.</para>
    /// </summary>
    public class ConexaoComFireBird : IConexaoComBancoDeDados
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ConexaoComFireBird()
        {
            Inicializar();
            RedefinirStringDeConexao();
            ConexaoEx.ConnectionString = StringDeConexao.ToString();
        }

        /// <summary>
        /// <para>Destrutor.</para>
        /// </summary>
        ~ConexaoComFireBird()
        {
            Conectado = false;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Permite informar a string de conexão.</para>
        /// <para>Se for necessário redefinir a string de conexão padrão
        /// chame pelo método <see cref="RedefinirStringDeConexao"/>.</para>
        /// </summary>
        /// <param name="stringDeConexao">
        /// <para>String de conexão com o banco de dados.</para>
        /// </param>
        public ConexaoComFireBird(string stringDeConexao)
        {
            Inicializar();
            StringDeConexao.Valor = stringDeConexao;
            ConexaoEx.ConnectionString = StringDeConexao.ToString();
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>
        /// Permite informar um arquivo local para 
        /// conexão Embedded (servidor embarcado).
        /// </para>
        /// </summary>
        /// <param name="arquivo">
        /// <para>Arquivo local para conexão.</para>
        /// </param>
        public ConexaoComFireBird(FileInfo arquivo)
        {
            Inicializar();
            StringDeConexao.Valor = string.Format("User=SYSDBA;Password=masterkey;DataSource=localhost;Database={0};Server Type=1", arquivo.FullName);
            ConexaoEx.ConnectionString = StringDeConexao.ToString();
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Permite informar alguns parâmetros adicionais da string de conexão.</para>
        /// </summary>
        /// <param name="port">
        /// <para>Porta de conexão de rede TCP/TP.</para>
        /// <para>Se for informado como <c>null</c> será mantido o valor padrão.</para>
        /// </param>
        /// <param name="dialect">
        /// <para>Dialeto da escrita de comandos SQL.</para>
        /// <para>Se for informado como <c>null</c> será mantido o valor padrão.</para>
        /// </param>
        /// <param name="role">
        /// <para>Role que será assumida durante a conexão.</para>
        /// <para>Se for informado como <c>null</c> será mantido o valor padrão.</para>
        /// </param>
        /// <param name="charset">
        /// <para>Charset que será assumido durante a conexão..</para>
        /// <para>Se for informado como <c>null</c> será mantido o valor padrão.</para>
        /// </param>
        /// <param name="serverType">
        /// <para>Tipo de servidor: padrão=0, embarcado=1 ou de contexto=2.</para>
        /// <para>Se for informado como <c>null</c> será mantido o valor padrão.</para>
        /// </param>
        public ConexaoComFireBird(int? port, int? dialect, string charset, string role, int? serverType)
            : this()
        {
            if (port != null)
            {
                AtributoPort = port.Value;
            }
            if (dialect != null)
            {
                AtributoDialect = dialect.Value;
            }
            if (charset != null)
            {
                AtributoCharset = charset;
            }
            if (role != null)
            {
                AtributoRole = role;
            }
            if (serverType != null)
            {
                AtributoServerType = serverType.Value;
            }
            ConexaoEx.ConnectionString = StringDeConexao.ToString();
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Instância da conexão estabelecida.</para>
        /// <para>Especializado como <see cref="FbConnection"/>.</para>
        /// </summary>
        protected FbConnection ConexaoEx
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>Inicializa variáveis desta classe.</para>
        /// </summary>
        private void Inicializar()
        {
            ConexaoEx = new FbConnection();
            
            InformacoesParaConexao informacoesParaConexao = new InformacoesParaConexao();
            informacoesParaConexao.QuandoAlterar += new EventHandler<EventArgs>(ProcessarEvento_InformacoesParaConexao_QuandoAlterar);
            this.informacoesParaConexao = informacoesParaConexao;

            StringDeConexao stringDeConexao = new StringDeConexao();
            stringDeConexao.QuandoAlterar += new EventHandler<EventArgs>(ProcessarEvento_StringDeConexao_QuandoAlterar);
            this.stringDeConexao = stringDeConexao;
        }

        /// <summary>
        /// <para>Método que atende o evento disparado quando a 
        /// propriedade <see cref="StringDeConexao"/> sofre alterações.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Objeto que disparou o evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informações sobre o evento.</para>
        /// </param>
        private void ProcessarEvento_StringDeConexao_QuandoAlterar(object sender, EventArgs e)
        {
            InformacoesParaConexao informacoesParaConexao = (InformacoesParaConexao)this.InformacoesParaConexao;
            informacoesParaConexao.InibirDisparoDeEventos = true;
            informacoesParaConexao.NomeDeUsuario = AtributoUser;
            informacoesParaConexao.SenhaDoUsuario = AtributoPassword;
            informacoesParaConexao.Endereco = AtributoDataSource;
            informacoesParaConexao.FonteDeDados = AtributoDatabase;
            informacoesParaConexao.InibirDisparoDeEventos = false;
        }

        /// <summary>
        /// <para>Método que atende o evento disparado quando a 
        /// propriedade <see cref="InformacoesParaConexao"/> sofre alterações.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Objeto que disparou o evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informações sobre o evento.</para>
        /// </param>
        private void ProcessarEvento_InformacoesParaConexao_QuandoAlterar(object sender, EventArgs e)
        {
            StringDeConexao stringDeConexao = (StringDeConexao)this.StringDeConexao;
            stringDeConexao.InibirDisparoDeEventos = true;
            AtributoUser = InformacoesParaConexao.NomeDeUsuario;
            AtributoPassword = InformacoesParaConexao.SenhaDoUsuario;
            AtributoDataSource = InformacoesParaConexao.Endereco;
            AtributoDatabase = InformacoesParaConexao.FonteDeDados;
            stringDeConexao.InibirDisparoDeEventos = false;
        }

        /// <summary>
        /// <para>Redefine os atributos da string de conexão para seus valores padrão.</para>
        /// </summary>
        public void RedefinirStringDeConexao()
        {
            StringDeConexao stringDeConexao = (StringDeConexao)this.StringDeConexao;
            stringDeConexao.InibirDisparoDeEventos = true;
            stringDeConexao.Limpar();
            stringDeConexao[AtributosDeConexaoFireBird.User] = "SYSDBA";
            stringDeConexao[AtributosDeConexaoFireBird.Password] = "masterkey";
            stringDeConexao[AtributosDeConexaoFireBird.DataSource] = "localhost";
            stringDeConexao[AtributosDeConexaoFireBird.Database] = "banco.fdb";
            stringDeConexao[AtributosDeConexaoFireBird.Port] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.Dialect] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.Charset] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.Role] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.ConnectionLifetime] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.ConnectionTimeout] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.Pooling] = string.Empty;
            stringDeConexao[AtributosDeConexaoFireBird.PacketSize] = string.Empty;
            stringDeConexao.InibirDisparoDeEventos = false;
            stringDeConexao[AtributosDeConexaoFireBird.ServerType] = string.Empty;
        }

        #region Atributos

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Usuário para conexão.</para>
        /// </summary>
        public string AtributoUser
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.User];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.User] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Senha do usuário.</para>
        /// </summary>
        public string AtributoPassword
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.Password];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Password] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Endereço do servidor.</para>
        /// </summary>
        public string AtributoDataSource
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.DataSource];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.DataSource] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Base de dados que será utilizada.</para>
        /// </summary>
        public string AtributoDatabase
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.Database];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Database] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Porta de conexão de rede TCP/TP.</para>
        /// </summary>
        public int AtributoPort
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.Port];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Port] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Dialeto da escrita de comandos SQL.</para>
        /// </summary>
        public int AtributoDialect
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.Dialect];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Dialect] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Charset que será assumido durante a conexão..</para>
        /// </summary>
        public string AtributoCharset
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.Charset];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Charset] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Role que será assumida durante a conexão.</para>
        /// </summary>
        public string AtributoRole
        {
            get
            {
                try
                {
                    return StringDeConexao[AtributosDeConexaoFireBird.Role];
                }
                catch (KeyNotFoundException)
                {
                    return string.Empty;
                }
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Role] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Temo de vida da conexão com o servidor.</para>
        /// </summary>
        public int AtributoConnectionLifetime
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.ConnectionLifetime];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.ConnectionLifetime] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Temo máximo de espera pela resposta do servidor.</para>
        /// </summary>
        public int AtributoConnectionTimeout
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.ConnectionTimeout];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.ConnectionTimeout] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Ativar o Pooling.</para>
        /// </summary>
        public bool AtributoPooling
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.Pooling];
                }
                catch (KeyNotFoundException)
                {
                    return false;
                }
                return Convert.ToBoolean(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.Pooling] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tamanho do pacote.</para>
        /// </summary>
        public int AtributoPacketSize
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.PacketSize];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.PacketSize] = value.ToString();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tipo de servidor: padrão=0, embarcado=1 ou de contexto=2.</para>
        /// </summary>
        public int AtributoServerType
        {
            get
            {
                string valor;
                try
                {
                    valor = StringDeConexao[AtributosDeConexaoFireBird.ServerType];
                }
                catch (KeyNotFoundException)
                {
                    return (int)FbServerType.Default;
                }
                return Convert.ToInt32(valor);
            }
            set
            {
                StringDeConexao[AtributosDeConexaoFireBird.ServerType] = ((int)value).ToString();
            }
        }

        #endregion
        
        /// <summary>
        /// <para>Configura um <see cref="DbCommand"/> para executar um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para><see cref="DbCommand"/> configurado.</para></returns>
        private DbCommand ObterDbCommandConfigurado(string commandText, params DbParameter[] parametros)
        {
            DbCommand command = ConexaoEx.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            if (parametros != null)
            {
                command.Parameters.AddRange(parametros);
            }
            return command;
        }

        #region IConexaoComBancoDeDados Members

        private IInformacoesParaConexao informacoesParaConexao;
        /// <summary>
        /// <para>Informações para conexão com o banco de dados.</para>
        /// </summary>
        public IInformacoesParaConexao InformacoesParaConexao
        {
            get
            {
                return informacoesParaConexao;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Instância da conexão estabelecida.</para>
        /// <para>É o mesmo que a propriedade <see cref="ConexaoEx"/>.</para>
        /// </summary>
        public DbConnection Conexao
        {
            get
            {
                return ConexaoEx;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Informação sobre se a conexão está ativa ou não.</para>
        /// <para>Pode se estabelecer ou encerrar uma conexão por atribuir um valor.</para>
        /// </summary>
        public bool Conectado
        {
            get
            {
                return Conexao != null && Conexao.State == ConnectionState.Open;
            }
            set
            {
                if (value && !Conectado)
                {
                    Conexao.ConnectionString = StringDeConexao.ToString();
                    Conexao.Open();
                }
                else if (!value && Conectado)
                {
                    Conexao.Close();
                }
            }
        }

        private IStringDeConexao stringDeConexao;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Manipulador da string de conexão com o banco de dados.</para>
        /// <para>Quando seu valor é definido as outras propriedades também são ajustadas.</para>
        /// </summary>
        public IStringDeConexao StringDeConexao
        {
            get
            {
                return stringDeConexao;
            }
        }
        
        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o status da operação. 
        /// Não retorna dados.</para></returns>
        public int ExecuteNonQuery(string commandText, params DbParameter[] parametros)
        {
            return ExecuteNonQuery(null, commandText, parametros);
        }

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="transacao"><para>Transação ativa.</para></param>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o status da operação. 
        /// Não retorna dados.</para></returns>
        public int ExecuteNonQuery(DbTransaction transacao, string commandText, params DbParameter[] parametros)
        {
            DbCommand command = ObterDbCommandConfigurado(commandText, parametros);
            command.Transaction = transacao;
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// <para>Inicia uma transação.</para>
        /// </summary>
        /// <returns><para>Objeto representando uma transação.</para></returns>
        public DbTransaction BeginTransaction()
        {
            return Conexao.BeginTransaction();
        }

        /// <summary>
        /// <para>Inicia uma transação.</para>
        /// </summary>
        /// <param name="isolationLevel"><para>Nível de isolamento.</para></param>
        /// <returns><para>Objeto representando uma transação.</para></returns>
        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return Conexao.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna as linhas selecionadas.</para></returns>
        public DbDataReader ExecuteReader(string commandText, params DbParameter[] parametros)
        {
            return ObterDbCommandConfigurado(commandText, parametros).ExecuteReader();
        }

        /// <summary>
        /// <para>Executa um comando SQL.</para>
        /// </summary>
        /// <param name="commandText"><para>Comando SQL.</para></param>
        /// /// <param name="parametros"><para>Lista de parâmetros.</para></param>
        /// <returns><para>Retorna o valor escalar selecionado.</para></returns>
        public object ExecuteScalar(string commandText, params DbParameter[] parametros)
        {
            return ObterDbCommandConfigurado(commandText, parametros).ExecuteScalar();
        }

        /// <summary>
        /// <para>Configura um parâmetro para ser usado em conjunto
        /// com um comando SQL.</para>
        /// </summary>
        /// <param name="nome"><para>Nome do parâmetro no comando SQL.</para></param>
        /// <param name="valor"><para>Valor que será aplicado.</para></param>
        /// <param name="tipo"><para>Tipo do valor.</para></param>
        /// <returns><para><see cref="DbParameter"/> configurado.</para></returns>
        public DbParameter ConfigurarParametro(string nome, object valor, DbType tipo)
        {
            return new FbParameter { ParameterName = nome, DbType = tipo, Value = valor };
        }        

        #endregion

    }
}
