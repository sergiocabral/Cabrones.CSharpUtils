using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Classe capaz de converter sequências de textos em registros de uma tabela.</para>
    /// <para>Implementa a interface <see cref="ITabela"/> para disponibilizar seus dados.</para>
    /// </summary>
    public class TextoParaTabela : ITabela
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public TextoParaTabela()
        {
            Dados = new DataTable();
            SeparadorDeCampos = ";";
        }

        #region ITabela Members

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Dados da tabela.</para>
        /// </summary>
        public DataTable Dados
        {
            get;
            private set;
        }

        #endregion

        #region Public

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Dados da tabela em formato texto.</para>
        /// </summary>
        public IList<string> DadosComoTexto
        {
            get
            {
                List<string> dados = new List<string>();
                foreach (DataRow row in Dados.Rows)
                {
                    dados.Add(CompactaValoresDosCampos(row));
                }
                return dados;
            }
        }

        private string separadorDeCampos;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Sequência de texto que indica a separação entre os campos.</para>
        /// <para>Aplicável aos parâmetros dos métodos nomeados como <c>dadosComoTexto</c>.</para>
        /// </summary>
        public string SeparadorDeCampos
        {
            get
            {
                return separadorDeCampos;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("É obrigatório informar o valor para a sequência de texto que separa os valores entre os campos.");
                }
                separadorDeCampos = value;
            }
        }

        /// <summary>
        /// <para>Adiciona registros em formato texto.</para>
        /// </summary>
        /// <param name="registroComoTexto">Registro em formato texto.</param>
        public void AdicionarRegistros(string[] registroComoTexto)
        {            
            foreach (string dado in registroComoTexto)
            {
                AdicionarRegistro(dado);
            }
        }

        /// <summary>
        /// <para>Adiciona um registro em formato texto.</para>
        /// </summary>
        /// <param name="registroComoTexto">Registro em formato texto.</param>
        public void AdicionarRegistro(string registroComoTexto)
        {
            List<string> campos = ExtrairValoresDosCampos(registroComoTexto);
            while (Dados.Columns.Count < campos.Count)
            {
                Dados.Columns.Add();

            }
            Dados.Rows.Add(campos.ToArray());
        }

        /// <summary>
        /// <para>Remove um registro informando-o em formato texto.</para>
        /// </summary>
        /// <param name="registroComoTexto">Registro em formato texto.</param>
        public void RemoverRegistro(string registroComoTexto)
        {
            List<string> valores = ExtrairValoresDosCampos(registroComoTexto);
            for (int r = Dados.Rows.Count - 1; r >= 0; r--)
            {
                bool igual = true;
                for (int c = 0; c < Dados.Columns.Count; c++)
                {
                    if (Dados.Rows[r][c].ToString() != valores[c])
                    {
                        igual = false;
                        break;
                    }
                }
                if (igual)
                {
                    Dados.Rows.RemoveAt(r);
                } 
            }
        }

        /// <summary>
        /// <para>Remove um registro informando-os em formato texto.</para>
        /// </summary>
        /// <param name="registroComoTexto">Registro em formato texto.</param>
        public void RemoverRegistros(string[] registroComoTexto)
        {
            foreach (string dado in registroComoTexto)
            {
                RemoverRegistro(dado);
            }
        }

        /// <summary>
        /// <para>Remove um registro pela posição ocupada.</para>
        /// </summary>
        /// <param name="posicao">índice de base zero indicando a posição.</param>
        public void RemoverRegistro(int posicao)
        {
            Dados.Rows.RemoveAt(posicao);
        }

        /// <summary>
        /// <para>Limpa todos os registros.</para>
        /// </summary>
        public void Limpar()
        {
            Dados.Clear();
            Dados.Columns.Clear();
        }

        /// <summary>
        /// <para>Localiza um registro.</para>
        /// </summary>
        /// <param name="coluna">Coluna a ser usada na pesquisa.</param>
        /// <param name="valor">Valor pesquisado correspondente à coluna informada.</param>
        /// <returns>Array com as posições (índice de base zero) do registros localizados.</returns>
        public int[] LocalizarRegistros(int coluna, string valor)
        {
            return LocalizarRegistros(new int[] { coluna }, new string[] { valor });
        }

        /// <summary>
        /// <para>Localiza um registro.</para>
        /// </summary>
        /// <param name="colunas">Colunas a serem usadas na pesquisa.</param>
        /// <param name="valores">Valores pesquisados correspondentes às colunas informadas.</param>
        /// <returns>Array com as posições (índice de base zero) do registros localizados.</returns>
        public int[] LocalizarRegistros(int[] colunas, string[] valores)
        {
            List<int> localizados = new List<int>();

            for (int r = 0; r < Dados.Rows.Count; r++)
            {
                bool igual = true;
                for (int c = 0; c < colunas.Length; c++)
                {
                    if (Dados.Rows[r][colunas[c]].ToString() != valores[c])
                    {
                        igual = false;
                        break;
                    }
                }
                if (igual)
                {
                    localizados.Add(r);
                }
            }

            return localizados.ToArray();
        }

        /// <summary>
        /// Extrai os valores dos campos de um registro em formato texto.
        /// </summary>
        /// <param name="registroComoTexto">Registro em formato texto.</param>
        /// <returns>Valores dos campos de um registro em formato texto</returns>
        public List<string> ExtrairValoresDosCampos(string registroComoTexto)
        {
            return new List<string>(registroComoTexto.Split(new string[] { SeparadorDeCampos }, StringSplitOptions.None));
        }

        /// <summary>
        /// Compacta os valores de um registro como um registro em formato texto.
        /// </summary>
        /// <param name="registro">Registro de uma tabela.</param>
        /// <returns>Valores de um registro como um registro em formato texto</returns>
        public string CompactaValoresDosCampos(DataRow registro)
        {
            StringBuilder registroComoTexto = new StringBuilder();
            foreach (DataColumn coluna in registro.Table.Columns)
            {
                registroComoTexto.Append(SeparadorDeCampos);
                registroComoTexto.Append(registro[coluna].ToString());                
            }
            if (registroComoTexto.Length > 0)
            {
                registroComoTexto.Remove(0, SeparadorDeCampos.Length);
            }
            return registroComoTexto.ToString();
        }
        
        #endregion
    }
}
