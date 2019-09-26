using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;

namespace Suporte.Controles.Forms
{
    //TODO: Esta classe deve ser reescrita usando os eventos: ControlAdded e ControlRemoved
    /// <summary>
    /// <para>Esta classe configura controles para organizar de forma
    /// alinhada os controles internos .</para>
    /// </summary>
    public class OrganizarControles : Component
    {
        public OrganizarControles()
        {
            Controle = null;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="controle"><para>Controle que será configurado</para></param>
        public OrganizarControles(Control controle)
        {
            Controle = controle;
            Ativado = true;
        }

        private Control controle;
        /// <summary>
        /// <para>Controle para organizar controles internos.</para>
        /// </summary>
        public Control Controle
        {
            get
            {
                return controle;
            }
            set
            {
                bool bkpAtivado = Ativado;
                Ativado = false;
                controle = value;
                Ativado = bkpAtivado;
            }
        }

        //Abaixo nomes de propriedades para serem usadas em BackupDePropriedades
        private readonly string propriedadeSize = "Size";

        private Dictionary<string, object> backupDePropriedades = new Dictionary<string, object>();
        /// <summary>
        /// <para>Armazena um backup com os valores das propriedades modificadas.</para>
        /// </summary>
        private Dictionary<string, object> BackupDePropriedades
        {
            get { return backupDePropriedades; }
        }

        private bool ativado = false;
        /// <summary>
        /// <para>Ativa ou desativa a ação de arrastar e soltar.</para>
        /// </summary>
        public bool Ativado
        {
            get
            {
                return ativado;
            }
            set
            {
                if (value && !ativado && Controle != null)
                {
                    AssociarEventos(Controle);
                }
                else if (!value && ativado)
                {
                    DesassociarEventos(Controle);
                }
                ativado = value;
            }
        }

        /// <summary>
        /// <para>Indica o espaçamento entre os controles.</para>
        /// </summary>
        public Padding EspacamentoEntreControles { get; set; }

        /// <summary>
        /// <para>Indica a margem do controle.</para>
        /// </summary>
        public Padding MargemDoControle { get; set; }

        /// <summary>
        /// <para>Permite habilitar integração com <see cref="ArrastarESoltar"/>.</para>
        /// </summary>
        public bool IntegracaoComArrastarESoltar { get; set; }

        /// <summary>
        /// <para>Associa os eventos para organizar controle internos.</para>
        /// </summary>
        /// <param name="Controle"><para>Controle afetado.</para></param>
        protected void AssociarEventos(Control controle)
        {
            if (controle != null)
            {
                controle.Paint += new PaintEventHandler(PinturaDoControle_Paint);
            }
            Timer.Tick += new EventHandler(TimerTick);            
        }

        /// <summary>
        /// <para>Desassocia os eventos para organizar controle internos.</para>
        /// </summary>
        /// <param name="Controle"><para>Controle afetado.</para></param>
        protected void DesassociarEventos(Control controle)
        {
            Timer.Tick -= new EventHandler(TimerTick);            
            if (controle != null)
            {
                controle.Paint -= new PaintEventHandler(PinturaDoControle_Paint);
            }
        }

        /// <summary>
        /// <para>Armazena o silhueta do controle sendo arrastado.</para>
        /// </summary>
        private Rectangle SilhuetaDoControleSendoArrastado { get; set; }

        /// <summary>
        /// <para>Método para evento.</para>
        /// <para>Quando o controle precisa ser redesenhado.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void PinturaDoControle_Paint(object sender, PaintEventArgs e)
        {
            if (SilhuetaDoControleSendoArrastado != Rectangle.Empty && IntegracaoComArrastarESoltar)
            {
                Graphics graphics = (sender as Control).CreateGraphics();
                graphics.DrawRectangle(new Pen(Color.Red, 1), SilhuetaDoControleSendoArrastado);
            }
        }

        /// <summary>
        /// <para>Indicar que deve se reorganizados os controles.</para>
        /// </summary>
        private bool Reorganizar { get; set; }

        /// <summary>
        /// <para>Força a reorganização dos controles.</para>
        /// </summary>
        public void ReorganizarControles()
        {
            Reorganizar = true;
        }

        /// <summary>
        /// <para>Evento disparado quando os controles são reorganizados.</para>
        /// </summary>
        public event EventHandler AoReorganizarControles;

        /// <summary>
        /// <para>Método para evento Tick do Timer.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void TimerTick(object sender, EventArgs e)
        {
            if (Controle.IsDisposed)
            {
                Ativado = false;
                return;
            }

            Point posicaoAbsolutaDoMouse = Cursor.Position;

            if (Control.MouseButtons == MouseButtons.Left &&
                Util.AplicacaoEstaAtiva() &&
                Util.EstaNaAreaDoControle(Controle, posicaoAbsolutaDoMouse))
            {
                Form form = Util.EncontrarForm(Controle);
                Point posicaoRelativaAoForm = form.PointToClient(posicaoAbsolutaDoMouse);
                Control controleNaPosicaoDoMouse = Util.EncontrarControleNaPosicaoDoCliqueNoControle(form, posicaoRelativaAoForm);

                if (controleNaPosicaoDoMouse != Controle && !(controleNaPosicaoDoMouse is Form))
                {
                    OrganizarControlesInternos(controleNaPosicaoDoMouse);
                }
                Reorganizar = true;
            }
            else if (BackupDePropriedades.ContainsKey(propriedadeSize) && (Size)BackupDePropriedades[propriedadeSize] != Controle.Size)
            {                
                Reorganizar = true;
            }
            else if (Reorganizar)
            {
                Reorganizar = false;
                SilhuetaDoControleSendoArrastado = Rectangle.Empty;
                OrganizarControlesInternos(null);
                if (AoReorganizarControles != null)
                {
                    AoReorganizarControles(this, new EventArgs());
                }
            }
            BackupDePropriedades[propriedadeSize] = Controle.Size;
        }

        /// <summary>
        /// <para>Organizar os controles internos, podendo tendo por base um controle
        /// sendo arrastado no momento.</para>
        /// </summary>
        /// <param name="controleSendoArrastado"><para>Controle sendo arrastado.
        /// Pode passar como <c>null</c>.</para></param>
        private void OrganizarControlesInternos(Control controleSendoArrastado)
        {
            List<Control> controles = new List<Control>(Util.ControlsComoList(Controle.Controls));
            if (controleSendoArrastado != null && IntegracaoComArrastarESoltar && controleSendoArrastado.Parent != Controle)
            {
                //Para ordenar com um controle sendo arrastado é necessário que
                //este seja filho do mesmo Parent que os outros controles da lista.
                Control bkpParent = controleSendoArrastado.Parent;
                Util.DefinirParentEAjustarPosicionamento(controleSendoArrastado, Controle);
                controles.Insert(0, controleSendoArrastado);
                controles = OrdernarControles(controles, controleSendoArrastado);
                Util.DefinirParentEAjustarPosicionamento(controleSendoArrastado, bkpParent);
                controleSendoArrastado.BringToFront();
            }
            else
            {
                controles = OrdernarControles(controles, null);
            }
            DefinirPosicionamento(controles, controleSendoArrastado);
            
            Controle.Invalidate();
        }

        /// <summary>
        /// <para>Define o posicionamento dos controles para organizar.</para>
        /// </summary>
        /// <param name="controles"><para>Lista de controles.</para></param>
        /// <param name="controleSendoArrastado"><para>Controle que ainda está sendo arrastado.</para></param>
        private void DefinirPosicionamento(List<Control> controles, Control controleSendoArrastado)
        {
            Control controlePai = Controle;

            Padding espacamento = EspacamentoEntreControles;
            Padding margem = MargemDoControle;

            int topAtual = margem.Top;

            List<KeyValuePair<Control, Rectangle>> linhaAtual = new List<KeyValuePair<Control, Rectangle>>();
            List<List<KeyValuePair<Control, Rectangle>>> todasAsLinhas = new List<List<KeyValuePair<Control, Rectangle>>>();

            for (int i = 0; i < controles.Count; i++)
            {
                Control controle = controles[i];
                Rectangle posicionamento = new Rectangle(new Point(controles[i].Location.X, controles[i].Location.Y), controles[i].Size);

                //Posição Left do controle.
                posicionamento.X =
                    (linhaAtual.Count == 0 ? margem.Left : 0) +
                    espacamento.Left +
                    (linhaAtual.Count == 0 ? 0 : 
                        linhaAtual[linhaAtual.Count - 1].Value.Left + 
                        linhaAtual[linhaAtual.Count - 1].Value.Width + 
                        espacamento.Right);

                bool linhaUltrapassouLimite = 
                    ModoDeExibicao == ModosDeExibicao.UnicaColuna ||
                    (ModoDeExibicao != ModosDeExibicao.UnicaLinha &&
                     (posicionamento.X + 
                      posicionamento.Width +
                      espacamento.Right > controlePai.Width - margem.Right)
                    );
                if (!linhaUltrapassouLimite || linhaAtual.Count == 0)
                {
                    linhaAtual.Add(new KeyValuePair<Control, Rectangle>(controle, posicionamento));
                }
                else
                {
                    i--;
                }

                if (linhaUltrapassouLimite || i == controles.Count - 1)
                {
                    int maiorAltura = 0;
                    foreach (KeyValuePair<Control, Rectangle> item in linhaAtual)
                    {
                        int altura = 
                            item.Value.Height + 
                            espacamento.Top + 
                            espacamento.Bottom;
                        if (altura > maiorAltura)
                        {
                            maiorAltura = altura;
                        }
                    }
                    for (int j = 0; j < linhaAtual.Count; j++)
                    {
                        int altura = (maiorAltura - (linhaAtual[j].Value.Height + espacamento.Top + espacamento.Bottom)) / 2;

                        //Posição Top do controle.
                        int y = 
                            topAtual + 
                            espacamento.Top +                            
                            altura - 1;

                        linhaAtual[j] = new KeyValuePair<Control, Rectangle>(linhaAtual[j].Key, new Rectangle(new Point(linhaAtual[j].Value.Location.X, y), linhaAtual[j].Value.Size));
                    };
                    topAtual += maiorAltura;
                    todasAsLinhas.Add(linhaAtual);
                    linhaAtual = new List<KeyValuePair<Control, Rectangle>>();
                }
            }
            foreach (List<KeyValuePair<Control, Rectangle>> linha in todasAsLinhas)
            {
                foreach (KeyValuePair<Control, Rectangle> itemDaLinha in linha)
                {
                    if (itemDaLinha.Key == controleSendoArrastado)
                    {
                        SilhuetaDoControleSendoArrastado = new Rectangle(itemDaLinha.Value.Location, new Size(itemDaLinha.Value.Width - 1, itemDaLinha.Value.Height - 1));
                    }
                    else
                    {
                        itemDaLinha.Key.Left = itemDaLinha.Value.Left;
                        itemDaLinha.Key.Top = itemDaLinha.Value.Top;
                    }
                }
            }
        }

        /// <summary>
        /// Modos de exibição dos paineis.
        /// </summary>
        public enum ModosDeExibicao { UnicaLinha, UnicaColuna, LadoALado };

        private ModosDeExibicao modoDeExibicao = ModosDeExibicao.LadoALado;
        /// <summary>
        /// Indica o modo como os paineis serão exibidos.
        /// </summary>
        public ModosDeExibicao ModoDeExibicao
        {
            get { return modoDeExibicao; }
            set
            {
                modoDeExibicao = value;
                Reorganizar = true;
            }
        }

        /// <summary>
        /// <para>Retorna a lista de subcontroles ordenados.</para>
        /// </summary>
        public IList<Control> ListaDeControlesOrdenados
        {
            get
            {
                return OrdernarControles(Util.ControlsComoList(Controle.Controls), null);
            }
        }

        /// <summary>
        /// <para>Ordena um lista de controles com base no seu posicionamento visual.</para>
        /// </summary>
        /// <param name="controles"><para>Lista de Controles.</para></param>
        /// <returns><para>Lista ordenada.</para></returns>
        private List<Control> OrdernarControles(IList<Control> controles, Control controleSendoArrastado)
        {
            List<Control> controlesOrdenados = new List<Control>(controles);

            //TODO: Necessário ajustar o algorítmo abaixo pois está alterando a ordem dos controles.
            controlesOrdenados.Sort(delegate(Control controle1, Control controle2)
            {
                const int Controle1_PRIMEIRO = -1;
                const int Controle1_SEGUNDO = 1;

                if (controle1 == controle2)
                {
                    //Comparação do mesmo controle.
                    return 0;
                }
                else if (controle1.Top + controle1.Height < controle2.Top)
                {
                    //Controle1 está mais acima. Ele é o primeiro.
                    return Controle1_PRIMEIRO;
                }
                else if (controle2.Top + controle2.Height < controle1.Top)
                {
                    //Controle1 está mais abaixo. Ele é o segundo.
                    return Controle1_SEGUNDO;
                }
                else //Ambos os controles estão na mesma linha.
                {   
                    if (controle1.Left < controle2.Left)
                    {
                        //Controle1 está mais a esquerda. Ele é o primeiro.
                        return Controle1_PRIMEIRO;
                    }
                    else if (controle2.Left < controle1.Left)
                    {
                        //Controle2 está mais a direita. Ele é o segundo.
                        return Controle1_SEGUNDO;
                    }
                    else
                    {
                        //Controles na mesma posição. Retorna em primeiro o que estiver em mais ao nível Z-Index.
                        int indexControle1 = Controle.Controls.GetChildIndex(controle1);
                        int indexControle2 = Controle.Controls.GetChildIndex(controle2);

                        return indexControle1.CompareTo(indexControle2);
                    }
                }
            });

            return controlesOrdenados;
        }

        #region Controlador do Timer para obter o posicionamento do mouse.

        /// <summary>
        /// <para>Construtor estático.</para>
        /// </summary>
        static OrganizarControles()
        {
            Timer = new Timer();
            Timer.Interval = 100;
            Timer.Enabled = LicenseManager.UsageMode != LicenseUsageMode.Designtime;
        }

        /// <summary>
        /// <para>Timer para processar informações sobre a posição do mouse.</para>
        /// </summary>
        private static Timer Timer { get; set; }

        #endregion
    }
}
