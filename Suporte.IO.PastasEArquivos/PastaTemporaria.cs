using System;
using System.IO;
using Suporte.Texto;

namespace Suporte.IO.PastasEArquivos
{
    /// <summary>
    /// <para>Esta classe disponibiliza algumas funcionalidades referente a 
    /// uma pasta de armazenamento temporário de arquivos.</para>
    /// </summary>
    public class PastaTemporaria : IPastaTemporaria
    {
        /// <summary>
        /// <para>Construtor disponível apenas para classes descendentes.</para>
        /// </summary>
        protected PastaTemporaria() { }

        /// <summary>
        /// <para>Construtor disponível apenas para classes descendentes.</para>
        /// </summary>
        /// <param name="caminhoPastaTemporaria">
        /// <para>Caminho da pasta temporária.</para>
        /// </param>
        public PastaTemporaria(string caminhoPastaTemporaria)
        {
            Caminho = new DirectoryInfo(caminhoPastaTemporaria);
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informa se o caminho da pasta temporária existe.</para>
        /// </summary>
        public bool CaminhoExiste
        {
            get { return Caminho != null && (new DirectoryInfo(Caminho.FullName)).Exists; }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna o caminho da pasta temporária.</para>
        /// </summary>
        public virtual DirectoryInfo Caminho
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Para cada consulta desta propriedade um nome único será retornado,
        /// nunca se repetindo.</para>
        /// </summary>
        public FileInfo ArquivoTemporarioUnico
        {
            get
            {
                if (!CaminhoExiste)
                {
                    return null;
                }
                else
                {
                    return new FileInfo((Caminho.FullName + "\\").Replace(@"\\", @"\") + (new ValorUnico()).UnicoAbsoluto);
                }
            }
        }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Esta propriedade retornará um nome único para cada usuário que
        /// acessar a aplicação. Cada usuário terá sempre o mesmo nome de arquivo, mas
        /// dois usuários diferentes terão nomes de arquivos diferentes.</para>
        /// <para>Obs.: O arquivo retornado por esta propriedade pode
        /// ser usado para gravar dados da sessão do usuário.</para>
        /// </summary>
        public FileInfo ArquivoTemporarioUnicoPorUsuario
        {
            get
            {
                if (!CaminhoExiste)
                {
                    return null;
                }
                else
                {
                    return new FileInfo((Caminho.FullName + "\\").Replace(@"\\", @"\") + (new ValorUnico()).PorUsuario);
                }
            }
        }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Esta propriedade retornará um nome de arquivo único para cada instância
        /// do aplicativo em execução.</para>
        /// </summary>
        public FileInfo ArquivoTemporarioUnicoPorSessao
        {
            get
            {
                if (!CaminhoExiste)
                {
                    return null;
                }
                else
                {
                    return new FileInfo((Caminho.FullName + "\\").Replace(@"\\", @"\") + (new ValorUnico()).PorSessao);
                }
            }
        }

        /// <summary>
        /// <para>Executa testes para verificar permissões de leitura, escrita e exclusão 
        /// na pasta temporária.</para>
        /// <para>Os valores serão armazenados para consulta através das 
        /// propriedades <see cref="PermissaoDeLeitura"/>, <see cref="PermissaoDeEscrita"/> e <see cref="PermissaoDeExclusao"/>.</para>
        /// </summary>
        private void AtualizaValoresSobrePermissao()
        {
            FileInfo arquivoTemporario = ArquivoTemporarioUnico;
            if (arquivoTemporario == null)
            {
                permissaoDeLeitura = false;
                permissaoDeEscrita = false;
                permissaoDeExclusao = false;
            }
            else
            {
                try
                {
                    Caminho.GetFiles();
                    permissaoDeLeitura = true;
                }
                catch (Exception)
                {
                    permissaoDeLeitura = false;
                }

                try
                {
                    StreamWriter strem = arquivoTemporario.CreateText();
                    strem.Write("teste de escrita nesta pasta.");
                    strem.Close();
                    permissaoDeEscrita = true;
                }
                catch (Exception)
                {
                    permissaoDeEscrita = false;
                }

                if (arquivoTemporario.Exists)
                {
                    try
                    {
                        arquivoTemporario.Delete();
                        permissaoDeExclusao = true;
                    }
                    catch (Exception)
                    {
                        permissaoDeExclusao = false;
                    }
                }
                else
                {
                    permissaoDeExclusao = false;
                }
            }
        }

        private bool permissaoDeLeitura;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna <c>true</c> quando é possível ler o conteúdo da pasta temporária.</para>
        /// </summary>
        public bool PermissaoDeLeitura
        {
            get
            {
                AtualizaValoresSobrePermissao();
                return permissaoDeLeitura;
            }
        }

        private bool permissaoDeEscrita;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna <c>true</c> quando é possível escrever na pasta temporária.</para>
        /// </summary>
        public bool PermissaoDeEscrita
        {
            get
            {
                AtualizaValoresSobrePermissao();
                return permissaoDeEscrita;
            }
        }

        private bool permissaoDeExclusao;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna <c>true</c> quando é possível escrever na pasta temporária.</para>
        /// </summary>
        public bool PermissaoDeExclusao
        {
            get
            {
                AtualizaValoresSobrePermissao();
                return permissaoDeExclusao;
            }
        }
    }
}
