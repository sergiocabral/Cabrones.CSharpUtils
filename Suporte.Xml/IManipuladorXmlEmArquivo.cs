using System.IO;

namespace Suporte.Xml
{
    /// <summary>
    /// <para>Interface para classe capaz de manipular arquivos XML em arquivo.</para>
    /// </summary>
    public interface IManipuladorXmlEmArquivo : IManipuladorXml
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Referência ao arquivo Xml.</para>
        /// </summary>
        FileInfo Arquivo { get; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Referência ao arquivo Xml de saída.</para>
        /// </summary>
        FileInfo ArquivoDeSaida { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> as alterações são salva automaticamente
        /// no arquivo referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        bool SalvarAutomaticamente { get; set; }

        /// <summary>
        /// <para>Carregar documento Xml do último arquivo carregado.</para>
        /// </summary>
        void Carregar();

        /// <summary>
        /// <para>Carregar documento Xml do arquivo.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        void Carregar(string caminhoArquivo);

        /// <summary>
        /// <para>Salva o documento Xml para o arquivo 
        /// referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        void Salvar();

        /// <summary>
        /// <para>Salva o documento Xml para o arquivo 
        /// referenciado na propriedade <see cref="ArquivoDeSaida"/>.</para>
        /// </summary>
        /// <param name="caminhoArquivo">
        /// <para>Caminho do arquivo Xml.</para>
        /// </param>
        void Salvar(string caminhoArquivo);

    }
}
