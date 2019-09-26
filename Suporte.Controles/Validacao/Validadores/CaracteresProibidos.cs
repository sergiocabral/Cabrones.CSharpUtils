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
    /// <para>Proibe a entrada de alguns caracteres.</para>
    /// </summary>
    public class CaracteresProibidos : Validar
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public CaracteresProibidos()
        {
            Lista = string.Empty;
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

                if (!string.IsNullOrEmpty(Lista))
                {
                    foreach (char c in Lista)
                    {
                        if (textBox.Text.Contains(c))
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
        /// <para>(Leitura/Escrita)</para>
        /// <para>Lista de caracteres proibidos.</para>
        /// </summary>
        public string Lista { get; set; }
    }
}
