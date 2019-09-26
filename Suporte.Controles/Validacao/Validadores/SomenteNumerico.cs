using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Suporte.Controles.Validacao.Validadores
{
    /// <summary>
    /// <para>Validação específica.</para>
    /// <para>Permite apenas valores numéricos.</para>
    /// </summary>
    public class SomenteNumerico : Validar
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public SomenteNumerico()
        {
            SomenteInteiro = false;
            ValorMinimo = decimal.MinValue;
            ValorMaximo = decimal.MaxValue;
        }

        /// <summary>
        /// <para>Validação de fato, implementada na classe filha de forma específica.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        protected override void ValidarCampo(object sender, CancelEventArgs e)
        {
            if (sender is TextBox)
            {                
                e.Cancel = false;
                string valor = (sender as TextBox).Text;
                decimal numero;

                try
                {
                    if (SomenteInteiro)
                    {
                        numero = (decimal)long.Parse(valor);
                    }
                    else
                    {
                        numero = decimal.Parse(valor);
                    }
                    if (numero < ValorMinimo || numero > ValorMaximo)
                    {
                        //Número fora do range.
                        e.Cancel = true;
                    }
                    else
                    {
                        (sender as TextBox).Text = numero.ToString();
                    }
                }
                catch (Exception)
                {
                    //Número em formato inválido.
                    e.Cancel = true;
                }
            }
            else
            {
                ThowExceptionNaoSuportado(sender);
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Aceita apenas números inteiros.</para>
        /// </summary>
        public bool SomenteInteiro { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Define o valor mínimo aceito como número.</para>
        /// </summary>
        public decimal ValorMinimo { get; set; }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Define o valor máximo aceito como número.</para>
        /// </summary>
        public decimal ValorMaximo { get; set; }
    }
}
