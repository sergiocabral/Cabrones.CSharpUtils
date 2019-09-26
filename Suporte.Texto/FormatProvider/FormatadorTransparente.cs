using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe remove não faz nenhuma operação.</para>
    /// </summary>
    public class FormatadorTransparente : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>FormatProvider que será decorado.</para>
        /// </summary>
        protected virtual IFormatProvider FormatProvider { get; private set; }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorTransparente()
            : this(null)
        {
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorTransparente(IFormatProvider formatProvider)
        {
            FormatProvider = formatProvider;
        }

        #region IFormatProvider Members

        /// <summary>
        /// <para>Retorna um objeto que fornece serviços de formatação 
        /// para um tipo especificado.</para>
        /// </summary>
        /// <param name="formatType">
        /// <para>Um objeto que especifica o tipo de formato de objeto para retornar.</para>
        /// </param>
        /// <returns>
        /// <para>Uma instância do objeto especificado pelo parâmetro <paramref name="formatType"/>, 
        /// se a implementação <see cref="IFormatProvider"/> pode fornecer esse 
        /// tipo de objeto, caso contrário, retorna <c>null</c>.</para>
        /// </returns>
        public virtual object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        #endregion

        #region ICustomFormatter Members

        /// <summary>
        /// <para>Converte o valor de um objeto especificado para um representação 
        /// de seqüência equivalente usando o formato especificado e 
        /// informações de formatação da região (culture-specific).</para>
        /// </summary>
        /// <param name="format"><para>A seqüência de formato que contém 
        /// especificações de formatação.</para></param>
        /// <param name="arg"><para>O objeto a ser formatado.</para></param>
        /// <param name="formatProvider"><para>Um objeto que fornece informações sobre 
        /// o formato da instância atual.</para></param>
        /// <returns><para>A sequência de texto <paramref name="arg"/> formatada
        /// conforme especificado pelos parâmetros <paramref name="format"/> e 
        /// <paramref name="formatProvider"/>.</para></returns>
        public virtual string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string result = arg.ToString();
            if (FormatProvider != null)
            {
                result = string.Format(FormatProvider, "{0}", result);
            }
            return result;
        }

        #endregion
    }
}
