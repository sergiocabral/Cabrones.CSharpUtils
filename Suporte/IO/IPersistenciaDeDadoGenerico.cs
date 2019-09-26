using Suporte.Dados;

namespace Suporte.IO
{
    /// <summary>
    /// <para>Interface para classe que possa 
    /// gravar <see cref="DadoGenerico&lt;T&gt;"/> em uma mídia.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Tipo do dado de <see cref="DadoGenerico&lt;T&gt;"/>.</para>
    /// </typeparam>
    public interface IPersistenciaDeDadoGenerico<T>
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Dado qualquer para armazenamento.</para>
        /// </summary>
        DadoGenerico<T> Dado { get; set; }

        /// <summary>
        /// <para>Gravar a propriedade <see cref="Dado"/>.</para>
        /// </summary>
        void Gravar();

        /// <summary>
        /// <para>Carrega o valor para a propriedade <see cref="Dado"/>.</para>
        /// </summary>
        void Carregar();
    }
}
