using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Suporte.Controles.Forms
{
    /// <summary>
    /// <para>Aplica máscaras para campos numéricos, como CPF, CNPJ, Telefone, Data, etc.</para>
    /// <para>Ter funcionalidades tipo <c>static</c> úteis.</para>
    /// </summary>
    public class MascaraParaNumeros
    {
        /// <summary>
        /// <para>Sentidos para preenchimento do valor no controle.</para>
        /// </summary>
        public enum Sentido
        {
            /// <summary>
            /// <para>Da esquerda para direita.</para>
            /// </summary>
            EsquerdaParaDireita,

            /// <summary>
            /// <para>Da direita para esquerda.</para>
            /// </summary>
            DireitaParaEsquerda
        };

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="textBox"><para>Controle configurado para receber a máscara.</para></param>
        public MascaraParaNumeros(TextBox textBox) : this(textBox, string.Empty, Sentido.DireitaParaEsquerda) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="textBox"><para>Controle configurado para receber a máscara.</para></param>
        /// <param name="mascara"><para>Inicialização da máscara.</para></param>
        /// <param name="sentidoDaMascara"><para>Sentido da aplicação da máscara.</para></param>
        public MascaraParaNumeros(TextBox textBox, string mascara, Sentido sentidoDaMascara)
        {
            Controle = textBox;
            Mascara = mascara;
            SentidoDaMascara = sentidoDaMascara;
            Ativar();
        }

        #region Disponíveis na Instância

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Controle configurado para receber a máscara.</para>
        /// </summary>
        public TextBox Controle { get; private set; }

        public string mascara = string.Empty;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Máscara em uso.</para>
        /// </summary>
        public string Mascara
        {
            get
            {
                return mascara + string.Empty;
            }
            set
            {
                if (mascara == value) { return; }
                mascara = value;
                ReaplicarMascara();
            }
        }

        private string prefixo = string.Empty;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Texto SEMPRE exibido no início da máscara.</para>
        /// </summary>
        public string Prefixo
        {
            get
            {
                return prefixo + string.Empty;
            }
            set
            {
                if (prefixo == value) { return; }
                prefixo = value;
                ReaplicarMascara();
            }
        }

        private string sufixo = string.Empty;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Texto SEMPRE exibido no final da máscara.</para>
        /// </summary>
        public string Sufixo
        {
            get
            {
                return sufixo + string.Empty;
            }
            set
            {
                if (sufixo == value) { return; }
                sufixo = value;
                ReaplicarMascara();
            }
        }

        private int comprimentoDeZerosAEsquerdaObrigatorio = 0;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quantidade mínima de zeros exibidos à esquerda do número.</para>
        /// <para>Necessário para valores monetários ou medidas.</para>
        /// </summary>
        public int ComprimentoDeZerosAEsquerdaObrigatorio
        {
            get
            {
                return comprimentoDeZerosAEsquerdaObrigatorio;
            }
            set
            {
                if (comprimentoDeZerosAEsquerdaObrigatorio == value) { return; }
                comprimentoDeZerosAEsquerdaObrigatorio = value;
                ReaplicarMascara();
            }
        }

        /// <summary>
        /// <para>(leitura)</para>
        /// <para>Valor contido no controle somente com números.</para>
        /// </summary>
        public string ValorNumerico
        {
            get
            {
                return ExtrairNumeros(Controle.Text);
            }
        }

        /// <summary>
        /// <para>(leitura)</para>
        /// <para>Valor contido no controle com números e máscara.</para>
        /// </summary>
        public string ValorComMascara
        {
            get
            {
                return Prefixo + Aplicar(Controle.Text, Mascara, SentidoDaMascara) + Sufixo;
            }
        }

        /// <summary>
        /// <para>(leitura)</para>
        /// <para>Verifica se o valor digitado corresponde à máscara completa.</para>
        /// </summary>
        public bool ValorDeAcordoComAMascara
        {
            get
            {
                string valor = Controle.Text;
                string regex = Regex.Escape(Mascara)
                    .Replace('0', '9')
                    .Replace('1', '9')
                    .Replace('2', '9')
                    .Replace('3', '9')
                    .Replace('4', '9')
                    .Replace('5', '9')
                    .Replace('6', '9')
                    .Replace('7', '9')
                    .Replace('8', '9')
                    .Replace("9", "[0-9]");

                return Regex.Replace(valor, regex, string.Empty) == string.Empty;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Sentido da aplicação da máscara.</para>
        /// </summary>
        public Sentido SentidoDaMascara { get; set; }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Indica se a formatação da máscara está ativa.</para>
        /// <para>Para modificar seu estado use <see cref="MascaraParaNumeros.Desativar"/>
        /// ou <see cref="MascaraParaNumeros.Ativar"/></para>
        /// </summary>
        public bool Ativado { get; private set; }

        /// <summary>
        /// <para>Desativa a formatação da máscara no controle configurado.</para>
        /// <para>Cancela os eventos atribuidos
        /// em <see cref="Control.KeyPress"/> e <see cref="Control.TextChanged"/>.</para>
        /// </summary>
        public void Desativar()
        {
            if (Ativado)
            {
                Ativado = false;

                Controle.KeyPress -= new KeyPressEventHandler(Controle_KeyPress);
                Controle.TextChanged -= new EventHandler(Controle_TextChanged);
            }
        }

        /// <summary>
        /// <para>Ativa a formatação da máscara no controle configurado.</para>
        /// <para>Atribui eventos em <see cref="Control.KeyPress"/> e <see cref="Control.TextChanged"/>.</para>
        /// </summary>
        public void Ativar()
        {
            if (!Ativado)
            {
                Ativado = true;

                Controle.KeyPress += new KeyPressEventHandler(Controle_KeyPress);
                Controle.TextChanged += new EventHandler(Controle_TextChanged);
            }
        }

        #endregion

        #region Eventos atribuidos ao controle

        /// <summary>
        /// <para>Quando o texto do controle é alterado.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void Controle_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            textBox.TextChanged -= new EventHandler(Controle_TextChanged);

            try
            {
                int selectionStart = textBox.SelectionStart;
                string valorAtual = textBox.Text;

                if (Prefixo.Length > 0 && valorAtual.IndexOf(Prefixo) == 0)
                {
                    valorAtual = valorAtual.Remove(0, Prefixo.Length);
                    selectionStart -= Prefixo.Length;
                    if (selectionStart < 0)
                    {
                        selectionStart = 0;
                    }
                }

                if (Sufixo.Length > 0 && valorAtual.IndexOf(Sufixo) == valorAtual.Length - Sufixo.Length)
                {
                    valorAtual = valorAtual.Substring(0, valorAtual.Length - Sufixo.Length);
                    if (selectionStart > valorAtual.Length)
                    {
                        selectionStart = valorAtual.Length;
                    }
                }

                valorAtual = IncluirZerosAEsquerda(valorAtual, ComprimentoDeZerosAEsquerdaObrigatorio);                

                string comMascara = Aplicar(valorAtual, Mascara, SentidoDaMascara, ref selectionStart);
                while (selectionStart < comMascara.Length && !char.IsDigit(comMascara[selectionStart]))
                {
                    selectionStart++;
                }

                if (comMascara.Length > 0)
                {
                    comMascara = Prefixo + comMascara + Sufixo;
                }

                textBox.Text = comMascara;
                textBox.SelectionStart = selectionStart + Prefixo.Length;

            }
            finally
            {
                textBox.TextChanged += new EventHandler(Controle_TextChanged);
            }
        }

        /// <summary>
        /// <para>Quando uma tecla é pressionada no controle.</para>
        /// </summary>
        /// <param name="sender"><para>Originador do evento.</para></param>
        /// <param name="e"><para>Informações sobre o evento.</para></param>
        private void Controle_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                if (e.KeyChar == '\b' && textBox.SelectionLength == 0)
                {
                    int selectionStart = textBox.SelectionStart;
                    string text = textBox.Text;

                    while (selectionStart > 0 && !char.IsDigit(text[selectionStart - 1]))
                    {
                        selectionStart--;
                    }

                    textBox.SelectionStart = selectionStart;
                }

                return;
            }

            if (textBox.Text.Length - textBox.SelectionLength >= Prefixo.Length + Mascara.Length + Sufixo.Length)
            {
                e.KeyChar = (char)0;
            }
        }

        #endregion

        #region Disponíveis estaticamente

        /// <summary>
        /// <para>Aplica uma máscara em um valor numerico.</para>
        /// <para>Qualquer caracter não numérico informado é desconsiderado. Logo, este método
        /// pode ser chamado para substituir uma máscara por outra sem que seja necessário
        /// remover a máscara anterior.</para>
        /// </summary>
        /// <param name="numero"><para>Número.</para></param>
        /// <param name="mascara"><para>Máscara.</para></param>
        /// <param name="sentidoDaMascara"><para>Sentido da aplicação da máscara.</para></param>
        /// <returns><para>Número com máscara aplicada.</para></returns>
        public static string Aplicar(string numero, string mascara, Sentido sentidoDaMascara)
        {
            int selectionStart = 0;
            return Aplicar(numero, mascara, sentidoDaMascara, ref selectionStart);
        }

        /// <summary>
        /// <para>Extrai os número de um valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        /// <returns>Somente os números do valor.</returns>
        private static string ExtrairNumeros(string valor)
        {
            int selectionStart = 0;
            return ExtrairNumeros(valor, ref selectionStart);
        }

        #endregion

        #region Suporte interno

        /// <summary>
        /// <para>Reaplica a máscara no controle.</para>
        /// </summary>
        private void ReaplicarMascara()
        {
            bool ativado = Ativado;
            Desativar();
            string valor = IncluirZerosAEsquerda(Controle.Text, ComprimentoDeZerosAEsquerdaObrigatorio);
            valor = Aplicar(valor, Mascara, SentidoDaMascara);
            Controle.Text = valor.Length == 0 ? string.Empty : Prefixo + valor + Sufixo;
            if (ativado)
            {
                Ativar();
            }
        }

        /// <summary>
        /// <para>Aplica uma máscara em um valor numerico.</para>
        /// <para>Qualquer caracter não numérico informado é desconsiderado. Logo, este método
        /// pode ser chamado para substituir uma máscara por outra sem que seja necessário
        /// remover a máscara anterior.</para>
        /// </summary>
        /// <param name="numero"><para>Número.</para></param>
        /// <param name="mascara"><para>Máscara.</para></param>
        /// <param name="sentidoDaMascara"><para>Sentido da aplicação da máscara.</para></param>
        /// <param name="selectionStart">
        /// <para>Posicionamento do cursor.</para>
        /// <para>Embora o retorno no método possa ter comprimento diferente,
        /// a posição do cursor é preservada.</para>
        /// </param>
        /// <returns><para>Número com máscara aplicada.</para></returns>
        private static string Aplicar(string numero, string mascara, Sentido sentidoDaMascara, ref int selectionStart)
        {
            string somenteNumeros = ExtrairNumeros(numero, ref selectionStart);

            if (sentidoDaMascara == Sentido.DireitaParaEsquerda)
            {
                mascara = ReverterString(mascara);
                somenteNumeros = ReverterString(somenteNumeros);
                selectionStart = somenteNumeros.Length - selectionStart;
            }

            string comMascara = somenteNumeros;

            for (int i = 0; i < comMascara.Length; i++)
            {
                if (i >= mascara.Length)
                {
                    comMascara = comMascara.Substring(0, sentidoDaMascara == Sentido.EsquerdaParaDireita ? i : i - 1);
                    break;
                }
                else if (!char.IsDigit(mascara[i]))
                {
                    int j = i;
                    string parteDaMascara = string.Empty;
                    do
                    {
                        parteDaMascara += mascara[j].ToString();
                        j++;
                    } while (j < mascara.Length && !char.IsDigit(mascara[j]));

                    comMascara = comMascara.Insert(i, parteDaMascara);
                    if (i < selectionStart)
                    {
                        selectionStart += parteDaMascara.Length;
                    }
                    i += parteDaMascara.Length;
                }
            }

            if (comMascara.Length > 0)
            {
                string parteFinalDaMascara = string.Empty;
                while (comMascara.Length + parteFinalDaMascara.Length < mascara.Length &&
                       !char.IsDigit(mascara[comMascara.Length + parteFinalDaMascara.Length]) &&
                       (!char.IsWhiteSpace(mascara[comMascara.Length + parteFinalDaMascara.Length]) ||
                       !Regex.IsMatch(mascara.Substring(comMascara.Length), @"[0-9]")))
                {
                    parteFinalDaMascara += mascara[comMascara.Length + parteFinalDaMascara.Length];
                }

                if (comMascara.Length + parteFinalDaMascara.Length < mascara.Length &&
                    char.IsDigit(mascara[comMascara.Length + parteFinalDaMascara.Length]))
                {
                    parteFinalDaMascara = string.Empty;
                }

                if (parteFinalDaMascara.Length > 0)
                {
                    comMascara += parteFinalDaMascara;
                }
            }

            if (sentidoDaMascara == Sentido.DireitaParaEsquerda)
            {
                comMascara = ReverterString(comMascara);
                selectionStart = comMascara.Length - selectionStart;
            }

            return comMascara;
        }

        /// <summary>
        /// <para>Extrai os número de um valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        /// <param name="selectionStart">
        /// <para>Posicionamento do cursor.</para>
        /// <para>Embora o retorno no método possa ter comprimento diferente,
        /// a posição do cursor é preservada.</para>
        /// </param>
        /// <returns>Somente os números do valor.</returns>
        private static string ExtrairNumeros(string valor, ref int selectionStart)
        {
            StringBuilder numeros = new StringBuilder();
            for (int i = 0; i < valor.Length; i++)
            {
                char c = valor[i];
                if (char.IsDigit(c))
                {
                    numeros.Append(c);
                }
                else if (numeros.Length < selectionStart)
                {
                    selectionStart--;
                }
            }
            return numeros.ToString();
        }

        /// <summary>
        /// <para>Reverte um string</para>
        /// </summary>
        /// <param name="valor"><para>String.</para></param>
        /// <returns><para>String revertida.</para></returns>
        private static string ReverterString(string valor)
        {
            char[] reverso = valor.ToCharArray();
            Array.Reverse(reverso);
            return new string(reverso);
        }

        /// <summary>
        /// <para>Inclui os zeros à esquerda de um valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor</para></param>
        /// <param name="zerosAEsquerda"><para>quantidade de zeros à esquerda.</para></param>
        /// <returns><para>Valor ajustado.</para></returns>
        private static string IncluirZerosAEsquerda(string valor, int zerosAEsquerda)
        {
            if (zerosAEsquerda > 0)
            {
                while (valor.Length > 1 && (!char.IsDigit(valor[0]) || valor[0] == '0'))
                {
                    valor = valor.Substring(1);
                }
                int comprimentoAtual = Regex.Replace(valor, @"[^0-9]", string.Empty).Length;
                if (comprimentoAtual < zerosAEsquerda)
                {
                    valor = string.Empty.PadLeft(zerosAEsquerda - comprimentoAtual, '0') + valor;
                }
            }
            return valor;
        }

        #endregion
    }
}
