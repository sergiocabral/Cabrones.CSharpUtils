using System;
using System.IO;
using System.Web;

namespace Suporte.IO.PastasEArquivos
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades referente a 
    /// uma pasta de armazenamento temporário em um nível anterior.</para>
    /// <para>A pasta temporária deve estar entre o nível atual e o nível raiz.
    /// Por exemplo, se existir o caminho <c>.\temp</c> ele será asumido como
    /// para temporária. Se ao invés disso for <c>..\..\..\temp</c> também será 
    /// localizado. Também é possível <c>\temp</c>.</para>
    /// <para>A prioridade será da pasta mais próximo do nível atual. Porém,
    /// se a pasta atual tive o nome de <c>temp</c>, ela será assumida.</para>
    /// <para>Não será localizada uma pasta dentro de outra pasta, 
    /// como por exemplo: <c>..\sub\temp</c></para>
    /// <para>Obs.: No exemplo acima o nome <c>temp</c> foi usado como exemplo.
    /// O seu valor é definido pelo programador na construção da classe.</para>
    /// </summary>
    public class PastaTemporariaEmNivelAnterior : PastaTemporaria
    {
        /// <summary>
        /// <para>Retorna o caminho atual da aplicação, seja Web ou Desktop.</para>
        /// </summary>
        private DirectoryInfo CaminhoDaAplicacao
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return new DirectoryInfo(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"]);
                }
                else
                {
                    return (new FileInfo(Environment.GetCommandLineArgs()[0])).Directory;
                }
            }
        }

        private string nomeDaPasta;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Nome da pasta temporária.</para>
        /// </summary>
        public string NomeDaPasta
        {
            get { return nomeDaPasta; }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="nomeDaPasta">
        /// <para>Nome válido para uma pasta Windows.</para>
        /// <para>Será feita uma busca por esta pasta desde o nível atual até o nível raiz.
        /// Uma vez encontrada, ela será a pasta temporária.</para>
        /// </param>
        public PastaTemporariaEmNivelAnterior(string nomeDaPasta)
        {
            if (string.IsNullOrEmpty(nomeDaPasta) || nomeDaPasta == string.Empty)
            {
                throw new NullReferenceException("Não foi informado o nome da pasta temporária.");
            }
            this.nomeDaPasta = nomeDaPasta;
        }


        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna o caminho da pasta temporária.</para>
        /// </summary>
        public override DirectoryInfo Caminho
        {
            get
            {
                //Localiza o diretorio temporario em cada nivel de pasta até a raiz.
                DirectoryInfo caminho = CaminhoDaAplicacao;
                do
                {
                    if (caminho.Name.ToLower() == NomeDaPasta.ToLower())
                    {
                        return caminho;
                    }
                    foreach (DirectoryInfo subdir in caminho.GetDirectories())
                    {
                        if (subdir.Name.ToLower() == NomeDaPasta.ToLower())
                        {
                            caminho = subdir;
                            return caminho;
                        }
                    }
                    caminho = caminho.Parent;
                } while (caminho != null);

                return null;
            }
        }
    }
}
