using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Disponibiliza funcionalidades para manipulação de uma
    /// string de conexão com o banco de dados.</para>
    /// </summary>
    public class StringDeConexao : IStringDeConexao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public StringDeConexao()
            : this(null) { }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor da string de conexão para inicializar a classe.</para>
        /// </param>
        public StringDeConexao(string valor)
        {
            Atributos = new Dictionary<string, string>();
            Valor = valor;
        }

        /// <summary>
        /// <para>Evento disparado quando alguma informações desta classe é alterada.</para>
        /// </summary>
        public event EventHandler<EventArgs> QuandoAlterar;

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
        /// <para>(Leitura/Escrita Privada)</para>
        /// <para>Lista de atributos que compõe a string de conexão.</para>
        /// </summary>
        protected IDictionary<string, string> Atributos
        {
            get;
            private set;
        }

        #region Override

        /// <summary>
        /// Retorna um string que representa o objeto atual.
        /// </summary>
        /// <returns>
        /// <para>String que representa o objeto atual.</para>
        /// </returns>
        public override string ToString()
        {
            return Valor;
        }

        #endregion

        #region IStringDeConexao Members

        /// <summary>
        /// <para>(Leitura/Escrita Privada)</para>
        /// <para>Lista de atributos que compõe a string de conexão.</para>
        /// </summary>
        public string this[string atributo]
        {
            get
            {
                return Atributos[atributo];
            }
            set
            {
                Atributos[atributo] = value;
                DispararEventoQuandoAlterar();
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Limpar todos os atributos da string de conexão.</para>
        /// </summary>
        public void Limpar()
        {
            Atributos.Clear();
            DispararEventoQuandoAlterar();
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>String de conexão com o banco de dados.</para>
        /// </summary>
        public string Valor
        {
            get
            {
                StringBuilder stringDeConexao = new StringBuilder();
                foreach (KeyValuePair<string, string> valor in Atributos)
                {
                    if (!string.IsNullOrEmpty(valor.Value))
                    {
                        stringDeConexao.Append(valor.Key);
                        stringDeConexao.Append("=");
                        stringDeConexao.Append(valor.Value);
                        stringDeConexao.Append(";");
                    }
                }
                return stringDeConexao.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Atributos.Clear();
                }
                else
                {
                    IDictionary<string, string> stringDeConexao = new Dictionary<string, string>();
                    string[] atributos = value.Split(';');
                    foreach (string atributo in atributos)
                    {
                        if (!string.IsNullOrEmpty(atributo))
                        {
                            string chave = atributo.Substring(0, atributo.IndexOf('='));
                            string valor = atributo.Substring(atributo.IndexOf('=') + 1);
                            stringDeConexao.Add(chave, valor);
                        }
                    }
                    Atributos = stringDeConexao;
                }
                DispararEventoQuandoAlterar();
            }
        }

        #endregion
    }
}
