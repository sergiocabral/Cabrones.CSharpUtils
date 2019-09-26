using System.Collections.Generic;
using System.Web.SessionState;
using Suporte.IO;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Padrão de projeto (Design Patterns): Decorator.</para>
    /// <para>Esta classe deriva da classe <see cref="Suporte.Web.HttpSessionStateDecorador" /> com 
    /// o objetivo de implementar o padrão de projeto Decorator sobre a 
    /// classe <see cref="System.Web.SessionState.HttpSessionState" />.</para>
    /// <para>Isso naturalmente não seria possível, visto 
    /// que <see cref="System.Web.SessionState.HttpSessionState" /> é do tipo 
    /// selead (não podendo ser herdada por uma nova classe).</para> 
    /// <para>Para maiores informações veja o descrivo 
    /// da classe <see cref="Suporte.Web.HttpSessionStateDecorador" />.</para>
    /// <para>Esta classe permite salvar o conteudo da 
    /// sessão ASP.NET em outra mídia de gravação além da memória do servidor.
    /// Para isso usa-se extensivamente a 
    /// interface <see cref="Suporte.IO.IPersistenciaDeDadoGenerico&lt;T&gt;"/>.</para>
    /// </summary>
    public class HttpSessionStatePersistente : HttpSessionStateDecorador
    {
        private IPersistenciaDeDadoGenerico<Dictionary<string, object>> dadosPersistente;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Coleção dos dados contidos na sessão ASP.NET atual.</para>
        /// <para>Esta coleção permite a gravação de seus dados em 
        /// uma mídia de gravação qualquer.</para>
        /// </summary>
        protected IPersistenciaDeDadoGenerico<Dictionary<string, object>> DadosPersistente
        {
            get { return dadosPersistente; }
        }

        private static IList<string> listaPrefixoIgnoraGravacao = new List<string>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Armazena uma lista com prefixos. Quando algum desses 
        /// prefixos é encontrado em um item da coleção dos dados contidos 
        /// na sessão ASP.NET, o item não é armazenado na mídia de gravação.</para>
        /// <para>O valor desta propriedade é do tipo <c>static</c>.</para>
        /// </summary>
        public IList<string> ListaPrefixoIgnoraGravacao
        {
            get { return listaPrefixoIgnoraGravacao; }
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        /// <param name="session">
        /// <para>Deve ser uma referência ao objeto da sessão atual, que pode ser acessado pelo 
        /// código <c>System.Web.HttpContext.Current.Session</c>.</para>
        /// </param>
        /// <param name="dadosPersistente">
        /// <para>Deve ser uma referência a um objeto derivado do 
        /// tipo <see cref="IPersistenciaDeDadoGenerico&lt;T&gt;" /> onde T seja 
        /// uma instância de <c>Dictionary&lt;string, object&gt;</c>.</para>
        /// </param>
        public HttpSessionStatePersistente(HttpSessionState session, IPersistenciaDeDadoGenerico<Dictionary<string, object>> dadosPersistente)
            : base(session)
        {
            this.dadosPersistente = dadosPersistente;
            CarregarParaSessao();
            SalvarDaSessao();
        }

        #region Operações de gravação e leitura

        /// <summary>
        /// <para>Quando igual a true, sempre força o carregamento para a sessão,
        /// mesmo que não se trate de uma nova sessão (ou seja, <c>IsNewSession==false</c>).</para>
        /// </summary>
        public bool SempreCarregarParaSessao { get; set; }

        /// <summary>
        /// <para>Verifica se um nome de campo na coleção dos dados contidos 
        /// na sessão ASP.NET deve ser ignorado para armazenamento 
        /// na mídia de gravação.</para>
        /// </summary>
        /// <param name="campo"><para>Nome do campo correspondente ao item da
        /// na coleção dos dados contidos na sessão ASP.NET</para></param>
        /// <param name="dados"><para>Valor vinculado ao campo.</para></param>
        /// <returns><para>Retorna <c>true</c> se o item da coleção dos dados contidos 
        /// na sessão ASP.NET enquadrar-se nos seguintes casos:</para>
        /// <para>1) Iniciar como algum dos prefixos contidos na 
        /// lista <see cref="ListaPrefixoIgnoraGravacao"/>.</para>
        /// <para>2) Possuir um valor armazenado que não permite serialização.</para>
        /// </returns>
        public bool CampoDeveSerIgnorador(string campo, object dados)
        {
            if (dados != null)
            {
                if (!dados.GetType().IsSerializable)
                {
                    return true;
                }

                foreach (string prefixo in ListaPrefixoIgnoraGravacao)
                {
                    if (!string.IsNullOrEmpty(prefixo) &&
                        campo.IndexOf(prefixo) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// <para>Carrega todos os dados contidos na coleção da mídia 
        /// de armazenamento (<see cref="DadosPersistente"/>) para a sessão ASP.NET atual.</para>
        /// <para>Porém, esta rotina apenas é executada se a sessão ASP.NET atual 
        /// for nova, ou seja, se a requisição atual tiver criado uma sessão 
        /// nova, sem dados.</para>
        /// <para>Caberá ao objeto em <see cref="DadosPersistente" />, do 
        /// tipo <see cref="IPersistenciaDeDadoGenerico&lt;T&gt;" />,
        /// carregar os dados da mídia de armazenamento.</para>
        /// </summary>
        public void CarregarParaSessao()
        {
            if (SempreCarregarParaSessao || (IsNewSession && SessionOriginal.Contents.Count == 0))
            {
                DadosPersistente.Carregar();
                if (DadosPersistente.Dado.Valor != null)
                {
                    foreach (KeyValuePair<string, object> valor in DadosPersistente.Dado.Valor)
                    {
                        SessionOriginal[valor.Key] = valor.Value;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Salva todos os dados contidos na sessão ASP.NET atual para a 
        /// coleção da mídia de armazenamento (<see cref="DadosPersistente" />).</para>
        /// <para>Caberá ao objeto em <see cref="DadosPersistente" />, do 
        /// tipo <see cref="IPersistenciaDeDadoGenerico&lt;T&gt;" />,
        /// salvar os dados na mídia de armazenamento.</para>
        /// </summary>
        public void SalvarDaSessao()
        {
            if (DadosPersistente.Dado.Valor == null)
            {
                DadosPersistente.Dado.Valor = new Dictionary<string, object>();
            }

            foreach (string campo in SessionOriginal.Keys)
            {
                if (!CampoDeveSerIgnorador(campo, SessionOriginal[campo]))
                {
                    if (!DadosPersistente.Dado.Valor.ContainsKey(campo))
                    {
                        DadosPersistente.Dado.Valor.Add(campo, SessionOriginal[campo]);
                    }
                    else
                    {
                        DadosPersistente.Dado.Valor[campo] = SessionOriginal[campo];
                    }
                }
                else
                {
                    DadosPersistente.Dado.Valor.Remove(campo);
                }
            }

            DadosPersistente.Gravar();
        }

        /// <summary>
        /// <para>Adiciona um novo item a coleção da 
        /// mídia de armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        /// <param name="campo"><para>Chave identificadora do campo.</para></param>
        /// <param name="dados"><para>Valor a ser gravado.</para>
        /// <para>Quando igual a <c>null</c>, apaga o item da coleção da 
        /// mídia de armazenamento (<see cref="DadosPersistente" />).</para></param>
        public void Gravar(string campo, object dados)
        {
            if (DadosPersistente.Dado.Valor == null)
            {
                DadosPersistente.Dado.Valor = new Dictionary<string, object>();
            }

            if (!CampoDeveSerIgnorador(campo, dados))
            {
                if (!DadosPersistente.Dado.Valor.ContainsKey(campo))
                {
                    if (dados != null)
                    {
                        DadosPersistente.Dado.Valor.Add(campo, dados);
                    }
                }
                else
                {
                    if (dados != null)
                    {
                        DadosPersistente.Dado.Valor[campo] = dados;
                    }
                    else
                    {
                        DadosPersistente.Dado.Valor.Remove(campo);
                    }
                }
            }
            DadosPersistente.Gravar();
        }

        /// <summary>
        /// <para>Apaga um item da coleção da mídia de 
        /// armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        /// <param name="campo"><para>Chave identificadora do campo.</para></param>
        public void Apagar(string campo)
        {
            Gravar(campo, null);
        }

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores da 
        /// coleção da mídia de armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        public void ApagarTudo()
        {
            foreach (string key in SessionOriginal.Keys)
            {
                Apagar(key);
            }
        }

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores 
        /// da coleção de dados da sessão.</para>
        /// <para>Adicionalmente, remove todas as chaves da coleção da mídia de 
        /// armazenamento (<see cref="DadosPersistente" />).</para>
        /// <para>Idêntico ao método <see cref="RemoveAll()" />.</para>
        /// <param name="limparTudo">
        /// <para>Quando igual a <c>true</c> limpa os dados da memória e do arquivo,
        /// quando igual a <c>false</c> limpa apenas a memória.</para>
        /// </param>
        /// </summary>
        public void Clear(bool limparTudo)
        {
            if (limparTudo)
            {
                ApagarTudo();
            }
            SessionOriginal.Clear();
        }

        #endregion

        #region Sobrecarga de métodos e propriedades

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referencia pelo índice um valor da coleção de dados da sessão.</para>
        /// <para>Caso a sessão tenha sido finalizada (isto é, a sessão caiu). 
        /// Carrega os dados da mídia de armazenamento para a atual nova sessão.</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        /// <returns><para>Valor associado ao índice informado.</para></returns>
        public override object this[int index]
        {
            get
            {
                CarregarParaSessao();
                return SessionOriginal[index];
            }
            set
            {
                Gravar(SessionOriginal.Keys[index], value);
                SessionOriginal[index] = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referencia pelo nome da chave um valor da coleção de dados da sessão.</para>
        /// <para>Caso a sessão tenha sido finalizada (isto é, a sessão caiu). 
        /// Carrega os dados da mídia de armazenamento para a atual nova sessão.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave associada a uma valor.</para></param>
        /// <returns><para>Valor associado a chave informada.</para></returns>
        public override object this[string name]
        {
            get
            {
                CarregarParaSessao();
                return SessionOriginal[name];
            }
            set
            {
                Gravar(name, value);
                SessionOriginal[name] = value;
            }
        }

        /// <summary>
        /// <para>Adiciona um novo item a coleção de dados da sessão.</para>
        /// <para>Adicionalmente, salva o novo item na coleção da 
        /// mídia de armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        /// <param name="value"><para>Conteúdo do valor.</para></param>
        public override void Add(string name, object value)
        {
            Gravar(name, value);
            SessionOriginal.Add(name, value);
        }

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores 
        /// da coleção de dados da sessão.</para>
        /// <para>Adicionalmente, remove todas as chaves da coleção da mídia de 
        /// armazenamento (<see cref="DadosPersistente" />).</para>
        /// <para>Idêntico ao método <see cref="RemoveAll()" />.</para>
        /// </summary>
        public override void Clear()
        {
            Clear(true);
        }

        /// <summary>
        /// <para>Apaga um item da coleção de dados da sessão.</para>
        /// <para>Adicionalmente, remove o item da coleção da mídia de 
        /// armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        public override void Remove(string name)
        {
            Apagar(name);
            SessionOriginal.Remove(name);
        }

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores 
        /// da coleção de dados da sessão.</para>
        /// <para>Adicionalmente, remove todas as chaves da coleção da mídia 
        /// de armazenamento (<see cref="DadosPersistente" />).</para>
        /// <para>Idêntico ao método <see cref="Clear()" />.</para>
        /// </summary>
        public override void RemoveAll()
        {
            ApagarTudo();
            SessionOriginal.RemoveAll();
        }

        /// <summary>
        /// <para>Apaga pelo índice um item da coleção de dados da sessão.</para>
        /// <para>Adicionalmente, remove o item da coleção da mídia de 
        /// armazenamento (<see cref="DadosPersistente" />).</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        public override void RemoveAt(int index)
        {
            Apagar(SessionOriginal.Keys[index]);
            SessionOriginal.RemoveAt(index);
        }

        #endregion

    }
}