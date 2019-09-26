using System;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Agrupa atributos que armazenam as informações necessário
    /// para realizar uma conexão com banco de dados.</para>
    /// </summary>
    public class InformacoesParaConexao : IInformacoesParaConexao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public InformacoesParaConexao()
        {
            InibirDisparoDeEventos = false;
        }

        private string nomeDeUsuario;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome do usuário.</para>
        /// </summary>
        public string NomeDeUsuario
        {
            get
            {
                return nomeDeUsuario;
            }
            set
            {
                nomeDeUsuario = value;
                DispararEventoQuandoAlterar();
            }
        }

        private string senhaDoUsuario;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Senha do usuário.</para>
        /// </summary>
        public string SenhaDoUsuario
        {
            get
            {
                return senhaDoUsuario;
            }
            set
            {
                senhaDoUsuario = value;
                DispararEventoQuandoAlterar();
            }
        }

        private string endereco;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Endereço do servidor.</para>
        /// </summary>
        public string Endereco
        {
            get
            {
                return endereco;
            }
            set
            {
                endereco = value;
                DispararEventoQuandoAlterar();
            }
        }

        private string fonteDeDados;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome da fonte dos dados que serão utilizados.</para>
        /// </summary>
        public string FonteDeDados
        {
            get
            {
                return fonteDeDados;
            }
            set
            {
                fonteDeDados = value;
                DispararEventoQuandoAlterar();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Quando igual a <c>true</c> o evento <see cref="QuandoAlterar"/>
        /// não é disparado.</para>
        /// </summary>
        public bool InibirDisparoDeEventos { get; set; }

        /// <summary>
        /// <para>Dispara o evento <see cref="QuandoAlterar"/>.</para>
        /// </summary>
        private void DispararEventoQuandoAlterar()
        {
            if (!InibirDisparoDeEventos && QuandoAlterar != null)
            {
                QuandoAlterar(this, new EventArgs());
            }
        }

        /// <summary>
        /// <para>Evento disparado quando alguma informações desta classe é alterada.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoAlterar;
    }
}
