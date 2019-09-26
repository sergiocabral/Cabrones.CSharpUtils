using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Suporte.Controles.Validacao.Validadores
{
    /// <summary>
    /// <para>Validação específica.</para>
    /// <para>Proibe valor vazio.</para>
    /// </summary>
    public class PalavrasProibidas : Validar
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public PalavrasProibidas()
        {
            Lista = new string[0];
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
                TextBox textBox = sender as TextBox;

                if (Lista != null)
                {
                    foreach (string palavra in Lista)
	                {
                        if (Regex.IsMatch(textBox.Text, @"\b" + Regex.Escape(palavra) + @"\b"))
                        {
                            e.Cancel = true;
                            break;
                        }
	                }
                }                
            }
            else
            {
                ThowExceptionNaoSuportado(sender);
            }
        }

        /// <summary>
        /// <para>Lista de palavras proibidas.</para>
        /// </summary>
        public string[] Lista { get; set; }
    }
}
