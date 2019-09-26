using System.Web;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Disponibiliza em propriedades cada item da 
    /// coleção <c>HttpContext.Current.Request.ServerVariables</c>.</para>
    /// <para>Tratam-se de variáveis de ambiente de servidores ASP e ASP.NET.</para>
    /// Como o passar do tempo e a chegada de novas tecnologias, algumas dessas propriedades
    /// podem ficar obsoletas e deixar de retornar valores. 
    /// E ainda, novas variáveis de ambiente podem passar a existir e não serem contempladas pelas
    /// propriedades desta classe. Neste último caso será necessário acessar tais variáveis
    /// pela chamada de <c>HttpContext.Current.Request.ServerVariables["nova_variavel"]</c>.
    /// <para>Para maiores detalhes consulte o 
    /// site: http://www.asp-dev.com/main.asp?page=54.
    /// </para>
    /// </summary>
    public static class ServerVariables
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Todo o cabeçalho HTTP enviado pelo cliente.</para>
        /// </summary>
        public static string AllHttp { get { return HttpContext.Current.Request.ServerVariables["ALL_HTTP"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Recupera todos os cabeçalhos na forma crua. 
        /// A diferença entre ALL_RAW e ALL_HTTP é que ALL_HTTP coloca um prefixo HTTP_ 
        /// antes do nome do cabeçalho e o cabeçalho de nome é sempre maiúscula. 
        /// Em ALL_RAW o nome do cabeçalho e os valores aparecem como elas são 
        /// enviadas pelo cliente.</para>
        /// </summary>
        public static string AllRaw { get { return HttpContext.Current.Request.ServerVariables["ALL_RAW"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Recupera o caminho da metabase para a (WAM) DLL ISAPI da aplicação.</para>
        /// </summary>
        public static string ApplMdPath { get { return HttpContext.Current.Request.ServerVariables["APPL_MD_PATH"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O servidor web IIS converte o APPL_MD_PATH para o caminho 
        /// físico (pasta Windows) e retorna este valor.</para>
        /// </summary>
        public static string ApplPhysicalPath { get { return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O valor inserido na tela de diálogo do cliente.
        /// Este valor apenas está disponível se for usado a autenticação simples
        /// (Basic authentication).</para>
        /// </summary>
        public static string AuthPassword { get { return HttpContext.Current.Request.ServerVariables["AUTH_PASSWORD"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O método de autenticação que o servidor usa para validar usuários 
        /// quando eles tentam acessar um script protegido.</para>
        /// </summary>
        public static string AuthType { get { return HttpContext.Current.Request.ServerVariables["AUTH_TYPE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Nome do usuário autenticado.</para>
        /// </summary>
        public static string AuthUser { get { return HttpContext.Current.Request.ServerVariables["AUTH_USER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>ID único para o certificado do cliente, retornado como uma string. 
        /// Pode ser usado como uma assinatura para qualquer certificado do cliente.</para>
        /// </summary>
        public static string CertCookie { get { return HttpContext.Current.Request.ServerVariables["CERT_COOKIE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>bit0 é definido como 1 se o certificado de cliente está presente.</para>
        /// <para>bit1 é definido para 1 se é Autoridade Certificadora do certificado 
        /// de cliente inválido (não na lista de reconhecidos CA no servidor).</para>
        /// </summary>
        public static string CertFlags { get { return HttpContext.Current.Request.ServerVariables["CERT_FLAGS"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo emissor do certificado do cliente.
        /// (O=MS, OU=IAS, CN=Nome do Usuário, C=USA)</para>
        /// </summary>
        public static string CertIssuer { get { return HttpContext.Current.Request.ServerVariables["CERT_ISSUER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Número de bits, tamanho da chave, na conexão Secure Sockets Layer (SSL).
        /// Por exemplo, 128.</para>
        /// </summary>
        public static string CertKeysize { get { return HttpContext.Current.Request.ServerVariables["CERT_KEYSIZE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Número de bits na chave privada do certificado do servidor.
        /// Por exemplo, 1024.</para>
        /// </summary>
        public static string CertSecretkeysize { get { return HttpContext.Current.Request.ServerVariables["CERT_SECRETKEYSIZE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo de número de série do certificado cliente.</para>
        /// </summary>
        public static string CertSerialnumber { get { return HttpContext.Current.Request.ServerVariables["CERT_SERIALNUMBER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo emissor do certificado do servidor.</para>
        /// </summary>
        public static string CertServerIssuer { get { return HttpContext.Current.Request.ServerVariables["CERT_SERVER_ISSUER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo de assunto do certificado do servidor.</para>
        /// </summary>
        public static string CertServerSubject { get { return HttpContext.Current.Request.ServerVariables["CERT_SERVER_SUBJECT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo de assunto do certificado do cliente.</para>
        /// </summary>
        public static string CertSubject { get { return HttpContext.Current.Request.ServerVariables["CERT_SUBJECT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Comprimento do conteúdo fornecido pelo cliente.</para>
        /// </summary>
        public static string ContentLength { get { return HttpContext.Current.Request.ServerVariables["CONTENT_LENGTH"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Tipo de dados do conteúdo. Usado em consultas acompanhadas de informações, 
        /// tais como os métodos de requisição HTTP: GET, POST e PUT.</para>
        /// </summary>
        public static string ContentType { get { return HttpContext.Current.Request.ServerVariables["CONTENT_TYPE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>A revisão da especificação CGI usado pelo servidor.
        /// O formato é CGI/revisão.</para>
        /// </summary>
        public static string GatewayInterface { get { return HttpContext.Current.Request.ServerVariables["GATEWAY_INTERFACE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna ON se o pedido veio através do canal seguro Secure Sockets Layer (SSL)
        /// ou retorna OFF se o pedido for para um canal não seguro.</para>
        /// </summary>
        public static string Https { get { return HttpContext.Current.Request.ServerVariables["HTTPS"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Número de bits da chave da conexão Secure Sockets Layer (SSL).
        /// Por exemplo, 128.</para>
        /// </summary>
        public static string HttpsKeysize { get { return HttpContext.Current.Request.ServerVariables["HTTPS_KEYSIZE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna a URL da página anterior que fez a requisição da página 
        /// atual através de uma tag <c>&lt;a&gt;</c>. Se a página é redirecionada
        /// o retorno será vazio.</para>
        /// </summary>
        public static string HttpReferer { get { return HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Número de bits da chave privada do certificado do servidor.
        /// Por exemplo, 1024.</para>
        /// </summary>
        public static string HttpsSecretkeysize { get { return HttpContext.Current.Request.ServerVariables["HTTPS_SECRETKEYSIZE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo emissor do certificado do servidor.</para>
        /// </summary>
        public static string HttpsServerIssuer { get { return HttpContext.Current.Request.ServerVariables["HTTPS_SERVER_ISSUER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Campo de assunto do certificado do servidor.</para>
        /// </summary>
        public static string HttpsServerSubject { get { return HttpContext.Current.Request.ServerVariables["HTTPS_SERVER_SUBJECT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O ID da instância do servidor web IIS no formato textual.
        /// Se o ID de instância é 1, ela aparece como uma string.
        /// Você pode usar esta variável para recuperar o ID da instância do servidor Web 
        /// (metabase) ao qual o pedido pertence.</para>
        /// </summary>
        public static string InstanceId { get { return HttpContext.Current.Request.ServerVariables["INSTANCE_ID"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O caminho da metabase para a instância do 
        /// servidor web IIS que responde ao pedido.</para>
        /// </summary>
        public static string InstanceMetaPath { get { return HttpContext.Current.Request.ServerVariables["INSTANCE_META_PATH"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna o endereço do servidor em que o pedido chegou.
        /// Isso é importante em máquinas multi-homed, onde pode haver vários endereços 
        /// IP ligado a uma máquina e você quer saber qual o endereço utilizado pedido.</para>
        /// </summary>
        public static string LocalAddr { get { return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Conta do usuário logado no Windows NT.</para>
        /// </summary>
        public static string LogonUser { get { return HttpContext.Current.Request.ServerVariables["LOGON_USER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações adicionais sobre o caminho dado pelo cliente.
        /// Você pode acessar scripts usando seu caminho virtual e 
        /// da variável de servidor PATH_INFO. Se esta informação vem de uma URL, 
        /// ela é decodificada pelo servidor antes de ser passado para o script CGI.</para>
        /// </summary>
        public static string PathInfo { get { return HttpContext.Current.Request.ServerVariables["PATH_INFO"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>A versão traduzida do PATH_INFO que pega o caminho e 
        /// executa qualquer mapeamento necessário de virtual para a físico.</para>
        /// </summary>
        public static string PathTranslated { get { return HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna a informação de consulta armazenada após o ponto de interrogação (?)
        /// na requisição HTTP.</para>
        /// </summary>
        public static string QueryString { get { return HttpContext.Current.Request.ServerVariables["QUERY_STRING"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O endereço IP do host remoto que fez a requisição.</para>
        /// </summary>
        public static string RemoteAddr { get { return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O nome do host que fez a requisição. Se o servidor não tem essa 
        /// informação, ele irá definir REMOTE_ADDR e deixar este em branco.</para>
        /// </summary>
        public static string RemoteHost { get { return HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O número da porta do host que fez a requisição.</para>
        /// </summary>
        public static string RemotePort { get { return HttpContext.Current.Request.ServerVariables["REMOTE_PORT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Nome de usuário (não mapeado) enviado pelo usuário.
        /// Este é o nome que foi realmente enviado pelo utilizador,
        /// em oposição aos que são modificados por qualquer filtro de 
        /// autenticação instalado no servidor.</para>
        /// </summary>
        public static string RemoteUser { get { return HttpContext.Current.Request.ServerVariables["REMOTE_USER"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O método utilizado para fazer a requisição.
        /// Para HTTP, isto pode ser: GET, HEAD, POST, e assim por diante.</para>
        /// </summary>
        public static string RequestMethod { get { return HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Caminho virtual para o script em execução. Isto é usado para 
        /// fazer auto-referência a URL atual.</para>
        /// </summary>
        public static string ScriptName { get { return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O nome do servidor host, alias DNS ou endereço IP, 
        /// uma vez que aparecem nas URLs de auto-referência.</para>
        /// </summary>
        public static string ServerName { get { return HttpContext.Current.Request.ServerVariables["SERVER_NAME"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O número da porta para onde a requisição foi enviada.</para>
        /// </summary>
        public static string ServerPort { get { return HttpContext.Current.Request.ServerVariables["SERVER_PORT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Uma seqüência que contém 0 ou 1. Se a solicitação está sendo tratada 
        /// numa porta segura, então este será 1. Caso contrário, será 0.</para>
        /// </summary>
        public static string ServerPortSecure { get { return HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O nome e revisão do protocolo de solicitação de informações.
        /// O formato é protocol/revisão.</para>
        /// </summary>
        public static string ServerProtocol { get { return HttpContext.Current.Request.ServerVariables["SERVER_PROTOCOL"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>O nome e versão do software de servidor que atende o pedido e executa o gateway. 
        /// O formato é name/versão.</para>
        /// </summary>
        public static string ServerSoftware { get { return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Fornece a parte básica da URL.</para>
        /// </summary>
        public static string Url { get { return HttpContext.Current.Request.ServerVariables["URL"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Tipo de conexão entre o servidor e o cliente.</para>
        /// </summary>
        public static string HttpConnection { get { return HttpContext.Current.Request.ServerVariables["HTTP_CONNECTION"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Tempo de permanência da conexão entre o aplicativo cliente e o servidor.</para>
        /// </summary>
        public static string HttpKeepAlive { get { return HttpContext.Current.Request.ServerVariables["HTTP_KEEP_ALIVE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Tipos de arquivos (mime-type) aceitos pelo aplicativo cliente.</para>
        /// </summary>
        public static string HttpAccept { get { return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Codificação de caracteres do aplicativo cliente.</para>
        /// </summary>
        public static string HttpAcceptCharset { get { return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_CHARSET"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Compressão suportada pelo aplicativo cliente.</para>
        /// </summary>
        public static string HttpAcceptEncoding { get { return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_ENCODING"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Idioma do aplicativo cliente.</para>
        /// </summary>
        public static string HttpAcceptLanguage { get { return HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Indica se a página web solicitou autorização por senha para acesso.</para>
        /// </summary>
        public static string HttpAuthorization { get { return HttpContext.Current.Request.ServerVariables["HTTP_AUTHORIZATION"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Retorna a string do cookie que foi incluída com a requisição.</para>
        /// </summary>
        public static string HttpCookie { get { return HttpContext.Current.Request.ServerVariables["HTTP_COOKIE"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Endereço de rede do cliente e porta de conexão, separados por dois pontos (:).</para>
        /// </summary>
        public static string HttpHost { get { return HttpContext.Current.Request.ServerVariables["HTTP_HOST"]; } }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informação parcial extraida de ALL_HTTP.</para>
        /// <para>Informações sobre o aplicativo cliente do usuário.</para>
        /// </summary>
        public static string HttpUserAgent { get { return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]; } }
    }
}
