using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suporte.Texto
{
    /// <summary>
    /// <para>Classe que agrupa funcionalidades para conversão de dados
    /// similar a classe Convert.</para>
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// <para>Divisor entre uma chave e seu valor.</para>
        /// </summary>
        private static readonly string divisorEntreChaveEValor = "=";

        /// <summary>
        /// <para>Divisor entre cada conjunto de chave e valor.</para>
        /// </summary>
        private static readonly string divisorEntreItens = ";";

        /// <summary>
        /// <para>Agrupador de palavras em um texto formatado como CSV.</para>
        /// </summary>
        private static readonly char agrupadorDePalavras = '"';

        /// <summary>
        /// <para>Divisor entre cada campo em um texto formatado como CSV.</para>
        /// </summary>
        private static readonly string divisorEntreCampos = ";";

        /// <summary>
        /// <para>Divisor entre cada linha em um texto formatado como CSV.</para>
        /// </summary>
        private static readonly string divisorEntreLinhas = "\r\n";

        /// <summary>
        /// <para>Retorna uma representação em string de uma lista de dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static string DeLista(IList dados)
        {
            return DeLista(dados, divisorEntreItens);
        }

        /// <summary>
        /// <para>Retorna uma representação em string de uma lista de dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <param name="divisorEntreItens">
        /// <para>Divisor entre cada conjunto de chave e valor.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static string DeLista(IList dados, string divisorEntreItens)
        {
            StringBuilder resultado = new StringBuilder();
            if (dados != null)
            {
                foreach (object dado in dados)
                {
                    resultado.Append(divisorEntreItens);
                    resultado.Append(dado == null ? "null" : dado);
                }
                if (resultado.Length > 0)
                {
                    resultado.Remove(0, divisorEntreItens.Length);
                }
            }
            return resultado.ToString();
        }
        
        /// <summary>
        /// <para>Retorna uma lista de string a partir de uma representação em string dos dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static IList<string> ParaLista(string dados)
        {
            return ParaLista(dados, divisorEntreItens);
        }

        /// <summary>
        /// <para>Retorna uma lista de string a partir de uma representação em string dos dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <param name="divisorEntreItens">
        /// <para>Divisor entre cada valor.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static IList<string> ParaLista(string dados, string divisorEntreItens)
        {
            List<string> lista = new List<string>();

            if (!string.IsNullOrEmpty(dados))
            {
                StringBuilder dadosEx = new StringBuilder(dados);
                while (dadosEx.Length > 0)
                {
                    string item;
                    int posDivisor = dadosEx.ToString().IndexOf(divisorEntreItens);
                    if (posDivisor < 0)
                    {
                        posDivisor = dadosEx.Length;
                    }
                    item = dadosEx.ToString().Substring(0, posDivisor);
                    dadosEx.Remove(0, item.Length);
                    if (posDivisor >= 0 && dadosEx.Length > 0)
                    {
                        dadosEx.Remove(0, divisorEntreItens.Length);
                    }
                    lista.Add(item);
                }
            }

            return lista;
        }

        /// <summary>
        /// <para>Retorna uma representação em string de um dicionário de dados.</para>
        /// <para>A chave e seu valor estará separado por '='.
        /// E cada item será separado por ';'.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static string DeDicionario(IDictionary<string, string> dados)
        {
            return DeDicionario(dados, divisorEntreChaveEValor, divisorEntreItens);
        }

        /// <summary>
        /// <para>Retorna uma representação em string de um dicionário de dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <param name="divisorEntreChaveEValor">
        /// <para>Divisor entre uma chave e seu valor.</para>
        /// </param>
        /// <param name="divisorEntreItens">
        /// <para>Divisor entre cada conjunto de chave e valor.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static string DeDicionario(IDictionary<string, string> dados, string divisorEntreChaveEValor, string divisorEntreItens)
        {
            StringBuilder resultado = new StringBuilder();

            foreach (KeyValuePair<string, string> item in dados)
            {
                resultado.Append(divisorEntreItens);
                resultado.Append(item.Key);
                resultado.Append(divisorEntreChaveEValor);
                resultado.Append(item.Value);
            }
            if (resultado.Length > 0)
            {
                resultado.Remove(0, divisorEntreItens.Length);
            }
            return resultado.ToString();
        }

        /// <summary>
        /// <para>Retorna um texto formatado como planilha CSV com base numa lista de dados.</para>
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <returns>Texto formatado como planilha CSV</returns>
        public static string DeListaComoTabela(IList lista)
        {
            List<string> campos = new List<string>();
            if (lista.Count > 0)
            {
                foreach (PropertyInfo property in lista[0].GetType().GetProperties())
                {
                    campos.Add(property.Name);
                }
            }
            return DeListaComoTabela(lista, campos.ToArray());
        }

        /// <summary>
        /// <para>Retorna um texto formatado como planilha CSV com base numa lista de dados.</para>
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <param name="campos">Campos da lista usados na conversão.</param>
        /// <returns>Texto formatado como planilha CSV</returns>
        public static string DeListaComoTabela(IList lista, params string[] campos)
        {
            return DeListaComoTabela(lista, agrupadorDePalavras, divisorEntreCampos, divisorEntreLinhas, campos);
        }

        /// <summary>
        /// <para>Retorna um texto formatado como planilha CSV com base numa lista de dados.</para>
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <param name="agrupadorDePalavras">Agrupador de palavras</param>
        /// <param name="divisorEntreCampos">Divisor entre cada campo.</param>
        /// <param name="divisorEntreLinhas">Divisor entre cada linha.</param>
        /// <param name="campos">Campos da lista usados na conversão.</param>
        /// <returns>Texto formatado como planilha CSV</returns>
        public static string DeListaComoTabela(IList lista, char agrupadorDePalavras, string divisorEntreCampos, string divisorEntreLinhas, params string[] campos)
        {
            StringBuilder texto = new StringBuilder();

            for (int i = 0; i < campos.Length; i++)
            {
                if (i > 0)
                {
                    texto.Append(divisorEntreCampos);
                }
                texto.Append(campos[i]);
            }
            texto.Append(divisorEntreLinhas);                        
            foreach (object item in lista)
            {
                for (int i = 0; i < campos.Length; i++)
                {
                    if (i > 0)
                    {
                        texto.Append(divisorEntreCampos);
                    }
                    string valor = Convert.ToString(item.GetType().GetProperty(campos[i]).GetValue(item, null));
                    if (valor == null)
                    {
                        valor = string.Empty;
                    }
                    texto.Append(agrupadorDePalavras);
                    texto.Append(valor.Replace(agrupadorDePalavras.ToString(), agrupadorDePalavras.ToString() + agrupadorDePalavras.ToString()));
                    texto.Append(agrupadorDePalavras);
                }
                texto.Append(divisorEntreLinhas);
            }
            return texto.ToString();
        }

        /// <summary>
        /// <para>Retorna um dicionário a partir de uma representação em string dos dados.</para>
        /// <para>A chave e seu valor estará separado por '='.
        /// E cada item será separado por ';'.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static IDictionary<string, string> ParaDicionario(string dados)
        {
            return ParaDicionario(dados, divisorEntreChaveEValor, divisorEntreItens);
        }

        /// <summary>
        /// <para>Retorna um dicionário a partir de uma representação em string dos dados.</para>
        /// </summary>
        /// <param name="dados">
        /// <para>Dados.</para>
        /// </param>
        /// <param name="divisorEntreChaveEValor">
        /// <para>Divisor entre uma chave e seu valor.</para>
        /// </param>
        /// <param name="divisorEntreItens">
        /// <para>Divisor entre cada conjunto de chave e valor.</para>
        /// </param>
        /// <returns>
        /// <para>Representação dos dados em tipo string.</para>
        /// </returns>
        public static IDictionary<string, string> ParaDicionario(string dados, string divisorEntreChaveEValor, string divisorEntreItens)
        {
            IDictionary<string, string> resultado = new Dictionary<string, string>();

            IList<string> itens = ParaLista(dados, divisorEntreItens);
            foreach (string item in itens)
            {
                string chave = item.Substring(0, item.IndexOf(divisorEntreChaveEValor));
                string valor = item.Substring(item.IndexOf(divisorEntreChaveEValor) + divisorEntreChaveEValor.Length);
                resultado.Add(chave, valor);
            }
            return resultado;
        }

        /// <summary>
        /// <para>Converte um valor tipo <see cref="bool"/> para <c>"Sim"</c> ou <c>"Não"</c>.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor.</para>
        /// </param>
        /// <returns>
        /// <para>Sim ou Não.</para>
        /// </returns>
        public static string ParaSimNao(bool? valor)
        {
            return ParaSimNao(valor, "Sim", "Não");
        }

        /// <summary>
        /// <para>Converte um valor tipo <see cref="bool"/> para <c>"Sim"</c> ou <c>"Não"</c>.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor.</para>
        /// </param>
        /// <param name="sim">
        /// <para>Palavra para <c>"Sim"</c>.</para>
        /// </param>
        /// <param name="nao">
        /// <para>Palavra para <c>"Não"</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Sim ou Não.</para>
        /// </returns>
        public static string ParaSimNao(bool? valor, string sim, string nao)
        {
            return valor == null ? string.Empty : valor.Value ? sim : nao;
        }
    }
}
