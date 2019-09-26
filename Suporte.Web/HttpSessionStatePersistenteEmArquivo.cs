using System.Collections.Generic;
using System.IO;
using System.Web.SessionState;
using Suporte.Dados;
using Suporte.IO;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Esta classe deriva da classe <see cref="Suporte.Web.HttpSessionStatePersistente" />. 
    /// Veja o seu descritivo para mais informações.</para>
    /// <para>O construtor da classe ancestral 
    /// (<see cref="Suporte.Web.HttpSessionStatePersistente(HttpSessionState, IPersistenciaDeDadoGenerico&lt;Dictionary&lt;string, object&gt;&gt;)" />) 
    /// recebe como parâmetro um objeto do 
    /// tipo <see cref="IPersistenciaDeDadoGenerico&lt;T&gt;" />, onde T é <c>Dictionary&lt;string, object&gt;</c>.</para>
    /// <para>Esta classe derivada (<see cref="Suporte.Web.HttpSessionStatePersistenteEmArquivo" />) sobreescreve 
    /// o construtor e passa a receber como parâmetro o caminho de um arquivo.
    /// Dessa forma, a coleção da mídia de armazenamento (<see cref="HttpSessionStatePersistente.DadosPersistente" />) é 
    /// especializada com o 
    /// tipo <see cref="PersistenciaDeDadoGenericoEmArquivo&lt;T&gt;" /> onde T é <c>Dictionary&lt;string, object&gt;</c>.</para>
    /// <para>Resumindo: esta classe permite salvar o conteudo da sessão ASP.NET 
    /// em um arquivo além da memória do servidor.</para>
    /// </summary>
    public class HttpSessionStatePersistenteEmArquivo : HttpSessionStatePersistente
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência ao arquivo onde os dados da 
        /// sessão ASP.NET são gravados.</para>
        /// </summary>
        public FileInfo Arquivo
        {
            get
            {
                return ((PersistenciaDeDadoGenericoEmArquivo<Dictionary<string, object>>)DadosPersistente).Arquivo;
            }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="session">
        /// <para>Deve ser uma referência ao objeto da sessão atual, que pode ser acessado pelo 
        /// código <c>System.Web.HttpContext.Current.Session</c>.</para>
        /// </param>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo onde será armazenado o conteudo da sessão ASP.NET.</para>
        /// </param>
        public HttpSessionStatePersistenteEmArquivo(HttpSessionState session, string caminhoArquivo)
            : base(session, new PersistenciaDeDadoGenericoEmArquivo<Dictionary<string, object>>(caminhoArquivo))
        {
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="session">
        /// <para>Deve ser uma referência ao objeto da sessão atual, que pode ser acessado pelo 
        /// código <c>System.Web.HttpContext.Current.Session</c>.</para>
        /// </param>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo onde será armazenado o conteudo da sessão ASP.NET.</para>
        /// </param>
        /// <param name="formatador">
        /// <para>Formatador usado para ler e escrever os dados no arquivo.</para>
        /// </param>
        public HttpSessionStatePersistenteEmArquivo(HttpSessionState session, string caminhoArquivo, IFormatador<Stream> formatador)
            : base(session, new PersistenciaDeDadoGenericoEmArquivo<Dictionary<string, object>>(caminhoArquivo, formatador))
        {
        }

    }
}