using System.IO;

namespace Suporte.IO.PastasEArquivos
{
    /// <summary>
    /// <para>Interface para classe que disponibiliza funcionalidades referente a 
    /// uma pasta de armazenamento temporário de arquivos.</para>
    /// </summary>
    public interface IPastaTemporaria
    {

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informa se o caminho da pasta temporária existe.</para>
        /// </summary>
        bool CaminhoExiste { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna o caminho da pasta temporária.</para>
        /// </summary>
        DirectoryInfo Caminho { get; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna <c>true</c> quando é possível escrever na pasta temporária.</para>
        /// </summary>
        bool PermissaoDeEscrita { get; }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Para cada consulta desta propriedade um nome único será retornado,
        /// nunca se repetindo.</para>
        /// </summary>
        FileInfo ArquivoTemporarioUnico { get; }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Esta propriedade retornará um nome único para cada usuário que
        /// acessar a aplicação. Cada usuário terá sempre o mesmo nome de arquivo, mas
        /// dois usuários diferentes terão nomes de arquivos diferentes.</para>
        /// <para>Obs.: O arquivo retornado por esta propriedade pode
        /// ser usado para gravar dados da sessão do usuário.</para>
        /// </summary>
        FileInfo ArquivoTemporarioUnicoPorUsuario { get; }

        /// <summary>
        /// <para>Retorna um caminho válido para um arquivo temporário.</para>
        /// <para>Esta propriedade retornará um nome de arquivo único para cada instância
        /// do aplicativo em execução.</para>
        /// </summary>
        FileInfo ArquivoTemporarioUnicoPorSessao { get; }

    }
}
