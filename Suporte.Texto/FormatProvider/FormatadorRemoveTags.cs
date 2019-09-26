using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe remove tags de um código XML ou HTML.</para>
    /// </summary>
    public class FormatadorRemoveTags : FormatadorTransparente
    {
        #region Public

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorRemoveTags() : base() { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorRemoveTags(IFormatProvider formatProvider) : base(formatProvider) { }
        
        /// <summary>
        /// <para>Contrutor.</para>
        /// </summary>
        /// <param name="tags">
        /// <para>Lista das tags HTML ou XML que devem ser removidas.</para>
        /// </param>
        public FormatadorRemoveTags(params string[] tags)
            :this(null, tags)
        {            
        }

        /// <summary>
        /// <para>Contrutor.</para>
        /// </summary>
        /// <param name="tags">
        /// <para>Lista das tags HTML ou XML que devem ser removidas.</para>
        /// </param>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorRemoveTags(IFormatProvider formatProvider, params string[] tags)
            : this(formatProvider)
        {
            ListaDeTags = new List<string>(tags);
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista das tags HTML ou XML que devem ser removidas.</para>
        /// </summary>
        public IList<string> ListaDeTags
        {
            get;
            private set;
        }

        #endregion

        #region Protected

        /// <summary>
        /// <para>Leitura</para>
        /// <para>Mesmo que a propriedade <see cref="ListaDeTags"/>,
        /// porém exibe as tags separadas por "|".</para>
        /// <para>Isso facilita o uso da lista numa Expresão Regular.</para>
        /// </summary>
        public string ListaDeTagsEx
        {
            get
            {
                StringBuilder lista = new StringBuilder();
                foreach (string tags in ListaDeTags)
                {
                    lista.Append("|");
                    lista.Append(tags);
                }
                if (lista.Length > 0)
                {
                    lista.Remove(0, 1);
                }
                return lista.ToString();
            }
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
        public override string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return Regex.Replace(base.Format(format, arg, formatProvider), @"</*(" + ListaDeTagsEx + ")[^>]*>", string.Empty);
        }

        #endregion
    }
}
