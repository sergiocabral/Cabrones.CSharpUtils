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
    /// <para>Proibe valor vazio.</para>
    /// </summary>
    public class NaoPermitidoCampoVazio : Validar
    {
        /// <summary>
        /// <para>Validação de fato, implementada na classe filha de forma específica.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        protected override void ValidarCampo(object sender, CancelEventArgs e)
        {
            /// Embora a implemenbtação para cada tipo pudesse ser feito
            /// neste método, isso o tornaria responsável por validar diversos 
            /// controles que resultaria em aumento de linhas sem união de objetivo.
            
            if (sender.GetType() == typeof(TextBox))
            {
                EventoValidating_TextBox(sender as TextBox, e);
            }
            else if (sender.GetType() == typeof(ComboBox))
            {
                EventoValidating_ComboBox(sender as ComboBox, e);
            }
            else
            {
                ThowExceptionNaoSuportado(sender);
            }
        }

        /// <summary>
        /// <para>Validação específica para <see cref="System.Windows.Forms.ComboBox"/>.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void EventoValidating_ComboBox(ComboBox sender, CancelEventArgs e)
        {
            e.Cancel = string.IsNullOrWhiteSpace(Convert.ToString(sender.SelectedValue));
        }

        /// <summary>
        /// <para>Validação específica para <see cref="System.Windows.Forms.TextBox"/>.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void EventoValidating_TextBox(TextBox sender, CancelEventArgs e)
        {
            TextBox controle = sender as TextBox;

            e.Cancel = string.IsNullOrWhiteSpace(Convert.ToString(sender.Text));
        }

    }
}
