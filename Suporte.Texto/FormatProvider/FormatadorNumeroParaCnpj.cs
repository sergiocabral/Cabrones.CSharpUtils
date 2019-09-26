using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Esta classe converte um número para o formato de numeração de CNPJ.</para>
    /// </summary>
    public class FormatadorNumeroParaCnpj : FormatadorTransparente
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorNumeroParaCnpj() : base() { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorNumeroParaCnpj(IFormatProvider formatProvider) : base(formatProvider) { }
        
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
            string numero = base.Format(format, arg, formatProvider);
            if (!Validador.Texto.Numerico(numero))
            {
                throw new FormatException(string.Format("O valor \"{0}\" não é numérico.", numero));
            }
            if (numero.Length != 14 && numero.Length != 15)
            {
                throw new FormatException("O número para CNPJ deve ter 14 ou 15 dígitos.");
            }
            int diferenca = numero.Length - 14;

            numero = numero.Insert(2 + diferenca, ".");
            numero = numero.Insert(6 + diferenca, ".");
            numero = numero.Insert(10 + diferenca, "/");
            numero = numero.Insert(15 + diferenca, "-");

            return numero;
        }

        #endregion
    }
}
