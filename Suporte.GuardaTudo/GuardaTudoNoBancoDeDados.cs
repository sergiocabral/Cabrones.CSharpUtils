using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suporte.Texto.FormatProvider;
using Suporte.BancoDeDados;
using System.Data.Common;
using Suporte.Texto;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>Estrutura que configura o banco de 
    /// dados para o <see cref="GuardaTudoNoBancoDeDados"/>.</para>
    /// </summary>
    public class ConfiguracaoDoBancoDeDados
    {
        /// <summary>
        /// <para>Nome da tabela no banco de dados.</para>
        /// </summary>
        public string Tabela { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Id único.</para>
        /// </summary>
        public string Campo_ID { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Id da informação pai.</para>
        /// </summary>
        public string Campo_ID_DO_PAI { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Nome da informação.</para>
        /// </summary>
        public string Campo_NOME { get; set; }

        /// <summary>
        /// <para>Tamanho do valor do campo: Nome da informação.</para>
        /// </summary>
        public int Campo_Tamanho_NOME { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Tipo de valor.</para>
        /// </summary>
        public string Campo_TIPO { get; set; }

        /// <summary>
        /// <para>Tamanho do valor do campo: Tipo de valor.</para>
        /// </summary>
        public int Campo_Tamanho_TIPO { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Referência a outro valor.</para>
        /// </summary>
        public string Campo_VALOR_REFERENCIA { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Valor texto.</para>
        /// </summary>
        public string Campo_VALOR_TEXTO { get; set; }

        /// <summary>
        /// <para>Tamanho do valor do campo: Valor texto.</para>
        /// </summary>
        public int Campo_Tamanho_VALOR_TEXTO { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Valor numérico.</para>
        /// </summary>
        public string Campo_VALOR_NUMERICO { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Valor data e hora.</para>
        /// </summary>
        public string Campo_VALOR_DATAHORA { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Valor booleano.</para>
        /// </summary>
        public string Campo_VALOR_BOOLEANO { get; set; }

        /// <summary>
        /// <para>Campo da tabela: Valor binário.</para>
        /// </summary>
        public string Campo_VALOR_BINARIO { get; set; }

        private string comandoSQLParaCriarATabela = null;
        /// <summary>
        /// <para>Comando SQL para criar a tabela no banco de dados.</para>
        /// <para>Quando atribuido um valor nulo ou vazio é assumido o 
        /// comando sql padrão.</para>
        /// </summary>
        public string ComandoSQLParaCriarATabela
        {
            get
            {
                if (string.IsNullOrWhiteSpace(comandoSQLParaCriarATabela))
                {
                    return string.Format(@"CREATE TABLE {0} ({1} INTEGER PRIMARY KEY, {2} INTEGER, {3} VARCHAR({4}), {5} VARCHAR({6}), {7} NUMERIC, {8} VARCHAR({9}), {10} NUMERIC, {11} TIMESTAMP, {12} SMALLINT, {13} BLOB);",
                        Tabela,                     //0
                        Campo_ID,                   //1
                        Campo_ID_DO_PAI,            //2
                        Campo_NOME,                 //3
                        Campo_Tamanho_NOME,         //4
                        Campo_TIPO,                 //5
                        Campo_Tamanho_TIPO,         //6
                        Campo_VALOR_REFERENCIA,     //7
                        Campo_VALOR_TEXTO,          //8
                        Campo_Tamanho_VALOR_TEXTO,  //9
                        Campo_VALOR_NUMERICO,       //10
                        Campo_VALOR_DATAHORA,       //11
                        Campo_VALOR_BOOLEANO,       //12
                        Campo_VALOR_BINARIO         //13
                        );
                }
                else
                {
                    return comandoSQLParaCriarATabela;
                }
            }
            set
            {
                comandoSQLParaCriarATabela = value;
            }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ConfiguracaoDoBancoDeDados()
        {
            Tabela = "TB_GUARDA_TUDO";
            Campo_ID = "ID";
            Campo_ID_DO_PAI = "ID_DO_PAI";
            Campo_NOME = "NOME";
            Campo_Tamanho_NOME = 255;
            Campo_TIPO = "TIPO";
            Campo_Tamanho_TIPO = 31;
            Campo_VALOR_REFERENCIA = "VALOR_REFERENCIA";
            Campo_VALOR_TEXTO = "VALOR_TEXTO";
            Campo_Tamanho_VALOR_TEXTO = 255;
            Campo_VALOR_NUMERICO = "VALOR_NUMERICO";
            Campo_VALOR_DATAHORA = "VALOR_DATAHORA";
            Campo_VALOR_BOOLEANO = "VALOR_BOOLEANO";
            Campo_VALOR_BINARIO = "VALOR_BINARIO";
        }
    }

    /// <summary>
    /// <para>Classe capaz de manipular <see cref="Informacao"/> e
    /// <see cref="Valor"/> e armazenar num banco de dados.</para>
    /// </summary>
    public class GuardaTudoNoBancoDeDados : GuardaTudo
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="conexaoComBancoDeDados">
        /// <para>Conexão com banco de dados.</para>
        /// </param>
        public GuardaTudoNoBancoDeDados(IConexaoComBancoDeDados conexaoComBancoDeDados, ConfiguracaoDoBancoDeDados configuracaoDoBancoDeDados)
        {
            Conexao = conexaoComBancoDeDados;
            ConfiguracaoDoBancoDeDados = configuracaoDoBancoDeDados;
            ConfigurarBancoDeDados(ConfiguracaoDoBancoDeDados);
        }

        /// <summary>
        /// <para>Estrutura que configura o banco de dados.</para>
        /// </summary>
        protected ConfiguracaoDoBancoDeDados ConfiguracaoDoBancoDeDados { get; private set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Conexão com banco de dados.</para>
        /// </summary>
        protected IConexaoComBancoDeDados Conexao { get; private set; }

        /// <summary>
        /// <para>Configura o banco de dados.</para>
        /// </summary>
        /// <param name="ConfiguracaoDoBancoDeDados">Informações de configuração 
        /// do banco de dados.</param>
        private void ConfigurarBancoDeDados(ConfiguracaoDoBancoDeDados ConfiguracaoDoBancoDeDados)
        {
            try
            {
                //A linha abaixo faz com que a tabela seja excluida para que seja criada novamente.
                //Conexao.ExecuteNonQuery("DROP TABLE " + ConfiguracaoDoBancoDeDados.Tabela);

                Conexao.ExecuteNonQuery(string.Format(@"SELECT {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} FROM {0}",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID,
                    ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI,
                    ConfiguracaoDoBancoDeDados.Campo_NOME,
                    ConfiguracaoDoBancoDeDados.Campo_TIPO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO));
            }
            catch (Exception)
            {
                try
                {
                    Conexao.ExecuteNonQuery(ConfiguracaoDoBancoDeDados.ComandoSQLParaCriarATabela);
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível criar a estrutura de banco de dados para o Guarda Tudo.", ex);
                }
            }
        }

        /// <summary>
        /// <para>Obtem os dados de um <see cref="DbDataReader"/> e 
        /// prepara uma instãncia de <see cref="Informacao"/>.</para>
        /// </summary>
        /// <param name="reader"><para><see cref="DbDataReader"/> já carregado.</para></param>
        /// <returns><para>Retorna uma instância de <see cref="Informacao"/>.</para></returns>
        private Informacao ObterInformacao(DbDataReader reader)
        {
            return new Informacao
            {
                Id = (int)reader[ConfiguracaoDoBancoDeDados.Campo_ID],
                Nome = (string)reader[ConfiguracaoDoBancoDeDados.Campo_NOME],
                Valor = ObterValor(reader),
                Pai = reader[ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI] == DBNull.Value ? null : new Informacao { Id = (int)reader[ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI] }
            };
        }

        /// <summary>
        /// <para>Obtem de um <see cref="DbDataReader"/> o <see cref="Valor"/>.</para>
        /// </summary>
        /// <param name="reader"><para><see cref="DbDataReader"/> já carregado.</para></param>
        /// <returns><para>Retorna uma instância de <see cref="Valor"/>.</para></returns>
        private Valor ObterValor(DbDataReader reader)
        {
            if (reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA] != DBNull.Value)
            {
                long idDaInformacaoDeReferencia = long.Parse(reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA].ToString());
                Informacao informacao = ObterInformacao(new Informacao { Id = idDaInformacaoDeReferencia });
                return new Valor(informacao);
            }
            else
            {
                TipoDeValor tipo = (TipoDeValor)Enum.Parse(typeof(TipoDeValor), Convert.ToString(reader[ConfiguracaoDoBancoDeDados.Campo_TIPO]));
                switch (tipo)
                {
                    case TipoDeValor.Nenhum:
                        return new Valor();
                    case TipoDeValor.Binario:
                        return new Valor((byte[])reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO]);
                    case TipoDeValor.Booleano:
                        return new Valor(Convert.ToBoolean(reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO]));
                    case TipoDeValor.DataHora:
                        return new Valor(Convert.ToDateTime(reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA]));
                    case TipoDeValor.Numerico:
                        return new Valor(Convert.ToDecimal(reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO]));
                    case TipoDeValor.Texto:
                        return new Valor(Convert.ToString(reader[ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO]));
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>/
        /// <para>Obter próximo Id para gravação de registro na tabela.</para>
        /// </summary>
        /// <returns><para>Próximo Id.</para></returns>
        private int ObterProximoId()
        {
            object valor = Conexao.ExecuteScalar(
                string.Format(
                    "select max({1}) + 1 from {0}",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID
                ), null);
            return valor == DBNull.Value ? 1 : (int)(Int64)valor;
        }

        /// <summary>
        /// <para>Prepara o comando SQL e os parâmetros para serem usados 
        /// em um uma consulta de banco de dados.</para>
        /// </summary>
        /// <param name="criterio"><para>Critério aplicado a consulta.</para></param>
        /// <param name="sql"><para>Armazena o comando SQL que será enviado para o banco de dados.</para></param>
        /// <param name="parametros"><para>Lista com os parâmetros usados no comando SQL.</para></param>
        private void PrepararConsulta(Criterio criterio, out string sql, ref List<DbParameter> parametros, int index)
        {
            index++;
            if (criterio != null)
            {
                TipoDeValor tipoDeValor = Valor.ObterTipo(criterio.Valor);
                string variavelValor = "@valor" + index.ToString();

                string campoParaComparacao;
                switch (criterio.CampoParaComparacao)
                {
                    case CamposParaComparacao.Identificador:
                        campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_ID;
                        break;
                    case CamposParaComparacao.Nome:
                        campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_NOME;
                        break;
                    case CamposParaComparacao.Valor:
                        switch (tipoDeValor)
                        {
                            case TipoDeValor.Binario:
                                campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO;
                                break;
                            case TipoDeValor.Booleano:
                                campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO;
                                break;
                            case TipoDeValor.DataHora:
                                campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA;
                                break;
                            case TipoDeValor.Numerico:
                                campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO;
                                break;
                            case TipoDeValor.Texto:
                                campoParaComparacao = ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO;
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

                string comparador;
                switch (criterio.Comparador)
                {
                    case Comparadores.Contem:
                        comparador = "like " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, "%" + criterio.Valor.ToString() + "%", Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.Diferente:
                        comparador = "<> " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.Igual:
                        comparador = "= " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.IniciaCom:
                        comparador = "like " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor.ToString() + "%", Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.Maior:
                        comparador = "> " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.MaiorIgual:
                        comparador = ">= " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.Menor:
                        comparador = "< " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.MenorIgual:
                        comparador = "<= " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, criterio.Valor, Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    case Comparadores.TerminaCom:
                        comparador = "like " + variavelValor;
                        parametros.Add(Conexao.ConfigurarParametro(variavelValor, "%" + criterio.Valor.ToString(), Valor.ObterTipoDeBancoDeDados(tipoDeValor)));
                        break;
                    default:
                        throw new NotImplementedException();
                }

                string criterioAtual = "(" + campoParaComparacao + " " + comparador + ")";
                if (criterio.OutroCriterio != null)
                {
                    string sqlOutroCriterio;
                    PrepararConsulta(criterio.OutroCriterio, out sqlOutroCriterio, ref parametros, index);

                    string juncao;
                    switch (criterio.OperadorParaOutroCriterio)
                    {
                        case OperadorDeAgrupamento.AND:
                            juncao = criterioAtual + " AND " + sqlOutroCriterio;
                            break;
                        case OperadorDeAgrupamento.NAND:
                            juncao = "NOT (" + criterioAtual + " AND " + sqlOutroCriterio + ")";
                            break;
                        case OperadorDeAgrupamento.NOR:
                            juncao = "NOT (" + criterioAtual + " OR " + sqlOutroCriterio + ")";
                            break;
                        case OperadorDeAgrupamento.NXOR:
                            juncao = "NOT ((" + criterioAtual + " AND NOT " + sqlOutroCriterio + ") OR (NOT " + criterioAtual + " AND " + sqlOutroCriterio + "))";
                            break;
                        case OperadorDeAgrupamento.OR:
                            juncao = criterioAtual + " OR " + sqlOutroCriterio;
                            break;
                        case OperadorDeAgrupamento.XOR:
                            juncao = "(" + criterioAtual + " AND NOT " + sqlOutroCriterio + ") OR (NOT " + criterioAtual + " AND " + sqlOutroCriterio + ")";
                            break;
                        default: throw new NotImplementedException();
                    }
                    sql = juncao;
                }
                else
                {
                    sql = criterioAtual;
                }
            }
            else
            {
                sql = string.Empty;
            }
        }

        #region Abstract

        /// <summary>
        /// <para>Indica o modo de comportamento do objeto.</para>
        /// </summary>
        public override ComportamentoParaGuardaTudo Comportamento
        {
            set
            {
                if ((value & ComportamentoParaGuardaTudo.IgnorarAcentuacaoAoComparar) == ComportamentoParaGuardaTudo.IgnorarAcentuacaoAoComparar ||
                    (value & ComportamentoParaGuardaTudo.IgnorarMaiusculasEMinusculasAoComparar) == ComportamentoParaGuardaTudo.IgnorarMaiusculasEMinusculasAoComparar)
                {
                    throw new NotImplementedException("A busca no banco de dados deve ser feita apenas por termos exatos.");
                }
                base.Comportamento = value;
            }
            get
            {
                return base.Comportamento;
            }
        }

        /// <summary>
        /// <para>Consulta uma ou mais <see cref="Informacao"/> da base de dados de acordo
        /// com os critérios de consultas.</para>
        /// </summary>
        /// <param name="criterio">
        /// <para>Critérios de consulta.</para>
        /// </param>
        /// <returns>
        /// <para>Lista com as <see cref="Informacao"/> encontradas.</para>
        ///// </returns>
        public override IList<Informacao> Consultar(Criterio criterio)
        {
            List<Informacao> consulta = new List<Informacao>();

            string sql;
            List<DbParameter> parametros = new List<DbParameter>();

            PrepararConsulta(criterio, out sql, ref parametros, 0);

            if (sql.Length > 0)
            {
                sql = sql.Insert(0, " where ");
            }
            sql = sql.Insert(0, "select * from " + ConfiguracaoDoBancoDeDados.Tabela);

            DbDataReader reader = Conexao.ExecuteReader(sql, parametros.ToArray());
            while (reader.Read())
            {
                Informacao i = ObterInformacao(reader);
                ValidarValoresDosFilhos(i);
                consulta.Add(i);
            }

            return consulta;
        }

        /// <summary>
        /// <para>Obtem a partir do identificador uma <see cref="Informacao"/> 
        /// cadastrada na base de dados.</para>
        /// </summary>
        /// <param name="informacao"><para><see cref="Informacao"/></para></param>
        /// <returns><para>Caso não exista retorna <c>null</c>.</para></returns>
        protected override Informacao ObterInformacao(Informacao informacao)
        {
            DbDataReader reader = Conexao.ExecuteReader(
                string.Format(
                    "select * from {0} where {1} = @Campo_ID",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID
                ),
                new DbParameter[] {
                    Conexao.ConfigurarParametro("Campo_ID", informacao.Id, System.Data.DbType.Int32)
                }
            );

            if (reader.Read())
            {
                Informacao i = ObterInformacao(reader);
                ValidarValoresDosFilhos(i);
                return i;
            }

            return null;
        }

        /// <summary>
        /// <para>Valida os valores dos filhos para garantir
        /// a integridade dos dados.</para>
        /// </summary>
        /// <param name="informacao"><see cref="Informacao"/> na base de dados.</param>
        protected override void ValidarValoresDosFilhos(Informacao informacao)
        {
            DbDataReader reader = Conexao.ExecuteReader(
                string.Format(
                    "select * from {0} where {1} = @Campo_ID",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI
                ),
                new DbParameter[] {
                    Conexao.ConfigurarParametro("Campo_ID", informacao.Id, System.Data.DbType.Int32)
                }
            );

            informacao.Filhos = new List<Informacao>();
            while (reader.Read())
            {
                Informacao i = ObterInformacao(reader);
                i.Pai = informacao;
                informacao.Filhos.Add(i);
            }
        }

        /// <summary>
        /// <para>Obtem o tipo de valor real, pesquisando
        /// mais a dentro quando for por referência.</para>
        /// </summary>
        /// <param name="informacao"><para>Informação</para></param>
        /// <returns><para>Tipo de valor.</para></returns>
        protected TipoDeValor ObterTipoDeValor(Informacao informacao)
        {
            if (informacao.Valor.Tipo != TipoDeValor.Referencia)
            {
                return informacao.Valor.Tipo;
            }
            else
            {
                return ObterTipoDeValor(informacao.Valor.Informacao);
            }
        }

        /// <summary>
        /// <para>
        /// Incluir uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected override void Incluir(Informacao informacao)
        {
            int id = ObterProximoId();
            
            TipoDeValor tipo = ObterTipoDeValor(informacao);            

            Conexao.ExecuteNonQuery(
                string.Format(
                    @"insert into {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}) 
                      values (@Campo_ID, @Campo_ID_DO_PAI, @Campo_NOME, @Campo_TIPO, @Campo_VALOR_REFERENCIA,
                              @Campo_VALOR_TEXTO, @Campo_VALOR_NUMERICO, @Campo_VALOR_DATAHORA, 
                              @Campo_VALOR_BOOLEANO, @Campo_VALOR_BINARIO)",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID,
                    ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI,
                    ConfiguracaoDoBancoDeDados.Campo_NOME,
                    ConfiguracaoDoBancoDeDados.Campo_TIPO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO
                ),
                new DbParameter[] {
                    Conexao.ConfigurarParametro("Campo_ID", id, System.Data.DbType.Int32),
                    Conexao.ConfigurarParametro("Campo_ID_DO_PAI", informacao.Pai == null ? null : (long?)informacao.Pai.Id, System.Data.DbType.Int32),
                    Conexao.ConfigurarParametro("Campo_NOME", informacao.Nome, System.Data.DbType.String),
                    Conexao.ConfigurarParametro("Campo_TIPO", informacao.Valor.Tipo.ToString(), System.Data.DbType.String),
                    Conexao.ConfigurarParametro("Campo_VALOR_REFERENCIA", informacao.Valor.Informacao == null ? null : (long?)informacao.Valor.Informacao.Id, System.Data.DbType.String),
                    Conexao.ConfigurarParametro("Campo_VALOR_TEXTO", tipo != TipoDeValor.Texto ? null : informacao.Valor.Texto, System.Data.DbType.String),
                    Conexao.ConfigurarParametro("Campo_VALOR_NUMERICO", tipo != TipoDeValor.Numerico ? null : (decimal?)informacao.Valor.Numerico, System.Data.DbType.Decimal),
                    Conexao.ConfigurarParametro("Campo_VALOR_DATAHORA", tipo != TipoDeValor.DataHora ? null : (DateTime?)informacao.Valor.DataHora, System.Data.DbType.DateTime),
                    Conexao.ConfigurarParametro("Campo_VALOR_BOOLEANO", tipo != TipoDeValor.Booleano ? null : (bool?)informacao.Valor.Booleano, System.Data.DbType.Boolean),
                    Conexao.ConfigurarParametro("Campo_VALOR_BINARIO", tipo != TipoDeValor.Binario ? null : informacao.Valor.Binario, System.Data.DbType.Binary)
                }
            );
            informacao.Id = id;
        }

        /// <summary>
        /// <para>
        /// Atualizar uma informação na base de dados.
        /// </para>
        /// </summary>
        /// <param name="informacao">
        /// <para>Informação para inclusão.</para>
        /// </param>
        protected override void Atualizar(Informacao informacao)
        {
            TipoDeValor tipo = ObterTipoDeValor(informacao);

            List<DbParameter> parametrosDosValores = new List<DbParameter>();
            parametrosDosValores.Add(Conexao.ConfigurarParametro("Campo_VALOR_TEXTO", tipo != TipoDeValor.Texto ? null : informacao.Valor.Texto, System.Data.DbType.String));
            parametrosDosValores.Add(Conexao.ConfigurarParametro("Campo_VALOR_NUMERICO", tipo != TipoDeValor.Numerico ? null : (decimal?)informacao.Valor.Numerico, System.Data.DbType.Decimal));
            parametrosDosValores.Add(Conexao.ConfigurarParametro("Campo_VALOR_DATAHORA", tipo != TipoDeValor.DataHora ? null : (DateTime?)informacao.Valor.DataHora, System.Data.DbType.DateTime));
            parametrosDosValores.Add(Conexao.ConfigurarParametro("Campo_VALOR_BOOLEANO", tipo != TipoDeValor.Booleano ? null : (bool?)informacao.Valor.Booleano, System.Data.DbType.Boolean));
            parametrosDosValores.Add(Conexao.ConfigurarParametro("Campo_VALOR_BINARIO", tipo != TipoDeValor.Binario ? null : informacao.Valor.Binario, System.Data.DbType.Binary));

            List<DbParameter> parametros = new List<DbParameter>();
            parametros.Add(Conexao.ConfigurarParametro("Campo_ID", informacao.Id, System.Data.DbType.Int32));
            parametros.Add(Conexao.ConfigurarParametro("Campo_ID_DO_PAI", informacao.Pai == null ? null : (long?)informacao.Pai.Id, System.Data.DbType.Int32));
            parametros.Add(Conexao.ConfigurarParametro("Campo_NOME", informacao.Nome, System.Data.DbType.String));
            parametros.Add(Conexao.ConfigurarParametro("Campo_TIPO", informacao.Valor.Tipo.ToString(), System.Data.DbType.String));
            parametros.Add(Conexao.ConfigurarParametro("Campo_VALOR_REFERENCIA", informacao.Valor.Informacao == null ? null : (long?)informacao.Valor.Informacao.Id, System.Data.DbType.String));
            parametros.AddRange(parametrosDosValores);

            List<long> listaDeReferenciasParaAtualizar = ObterReferenciasParaAtualizar(informacao.Id);

            DbTransaction transaction = Conexao.BeginTransaction();

            Conexao.ExecuteNonQuery(
                transaction,
                string.Format(
                    @"update {0}  set
                             {2}  = @Campo_ID_DO_PAI, 
                             {3}  = @Campo_NOME, 
                             {4}  = @Campo_TIPO, 
                             {5}  = @Campo_VALOR_REFERENCIA, 
                             {6}  = @Campo_VALOR_TEXTO, 
                             {7}  = @Campo_VALOR_NUMERICO, 
                             {8}  = @Campo_VALOR_DATAHORA, 
                             {9}  = @Campo_VALOR_BOOLEANO, 
                             {10} = @Campo_VALOR_BINARIO
                       where {1}  = @Campo_ID",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID,
                    ConfiguracaoDoBancoDeDados.Campo_ID_DO_PAI,
                    ConfiguracaoDoBancoDeDados.Campo_NOME,
                    ConfiguracaoDoBancoDeDados.Campo_TIPO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO
                ),
                parametros.ToArray()
            );

            if (listaDeReferenciasParaAtualizar.Count > 0)
            {
                string referenciasParaAtualizar = Converter.DeLista(listaDeReferenciasParaAtualizar, ",");
                Conexao.ExecuteNonQuery(
                    transaction,
                    string.Format(
                        @"update {1} set
                             {3} = @Campo_VALOR_TEXTO, 
                             {4} = @Campo_VALOR_NUMERICO, 
                             {5} = @Campo_VALOR_DATAHORA, 
                             {6} = @Campo_VALOR_BOOLEANO, 
                             {7} = @Campo_VALOR_BINARIO
                       where {2} in ({0})",
                        referenciasParaAtualizar,
                        ConfiguracaoDoBancoDeDados.Tabela,
                        ConfiguracaoDoBancoDeDados.Campo_ID,
                        ConfiguracaoDoBancoDeDados.Campo_VALOR_TEXTO,
                        ConfiguracaoDoBancoDeDados.Campo_VALOR_NUMERICO,
                        ConfiguracaoDoBancoDeDados.Campo_VALOR_DATAHORA,
                        ConfiguracaoDoBancoDeDados.Campo_VALOR_BOOLEANO,
                        ConfiguracaoDoBancoDeDados.Campo_VALOR_BINARIO
                    ),
                    parametrosDosValores.ToArray()
                );
            }

            transaction.Commit();
        }

        /// <summary>
        /// <para>Obtem uma lista de id de informação que tem seu valor
        /// por referência e precisam ser atualizados.</para>
        /// </summary>
        /// <param name="informacaoId"><para>Id da informação referenciada.</para></param>
        /// <returns><para>Lista de id de informação.</para></returns>
        private List<long> ObterReferenciasParaAtualizar(long informacaoId)
        {
            List<long> listaDeReferencia = new List<long>();

            DbDataReader reader = Conexao.ExecuteReader(
                string.Format(
                    "select * from {0} where {1} = @Campo_VALOR_REFERENCIA",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_VALOR_REFERENCIA
                ),
                new DbParameter[] {
                    Conexao.ConfigurarParametro("Campo_VALOR_REFERENCIA", informacaoId, System.Data.DbType.String)
                });
            while (reader.Read())
            {
                Informacao i = ObterInformacao(reader);
                listaDeReferencia.Add(i.Id);
                listaDeReferencia.AddRange(ObterReferenciasParaAtualizar(i.Id));
            }
            return listaDeReferencia;
        }

        /// <summary>
        /// <para>Excluir uma informação da base de dados.</para>
        /// </summary>
        /// <param name="informacao">Informação para exclusão.</param>
        protected override void Excluir(Informacao informacao)
        {
            Conexao.ExecuteNonQuery(
                string.Format(
                    @"delete from {0} where {1} = @Campo_ID",
                    ConfiguracaoDoBancoDeDados.Tabela,
                    ConfiguracaoDoBancoDeDados.Campo_ID
                ),
                new DbParameter[] {
                    Conexao.ConfigurarParametro("Campo_ID", informacao.Id, System.Data.DbType.Int32)
                }
            );
        }

        #endregion

    }
}
