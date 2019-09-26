using System;
using System.IO;
using System.Security.Cryptography;
using Suporte.Dados;

namespace Suporte.Criptografia
{
    /// <summary>
    /// <para>Esta classe formata uma <see cref="Stream"/> para ser escrita ou lida através 
    /// de um algorítmo de criptografia simétrica.</para>
    /// </summary>
    /// <typeparam name="TSymmetricAlgorithmProvider">Tipo do algoritimo de criptografia simétrica.</typeparam>
    public class FormatadorComCriptografiaSimetricaStream<TSymmetricAlgorithmProvider> : IFormatador<Stream> where TSymmetricAlgorithmProvider : SymmetricAlgorithm 
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        public FormatadorComCriptografiaSimetricaStream(string senha, byte[] bytesSalt)
            : this(new FormatadorGenerico<Stream>(), senha, bytesSalt) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para um formatador mais interno.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        public FormatadorComCriptografiaSimetricaStream(IFormatador<Stream> formatador, string senha, byte[] bytesSalt)
            : this(
            formatador,
            senha,
            bytesSalt,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesKey,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesIV) { }
        
        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Informa o comprimento de bytes usado na criptografia.</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
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
        public FormatadorComCriptografiaSimetricaStream(string senha, byte[] bytesSalt, int bytesKey, int bytesIV)
            : this(new FormatadorGenerico<Stream>(), senha, bytesSalt, bytesKey, bytesIV) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Informa o comprimento de bytes usado na criptografia.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para um formatador mais interno.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
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
        public FormatadorComCriptografiaSimetricaStream(IFormatador<Stream> formatador, string senha, byte[] bytesSalt, int bytesKey, int bytesIV)
        {
            this.formatador = formatador;
            this.senha = senha;
            this.bytesSalt = bytesSalt;
            this.bytesKey = bytesKey;
            this.bytesIV = bytesIV;
        }

        private string senha;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </summary>
        public string Senha
        {
            get { return senha; }
        }

        private byte[] bytesSalt = new byte[] { };
        /// <summary>
        /// <para>(Leitura></para>
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </summary>
        public byte[] BytesSalt
        {
            get { return bytesSalt; }
        }

        private int bytesKey;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Armazena o total de bytes usado na palavra chave (Key).</para>
        /// </summary>
        public int BytesKey
        {
            get { return bytesKey; }
        }

        private int bytesIV;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Armazena o total de bytes usado no vetor de inicialização (IV).</para>
        /// </summary>
        public int BytesIV
        {
            get { return bytesIV; }
        }

        #region IFormatador<Stream> Members

        private IFormatador<Stream> formatador;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um formatador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        public IFormatador<Stream> Formatador
        {
            get
            {
                return formatador;
            }
        }

        /// <summary>
        /// <para>Formata ou remove a formatação.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c> considera será feita uma escrita e, portanto, deve ser formatada. 
        /// Se igual a <c>false</c> considera que será feita uma leitura e a formatação é removida.</para>
        /// </param>
        /// <param name="objeto">
        /// <para>Objeto que será des/formatado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o mesmo <paramref name="objeto"/> de entrada, porém modificado conforme formatação.</para>
        /// </returns>
        public Stream Formatar(bool paraEntrada, Stream objeto)
        {
            objeto = Formatador.Formatar(paraEntrada, objeto);

            if (!EstaFormatado(objeto))
            {
                CriptografiaSimetrica<TSymmetricAlgorithmProvider> criptografiaSimetrica = new CriptografiaSimetrica<TSymmetricAlgorithmProvider>(BytesKey, BytesIV);
                ICryptoTransform cryptoTransform = criptografiaSimetrica.ObterCryptoTransform(paraEntrada, Senha, BytesSalt);
                if (paraEntrada)
                {
                    return new CryptoStream(objeto, cryptoTransform, CryptoStreamMode.Write);
                }
                else
                {
                    return objeto.Length > 0 ? new CryptoStream(objeto, cryptoTransform, CryptoStreamMode.Read) : objeto;
                }
            }
            return objeto;
        }

        /// <summary>
        /// <para>Verifica se um objeto já está formatado.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Objeto que será verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Quando o objeto já está formatado retorna <c>true</c>.</para>
        /// </returns>
        public bool EstaFormatado(Stream objeto)
        {
            try
            {
                long length = objeto.Length;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        #endregion
    }
}
