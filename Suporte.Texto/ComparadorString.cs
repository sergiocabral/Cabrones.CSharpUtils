using System;
using Suporte.Dados;

namespace Suporte.Texto
{
    /// <summary>
    /// <para>Implementa funcionalidades para comparação de objetos tipo <see cref="String"/>.</para>
    /// </summary>
    public class ComparadorString : IComparador<string>
    {

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ComparadorString()
            : this(new ComparadorGenerico<string>()) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="comparador">
        /// <para>Interface para um comparador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </param>
        public ComparadorString(IComparador<string> comparador)
        {
            this.comparador = comparador;
        }

        private bool ignorarAcentuacao = false;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando <c>true</c> ignora a acentuação.</para>
        /// </summary>
        public bool IgnorarAcentuacao
        {
            get
            {
                return ignorarAcentuacao;
            }
            set
            {
                ignorarAcentuacao = value;
            }
        }

        private bool ignorarMaiusculaMinuscula = false;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando <c>true</c> ignora maiúsculas e minúsculas.</para>
        /// </summary>
        public bool IgnorarMaiusculaMinuscula
        {
            get
            {
                return ignorarMaiusculaMinuscula;
            }
            set
            {
                ignorarMaiusculaMinuscula = value;
            }
        }

        #region IComparador<string> Members

        private IComparador<string> comparador;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Interface para um comparador mais interno.</para>
        /// <para>Padrão de projeto Decorator.</para>
        /// </summary>
        public IComparador<string> Comparador
        {
            get
            {
                return comparador;
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
        public bool SaoIguais(string objeto1, string objeto2)
        {
            return SaoIguais(new FormatadorGenerico<string>(), objeto1, objeto2);
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
        public bool SaoIguais(IFormatador<string> formatador, string objeto1, string objeto2)
        {
            objeto1 = formatador.Formatar(false, objeto1);
            objeto2 = formatador.Formatar(false, objeto2);

            if (IgnorarAcentuacao)
            {
                IFormatProvider formatProvider = new FormatProvider.FormatadorRemoveAcentuacao();
                objeto1 = objeto1 == null ? null : string.Format(formatProvider, "{0}", objeto1);
                objeto2 = objeto2 == null ? null : string.Format(formatProvider, "{0}", objeto2);
            }
            if (IgnorarMaiusculaMinuscula)
            {
                objeto1 = objeto1 == null ? null : objeto1.ToLower();
                objeto2 = objeto2 == null ? null : objeto2.ToLower();
            }

            return string.Equals(objeto1, objeto2) && Comparador.SaoIguais(formatador, objeto1, objeto2);
        }

        #endregion
    }
}
