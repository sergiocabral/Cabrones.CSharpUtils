using System;
using System.Collections.Generic;
using System.Timers;

namespace Suporte.Texto
{
    /// <summary>
    /// <para>Armazena uma lista de mensagens para exibição sequencial como num carrossel.</para>
    /// <para>Caso muitas mensagens sejam adicionadas elas entram numa fila
    /// de modo que todas sejam exibidas, uma após a outra.</para>
    /// </summary>
    public class CarrosselDeMensagem
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="metodoParaProcessarEventoQuandoExibir">
        /// <para>Delegate para um método que será chamado cada vez que 
        /// uma nova mensagem for enviada para exibição.</para>
        /// </param>
        public CarrosselDeMensagem(EventHandler<EventArgs> metodoParaProcessarEventoQuandoExibir)
        {
            InterromperExibicao = false;
            ExibicaoContinua = false;
            mensagensJaExibidas = new List<string>();
            mensagens = new List<string>();
            Temporizador = new System.Timers.Timer();
            Temporizador.Elapsed += new ElapsedEventHandler(ProcessarEvento_Timer_Elapsed);
            QuandoExibir += metodoParaProcessarEventoQuandoExibir;
            Temporizador.Enabled = true;
        }

        /// <summary>
        /// <para>Evento disparado quando as mensagens são limpas.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoLimpar;

        /// <summary>
        /// <para>Evento disparado quando uma nova mensagem é exibida.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoExibir;

        /// <summary>
        /// <para>Evento disparado quando todas as mensagens terminam de ser exibidas.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoTerminar;

        /// <summary>
        /// <para>(Leitura Protegida)</para>
        /// <para>Timer que controla o ciclo de exibição das mensagens.</para>
        /// </summary>
        protected Timer Temporizador
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tempo (em milisegundos) entre as exibições das mensagens.</para>
        /// </summary>
        public double Intervalo
        {
            get
            {
                return Temporizador.Interval;
            }
            set
            {
                Temporizador.Interval = value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c>, ao terminar de exibir a lista de mensagens,
        /// repete a lista e exibe novamente.</para>
        /// </summary>
        public bool ExibicaoContinua
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Laitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> dá uma pausainterrompe as exibições
        /// das mensagn</para>
        /// </summary>
        public bool InterromperExibicao
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Mensagem atualmente exibida.</para>
        /// </summary>
        public string MensagemAtual
        {
            get
            {
                try
                {
                    return mensagensJaExibidas[mensagensJaExibidas.Count - 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// <para>Atributo que serve de fonte para a 
        /// propriedade <see cref="Mensagens"/>.</para>
        /// </summary>
        protected IList<string> mensagens;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Fila com as mensagens que restam ser exibidas.</para>
        /// <para></para>
        /// </summary>
        public IList<string> Mensagens
        {
            get
            {
                return new List<string>(mensagens);
            }
        }

        /// <summary>
        /// <para>Atributo que serve de fonte para a 
        /// propriedade <see cref="MensagensJaExibidas"/>.</para>
        /// </summary>
        protected IList<string> mensagensJaExibidas;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Fila com as mensagens que já foram exibidas.</para>
        /// </summary>
        public IList<string> MensagensJaExibidas
        {
            get
            {
                return new List<string>(mensagensJaExibidas);
            }
        }

        private IList<KeyValuePair<DateTime, string>> historico = new List<KeyValuePair<DateTime, string>>();
        /// <summary>
        /// <para>Histórico com data e hora de todas as mensagens exibidas.</para>
        /// </summary>
        public IList<KeyValuePair<DateTime, string>> Historico
        {
            get
            {
                return new List<KeyValuePair<DateTime, string>>(historico);
            }
        }

        /// <summary>
        /// <para>Adiciona uma mensagem nafila de exibição.</para>
        /// </summary>
        /// <param name="mensagem">
        /// <para>Mensagem para exibição.</para>
        /// </param>
        public void Adicionar(string mensagem)
        {
            mensagens.Add(mensagem);
            historico.Add(new KeyValuePair<DateTime, string>(DateTime.Now, mensagem));
            if (mensagensJaExibidas.Count == 0)
            {
                GirarCarrosselDeMensagem();
            }
        }

        /// <summary>
        /// <para>Limpar todas as mensagens.</para>
        /// </summary>
        public void Limpar()
        {
            historico.Clear();
            mensagensJaExibidas.Clear();
            mensagens.Clear();
            if (QuandoLimpar != null)
            {
                QuandoLimpar(this, new EventArgs());
            }
        }

        /// <summary>
        /// <para>Exibe novamente a mensagem atual.</para>
        /// </summary>
        public void ReexibirMensagemAtual()
        {
            if (QuandoExibir != null)
            {
                QuandoExibir(this, new EventArgs());
            }

        }

        /// <summary>
        /// <para>Faz com que a mensagem atual seja arquivada na 
        /// propriedade <see cref="MensagensJaExibidas"/> e obtem 
        /// a próxima mensagem da propriedade <see cref="Mensagens"/>.</para>
        /// </summary>
        private void GirarCarrosselDeMensagem()
        {
            if (mensagens.Count > 0)
            {
                mensagensJaExibidas.Add(mensagens[0]);
                mensagens.RemoveAt(0);
                if (QuandoExibir != null)
                {
                    QuandoExibir(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// <para>Faz com que a mensagem atual seja arquivada na 
        /// propriedade <see cref="MensagensJaExibidas"/> e obtem 
        /// a próxima mensagem da propriedade <see cref="Mensagens"/>
        /// imediatamente, desconsiderando o temporizador.</para>
        /// </summary>
        public void ExibirProximaMensagem()
        {
            GirarCarrosselDeMensagem();
            Temporizador.Enabled = false;
            Temporizador.Enabled = true;
            ProcessarEvento_Timer_Elapsed(Temporizador, null);
        }

        /// <summary>
        /// <para>Faz com que todas as mensagem que restam sejam 
        /// exibidas e arquivadas na propriedade <see cref="MensagensJaExibidas"/>.</para>
        /// </summary>
        public void ExibirTodasAsMensagemRestantes()
        {
            while (Mensagens.Count > 0)
            {
                ExibirProximaMensagem();
            }
        }

        /// <summary>
        /// <para>Faz com que as mensagens já exibidas (propriedade <see cref="MensagensJaExibidas"/>) 
        /// retornem para a lista de mensagens a exibir (propriedade <see cref="Mensagens" />).</para>
        /// <para>Somente surtirá efeito se a lista de mensagems a exibir
        /// estiver vazia.</para>
        /// </summary>
        private void ReiniciarListaDeMensagens()
        {
            if (Mensagens.Count == 0)
            {
                mensagens.Clear();
                ((List<string>)mensagens).AddRange(mensagensJaExibidas);
                mensagensJaExibidas.Clear();
            }
        }

        /// <summary>
        /// <para>Método que atende o evento disparado quando a
        /// propriedade <see cref="Temporizador"/> completa um ciclo de espera.</para>
        /// </summary>
        /// <param name="sender">
        /// <para>Objeto que disparou o evento.</para>
        /// </param>
        /// <param name="e">
        /// <para>Informações sobre o evento.</para>
        /// </param>
        private void ProcessarEvento_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!InterromperExibicao)
            {
                if (mensagens.Count > 0)
                {
                    GirarCarrosselDeMensagem();

                    if (mensagens.Count == 0 && QuandoTerminar != null)
                    {
                        QuandoTerminar(this, new EventArgs());
                    }

                    if (ExibicaoContinua)
                    {
                        ReiniciarListaDeMensagens();
                    }
                }
            }
        }

    }
}
