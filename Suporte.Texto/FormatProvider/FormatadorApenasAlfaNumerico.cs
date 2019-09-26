using System;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe formata uma sequência de texto de modo que sejam removidos
    /// todos os caracteres que não sejam letras ou números.</para>
    /// </summary>
    public class FormatadorApenasAlfaNumerico : FormatadorTransparente
    {
        private string caracteresPermitidosComoExcecao;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Os caracteres nesta propriedade serão tidos como exceção a 
        /// regra de 'apenas caracteres alfanuméricos' e também serão mantidos.</para>
        /// <para>Atenção: O valor desta propriedade será usado como 
        /// parte de uma notação de expressão regular.</para>
        /// </summary>
        public string CaracteresPermitidosComoExcecao
        {
            get { return caracteresPermitidosComoExcecao; }
            set { caracteresPermitidosComoExcecao = value; }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorApenasAlfaNumerico(IFormatProvider formatProvider) : base(formatProvider) { }
        
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorApenasAlfaNumerico() : this(string.Empty) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Permite informar alguns caracteres além de letras e números que
        /// devem ser mantidos na sequência de testo.</para>
        /// <para>Por exemplo, pode-se informar <c>" _"</c> para permitir
        /// espaços em branco e underline.</para>
        /// </summary>
        /// <param name="caracteresPermitidosComoExcecao">
        /// <para>Sequência de caracteres permitidos.</para>
        /// <para>Atenção: O valor desta propriedade será usado como 
        /// parte de uma notação de expressão regular.</para>
        /// </param>
        public FormatadorApenasAlfaNumerico(string caracteresPermitidosComoExcecao)
            : this(null, caracteresPermitidosComoExcecao)
        {
            this.caracteresPermitidosComoExcecao = caracteresPermitidosComoExcecao;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Permite informar alguns caracteres além de letras e números que
        /// devem ser mantidos na sequência de testo.</para>
        /// <para>Por exemplo, pode-se informar <c>" _"</c> para permitir
        /// espaços em branco e underline.</para>
        /// </summary>
        /// <param name="caracteresPermitidosComoExcecao">
        /// <para>Sequência de caracteres permitidos.</para>
        /// <para>Atenção: O valor desta propriedade será usado como 
        /// parte de uma notação de expressão regular.</para>
        /// </param>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorApenasAlfaNumerico(IFormatProvider formatProvider, string caracteresPermitidosComoExcecao)
            : this(formatProvider)
        {
            this.caracteresPermitidosComoExcecao = caracteresPermitidosComoExcecao;
        }

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
            string expressaoRegular = "[^a-z,A-Z,0-9]";
            if (!string.IsNullOrEmpty(caracteresPermitidosComoExcecao))
            {
                expressaoRegular = expressaoRegular.Replace("]", "," + caracteresPermitidosComoExcecao + "]");
            }
            return System.Text.RegularExpressions.Regex.Replace(base.Format(format, arg, formatProvider), expressaoRegular, string.Empty);
        }

        #endregion
    }
}
