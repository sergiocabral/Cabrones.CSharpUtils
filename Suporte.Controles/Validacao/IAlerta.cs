using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Suporte.Controles.Validacao
{
    /// <summary>
    /// <para>Interface para alerta générico.</para>
    /// </summary>
    public interface IAlerta
    {
        /// <summary>
        /// <para>Exibe de fato o alerta ao usuário</para>
        /// </summary>
        /// <param name="controle"><para>Controle inválido que disparou o alerta.</para></param>
        void Disparar(Control controle);
    }
}
