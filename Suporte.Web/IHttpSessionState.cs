using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Esta interface provê as assinaturas dos métodos e propriedades da 
    /// classe <see cref="System.Web.SessionState.HttpSessionState" />.</para>
    /// <para>A classe <see cref="System.Web.SessionState.HttpSessionState" /> foi construida 
    /// como sealed (não podendo ser herdada por uma nova classe).
    /// Deste modo, esta interface permite que uma nova classe possa pelo menos 
    /// declarar as mesmas assinaturas que a 
    /// classe <see cref="System.Web.SessionState.HttpSessionState" />.</para>
    /// <para>Importante lembrar que <see cref="System.Web.SessionState.HttpSessionState" /> não fará 
    /// cast (conversão de tipo) com esta interface aqui declarada.</para>
    /// </summary>
    public interface IHttpSessionState : ICollection, IEnumerable
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Define o código de página, conjunto de caracteres, 
        /// usados pela sessão atual.</para>
        /// </summary>
        int CodePage { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referencia a coleção de dados da sessão atual.</para>
        /// </summary>
        HttpSessionState Contents { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor que indica quando a aplicação está 
        /// configurada para sessões sem uso de cookie.</para>
        /// </summary>
        HttpCookieMode CookieMode { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor que indica quando o ID da sessão está 
        /// embutido numa URL ou armazenada num cookie HTTP.</para>
        /// </summary>
        bool IsCookieless { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor que indica quando a sessão foi criada 
        /// pela requisição atual.</para>
        /// </summary>
        bool IsNewSession { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor que indica quandoa sessão é somente leitura.</para>
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Coleção de chaves para todos os valores armazenados
        /// na coleção do dados da sessão.</para>
        /// </summary>
        NameObjectCollectionBase.KeysCollection Keys { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>LoCale IDentifier (LCID) da sessão atual.</para>
        /// </summary>
        int LCID { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor atual do modo do estado da sessão.</para>
        /// </summary>
        SessionStateMode Mode { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Identificador único da sessão.</para>
        /// </summary>
        string SessionID { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Coleção de objetos declarados pela 
        /// tag <c>&lt;object Runat="Server" Scope="Session"/&gt;</c> dentro 
        /// do arquivo Global.asax da aplicação ASP.NET.</para>
        /// </summary>
        HttpStaticObjectsCollection StaticObjects { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tempo total, em minutos, permitidos entre as requisições 
        /// antes da coleção de dados da sessão ser terminada pelo Provider.</para>
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referencia pelo índice um valor da coleção de dados da sessão.</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        /// <returns><para>Valor associado ao índice informado.</para></returns>
        object this[int index] { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referencia pelo nome da chave um valor da coleção 
        /// de dados da sessão.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave associada a uma valor.</para></param>
        /// <returns><para>Valor associado a chave informada.</para></returns>
        object this[string name] { get; set; }

        /// <summary>
        /// <para>Cancela a sessão atual.</para>
        /// </summary>
        void Abandon();

        /// <summary>
        /// <para>Adiciona um novo item a coleção de dados da sessão.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        /// <param name="value"><para>Conteúdo do valor.</para></param>
        void Add(string name, object value);

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores 
        /// da coleção de dados da sessão.</para>
        /// <para>Idêntico ao método <see cref="RemoveAll()" />.</para>
        /// </summary>
        void Clear();

        /// <summary>
        /// <para>Apaga um item da coleção de dados da sessão.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        void Remove(string name);

        /// <summary>
        /// <para>Remove todas as chaves e seus respectivos valores 
        /// da coleção de dados da sessão.</para>
        /// <para>Idêntico ao método <see cref="Clear()" />.</para>
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// <para>Apaga pelo índice um item da coleção de dados da sessão.</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        void RemoveAt(int index);

        #region ICollection Members

        /// <summary>
        /// <para>Copia os valores da coleção de dados da sessão 
        /// para um array unidimensional, começando pelo índice especificado.</para>
        /// </summary>
        /// <param name="array"><para>O Array que recebeu os valores da sessão.</para></param>
        /// <param name="index"><para>O índice do array de origem por a cópia será iniciada.</para></param>
        new void CopyTo(Array array, int index);

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Total de itens na coleção de dados da sessão.</para>
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Valor que indica quando o acesso a coleção 
        /// dos dados da sessão está sincronizado (thread safe).</para>
        /// </summary>
        new bool IsSynchronized { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Objeto que pode ser usado para sincronizar 
        /// o acesso a coleção de dados da sessão.</para>
        /// </summary>
        new object SyncRoot { get; }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// <para>Retorna um Enumerator que pode ser usado para ler 
        /// todas as variáveis da coleção de dados da sessão atual.</para>
        /// </summary>
        /// <returns><para>Trata-se de um IEnumerator que pode interagir 
        /// com os valores na coleção de dados da sessão.</para></returns>
        new IEnumerator GetEnumerator();

        #endregion

    }

}