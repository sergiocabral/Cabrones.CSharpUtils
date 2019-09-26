using System;

namespace Suporte.Controles.WPF
{
    /// <summary>
    /// <para>Representa um método que processa o evento 
    /// quando a <see cref="CaixaDeConfirmacao"/> é respondida.</para>
    /// </summary>
    /// <param name="sender">
    /// <para>Originador do evento.</para>
    /// </param>
    /// <param name="e">
    /// <para>Informacoes do evento.</para>
    /// </param>
    public delegate void ResponderCaixaDeConfirmacao(object sender, EventoCaixaDeConfirmacao e);

    /// <summary>
    /// <para>Armazena informações sobre o evento </para>
    /// </summary>
    public class EventoCaixaDeConfirmacao : EventArgs
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="pergunta">
        /// <para>Mensagem exibida como pergunta.</para>
        /// </param>
        /// <param name="botaoSelecionado">
        /// <para>Nome do botão selecionado.</para>
        /// </param>
        /// <param name="indiceDoBotaoSelecionado">
        /// <para>Índice (base zero) do botão selecionado, segundo sua posição.</para>
        /// </param>
        public EventoCaixaDeConfirmacao(
            string pergunta,
            string botaoSelecionado,
            int indiceDoBotaoSelecionado)
        {
            Pergunta = pergunta;
            BotaoSelecionado = botaoSelecionado;
            IndiceDoBotaoSelecionado = indiceDoBotaoSelecionado;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Mensagem exibida como pergunta.</para>
        /// </summary>
        public string Pergunta
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>Nome do botão selecionado.</para>
        /// </summary>
        public string BotaoSelecionado
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Índice (base zero) do botão selecionado, segundo sua posição.</para>
        /// </summary>
        public int IndiceDoBotaoSelecionado
        {
            get;
            private set;
        }
    }
}
