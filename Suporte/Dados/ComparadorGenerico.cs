
namespace Suporte.Dados
{
    /// <summary>
    /// <para>Esta classe não implementa nenhum código ou regra.
    /// Seu objetivo é ser usada como instância genérica para a 
    /// interface <see cref="IComparador&lt;T&gt;"/>.</para>
    /// <para></para>
    /// </summary>
    public class ComparadorGenerico<T> : IComparador<T>
    {

        #region IComparador<T> Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um comparador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        public IComparador<T> Comparador
        {
            get
            {
                return null;
            }
        }

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
        public bool SaoIguais(T objeto1, T objeto2)
        {
            return SaoIguais(new FormatadorGenerico<T>(), objeto1, objeto2);
        }

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
        public bool SaoIguais(IFormatador<T> formatador, T objeto1, T objeto2)
        {
            return object.Equals(objeto1, objeto2);
        }

        #endregion
    }
}