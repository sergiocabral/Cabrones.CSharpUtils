using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Suporte.Controles.WPF
{
    /// <summary>
    /// <para>Implementa uma caixa de confirmação para o usuário 
    /// responder.</para>
    /// <para>Uma estrutura com mensagem e botões é exibida dentro de um controle
    /// já existente na tela bloqueando o que há por trás.</para>
    /// </summary>
    public class CaixaDeConfirmacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public CaixaDeConfirmacao() : this(null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="ControleBase">
        /// <para>Controle tipo painel que conterá a estrutura com mensagem e botões.</para>
        /// </param>
        public CaixaDeConfirmacao(Panel controleBase)
        {
            Inicializar(controleBase);
        }

        /// <summary>
        /// <para>Inicializar atributos e propriedades com seus valores padrão.</para>
        /// </summary>
        /// <param name="ControleBase">
        /// <para>Controle tipo painel que conterá a estrutura com mensagem e botões.</para>
        /// </param>
        private void Inicializar(Panel controleBase)
        {
            ControleBase = controleBase;

            Pergunta = "Confirmar?";

            Botoes = new string[] { "Sim", "Não" };

            LinearGradientBrush corDoFundo = new LinearGradientBrush();
            corDoFundo.EndPoint = new Point(0.5, 1);
            corDoFundo.StartPoint = new Point(0.5, 0);
            Color corDoFundoGradiente1 = new Color();
            corDoFundoGradiente1.A = 0xFF; corDoFundoGradiente1.R = 0x29; corDoFundoGradiente1.G = 0x39; corDoFundoGradiente1.B = 0x55;
            Color corDoFundoGradiente2 = new Color();
            corDoFundoGradiente2.A = 0x90; corDoFundoGradiente2.R = 0xFF; corDoFundoGradiente2.G = 0xFF; corDoFundoGradiente2.B = 0xFF;
            corDoFundo.GradientStops.Add(new GradientStop(corDoFundoGradiente2, 1));
            corDoFundo.GradientStops.Add(new GradientStop(corDoFundoGradiente1, 0.254));
            CorDoFundo = corDoFundo;

            CorDoTexto = Brushes.White;

            Temporizador = new Timer();
            Temporizador.Elapsed += new ElapsedEventHandler(ProcessarEvento_Temporizador_Elapsed);
            TempoDeExibicao = 0;
        }

        public Panel controleBase;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Controle base que contem a estrutura de mensagem e botões.</para>
        /// </summary>
        public Panel ControleBase
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Indica se o usuario já respondeu ou não.</para>
        /// </summary>
        public bool AguardandoResposta
        {
            get
            {
                return Estrutura != null;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Mensagem exibida como pergunta.</para>
        /// </summary>
        public string Pergunta
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tempo (em milisegundos) de exibição da pergunta. 
        /// Após isso confirmação é fechada.</para>
        /// <para>Use 0 (zero) para ignorar este campo.</para>
        /// </summary>
        public double TempoDeExibicao
        {
            get
            {
                return Temporizador.Interval - 1;
            }
            set
            {
                if (value <= 0)
                {
                    Temporizador.Enabled = false;
                }
                Temporizador.Interval = value + 1;
            }
        }

        private string[] botoes;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nomes dos botões.</para>
        /// </summary>
        public string[] Botoes
        {
            get
            {
                return botoes;
            }
            set
            {
                if (value.Length > 0)
                {
                    botoes = value;
                    IndiceDoBotaoPadrao = value.Length - 1;
                }
                else
                {
                    throw new Exception("É necessário informar pelo menos um botão.");
                }
            }
        }

        private int indiceDoBotaoPadrao;
        /// <summary>
        /// <para>Índice (base zero) do botão padrão.</para>
        /// </summary>
        public int IndiceDoBotaoPadrao
        {
            get
            {
                return indiceDoBotaoPadrao;
            }
            set
            {
                if (value < Botoes.Length)
                {
                    indiceDoBotaoPadrao = value;
                }
                else
                {
                    throw new Exception(string.Format("Não existe botão com índice \"{0}\".", value));
                }
            }
        }

        /// <summary>
        /// <para>Nome do botão padrão.</para>
        /// </summary>
        public string BotaoPadrao
        {
            get
            {
                return Botoes[IndiceDoBotaoPadrao];
            }
            set
            {
                if (Botoes.Contains(value))
                {
                    indiceDoBotaoPadrao = Array.IndexOf<string>(Botoes, value);
                }
                else
                {
                    throw new Exception(string.Format("O botão \"{0}\" não existe.", value));
                }
            }
        }

        public Brush corDoFundo;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Cor do fundo.</para>
        /// </summary>
        public Brush CorDoFundo
        {
            get
            {
                return corDoFundo;
            }
            set
            {
                if (value != null)
                {
                    corDoFundo = value;
                }
                else
                {
                    throw new Exception("É necessário definir uma cor para o fundo.");
                }
            }
        }

        public Brush corDoTexto;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Cor do texto.</para>
        /// </summary>
        public Brush CorDoTexto
        {
            get
            {
                return corDoTexto;
            }
            set
            {
                if (value != null)
                {
                    corDoTexto = value;
                }
                else
                {
                    throw new Exception("É necessário definir uma cor para o texto.");
                }
            }
        }

        /// <summary>
        /// <para>Imagem de fundo.</para>
        /// </summary>
        public BitmapImage ImagemDeFundo
        {
            get;
            set;
        }

        /// <summary>
        /// <para>Evento disparado antes de se exibir a estrutura de mensagem e botões.</para>
        /// </summary>
        public event EventHandler<EventArgs> AntesDeExibirConfirmacao;

        /// <summary>
        /// <para>Evento disparado quando a caixa de confirmação é respondida.</para>
        /// </summary>
        public event ResponderCaixaDeConfirmacao QuandoResponder;

        /// <summary>
        /// <para>Evento disparado depois que a estrutura de mensagem e botões é fechada.</para>
        /// </summary>
        public event EventHandler<EventArgs> DepoisDeFecharConfirmacao;

        /// <summary>
        /// <para>Estrutura de mensagem e botões em exibição.</para>
        /// </summary>
        protected Panel Estrutura
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>Método adicional para processar o evento <see cref="QuandoResponder"/>.</para>
        /// <para>Esta propriedade é definida na chamada do método <c>this.Exibir()</c>.</para>
        /// </summary>
        private ResponderCaixaDeConfirmacao MetodoExtraParaEventoQuandoResponder
        {
            get;
            set;
        }

        /// <summary>
        /// <para>Temporizador para fechar automaticamente se o usuário não responder.</para>
        /// </summary>
        private Timer Temporizador
        {
            get;
            set;
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        public void Exibir()
        {
            Exibir(Pergunta, TempoDeExibicao, null);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="pergunta">
        /// <para>Mensagem exibida como pergunta.</para>
        /// </param>
        public void Exibir(string pergunta)
        {
            Exibir(pergunta, TempoDeExibicao, null);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="tempoDeExibicao">
        /// <para>Tempo (em milisegundos) de exibição da pergunta. 
        /// Após isso confirmação é fechada.</para>
        /// <para>Use 0 (zero) para ignorar este campo.</para>
        /// </param>
        public void Exibir(double tempoDeExibicao)
        {
            Exibir(Pergunta, tempoDeExibicao, null);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="metodo">
        /// <para>Método que processára o evento disparado quando 
        /// for recebida a resposta do usuário.</para>
        /// </param>
        public void Exibir(ResponderCaixaDeConfirmacao metodo)
        {
            Exibir(Pergunta, TempoDeExibicao, metodo);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="pergunta">
        /// <para>Mensagem exibida como pergunta.</para>
        /// </param>
        /// <param name="tempoDeExibicao">
        /// <para>Tempo (em milisegundos) de exibição da pergunta. 
        /// Após isso confirmação é fechada.</para>
        /// <para>Use 0 (zero) para ignorar este campo.</para>
        /// </param>
        public void Exibir(string pergunta, double tempoDeExibicao)
        {
            Exibir(pergunta, tempoDeExibicao, null);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="pergunta">
        /// <para>Mensagem exibida como pergunta.</para>
        /// </param>
        /// <param name="metodo">
        /// <para>Método que processára o evento disparado quando 
        /// for recebida a resposta do usuário.</para>
        /// </param>
        public void Exibir(string pergunta, ResponderCaixaDeConfirmacao metodo)
        {
            Exibir(pergunta, TempoDeExibicao, metodo);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="tempoDeExibicao">
        /// <para>Tempo (em milisegundos) de exibição da pergunta. 
        /// Após isso confirmação é fechada.</para>
        /// <para>Use 0 (zero) para ignorar este campo.</para>
        /// </param>
        /// <param name="metodo">
        /// <para>Método que processára o evento disparado quando 
        /// for recebida a resposta do usuário.</para>
        /// </param>
        public void Exibir(int tempoDeExibicao, ResponderCaixaDeConfirmacao metodo)
        {
            Exibir(Pergunta, tempoDeExibicao, metodo);
        }

        /// <summary>
        /// <para>Exibe pergunta.</para>
        /// </summary>
        /// <param name="pergunta">
        /// <para>Mensagem exibida como pergunta.</para>
        /// </param>
        /// <param name="tempoDeExibicao">
        /// <para>Tempo (em milisegundos) de exibição da pergunta. 
        /// Após isso confirmação é fechada.</para>
        /// <para>Use 0 (zero) para ignorar este campo.</para>
        /// </param>
        /// <param name="metodo">
        /// <para>Método que processára o evento disparado quando 
        /// for recebida a resposta do usuário.</para>
        /// </param>
        public void Exibir(string pergunta, double tempoDeExibicao, ResponderCaixaDeConfirmacao metodo)
        {
            if (ControleBase == null)
            {
                throw new Exception("É necessário definir um controle base na propriedade ControleBase.");
            }
            else if (AguardandoResposta)
            {
                throw new Exception("A confirmação anterior ainda não foi respondida. Use o método Fechar().");
            }

            MetodoExtraParaEventoQuandoResponder = metodo;

            /////////////////////////////////////
            //Abaixo, apenas estrutura é montada segundo sua hierarquia.

            Grid estruturaGeral = new Grid();

            Image imagem = new Image();
            Grid estruturaDaMensagem = new Grid();

            estruturaGeral.Children.Add(imagem);
            estruturaGeral.Children.Add(estruturaDaMensagem);

            StackPanel painelDaMensagem = new StackPanel();

            estruturaDaMensagem.Children.Add(painelDaMensagem);

            TextBlock mensagem = new TextBlock();
            StackPanel painelDeBotoes = new StackPanel();

            painelDaMensagem.Children.Add(mensagem);
            painelDaMensagem.Children.Add(painelDeBotoes);

            /////////////////////////////////////
            //Abaixo, layout da estrutura.

            estruturaGeral.Background = CorDoFundo;

            if (ImagemDeFundo != null)
            {
                imagem.Source = ImagemDeFundo;
                imagem.Stretch = Stretch.Uniform;
                imagem.HorizontalAlignment = HorizontalAlignment.Left;
                imagem.VerticalAlignment = VerticalAlignment.Top;
            }

            mensagem.Text = pergunta;
            mensagem.TextWrapping = TextWrapping.Wrap;
            mensagem.Margin = new Thickness(20);
            mensagem.FontSize *= 1.3;
            mensagem.Foreground = CorDoTexto;

            painelDeBotoes.Orientation = Orientation.Horizontal;
            painelDeBotoes.HorizontalAlignment = HorizontalAlignment.Center;

            foreach (string nomeDoBotao in Botoes)
            {
                Button botao = new Button();
                botao.Content = nomeDoBotao;
                botao.Height = 25;
                botao.Margin = new Thickness(5);
                botao.Padding = new Thickness(10, 1, 10, 1);
                botao.Click += new RoutedEventHandler(ProcessarEvento_Botao_Click);
                painelDeBotoes.Children.Add(botao);
            }

            /////////////////////////////////////
            
            if (tempoDeExibicao > 0)
            {
                Temporizador.Enabled = false;
                TempoDeExibicao = tempoDeExibicao;                
                Temporizador.Enabled = true;
            }

            if (AntesDeExibirConfirmacao != null)
            {
                AntesDeExibirConfirmacao(this, new EventArgs());
            }

            ControleBase.Children.Add(estruturaGeral);
            painelDeBotoes.Children[IndiceDoBotaoPadrao].Focus();
            Estrutura = estruturaGeral;
        }

        /// <summary>
        /// <para>Fecha a exibição da confirmação.</para>
        /// </summary>
        public void Fechar()
        {
            if (Estrutura != null)
            {
                Estrutura.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(delegate()
                    {
                        (Estrutura.Parent as Panel).Children.Remove(Estrutura);
                        if (DepoisDeFecharConfirmacao != null)
                        {
                            DepoisDeFecharConfirmacao(this, new EventArgs());
                        }
                        Estrutura = null;
                    })
                );
                while (Estrutura != null)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// <para>Método que processa o evento disparado quando o usuário NÃO responde
        /// e o temporizador notifica isso.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Originador do evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informaçõe sobre o evento.</para>
        /// </param>
        private void ProcessarEvento_Temporizador_Elapsed(object sender, ElapsedEventArgs e)
        {
            (sender as Timer).Enabled = false;
            Fechar();
            if (QuandoResponder != null)
            {
                QuandoResponder(this,
                    new EventoCaixaDeConfirmacao(
                        Pergunta,
                        null,
                        -1
                    )
                );
            }
        }

        /// <summary>
        /// <para>Método que processa o evento disparado quando o usuário responde
        /// por clicar em um dos botões.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Originador do evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informaçõe sobre o evento.</para>
        /// </param>
        private void ProcessarEvento_Botao_Click(object sender, RoutedEventArgs e)
        {
            EventoCaixaDeConfirmacao evento = new EventoCaixaDeConfirmacao(
                    Pergunta,
                    (sender as Button).Content.ToString(),
                    Array.IndexOf<string>(Botoes, (sender as Button).Content.ToString())
                );
            if (QuandoResponder != null)
            {
                QuandoResponder(this, evento);
            }
            if (MetodoExtraParaEventoQuandoResponder != null)
            {
                MetodoExtraParaEventoQuandoResponder(this, evento);
                MetodoExtraParaEventoQuandoResponder = null;
            }
            Fechar();
        }
    }

}
