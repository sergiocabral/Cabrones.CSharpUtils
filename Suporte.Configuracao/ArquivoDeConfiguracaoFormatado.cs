using System;
using System.IO;
using Suporte.Dados;
using Suporte.Xml;

namespace Suporte.Configuracao
{
    /// <summary>
    /// <para>Esta classe permite formatar os valores lidos do arquivo de configuração.</para>
    /// </summary>
    public class ArquivoDeConfiguracaoFormatado : ManipuladorXmlEmArquivo<XmlTagInfoNotacaoCabral>
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ArquivoDeConfiguracaoFormatado()
            : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
        {
            Inicializar();
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ArquivoDeConfiguracaoFormatado(IFormatador<string> formatador)
            : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, formatador)
        {
            Inicializar();
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="comparador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        public ArquivoDeConfiguracaoFormatado(IComparador<string> comparador)
            : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, comparador)
        {
            Inicializar();
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Interface para o formatador que rege o modo como o texto é lido e escrito.</para>
        /// </param>
        /// <param name="comparador">
        /// <para>Interface para o comparador que rege o modo como o texto comparado.</para>
        /// </param>
        public ArquivoDeConfiguracaoFormatado(IFormatador<string> formatador, IComparador<string> comparador)
            : base(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, formatador, comparador)
        {
            Inicializar();
        }

        /// <summary>
        /// <para>Código a ser executado após durante a construção da instância desta classe.</para>
        /// </summary>
        private void Inicializar()
        {
            ArquivoDeSaida = new FileInfo(Arquivo.FullName + ".formatado");
            AoLerValorFormatarAutomaticamente = true;
            SalvarAutomaticamente = true;
        }

    }
}
