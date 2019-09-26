using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades para validar expressões 
    /// que resultam em objetos.</para>
    /// </summary>
    public static class Objeto
    {
        /// <summary>
        /// <para>Obtem o valor de uma propriedade de um objeto.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Instância do objeto.</para>
        /// </param>
        /// <param name="propriedade">
        /// <para>String contendo o caminho da propriedade do objeto.</para>
        /// <para>É possível referenciar propriedades aninhadas, 
        /// por exemplo: <c>"Pessoa.Enderecos[2].Cidade"</c></para>
        /// </param>
        /// <returns>
        /// <para>Valor da propriedade.</para>
        /// </returns>
        public static object ObterPropriedade(object objeto, string propriedade)
        {
            return ObterDefinirPropriedade(objeto, propriedade, null);
        }

        /// <summary>
        /// <para>Define o valor de uma propriedade de um objeto.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Instância do objeto.</para>
        /// </param>
        /// <param name="propriedade">
        /// <para>String contendo o caminho da propriedade do objeto.</para>
        /// <para>É possível referenciar propriedades aninhadas, 
        /// por exemplo: <c>"Pessoa.Enderecos[2].Cidade"</c></para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor que será atribuido à propriedade.</para>
        /// </param>
        private static void DefinirPropriedade(object objeto, string propriedade, object valor)
        { 
            ObterDefinirPropriedade(objeto, propriedade, valor);
        }

        /// <summary>
        /// <para>Obtem ou define o valor de uma propriedade de um objeto.</para>
        /// </summary>
        /// <param name="objeto">
        /// <para>Instância do objeto.</para>
        /// </param>
        /// <param name="propriedade">
        /// <para>String contendo o caminho da propriedade do objeto.</para>
        /// <para>É possível referenciar propriedades aninhadas, 
        /// por exemplo: <c>"Pessoa.Enderecos[2].Cidade"</c></para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor que será atribuido à propriedade.</para>
        /// <para>Este valor somente será considerado se for diferente de <c>null</c>.</para>
        /// </param>
        /// <returns>
        /// <para>Valor da propriedade.</para>
        /// <para>Caso o parâmetro <paramref name="valor"/> seja informado,
        /// será retornado o valor antes da atribuição.</para>
        /// </returns>
        private static object ObterDefinirPropriedade(object objeto, string propriedade, object valor)
        {
            MatchCollection matchCollection = Regex.Matches(propriedade, @"[^\.\[\]]*");

            //Alguns matches vem em branco. Não sendo possível saber qual é o primeiro match ou o total de maches reais.
            //matchCount e matchIdx auxiliará nisso.
            //Melhor seria que esse trecho não existisse.
            int matchCount = 0;
            int matchIdx = -1;
            for (int i = 0; i < matchCollection.Count; i++)
            {
                if (!string.IsNullOrEmpty(matchCollection[i].Value))
                {
                    if (matchIdx < 0)
                    {
                        matchIdx = i;
                    }
                    matchCount++;
                }
            }

            //Remove o match que será tratado.
            propriedade = propriedade.Remove(propriedade.IndexOf(matchCollection[matchIdx].Value), matchCollection[matchIdx].Value.Length);

            //Valor da propriedade de objBase
            object valorAtual = null;

            //Quando permanece ==false indica que a propriedade não existe.
            bool valorDefindo = false;

            //Se >=0 indica que implementa IEnumerable.
            int listaIndex;
            try
            {
                listaIndex = int.Parse(matchCollection[matchIdx].Value);
                foreach (object obj in (IEnumerable)objeto)
                {
                    if (listaIndex == 0)
                    {
                        valorAtual = obj;
                        valorDefindo = true;
                        break;
                    }
                    listaIndex--;
                }
            }
            catch (FormatException)
            {
                listaIndex = -1;
                foreach (PropertyInfo propInfo in objeto.GetType().GetProperties())
                {
                    if (propInfo.Name == matchCollection[matchIdx].Value)
                    {
                        valorAtual = propInfo.GetValue(objeto, null);
                        valorDefindo = true;
                        break;
                    }
                }
            }

            if (!valorDefindo)
            {
                if (listaIndex < 0)
                {
                    throw new ArgumentNullException(string.Format("\"{0}\" não pertence ao objeto.", matchCollection[matchIdx].Value));
                }
                else
                {
                    throw new ArgumentNullException(string.Format("Índice {0} fora do range.", matchCollection[matchIdx].Value));
                }
            }

            if (matchCount > 1)
            {
                if (valorAtual == null)
                {
                    throw new ArgumentNullException(string.Format("\"{0}\"", matchCollection[matchIdx].Value));
                }
                valorAtual = ObterDefinirPropriedade(valorAtual, propriedade, valor);
            }

            return valorAtual;
        }

        /// <summary>
        /// Operações disponíveis para realizar com as propriedades da uma lista.
        /// </summary>
        public enum OperacaoEmPropriedadeDeLista
        {
            /// <summary>
            /// <para>Somar</para>
            /// </summary>
            Somar,

            /// <summary>
            /// <para>Multiplicar</para>
            /// </summary>
            Multiplicar,

            /// <summary>
            /// <para>Concatenar</para>
            /// </summary>
            Concatenar
        }

        /// <summary>
        /// <para>Para cada item na lista, realiza uma operação com o valor da
        /// propriedade informada.</para>
        /// <para>Nenhuma exceção é gerada por este método</para>
        /// </summary>
        /// <param name="lista"><para>Lista.</para></param>
        /// <param name="propriedade"><para>Nome da propriedade dos itens da lista.</para></param>
        /// <param name="operacao"><para>Operação que será realizada.</para></param>
        /// <returns>
        /// <para>Resultado final da operação realizada.</para>
        /// <para>O resultado pode ser do tipo <see cref="String"/> ou <see cref="Double"/>.</para>
        /// </returns>
        public static object RealizarOperacaoComPropriedadeEmLista(IEnumerable lista, string propriedade, OperacaoEmPropriedadeDeLista operacao)
        {
            object result = null;

            foreach (object item in lista)
            {
                if (item != null)
                {
                    PropertyInfo prop = item.GetType().GetProperty(propriedade);
                    if (prop != null)
                    {
                        try
                        {
                            object valor = prop.GetValue(item, null);

                            switch (operacao)
                            {
                                case OperacaoEmPropriedadeDeLista.Somar:
                                    result = (result == null ? 0.0 : (double)result) + (double)valor;
                                    break;
                                case OperacaoEmPropriedadeDeLista.Multiplicar:
                                    result = (result == null ? 1.0 : (double)result) * (double)valor;
                                    break;
                                case OperacaoEmPropriedadeDeLista.Concatenar:
                                    result = (result == null ? string.Empty : (string)result) + (string)valor;
                                    break;
                                default:
                                    throw new NotImplementedException(string.Format("Operação {0} não implementada para este método.", operacao));
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            return result;
        }
    }
}
