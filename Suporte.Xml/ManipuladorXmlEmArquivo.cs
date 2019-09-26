using System;
using System.IO;
using Suporte.Dados;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Classe capaz de manipular documentos XML em arquivo.</para>
    /// </summary>
    /// <typeparam name="TXmlTagInfo">
    /// <para>Precisa ser um tipo de classe não abstrata com as regras que regem a escrita da referência Xml.</para>
    /// </typeparam>
    public class ManipuladorXmlEmArquivo<TXmlTagInfo> : ManipuladorXml<TXmlTagInfo>, IManipuladorXmlEmArquivo where TXmlTagInfo : IXmlTagInfo
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        public ManipuladorXmlEmArquivo(string caminhoArquivo)
            : base()
        {
            Inicializar(caminhoArquivo);
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ManipuladorXmlEmArquivo(string caminhoArquivo, IFormatador<string> formatador)
            : base(formatador)
        {
            Inicializar(caminhoArquivo);
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        /// <param name="comparador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ManipuladorXmlEmArquivo(string caminhoArquivo, IComparador<string> comparador)
            : base(comparador)
        {
            Inicializar(caminhoArquivo);
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        /// <param name="comparador">
        /// <para>Interface para o comparador que rege o modo como o texto comparado.</para>
        /// </param>
        public ManipuladorXmlEmArquivo(string caminhoArquivo, IFormatador<string> formatador, IComparador<string> comparador)
            : base(formatador, comparador)
        {
            Inicializar(caminhoArquivo);
        }

        /// <summary>
        /// <para>Código a ser executado após durante a construção da instância desta classe.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        private void Inicializar(string caminhoArquivo)
        {
            Carregar(caminhoArquivo);
            QuandoAlterar += new EventHandler<EventArgs>(ProcessarEventoQuandoAlterar);
        }

        FileInfo arquivo;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência ao arquivo Xml.</para>
        /// </summary>
        public FileInfo Arquivo
        {
            get { return arquivo; }
        }

        FileInfo arquivoDeSaida;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referência ao arquivo Xml de saída.</para>
        /// </summary>
        public FileInfo ArquivoDeSaida
        {
            get { return arquivoDeSaida; }
            set { arquivoDeSaida = value; }
        }

        private bool salvarAutomaticamente = true;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> as alterações são salva automaticamente
        /// no arquivo referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        public bool SalvarAutomaticamente
        {
            get { return salvarAutomaticamente; }
            set { salvarAutomaticamente = value; }
        }

        /// <summary>
        /// <para>Método que atende o evento disparado quando o documento Xml sofre alterações.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Objeto que disparou o evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informações sobre o evento.</para>
        /// </param>
        private void ProcessarEventoQuandoAlterar(object sender, EventArgs e)
        {
            if (SalvarAutomaticamente)
            {
                Salvar();
            }
        }

        /// <summary>
        /// <para>Carregar documento Xml do último arquivo carregado.</para>
        /// </summary>
        public void Carregar()
        {
            Carregar(Arquivo.FullName);
        }

        /// <summary>
        /// <para>Carregar documento Xml do arquivo.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        public void Carregar(string caminhoArquivo)
        {
            try
            {
                Xml.Load(caminhoArquivo);
            }
            catch (FileNotFoundException)
            {
            }
            arquivo = new FileInfo(caminhoArquivo);
            if (arquivoDeSaida == null)
            {
                arquivoDeSaida = arquivo;
            }
        }

        /// <summary>
        /// <para>Salva o documento Xml para o arquivo 
        /// referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        public void Salvar()
        {
            Xml.Save(ArquivoDeSaida.FullName);
        }

        /// <summary>
        /// <para>Salva o documento Xml para o arquivo 
        /// referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        public void Salvar(string caminhoArquivo)
        {
            Xml.Save(caminhoArquivo);
        }
    }
}
