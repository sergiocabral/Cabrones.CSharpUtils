using System.Collections.Generic;
using System.Security.Cryptography;
using Suporte.Configuracao;
using Suporte.Xml;

namespace Suporte.Petrobras
{
    /// <summary>
    /// <para>Esta classe é capazs de criptografar os campos confidenciais do 
    /// framework FCorp da Petrobras no arquivo de configuração (App.Config ou Web.Config).</para>
    /// <para>A criptografia ocorre a media que se lê ou escreve valores ou atributos
    /// no documento Xml do arquivo de configuração.</para>
    /// </summary>
    /// <typeparam name="TSymmetricAlgorithmProvider">Tipo do algoritimo de criptografia simétrica.</typeparam>
    public class ArquivoDeConfiguracaoCriptografadoFCorp<TSymmetricAlgorithmProvider> : ArquivoDeConfiguracaoCriptografado<TSymmetricAlgorithmProvider> where TSymmetricAlgorithmProvider : SymmetricAlgorithm
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
        public ArquivoDeConfiguracaoCriptografadoFCorp(string senha, byte[] bytesSalt)
            : base(senha, bytesSalt) { }

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
        public ArquivoDeConfiguracaoCriptografadoFCorp(string senha, byte[] bytesSalt, int bytesKey, int bytesIV)
            : base(senha, bytesSalt, bytesKey, bytesIV) { }

        /// <summary>
        /// <para>Criptografa os campos confidenciais do framework FCorp da Petrobras
        /// no arquivo de configuração (App.Config ou Web.Config).</para>
        /// <para>Os campos criptografados são: 
        /// (1) Informações do Controle de Acesso,
        /// (2) String de conexão com o banco de dados e 
        /// (3) Comandos SQL para execução no banco de dados.</para>
        /// </summary>
        public void Criptografar()
        {
            Fcorp.Configuration.FcorpSetting.Instance.SecuritySetting.ControlAccess.CodigoAmbiente =
                LerAtributo("codigo", "configuration[0].frameworkCorporativo[0].security[0].controleAcesso[0].ambiente[0]", null);

            Fcorp.Configuration.FcorpSetting.Instance.SecuritySetting.ControlAccess.CodigoRegional =
                LerAtributo("codigo", "configuration[0].frameworkCorporativo[0].security[0].controleAcesso[0].regional[0]", null);

            Fcorp.Configuration.FcorpSetting.Instance.SecuritySetting.ControlAccess.CodigoSistema =
                LerAtributo("codigo", "configuration[0].frameworkCorporativo[0].security[0].controleAcesso[0].sistema[0]", null);

            Fcorp.Configuration.FcorpSetting.Instance.SecuritySetting.ControlAccess.SenhaSistema =
                LerAtributo("codigo", "configuration[0].frameworkCorporativo[0].security[0].controleAcesso[0].senha[0]", null);

            foreach (KeyValuePair<string, Fcorp.Configuration.Persistence.ConnectionSetting> conexao in Fcorp.Configuration.FcorpSetting.Instance.PersistenceSetting.Connections)
            {
                Fcorp.Configuration.FcorpSetting.Instance.PersistenceSetting.Connections[conexao.Key].ConnectionString =
                    LerAtributo("connectionString", "configuration[0].connectionStrings[0].add[]", new XmlCriterios(new XmlCriterioAtributoIgual("add", "name", conexao.Key)));

                for (int i = 0; i < Fcorp.Configuration.FcorpSetting.Instance.PersistenceSetting.Connections[conexao.Key].EnvironmentSettings.Count; i++)
                {
                    Fcorp.Configuration.FcorpSetting.Instance.PersistenceSetting.Connections[conexao.Key].EnvironmentSettings[i] =
                        LerValor(string.Format("configuration[0].frameworkCorporativo[0].persistence[0].connections[0].connection[].environmentSettings[0].sqlCommand[{0}]", i), new XmlCriterios(new XmlCriterioAtributoIgual("connection", "name", conexao.Key)));
                }
            }
        }

    }
}
