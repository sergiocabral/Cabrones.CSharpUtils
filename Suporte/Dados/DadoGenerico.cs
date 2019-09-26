using System;

namespace Suporte.Dados
{
    /// <summary>
    /// <para>O objetivo desta classe é armazenar um dado qualquer.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Tipo do dado armazenado.</para>
    /// </typeparam>
    /// <remarks>
    /// <para>Esta classe possui o atributo <see cref="SerializableAttribute"/> 
    /// para permitir serialização do dado armazenado.</para>
    /// </remarks>
    [Serializable]
    public class DadoGenerico<T>
    {
        private T valor;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Dado qualquer para armazenamento.</para>
        /// </summary>
        public T Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
