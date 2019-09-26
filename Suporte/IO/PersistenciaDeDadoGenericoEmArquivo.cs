using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Suporte.Dados;

namespace Suporte.IO
{
    /// <summary>
    /// <para>Esta classe é capaz de gravar em um arquivo um dado 
    /// qualquer armazenado em <see cref="DadoGenerico&lt;T&gt;"/>.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Tipo do dado de <see cref="DadoGenerico&lt;T&gt;"/>.</para>
    /// </typeparam>
    public class PersistenciaDeDadoGenericoEmArquivo<T> : IPersistenciaDeDadoGenerico<T>
    {
        int maximoDeTentativas = 3;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número máximo de tentativas para se tentar gravar o dado no arquivo.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        public int MaximoDeTentativas
        {
            get
            {
                return maximoDeTentativas;
            }
            set
            {
                maximoDeTentativas = value;
            }
        }

        private int tempoEntreAsTentativas = 300;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número correspondente ao tempo em milisegundos entre as tentativas
        /// para se tentar gravar o dado no arquivo.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        public int TempoEntreAsTentativas
        {
            get
            {
                return tempoEntreAsTentativas;
            }
            set
            {
                tempoEntreAsTentativas = value;
            }
        }

        private FileInfo arquivo;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Arquivo onde o dado será persistido.</para>
        /// </summary>
        public FileInfo Arquivo
        {
            get
            {
                return arquivo;
            }
            set
            {
                arquivo = value;
            }
        }

        private IFormatador<Stream> formatador;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Formatador usado para ler e escrever no arquivo.</para>
        /// </summary>
        public IFormatador<Stream> Formatador
        {
            get { return formatador; }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoDoArquivo">
        /// <para>Caminho físico do arquivo onde o dado será gravado.</para>
        /// </param>
        public PersistenciaDeDadoGenericoEmArquivo(string caminhoDoArquivo) : this(caminhoDoArquivo, new FormatadorGenerico<Stream>()) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoDoArquivo">
        /// <para>Caminho físico do arquivo onde o dado será gravado.</para>
        /// </param>
        /// <param name="formatador">
        /// <para>Formatador usado para ler e escrever no arquivo.</para>
        /// </param>
        public PersistenciaDeDadoGenericoEmArquivo(string caminhoDoArquivo, IFormatador<Stream> formatador)
        {
            arquivo = new FileInfo(caminhoDoArquivo);
            this.formatador = formatador;
        }

        /// <summary>
        /// <para>Tenta abrir uma stream para um arquivo.
        /// Caso não seja possível tenta novamente.</para>
        /// </summary>
        /// <param name="caminhoDoArquivo">
        /// <para>Caminho físico do arquivo onde o dado será gravado.</para>
        /// </param>
        /// <param name="fileMode">
        /// <para>Modo de abertura do arquivo.</para>
        /// </param>
        /// <param name="fileAccess">
        /// <para>Tipo de acesso ao arquivo.</para>
        /// </param>
        /// <returns>
        /// <para>Stream aberta para o caminho do arquivo informado.</para>
        /// <para>Sempre retornará valor. Caso contrário será disparada uma exception.</para>
        /// </returns>
        private Stream ObterStreamParaArquivo(string caminhoDoArquivo, FileMode fileMode, FileAccess fileAccess)
        {
            ITentativaDeObterStream<FileStream> tentativaDeStream = new TentativaDeObterFileStream(caminhoDoArquivo, fileMode, fileAccess);
            tentativaDeStream.MaximoDeTentativas = MaximoDeTentativas;
            tentativaDeStream.TempoEntreAsTentativas = TempoEntreAsTentativas;
            return tentativaDeStream.TentarObterStream();
        }

        #region IPersistirDadoGenerico<T> Members

        private DadoGenerico<T> dado = new DadoGenerico<T>();
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Dado qualquer para armazenamento.</para>
        /// </summary>
        public DadoGenerico<T> Dado
        {
            get
            {
                return dado;
            }
            set
            {
                dado = value;
            }
        }

        /// <summary>
        /// <para>Gravar a propriedade <see cref="Dado"/>.</para>
        /// </summary>
        public void Gravar()
        {
            Stream stream = ObterStreamParaArquivo(Arquivo.FullName, FileMode.Create, FileAccess.Write);
            try
            {
                stream = Formatador.Formatar(true, stream);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, Dado);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// <para>Carrega o valor para a propriedade <see cref="Dado"/>.</para>
        /// </summary>
        public void Carregar()
        {
            Stream streamOriginal = ObterStreamParaArquivo(Arquivo.FullName, FileMode.OpenOrCreate, FileAccess.Read);
            Stream stream = streamOriginal;
            try
            {                
                if (stream.Length > 0)
                {
                    stream = Formatador.Formatar(false, stream);
                    BinaryFormatter bf = new BinaryFormatter();
                    Dado = (DadoGenerico<T>)bf.Deserialize(stream);
                }
                else
                {
                    Dado = new DadoGenerico<T>();
                }
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch (Exception)
                {
                    streamOriginal.Close();
                }
            }
        }

        #endregion
    }
}
