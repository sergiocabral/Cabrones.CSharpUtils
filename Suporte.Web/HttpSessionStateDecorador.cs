using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Esta classe abstrata permite implementar o padrão de projeto 
    /// Decorator sobre a classe <see cref="System.Web.SessionState.HttpSessionState" />.</para>
    /// <para>Isso naturalmente não seria possível, visto 
    /// que <see cref="System.Web.SessionState.HttpSessionState" /> é do tipo selead 
    /// (não podendo ser herdada por uma nova classe). 
    /// Deste modo, esta classe declara pelo menos as mesmas 
    /// assinaturas que a classe <see cref="System.Web.SessionState.HttpSessionState" />
    /// e faz um by-pass, uma chamada direta de seus métodos e propriedades.</para>
    /// </summary>
    public abstract class HttpSessionStateDecorador : IHttpSessionState
    {
        private HttpSessionState sessionOriginal;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência para o objeto da sessão original 
        /// informada como parâmetro para o construtor desta classe.</para>
        /// </summary>
        public HttpSessionState SessionOriginal
        {
            get { return sessionOriginal; }
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        /// <param name="session"><para>Deve ser uma referência ao objeto 
        /// da sessão atual, ou seja, deve equivaler ao 
        /// código <c>System.Web.HttpContext.Current.Session</c>.</para></param>
        public HttpSessionStateDecorador(HttpSessionState session)
        {
            this.sessionOriginal = session;
        }

        #region ISession Members

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.CodePage" />.</para>
        /// </summary>
        public virtual int CodePage
        {
            get { return this.SessionOriginal.CodePage; }
            set { this.SessionOriginal.CodePage = value; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Contents" />.</para>
        /// </summary>
        public virtual HttpSessionState Contents
        {
            get { return this.SessionOriginal.Contents; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.CookieMode" />.</para>
        /// </summary>
        public virtual HttpCookieMode CookieMode
        {
            get { return this.SessionOriginal.CookieMode; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.IsCookieless" />.</para>
        /// </summary>
        public virtual bool IsCookieless
        {
            get { return this.SessionOriginal.IsCookieless; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.IsNewSession" />.</para>
        /// </summary>
        public virtual bool IsNewSession
        {
            get { return this.SessionOriginal.IsNewSession; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.IsReadOnly" />.</para>
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return this.SessionOriginal.IsReadOnly; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Keys" />.</para>
        /// </summary>
        public virtual NameObjectCollectionBase.KeysCollection Keys
        {
            get { return this.SessionOriginal.Keys; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.LCID" />.</para>
        /// </summary>
        public virtual int LCID
        {
            get { return this.SessionOriginal.LCID; }
            set { this.SessionOriginal.LCID = value; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Mode" />.</para>
        /// </summary>
        public virtual SessionStateMode Mode
        {
            get { return this.SessionOriginal.Mode; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.SessionID" />.</para>
        /// </summary>
        public virtual string SessionID
        {
            get { return this.SessionOriginal.SessionID; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.StaticObjects" />.</para>
        /// </summary>
        public virtual HttpStaticObjectsCollection StaticObjects
        {
            get { return this.SessionOriginal.StaticObjects; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Timeout" />.</para>
        /// </summary>
        public virtual int Timeout
        {
            get { return this.SessionOriginal.Timeout; }
            set { this.SessionOriginal.Timeout = value; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState" />.</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        /// <returns><para>Valor associado ao índice informado.</para></returns>
        public virtual object this[int index]
        {
            get { return this.SessionOriginal[index]; }
            set { this.SessionOriginal[index] = value; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState" />.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave associada a uma valor.</para></param>
        /// <returns><para>Valor associado a chave informada.</para></returns>
        public virtual object this[string name]
        {
            get { return this.SessionOriginal[name]; }
            set { this.SessionOriginal[name] = value; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Abandon" />.</para>
        /// </summary>
        public virtual void Abandon()
        {
            this.SessionOriginal.Abandon();
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Add" />.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        /// <param name="value"><para>Conteúdo do valor.</para></param>
        public virtual void Add(string name, object value)
        {
            this.SessionOriginal.Add(name, value);
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Clear" />.</para>
        /// </summary>
        public virtual void Clear()
        {
            this.SessionOriginal.Clear();
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Remove" />.</para>
        /// </summary>
        /// <param name="name"><para>Nome da chave.</para></param>
        public virtual void Remove(string name)
        {
            this.SessionOriginal.Remove(name);
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.RemoveAll" />.</para>
        /// </summary>
        public virtual void RemoveAll()
        {
            this.SessionOriginal.RemoveAll();
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.RemoveAt" />.</para>
        /// </summary>
        /// <param name="index"><para>Índice do item na coleção.</para></param>
        public virtual void RemoveAt(int index)
        {
            this.SessionOriginal.RemoveAt(index);
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.CopyTo" />.</para>
        /// </summary>
        /// <param name="array"><para>O Array que recebeu os valores da sessão.</para></param>
        /// <param name="index"><para>O índice do array de origem por a cópia será iniciada.</para></param>
        public virtual void CopyTo(Array array, int index)
        {
            this.SessionOriginal.CopyTo(array, index);
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.Count" />.</para>
        /// </summary>
        public virtual int Count
        {
            get { return this.SessionOriginal.Count; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.IsSynchronized" />.</para>
        /// </summary>
        public virtual bool IsSynchronized
        {
            get { return this.SessionOriginal.IsSynchronized; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.SyncRoot" />.</para>
        /// </summary>
        public virtual object SyncRoot
        {
            get { return this.SessionOriginal.SyncRoot; }
        }

        /// <summary>
        /// <para>Este método/propriedade faz um by-pass (chamada direta) para o
        /// objeto da sessão original informada como parâmetro para o construtor desta classe.</para>
        /// <para>Veja maiores detalhes em <see cref="Suporte.Web.IHttpSessionState.GetEnumerator" />.</para>
        /// </summary>
        public virtual IEnumerator GetEnumerator()
        {
            return this.SessionOriginal.GetEnumerator();
        }

        #endregion

    }

}
