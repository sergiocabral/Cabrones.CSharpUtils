using System.Web;
using System.Collections.Generic;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Disponibiliza em propriedades as definições dos códigos de erro web do cliente.</para>
    /// <para>Para maiores detalhes consulte 
    /// o site: http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html.
    /// </para>
    /// </summary>
    public static class StatusCode
    {
        private static IDictionary<int, KeyValuePair<string, string>> lista = null;
        /// <summary>
        /// <para>Retorna uma lista com todos os códigos de erros conhecidos.</para>
        /// </summary>
        /// <returns>Lista com todos os códigos de erros conhecidos.</returns>
        public static IDictionary<int, KeyValuePair<string, string>> Lista
        {
            get
            {
                if (lista == null)
                {
                    lista = new Dictionary<int, KeyValuePair<string, string>>();
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(400, new KeyValuePair<string, string>("Bad Request", "A requisição não pôde ser entendida pelo servidor devido à sintaxe.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(401, new KeyValuePair<string, string>("Unauthorized", "A requisição requer autenticação do usuário.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(402, new KeyValuePair<string, string>("Payment Required", "Código de erro reservado.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(403, new KeyValuePair<string, string>("Forbidden", "O servidor entendeu a requisição, mas se recusa a cumpri-la.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(404, new KeyValuePair<string, string>("Not Found", "O servidor não encontrou nada que correspondesse a requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(405, new KeyValuePair<string, string>("Method Not Allowed", "O método especificado na requisição não é permitido para o recurso identificado na requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(406, new KeyValuePair<string, string>("Not Acceptable", "O recurso que você tentou utilizar foi negado pelo servidor, possívelmente a requisição que você tentou executar é insegura.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(407, new KeyValuePair<string, string>("Proxy Authentication Required", "A requisição requer autenticação do usuário, mas o cliente deve primeiro se autenticar no proxy.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(408, new KeyValuePair<string, string>("Request Timeout", "O tempo da requisição expirou.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(409, new KeyValuePair<string, string>("Conflict", "A requisição não pôde ser concluída devido a um conflito com o estado atual do recurso.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(410, new KeyValuePair<string, string>("Gone", "O recurso solicitado não está mais disponível no servidor e nenhum endereço de encaminhamento é conhecido.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(411, new KeyValuePair<string, string>("Length Required", "O servidor se recusa a aceitar a requisição sem um número definido de tamanho de conteúdo (Content-Length).")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(412, new KeyValuePair<string, string>("Precondition Failed", "A condição dada em um ou mais dos campos do cabeçalho de requisição foi avaliada como falsa quando foi testada no servidor.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(413, new KeyValuePair<string, string>("Request Entity Too Large", "O servidor está se recusando a processar a requisição porque esta é maior do que o servidor está disposto ou é capaz de processar.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(414, new KeyValuePair<string, string>("Request-URI Too Long", "O servidor está se recusando a processar a requisição porque o URI é maior do que o servidor está disposto a interpretar.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(415, new KeyValuePair<string, string>("Unsupported Media Type", "O servidor está se recusando a atender à requisição porque a entidade da requisição está em um formato que não é compatível com o recurso solicitado para o método solicitado.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(416, new KeyValuePair<string, string>("Requested Range Not Satisfiable", "O recurso que está sendo acessado não abrange a faixa de bytes indicada no cabeçalho da requisição")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(417, new KeyValuePair<string, string>("Falha na expectativa", "O servidor considera que o fluxo de dados HTTP enviado pelo cliente contém uma requisição 'Prevista' que não pode ser satisfeita.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(500, new KeyValuePair<string, string>("Internal Server Error", "O servidor encontrou uma condição inesperada que o impediu de cumprir a requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(501, new KeyValuePair<string, string>("Not Implemented", "O servidor não suporta a funcionalidade necessária para atender a requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(502, new KeyValuePair<string, string>("Bad Gateway", "O servidor, enquanto age como um gateway ou proxy, recebeu uma resposta inválida do servidor superior acessado na tentativa de atender à requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(503, new KeyValuePair<string, string>("Service Unavailable", "O servidor está atualmente incapaz de processar a requisição devido a uma sobrecarga temporária ou manutenção do servidor.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(504, new KeyValuePair<string, string>("Gateway Timeout", "O servidor, enquanto age como um gateway ou proxy, não recebeu em tempo uma resposta a partir do servidor upstream especificado pela URI, ou algum outro servidor auxiliar é necessário para aceder ao tentar completar a requisição.")));
                    lista.Add(new KeyValuePair<int, KeyValuePair<string, string>>(505, new KeyValuePair<string, string>("HTTP Version Not Supported", "O servidor não suporta, ou se recusar a apoiar, a versão do protocolo HTTP que foi usado na mensagem de requisição.")));
                }
                return new Dictionary<int, KeyValuePair<string, string>>(lista);
            }
        }
    }
}
