
namespace Suporte.Dados
{
    /// <summary>
    /// <para>Interface para classes que implementam comparadores.</para>
    /// </summary>
    public interface IComparador<T>
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um comparador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        IComparador<T> Comparador { get; }

        /// <summary>
        /// <para>Verifica se dois objetos são iguais.</para>
        /// </summary>
        /// <param name="objeto1">
        /// <para>Objeto a ser comparado.</para>
        /// </param>
        /// <param name="objeto2">
        /// <para>Objeto a ser comparado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando os objetos são iguais.</para>
        /// </returns>
        bool SaoIguais(T objeto1, T objeto2);

        /// <summary>
        /// <para>Verifica se dois objetos são iguais.</para>
        /// </summary>
        /// <param name="formatador">
        /// <para>Antes de comparar remove a formatação dos objetos por este formatador.</para>
        /// </param>
        /// <param name="objeto1">
        /// <para>Objeto a ser comparado.</para>
        /// </param>
        /// <param name="objeto2">
        /// <para>Objeto a ser comparado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando os objetos são iguais.</para>
        /// </returns>
        bool SaoIguais(IFormatador<T> formatador, T objeto1, T objeto2);

    }
}
