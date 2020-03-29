using System.IO;
using System.Reflection;

namespace Cabrones.Utils.Configuration
{
    /// <summary>
    /// Funções utilitárias para Assembly. 
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retorna o arquivo de um assembly com base na propriedade Location.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>FileInfo</returns>
        public static FileInfo? FileInfo(this Assembly assembly)
        {
            return assembly != null
                ? new FileInfo(assembly.Location)
                : null;
        }

        /// <summary>
        /// Retorna o diretório de um assembly com base na propriedade Location.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>DirectoryInfo</returns>
        public static DirectoryInfo? DirectoryInfo(this Assembly assembly)
        {
            return FileInfo(assembly)?.Directory;
        }
    }
}