using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Suporte.Controles.Validacao.Alerta
{
    /// <summary>
    /// Alerta que faz o fundo do controle piscar com outra cor.
    /// </summary>
    public class Piscar : IAlerta
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public Piscar()
        {
            TotalDePiscadas = 3;
            Velocidade = 100;
            Cor = Color.Red;
        }

        /// <summary>
        /// <para>Dispara de fato o alerta ao usuário</para>
        /// </summary>
        /// <param name="controle"><para>Controle inválido que disparou o alerta.</para></param>
        public void Disparar(Control controle)
        {
            Color corOriginal = controle.BackColor;
            int piscadas = TotalDePiscadas * 2;
            new Timer { Enabled = true, Interval = Velocidade }.Tick += (sender, e) =>
                {
                    piscadas--;

                    if (controle.BackColor == Cor)
                    {
                        controle.BackColor = corOriginal;
                    }
                    else
                    {
                        controle.BackColor = Cor;
                    }

                    if (piscadas == 0)
                    {
                        controle.BackColor = corOriginal;
                        (sender as Timer).Enabled = false;
                        (sender as Timer).Dispose();
                    }
                };
        }

        /// <summary>
        /// <para>Cor</para>
        /// </summary>
        public Color Cor { get; set; }

        /// <summary>
        /// <para>Quantidade de vezes que a cor vai piscar.</para>
        /// </summary>
        public int TotalDePiscadas { get; set; }

        /// <summary>
        /// <para>Velocidade, em milisegundos, que vai piscar a cor.</para>
        /// <para>Quando maior, mais demorado.</para>
        /// </summary>
        public int Velocidade { get; set; }
    }
}
