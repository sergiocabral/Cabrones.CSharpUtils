using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Suporte.Controles.Forms
{
    /// <summary>
    /// <para>Esta classe configura controles para serem movidos com
    /// o mouse numa ação de arrastar e soltar.</para>
    /// </summary>
    public class ArrastarESoltar : Component
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public ArrastarESoltar()
        {
            ControlesDeDestino = null;
            Controle = null;
        }

        /// <summary>
        /// <para>Construtor</para>
        /// </summary>
        /// <param name="controle">
        /// <para>Controle que será configurado para arrastar e solta.</para>
        /// </param>
        /// <param name="controlesDeDestino">
        /// <para>Controles que aceitarão receber o controle arrastado.</para>
        /// </param>
        public ArrastarESoltar(Control controle, params Control[] controlesDeDestino)
        {
            ControlesDeDestino = controlesDeDestino == null ? null : new List<Control>(controlesDeDestino);
            Controle = controle;
            Ativado = true;
        }

        private Control controle;
        /// <summary>
        /// <para>Controle para arrastar e soltar.</para>
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

        /// <summary>
        /// <para>Define um único controle que poderá aceitar este controle sendo arrastado.</para>
        /// <para>Para definir mais controles use <see cref="ControlesDeDestino"/>.</para>
        /// </summary>
        public Control ControleDeDestino
        {
            get
            {
                return ControlesDeDestino == null || ControlesDeDestino.Count > 1 ? null : ControlesDeDestino[0];
            }
            set
            {
                ControlesDeDestino = new List<Control>(new Control[] { value });
            }
        }

        /// <summary>
        /// <para>Controles que aceitarão receber o controle arrastado.</para>
        /// <para>Quando definido ==null, qualquer controle aceitará receber este 
        /// controle arrastado sobre ele.</para>
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<Control> ControlesDeDestino { get; set; }

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
        /// <para>Associa os eventos para arrastar e soltar um controle.</para>
        /// </summary>
        /// <param name="Controle"><para>Controle afetado.</para></param>
        protected void AssociarEventos(Control controle)
        {
            if (controle != null)
            {
                controle.MouseDown += new MouseEventHandler(MovimentacaoDoControle_MouseDown);
                controle.MouseUp += new MouseEventHandler(MovimentacaoDoControle_MouseUp);
                controle.MouseMove += new MouseEventHandler(MovimentacaoDoControle_MouseMove);
                controle.MouseHover += new EventHandler(MovimentacaoDoControle_MouseHover);
                controle.MouseLeave += new EventHandler(MovimentacaoDoControle_MouseLeave);
                controle.MouseEnter += new EventHandler(MovimentacaoDoControle_MouseEnter);
            }
        }

        /// <summary>
        /// <para>Desassocia os eventos para arrastar e soltar um controle.</para>
        /// </summary>
        /// <param name="Controle"><para>Controle afetado.</para></param>
        protected void DesassociarEventos(Control controle)
        {
            if (controle != null)
            {
                controle.MouseDown -= new MouseEventHandler(MovimentacaoDoControle_MouseDown);
                controle.MouseUp -= new MouseEventHandler(MovimentacaoDoControle_MouseUp);
                controle.MouseMove -= new MouseEventHandler(MovimentacaoDoControle_MouseMove);
                controle.MouseHover -= new EventHandler(MovimentacaoDoControle_MouseHover);
                controle.MouseLeave -= new EventHandler(MovimentacaoDoControle_MouseLeave);
                controle.MouseEnter -= new EventHandler(MovimentacaoDoControle_MouseEnter);
            }
        }

        #region Informações vinculadas ao controle
        
        /// <summary>
        /// <para>Comportamentos possíveis do mouse referente ao controle.</para>
        /// </summary>
        public enum TipoDeComportamentosDoMouse { NenhumContato, PassandoPorCima, Arrastando }

        /// <summary>
        /// <para>Indica o comportamento do mouse referente ao controle.</para>
        /// </summary>
        private TipoDeComportamentosDoMouse ComportamentoDoMouse { get; set; }

        //Abaixo nomes de propriedades para serem usadas em BackupDePropriedades
        private readonly string propriedadeCursor = "Cursor";
        private readonly string propriedadeParent = "Parent";
        private readonly string propriedadeLeft = "Left";
        private readonly string propriedadeTop = "Top";
        
        private Dictionary<string, object> backupDePropriedades = new Dictionary<string, object>();
        /// <summary>
        /// <para>Armazena um backup com os valores das propriedades modificadas.</para>
        /// </summary>
        private Dictionary<string, object> BackupDePropriedades
        {
            get { return backupDePropriedades; }
        }

        /// <summary>
        /// <para>Informações de posicionamento quando o controle foi selecionado para arrastar.</para>
        /// </summary>
        private MouseEventArgs InformacoesParaArrastarControle { get; set; }

        #endregion

        #region Eventos para arrastar e soltar

        /// <summary>
        /// <para>Evento: MouseDown.</para>
        /// <para>Mouse é pressionado.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            ComportamentoDoMouse = TipoDeComportamentosDoMouse.Arrastando;
            InformacoesParaArrastarControle = e;
            ConfigurarParent(e);
            (sender as Control).BringToFront();
        }

        /// <summary>
        /// <para>Evento: MouseUp.</para>
        /// <para>Mouse teve seu clique liberado.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseUp(object sender, MouseEventArgs e)
        {
            if (InformacoesParaArrastarControle == null) { return; }
            ComportamentoDoMouse = TipoDeComportamentosDoMouse.PassandoPorCima;
            ConfigurarParent(e);
            InformacoesParaArrastarControle = null;
            (sender as Control).BringToFront();
        }
        
        /// <summary>
        /// <para>Evento: MouseMove.</para>
        /// <para>Mouse está se movendo sobre o controle.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseMove(object sender, MouseEventArgs e)
        {
            if (InformacoesParaArrastarControle != null)
            {
                Controle.Left = Controle.Left + (e.X - InformacoesParaArrastarControle.X);
                Controle.Top = Controle.Top + (e.Y - InformacoesParaArrastarControle.Y);
            }
        }

        /// <summary>
        /// <para>Evento: MouseHover.</para>
        /// <para>Mouse passa por cima do controle.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseHover(object sender, EventArgs e)
        {
            ComportamentoDoMouse = TipoDeComportamentosDoMouse.PassandoPorCima;
        }

        /// <summary>
        /// <para>Evento: MouseLeave.</para>
        /// <para>Mouse saiu da cima do controle.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseLeave(object sender, EventArgs e)
        {
            ComportamentoDoMouse = TipoDeComportamentosDoMouse.NenhumContato;
            ConfigurarCursorDoMouse();
        }

        /// <summary>
        /// <para>Evento: MouseEnter.</para>
        /// <para>Mouse entra na área do controle.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void MovimentacaoDoControle_MouseEnter(object sender, EventArgs e)
        {
            ComportamentoDoMouse = TipoDeComportamentosDoMouse.PassandoPorCima;
            ConfigurarCursorDoMouse();
        }

        #endregion

        /// <summary>
        /// <para>Configura a exibição do cursor do mouse.</para>
        /// </summary>
        private void ConfigurarCursorDoMouse()
        {            
            if (!BackupDePropriedades.ContainsKey(propriedadeCursor))
            {
                BackupDePropriedades[propriedadeCursor] = Controle.Cursor;
            }
            Controle.Cursor = ComportamentoDoMouse == TipoDeComportamentosDoMouse.NenhumContato ? (Cursor)BackupDePropriedades[propriedadeCursor] : Cursors.Hand;
        }

        /// <summary>
        /// <para>Configura o Parent do controle para que 
        /// seja possível arrastar pela janela do Form.</para>
        /// </summary>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void ConfigurarParent(MouseEventArgs e)
        {
            if (ComportamentoDoMouse == TipoDeComportamentosDoMouse.Arrastando)
            {
                ConfigurarParentQuandoArrastando(e);
            }
            else
            {
                ConfigurarParentQuandoSolto(e);
            }
        }

        /// <summary>
        /// <para>Configura o Parent do controle quando este está sendo arrastado.</para>
        /// </summary>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void ConfigurarParentQuandoArrastando(MouseEventArgs e)
        {
            //Backup do posicionamento caso não tenha sido feito.
            if (!BackupDePropriedades.ContainsKey(propriedadeParent))
            {
                BackupDePropriedades[propriedadeParent] = Controle.Parent;
                BackupDePropriedades[propriedadeLeft] = Controle.Left;
                BackupDePropriedades[propriedadeTop] = Controle.Top;
            }

            //Obtem informações sobre o comportamento do evento Soltar
            AoArrastarEventArgs comportamento = new AoArrastarEventArgs();
            comportamento.ParentOriginal = Controle.Parent;
            if (AoArrastar != null) { AoArrastar(this, comportamento); }

            if (!comportamento.ArrastarPeloMesmoControle)
            {
                //Para arrastar, define o Controle como filho direto do Form.
                Form form = Util.EncontrarForm(Controle);
                form.Controls.Add(Controle);
            }

            if (AposArrastar != null) { AposArrastar(this, comportamento); }
        }

        /// <summary>
        /// <para>Configura o Parent do controle quando este está é solto.</para>
        /// </summary>
        /// <param name="e"><para>Informações do evento.</para></param>
        private void ConfigurarParentQuandoSolto(MouseEventArgs e)
        {
            //Armazena a referência ao Form.
            Control controlePai = Controle.Parent;

            Controle.Parent = null;

            //Encontra o controle na posição do clique do mouse.
            Control ControleNaPosicaoDoClique = Util.EncontrarControleNaPosicaoDoCliqueNoControle(controlePai, new Point(Controle.Left + e.X, Controle.Top + e.Y));

            //Obtem informações sobre o comportamento do evento Soltar
            AoSoltarEventArgs comportamento = new AoSoltarEventArgs();
            comportamento.ControleDeDestino = ControleNaPosicaoDoClique;
            if (AoSoltar != null) { AoSoltar(this, comportamento); }
            ControleNaPosicaoDoClique = comportamento.ControleDeDestino;

            if (!comportamento.Cancelar && (ControlesDeDestino == null || ControlesDeDestino.Contains(ControleNaPosicaoDoClique)))
            {
                if (comportamento.Clonar)
                {
                    ClonarEventArgs informacoesDoClone = new ClonarEventArgs { Original = Controle };
                    AoRequisitarClonar(this, informacoesDoClone);                    

                    //Adiciona o Controle Clonado ao controle na posição do clique.
                    Util.DefinirParentEAjustarPosicionamento(informacoesDoClone.Clonado, ControleNaPosicaoDoClique);

                    //Restaura o posicionamento anterior do Controle.
                    Controle.Parent = (Control)BackupDePropriedades[propriedadeParent];
                    Controle.Left = (int)BackupDePropriedades[propriedadeLeft];
                    Controle.Top = (int)BackupDePropriedades[propriedadeTop];
                }
                else
                {
                    //Adiciona o Controle ao controle na posição do clique.
                    Util.DefinirParentEAjustarPosicionamento(Controle, ControleNaPosicaoDoClique);
                }
            }
            else
            {
                if (comportamento.ExcluirSeDestinoNaoEAutorizado)
                {
                    //Torna o controle órfão para exclusão.
                    Controle.Parent = null;
                    Controle.Dispose();
                }
                else
                {
                    //Restaura o posicionamento anterior.
                    Controle.Parent = (Control)BackupDePropriedades[propriedadeParent];
                    Controle.Left = (int)BackupDePropriedades[propriedadeLeft];
                    Controle.Top = (int)BackupDePropriedades[propriedadeTop];
                }
            }

            //Apaga o backup de posicionamento.
            BackupDePropriedades.Remove(propriedadeParent);
            BackupDePropriedades.Remove(propriedadeLeft);
            BackupDePropriedades.Remove(propriedadeTop);

            if (AposSoltar != null) { AposSoltar(this, comportamento); }
        }

        #region Eventos

        /// <summary>
        /// <para>Classe que agrupa informações do evento <see cref="AoArrastar"/>.</para>
        /// </summary>
        public class AoArrastarEventArgs : EventArgs
        {
            /// <summary>
            /// <para>Limita o arrastar dentro do próprio Parent.</para>
            /// </summary>
            public bool ArrastarPeloMesmoControle { get; set; }

            /// <summary>
            /// <para>Referência para o Parent original.</para>
            /// </summary>
            public Control ParentOriginal { get; set; }
        }

        /// <summary>
        /// <para>Evento disparado quando o controle começa a ser arrastado.</para>
        /// </summary>
        public event EventHandler<AoArrastarEventArgs> AoArrastar;

        /// <summary>
        /// <para>Evento disparado quando o controle passa a ser arrastado.</para>
        /// </summary>
        public event EventHandler<AoArrastarEventArgs> AposArrastar;

        /// <summary>
        /// <para>Classe que agrupa informações do evento <see cref="AoSoltar"/>.</para>
        /// </summary>
        public class AoSoltarEventArgs : EventArgs
        {
            /// <summary>
            /// <para>Cancelar o arrastar do controle.</para>
            /// </summary>
            public bool Cancelar { get; set; }

            /// <summary>
            /// <para>Não arrastar o controle, mas cria um clone na nova posição.</para>
            /// </summary>
            public bool Clonar { get; set; }

            /// <summary>
            /// <para>Exclui o controle quando este é arrastado para um controle
            /// de destino não presente na listagem <see cref="ArrastarESoltar.ControlesDeDestino"/>.</para>
            /// </summary>
            public bool ExcluirSeDestinoNaoEAutorizado { get; set; }

            /// <summary>
            /// <para>Referência para o controle de destino.</para>
            /// </summary>
            public Control ControleDeDestino { get; set; }
        }

        /// <summary>
        /// <para>Evento disparado quando o controle é solto.</para>
        /// </summary>
        public event EventHandler<AoSoltarEventArgs> AoSoltar;

        /// <summary>
        /// <para>Evento disparado depois que quando o controle é solto.</para>
        /// </summary>
        public event EventHandler<AoSoltarEventArgs> AposSoltar;

        /// <summary>
        /// <para>Classe que agrupa informações do evento <see cref="EventHandler AoClonar"/>.</para>
        /// </summary>
        public class ClonarEventArgs : EventArgs
        {
            /// <summary>
            /// <para>Controle original que precisa ser clonado.</para>
            /// </summary>
            public Control Original { get; internal set; }

            /// <summary>
            /// <para>Controle clonado.</para>
            /// </summary>
            public Control Clonado { get; set; }
        }

        /// <summary>
        /// <para>Evento disparado quando é necessário obter um clone de um objeto.</para>
        /// </summary>
        public event EventHandler<ClonarEventArgs> AoRequisitarClonar;

        #endregion

    }
}
