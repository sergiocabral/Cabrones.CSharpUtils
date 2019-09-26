using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Suporte.Controles.Validacao.Alerta
{
    /// <summary>
    /// Alerta que exibe uma mensagem pop-up para o usuário.
    /// </summary>
    public class Mensagem : IAlerta
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public Mensagem()
        {
            Texto = "Valor inválido";
            Titulo = "Preenchimento incorreto";
            Icone = MessageBoxIcon.Warning;
        }

        /// <summary>
        /// <para>Dispara de fato o alerta ao usuário</para>
        /// </summary>
        /// <param name="controle"><para>Controle inválido que disparou o alerta.</para></param>
        public void Disparar(Control controle)
        {
            MessageBox.Show(Texto, Titulo, MessageBoxButtons.OK, Icone);
        }

        /// <summary>
        /// <para>Texto de exibição para o usuário.</para>
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// <para>Título da janela.</para>
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// <para>Ícone na janela.</para>
        /// </summary>
        public MessageBoxIcon Icone { get; set; }
    }
}
