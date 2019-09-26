using System.Web.SessionState;

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
    /// <para>Esta classe não implementa funcionalidades, mas faz apenas um
    /// redirecionamento (by-pass) das chamadas para a sessão ASP.NET 
    /// na memória do servidor.</para>
    /// </summary>
    public class HttpSessionStateGenerico : HttpSessionStateDecorador
    {
        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        public HttpSessionStateGenerico() : this(System.Web.HttpContext.Current.Session) { }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        /// <param name="session">
        /// <para>Deve ser uma referência ao objeto da sessão atual, que pode ser acessado pelo 
        /// código <c>System.Web.HttpContext.Current.Session</c>.</para>
        /// </param>
        public HttpSessionStateGenerico(HttpSessionState session) : base(session) { }
    }
}