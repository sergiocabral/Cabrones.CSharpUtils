using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Suporte.Controles.Validacao
{
    /// <summary>
    /// <para>Classe que executa os procedimentos genéricos para uma validação qualquer.</para>
    /// </summary>
    public abstract class Validar
    {
        /// <summary>
        /// <para>Listas de alertas disparados quando a validação falha.</para>
        /// </summary>
        public IAlerta[] Alertas { get; set; }

        /// <summary>
        /// <para>Aplica este validador para um controle específico.</para>
        /// </summary>
        /// <param name="controle"><para>Controle</para></param>
        public void AplicarEsteValidador(Control controle)
        {
            //Atribui ao evento nativo do C# que faz a validação do controle.
            controle.Validating += new CancelEventHandler(EventoValidating);
        }

        /// <summary>
        /// <para>Método para o evento <see cref="Control.Validating"/>.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void EventoValidating(object sender, CancelEventArgs e)
        {            
            /// Se o Cancel já é true significa que alguma 
            /// coisa (desse código ou do próprio C#) já definiu
            /// como inválido.
            /// Não é necessário validar o que já foi validado.
            if (e.Cancel) { return; }

            /// Método Abstract 
            /// Delega para uma classe filha a validação.
            ValidarCampo(sender, e);

            if (e.Cancel && Alertas != null)
            {
                /// Se Cancel é true, significa que não passou na validação
                /// e precisa exibir alertas (se houver) ao usuário.
                foreach (IAlerta alerta in Alertas)
                {
                    /// Dispara cada um dos alertas associados.
                    alerta.Disparar(sender as Control);
                }
            }
        }

        /// <summary>
        /// <para>Padroniza o disparo de uma exceção.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        protected void ThowExceptionNaoSuportado(object sender)
        {
            throw new NotImplementedException("O validador " + this.GetType().Name + " não suporta o tipo " + sender.GetType().FullName);
        }

        /// <summary>
        /// <para>Validação de fato, implementada na classe filha de forma específica.</para>
        /// </summary>
        /// <param name="sender"><para>Controle que dispara o evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        protected abstract void ValidarCampo(object sender, CancelEventArgs e);
    }
}
