using System.IO;

namespace Suporte.IO.PastasEArquivos
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades referente a 
    /// uma pasta de armazenamento temporário do ambiente Windows.</para>
    /// </summary>
    public class PastaTemporariaWindows : PastaTemporaria
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public PastaTemporariaWindows() : base(Path.GetTempPath()) { }
    }
}
