using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Suporte.Controles.Validacao.Alerta
{
    /// <summary>
    /// Alerta que permite ser personalizado.
    /// </summary>
    public class Personalizado : IAlerta
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="handler"><para>Handler executado para personalizar o alerta.</para></param>
        public Personalizado(EventHandler handler)
        {
            Handler = handler;
        }

        /// <summary>
        /// <para>Dispara de fato o alerta ao usuário</para>
        /// </summary>
        /// <param name="controle"><para>Controle inválido que disparou o alerta.</para></param>
        public void Disparar(Control controle)
        {
            if (Handler != null)
            {
                Handler(controle, new EventArgs());
            }
        }

        /// <summary>
        /// <para>Handler executado para personalizar o alerta.</para>
        /// </summary>
        private EventHandler Handler { get; set; }
    }
}
