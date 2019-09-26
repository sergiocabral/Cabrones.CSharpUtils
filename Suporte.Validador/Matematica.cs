using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades para validar expressões matemáticas</para>
    /// </summary>
    public static class Matematica
    {
        /// <summary>
        /// <para>Calcula um expressão matemática e retorna o seu valor em formato numérico.</para>
        /// </summary>
        /// <param name="expressaoMatematica">
        /// <para>Expressão matemática.</para>
        /// </param>
        /// <returns>
        /// <para>Resultado numérico da expressão matemática.</para>
        /// </returns>
        public static double Calcular(string expressaoMatematica)
        {
            XPathDocument xPathDocument = new XPathDocument(new StringReader("<r/>"));

            StringBuilder expressao = new StringBuilder(new Regex(@"([\+\-\*])").Replace(expressaoMatematica, " ${1} "), expressaoMatematica.Length * 2);
            expressao.Replace("/", " div ");
            expressao.Replace("%", " mod ");
            expressao.Replace("[", "(");
            expressao.Replace("{", "(");
            expressao.Replace("]", ")");
            expressao.Replace("}", ")");

            expressaoMatematica = string.Format("number({0})", expressao.ToString());

            double resultado = (double)xPathDocument.CreateNavigator().Evaluate(expressaoMatematica);

            return resultado;
        }

    }
}
