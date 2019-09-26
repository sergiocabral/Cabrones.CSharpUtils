using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Suporte.Controles.Validacao.Alerta
{
    /// <summary>
    /// Alerta que dispara um beep no pc speaker.
    /// </summary>
    public class EmitirBeep : IAlerta
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public EmitirBeep()
        {
            Frequencia = 1000;
            Duracao = 1000;
        }

        /// <summary>
        /// <para>Dispara de fato o alerta ao usuário</para>
        /// </summary>
        /// <param name="controle"><para>Controle inválido que disparou o alerta.</para></param>
        public void Disparar(Control controle)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (sender, e) =>
                {
                    Console.Beep(Frequencia, Duracao);
                };
            backgroundWorker.RunWorkerAsync();
            backgroundWorker.RunWorkerCompleted += (sender, e) =>
                {
                    (sender as BackgroundWorker).Dispose();
                };
        }

        /// <summary>
        /// <para>Frequência do beep.</para>
        /// </summary>
        public int Frequencia { get; set; }

        /// <summary>
        /// <para>Duração do beep.</para>
        /// </summary>
        public int Duracao { get; set; }
    }
}
