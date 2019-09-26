using System;

namespace Suporte.Texto.FormatProvider
{
    /// <summary>
    /// <para>Implementa a interface <see cref="IFormatProvider"/>.</para>
    /// <para>Formata um valor <see cref="DateTime"/> para uma saudação do 
    /// tipo "Bom dia", "Boa tarde" ou "Boa noite".</para>
    /// </summary>
    public class FormatadorSaudacaoBomDiaTardeNoite : FormatadorTransparente
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorSaudacaoBomDiaTardeNoite(IFormatProvider formatProvider) : base(formatProvider) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public FormatadorSaudacaoBomDiaTardeNoite()
            : this("Bom dia", "Boa tarde", "Boa noite") { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="bomDia">
        /// <para>Informe o texto personalizado para "Bom Dia".</para>
        /// </param>
        /// <param name="boaTarde">
        /// <para>Informe o texto personalizado para "Boa Tarde".</para>
        /// </param>
        /// <param name="boaNoite">
        /// <para>Informe o texto personalizado para "Boa Noite".</para>
        /// </param>
        public FormatadorSaudacaoBomDiaTardeNoite(string bomDia, string boaTarde, string boaNoite)
            : this(null, bomDia, boaTarde, boaNoite)
        {
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="bomDia">
        /// <para>Informe o texto personalizado para "Bom Dia".</para>
        /// </param>
        /// <param name="boaTarde">
        /// <para>Informe o texto personalizado para "Boa Tarde".</para>
        /// </param>
        /// <param name="boaNoite">
        /// <para>Informe o texto personalizado para "Boa Noite".</para>
        /// </param>
        /// <param name="formatProvider"><para>Decora um FormatProvider já existente.</para></param>
        public FormatadorSaudacaoBomDiaTardeNoite(IFormatProvider formatProvider, string bomDia, string boaTarde, string boaNoite)
            : this(formatProvider)
        {
            BomDia = bomDia;
            BoaTarde = boaTarde;
            BoaNoite = boaNoite;
        }

        /// <summary>
        /// <para>texto personalizado para "Bom Dia"</para>
        /// </summary>
        public string BomDia
        {
            get;
            set;
        }

        /// <summary>
        /// <para>texto personalizado para "Boa Tarde"</para>
        /// </summary>
        public string BoaTarde
        {
            get;
            set;
        }

        /// <summary>
        /// <para>texto personalizado para "Boa Noite"</para>
        /// </summary>
        public string BoaNoite
        {
            get;
            set;
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
            try
            {
                DateTime hora = Convert.ToDateTime(base.Format(format, arg, formatProvider));
                if (hora.Hour < 12)
                {
                    return BomDia;
                }
                else if (hora.Hour < 18)
                {
                    return BoaTarde;
                }
                else
                {
                    return BoaNoite;
                }
            }
            catch (Exception ex)
            {
                throw new FormatException("Parâmetro não é do tipo DateTime.", ex);
            }
        }

        #endregion
    }
}
