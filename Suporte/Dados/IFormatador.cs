
namespace Suporte.Dados
{
    /// <summary>
    /// <para>Interface para classes que implementam formatadores.</para>
    /// </summary>
    public interface IFormatador<T>
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um formatador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        IFormatador<T> Formatador { get; }

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
        T Formatar(bool paraEntrada, T objeto);

        /// <summary>
        /// <para>Verifica se um objeto já está formatado.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Objeto que será verificado.</para>
        /// </param>
        /// <returns>
        /// <para>Quando o objeto já está formatado retorna <c>true</c>.</para>
        /// </returns>
        bool EstaFormatado(T objeto);
    }
}
