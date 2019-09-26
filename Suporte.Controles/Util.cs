using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace Suporte.Controles
{
    /// <summary>
    /// <para>Classe utilitária com funcionalidades que auxiliam a manipulação de controles.</para>
    /// </summary>
    public static class Util
    {

        #region DllImport

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        #endregion

        /// <summary>
        /// <para>Determina se a aplicação está ativa para o sistema operacional, ou seja,
        /// se é a aplicação com o foco do usuário.</para>
        /// </summary>
        /// <returns><para>Retorna <c>==true</c> se a aplicação estiver ativa.</para></returns>
        public static bool AplicacaoEstaAtiva()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow != IntPtr.Zero)
            {
                int currentProcess = Process.GetCurrentProcess().Id;
                int windowThreadProcessId;
                GetWindowThreadProcessId(foregroundWindow, out windowThreadProcessId);
                return currentProcess == windowThreadProcessId;
            }
            return false;
        }

        /// <summary>
        /// <para>Verifica se uma posição absoluta do mouse está sobre um controle.</para>
        /// </summary>
        /// <param name="controle"><para>Controle.</para></param>
        /// <param name="posicaoAbsolutaDoMouse"><para>Posição absoluta do mouse.</para></param>
        /// <returns><para>Resposta <c>true</c> ou <c>false</c> sobre
        /// se uma posição está sobre um controle.</para></returns>
        public static bool EstaNaAreaDoControle(Control controle, Point posicaoAbsolutaDoMouse)
        {
            Point posicaoRelativaAoControle = controle.PointToClient(posicaoAbsolutaDoMouse);

            return
                posicaoRelativaAoControle.X >= -1 &&
                posicaoRelativaAoControle.Y >= -1 &&
                posicaoRelativaAoControle.X < controle.Width - 1 &&
                posicaoRelativaAoControle.Y < controle.Height - 1;
        }

        /// <summary>
        /// <para>Clona propriedades gerais de um controle.</para>
        /// </summary>
        /// <param name="original"><para>Controle original.</para></param>
        /// <param name="clonado"><para>Controle clonado.</para></param>
        public static void ClonarPropriedadesGeraisDoControle(Control original, Control clonado)
        {
            clonado.Parent = original.Parent;
            clonado.Width = original.Width;
            clonado.Height = original.Height;
            clonado.Left = original.Left;
            clonado.Top = original.Top;
            clonado.Name = original.Name;
        }

        /// <summary>
        /// <para>Encontra um controle na posição do clique do mouse em um controle.</para>
        /// </summary>
        /// <param name="controle"><para>Controle onde será pesquisado.</para></param>
        /// <param name="point"><para>Posição do clique em relação ao controle.</para></param>
        /// <returns><para>Controle encontrado.</para>
        /// <para>Retorna o próprio <see cref="Form"/> caso não haja nenhum.</para></returns>
        public static Control EncontrarControleNaPosicaoDoCliqueNoControle(Control controle, Point point)
        {
            Control controleEncontrado = controle;
            while (controle != null)
            {
                controle = controle.GetChildAtPoint(point);
                if (controle != null)
                {
                    controleEncontrado = controle;
                    point = new Point(point.X - controle.Left, point.Y - controle.Top);
                }
            };
            return controleEncontrado;
        }

        /// <summary>
        /// <para>Encontra o <see cref="Form"/> que contém o controle.</para>
        /// </summary>
        /// <param name="controle"><para>Controle</para></param>
        /// <returns><para><see cref="Form"/> que contém o controle.</para></returns>
        public static Form EncontrarForm(Control controle)
        {
            Control parentForm = controle;
            while (parentForm != null && !typeof(Form).IsAssignableFrom(parentForm.GetType()))
            {
                parentForm = parentForm.Parent;
            }
            return parentForm as Form;
        }

        /// <summary>
        /// <para>Define a propriedade Parent do controle e ajusta o
        /// posicionamento para que ele fique visualmente na mesma posição.</para>
        /// </summary>
        /// <param name="controle"><para>Constrole</para></param>
        /// <param name="parent"><para>Parent</para></param>
        public static void DefinirParentEAjustarPosicionamento(Control controle, Control parent)
        {
            if (controle.Parent == parent)
            {
                //Se não houver mudança no Parent atual, nada pecisa ser feito.
                return;
            }

            //O if abaixo faz um ajuste de posicionamento no caso do Parent
            //ser do tipo Panel. Pois, por algum motivo, ocorre uma diferença de 1 pixel.
            if (typeof(Panel).IsAssignableFrom(parent.GetType()))
            {
                controle.Top -= 1;
                controle.Left -= 1;
            }

            controle.Parent = parent;
            while (!typeof(Form).IsAssignableFrom(parent.GetType()))
            {
                controle.Top -= parent.Top;
                controle.Left -= parent.Left;
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// <para>Converte uma <see cref="Control.ControlCollection"/> para um <see cref="System.Collections.Generic.IList&lt;T&gt;"/>.</para>
        /// </summary>
        /// <param name="controls"><para>Propriedade Controls de um controle.</para></param>
        /// <returns><para>Lista <see cref="System.Collections.Generic.IList&lt;T&gt;"/>.</para></returns>
        public static IList<Control> ControlsComoList(Control.ControlCollection controls)
        {
            Control[] controles = new Control[controls.Count];
            controls.CopyTo(controles, 0);

            return new List<Control>(controles);
        }

        /// <summary>
        /// <para>Calcula o centro de um controle.</para>
        /// </summary>
        /// <param name="controle"><para>Controle.</para></param>
        /// <returns><para>Ponto do centro do controle.</para></returns>
        public static Point CalcularLeftTopDoCentroDoControle(Control controle)
        {
            return new Point(controle.Left + (controle.Width / 2), controle.Top + (controle.Height / 2));
        }
    }
}
