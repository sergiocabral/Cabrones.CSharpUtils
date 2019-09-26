using System.Security.Cryptography;
using Suporte.Criptografia;
using Suporte.Texto;

namespace Suporte.Configuracao
{
    /// <summary>
    /// <para>Esta classe é capaz de criptografar campos confidenciais 
    /// no arquivo de configuração (App.Config ou Web.Config).</para>
    /// <para>A criptografia ocorre a media que se lê ou escreve valores ou atributos
    /// no documento Xml do arquivo de configuração.</para>
    /// </summary>
    /// <typeparam name="TSymmetricAlgorithmProvider">Tipo do algoritimo de criptografia simétrica.</typeparam>
    public class ArquivoDeConfiguracaoCriptografado<TSymmetricAlgorithmProvider> : ArquivoDeConfiguracaoFormatado where TSymmetricAlgorithmProvider : SymmetricAlgorithm 
    {
        /// <summary>
        /// <para>Construtor</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        public ArquivoDeConfiguracaoCriptografado(string senha, byte[] bytesSalt)
            : this(
            senha,
            bytesSalt,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesKey,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesIV) { }


        /// <summary>
        /// <para>Construtor</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        /// <param name="bytesKey">
        /// <para>Total de bytes usados na palavra chave (Key) do algorítmo de criptografia.</para>
        /// </param>
        /// <param name="bytesIV">
        /// <para>Total de bytes usados no vetor de inicialização (IV) do algorítmo de criptografia.</para>
        /// </param>
        public ArquivoDeConfiguracaoCriptografado(string senha, byte[] bytesSalt, int bytesKey, int bytesIV)
            : base(new FormatadorComCriptografiaSimetricaString<TSymmetricAlgorithmProvider>(senha, bytesSalt), new ComparadorString())
        {
            ArquivoDeSaida = Arquivo;
            ((ComparadorString)Comparador).IgnorarMaiusculaMinuscula = true;
        }
        
    }
}
