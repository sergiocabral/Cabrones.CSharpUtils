using System;
using System.Collections.Generic;
using System.IO;

namespace Suporte.IO
{
    /// <summary>
    /// <para>Interface para classe que realiza uma ou mais tentativas 
    /// para obter a abertura de uma <see cref="Stream"/>.</para>
    /// </summary>
    public interface ITentativaDeObterStream<TStream> where TStream : Stream
    {
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número máximo de tentativas para se obter a <see cref="Stream"/>.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        int MaximoDeTentativas { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número correspondente ao tempo em milisegundos entre as tentativas
        /// para se obter a <see cref="Stream"/>.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        int TempoEntreAsTentativas { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista das exceptions disparadas enquanto se 
        /// tentava obter a <see cref="Stream"/>.</para>
        /// </summary>
        IList<Exception> ListaDasExceptions { get; }

        /// <summary>
        /// <para>Tenta obter a <see cref="Stream"/> conforme possível.</para>
        /// </summary>
        /// <returns>
        /// <para>Sempre retornará a <see cref="Stream"/>. 
        /// Caso contrário, será disparada uma <see cref="Exception" />.
        /// Se necessário consulta a propriedade <c>InnerException</c> da <see cref="Exception"/>,
        /// dê preferência a propriedade <see cref="ListaDasExceptions"/> desta classe.</para>
        /// </returns>
        Stream TentarObterStream();
    }
}
