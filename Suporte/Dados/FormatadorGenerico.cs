
namespace Suporte.Dados
{
    /// <summary>
    /// <para>Esta classe não implementa nenhum código ou regra.
    /// Seu objetivo é ser usada como instância genérica para a 
    /// interface <see cref="IFormatador&lt;T&gt;"/>.</para>
    /// <para></para>
    /// </summary>
    public class FormatadorGenerico<T> : IFormatador<T>
    {
        #region IFormatador<T> Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um formatador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        public IFormatador<T> Formatador
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// <para>Formata ou remove a formatação.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c> considera será feita uma escrita e, portanto, deve ser formatada. 
        /// Se igual a <c>false</c> considera que será feita uma leitura e a formatação é removida.</para>
        /// </param>
        /// <param name="objeto">
        /// <para>Objeto que será des/formatado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna o mesmo <paramref name="objeto"/> de entrada, porém modificado conforme formatação.</para>
        /// </returns>
        public T Formatar(bool paraEntrada, T objeto)
        {
            return objeto;
        }

        /// <summary>
        /// <para>Verifica se um objeto já está formatado.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Objeto que será verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Quando o objeto já está formatado retorna <c>true</c>.</para>
        /// </returns>
        public bool EstaFormatado(T objeto)
        {
            return true;
        }

        #endregion
    }
}