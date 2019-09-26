using Suporte.IO.PastasEArquivos;
using Suporte.Validador;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Suporte.Relatorio
{
    /// <summary>
    /// <para>Substitui variáveis num código HTML pelos seus valores reais.</para>
    /// </summary>
    public class Html
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public Html()
        {
            UsarCache = true;
            RegexParaLocalizarVariaveis = @"\|.*?\|";
            RegexParaLocalizarParametrosNaVariavel = @"(?<=(,|\|)).*?(?=(,|\|))";
            UsarReferenciaComoUrl = true;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="caminho"><para>Caminho do arquivo que contém o 
        /// código HTML que servirá de base para o relatório final</para></param>
        public Html(string caminho) : this()
        {
            CarregarCodigoHtmlDeArquivo(caminho);            
        }

        /// <summary>
        /// <para>Carrega o código HTML de um arquivo.</para>
        /// </summary>
        /// <param name="caminho"><para>Arquivo que contém o código HTML.</para></param>
        public void CarregarCodigoHtmlDeArquivo(string caminho)
        {
            CodigoHtml = LerArquivo(caminho);
        }

        /// <summary>
        /// <para>Código HTML que servirá de base para o relatório final.</para>
        /// </summary>
        public string CodigoHtml { get; set; }

        /// <summary>
        /// <para>Quando <c>==true</c> não consulta o valor de variáveis
        /// já consultadas.</para>
        /// </summary>
        public bool UsarCache { get; set; }

        /// <summary>
        /// <para>Quando <c>==true</c> (compatível com Chrome e Firefox e com a 
        /// classe <see cref="Suporte.Relatorio.EvoHtmlToPdf.EvoHtmlToPdf"/>) troca referência ao caminho do projeto
        /// deixando com formato <c>file:///c:/</c>. Quando <c>==false</c> (compátivel
        /// com Internet Exploter e com a classe <see cref="Suporte.Relatorio.WnvHtmlToPdf.WnvHtmlToPdf"/>) 
        /// usa o formato comum <c>C:\</c></para>
        /// </summary>
        public bool UsarReferenciaComoUrl { get; set; }

        /// <summary>
        /// <para>Expressão Regular para detectar as variáveis no código HTML.</para>
        /// <para>O padrão é definido para localizar nomes entre barras verticais,
        /// por exemplo: <c>\|.*?\|</c> localiza <c>|NomeDaVariavel|</c></para>
        /// </summary>
        public string RegexParaLocalizarVariaveis { get; set; }

        /// <summary>
        /// <para>Expressão Regular para detectar os parâmetros na variáveis localizada.</para>
        /// <para>O padrão é definido para localizar nomes entre barras verticais,
        /// por exemplo: <c>(?<=(,|\|)).*?(?=(,|\|))</c> 
        /// localiza <c>NomeDaVariavel<\c>, <c>modelo.html</c> e <c>nulo.html</c>
        /// em <c>|NomeDaVariavel,modelo.html,nulo.html|</c></para>
        /// </summary>
        public string RegexParaLocalizarParametrosNaVariavel { get; set; }

        /// <summary>
        /// <para>Carrega as variáveis de um código HTML pelo seus valores reais em 
        /// um objeto instanciado.</para>
        /// </summary>
        /// <param name="dados"><para>Objeto com os dados que serão exibidos no relatório final.</para></param>
        /// <returns>Texto como código HTML do relatório final.</returns>
        public string Montar(object dados)
        {
            return Montar(CodigoHtml, dados);
        }

        #region Funcionalidades de apoio

        /// <summary>
        /// <para>Índice da posição do NOME DA VARIÁVEL na lista de parâmetros.</para>
        /// </summary>
        private const int VARIAVEL = 0;

        /// <summary>
        /// <para>Índice da posição do CAMINHO DO ARQUIVO HTML na lista de parâmetros.</para>
        /// </summary>
        private const int ARQUIVO_HTML = 1;

        /// <summary>
        /// <para>Índice da posição do CAMINHO DO ARQUIVO HTML QUANDO A LISTA É VAZIA na lista de parâmetros.</para>
        /// </summary>
        private const int ARQUIVO_HTML_PARA_VAZIO = 2;

        /// <summary>
        /// <para>Carrega as variáveis de um código HTML pelo seus valores reais em 
        /// um objeto instanciado.</para>
        /// </summary>
        /// <param name="codigoHtml"><para>Código HTML que servirá de base para o relatório final.</para></param>
        /// <param name="dados"><para>Objeto com os dados que serão exibidos no relatório final.</para></param>
        /// <returns>Texto como código HTML do relatório final.</returns>
        private string Montar(string codigoHtml, object dados)
        {
            //Referência para o caminho raiz do projeto.
            string caminhoRaiz = ArquivoUtil.MapPath("~") + "\\";
                
            if (UsarReferenciaComoUrl)
            {
                caminhoRaiz = HttpUtility.UrlEncode("file:///" + (caminhoRaiz.Replace("\\", "/")).Replace("//", "/")).Replace("%3a", ":").Replace("%2f", "/");
            }

            //Substitui referências para o caminho raiz do projeto para seu caminho físico real.
            // Para UsarReferenciaComoUrl==false   ="~/Relatorio/Teste.html"   ==>   ="C:\Caminho\Completo\Relatorio\Teste.html"
            // Para UsarReferenciaComoUrl==true    ="~/Relatorio/Teste.html"   ==>   ="file:///C:/Caminho/Completo/Relatorio/Teste.html"
            codigoHtml = Regex.Replace(codigoHtml, @"(?<=\=\"")\~\/(?=.*?\"")", caminhoRaiz);

            List<string> substituidos = new List<string>();

            //Localiza os nomes das propriedades que devem ser substituídas por valores.
            //Devem estar entre barras verticais.
            //   |NomeDaVariavel|
            foreach (Match match in Regex.Matches(codigoHtml, RegexParaLocalizarVariaveis))
            {
                if (UsarCache)
                {
                    //Faz um controle das propriedades que já foram substituidas para evitar substituições em vão.
                    if (substituidos.Contains(match.Value)) { continue; }
                    substituidos.Add(match.Value);
                }

                string[] parametros = ExtrairParametros(match.Value);
                object valor;
                try
                {
                    valor = Objeto.ObterPropriedade(dados, parametros[VARIAVEL]);
                }
                catch (Exception)
                {
                    continue; //Propriedade não encontrada, ignora.
                }

                if (parametros.Length - 1 == VARIAVEL)
                {   //Substituição simples de valor.                    
                    codigoHtml = codigoHtml.Replace(match.Value, Convert.ToString(valor));
                }
                else
                {   //Substituição por lista de valores
                    string htmlDoArquivo = null;
                    StringBuilder listaHtml = new StringBuilder();
                    int count = 0;

                    if (valor != null)
                    {
                        foreach (object item in (IEnumerable)valor)
                        {
                            count++;
                            if (item == null) { continue; } //Ignora se o valor é ==null
                            if (htmlDoArquivo == null) { htmlDoArquivo = LerArquivo(parametros[ARQUIVO_HTML]); }
                            if (!string.IsNullOrEmpty(htmlDoArquivo))
                            {
                                string htmlFormatado = Montar(htmlDoArquivo, item);
                                htmlFormatado = htmlFormatado.Replace("|count|", count.ToString());
                                listaHtml.Append(htmlFormatado);
                            }
                        }
                    }

                    if (count == 0)
                    {
                        if (parametros.Length - 1 != ARQUIVO_HTML_PARA_VAZIO) { continue; } //Ignora substituição se o valor é vazio e não foi indicado arquivo para valor vazio.
                        htmlDoArquivo = LerArquivo(parametros[ARQUIVO_HTML_PARA_VAZIO]);
                        if (!string.IsNullOrEmpty(htmlDoArquivo))
                        {
                            listaHtml.Append(htmlDoArquivo);
                        }
                    }

                    codigoHtml = codigoHtml.Replace(match.Value, listaHtml.ToString());
                }
            }

            return codigoHtml;
        }

        /// <summary>
        /// <para>Extrai os parâmetros dá variável.</para>
        /// </summary>
        /// <param name="variavel"><para>Contúdo da variável.</para></param>
        /// <returns>Array com os parâmetros encontrados.</returns>
        private string[] ExtrairParametros(string variavel)
        {
            List<string> parametros = new List<string>();
            foreach (Match match in Regex.Matches(variavel, RegexParaLocalizarParametrosNaVariavel, RegexOptions.IgnoreCase))
            {
                parametros.Add(match.Value);   
            }
            return parametros.ToArray();
        }

        /// <summary>
        /// <para>Lê o conteúdo de um arquivo.</para>
        /// <para>Se ocorrer alguma exceção na leitura do arquivo será retornado <see cref="sring.Empty"/>.</para>
        /// </summary>
        /// <param name="caminho">
        /// <para>Caminho do arquivo.</para>
        /// <para>O caminho pode fazer referência ao caminho do projeto por usar: <c>~/</c></para>
        /// </param>
        /// <returns><para>Conteúdo do arquivo.</para></returns>
        private string LerArquivo(string caminho)
        {
            try
            {
                using (StreamReader stream = new StreamReader(ArquivoUtil.MapPath(caminho)))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

    }
}
