using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Classe que agrupa algoritmos diferentes para calcular a data da Páscoa</para>
    /// <para>O cálculo da data da Páscoa é fundamental na definição do calendário cristão 
    /// desde os primórdios da cristandade, tornando-se definido na Idade Média.</para>
    /// <para>A Páscoa é celebrada no primeiro domingo após a primeira lua cheia que 
    /// ocorre depois do equinócio da Primavera (no hemisfério norte, outono no hemisfério sul), 
    /// ou seja, é equivalente à antiga regra de que seria o primeiro Domingo após 
    /// o 14º dia do mês lunar de Nisan.</para>
    /// <para>Poderá assim ocorrer entre 22 de Março e 25 de Abril.</para>
    /// <para>Para mais informações: http://pt.wikipedia.org/wiki/C%C3%A1lculo_da_P%C3%A1scoa</para>
    /// </summary>
    public static class Pascoa
    {
        /// <summary>
        /// <para>Mensagem padrão quando o algoritmo resulta em erro.</para>
        /// </summary>
        private static string msgErroNoAlgoritmo = "O \"{0}\" falhou ao calcular a data da Páscoa. Faça-nos um favor: Avise isso ao mundo! Visite: http://pt.wikipedia.org/wiki/C%C3%A1lculo_da_P%C3%A1scoa";

        /// <summary>
        /// <para>Calcula a data da Páscoa para uma determinado ano.</para>
        /// <para>Método: Usando Tabela Simples</para>
        /// <para> (1) Divida o ano de interesse por 19. (2) Some 1 ao resto dessa divisão.</para>
        /// <para>Ao número final chamaremos de "X". 
        /// Esse número é o "número dourado" que corresponde a uma 
        /// data específica dada na tabela abaixo.</para>
        /// <para>A Páscoa será celebrada ao domingo 
        /// seguinte à data encontrada na tabela.</para>
        /// <para>Caso a data já seja um domingo, 
        /// a Páscoa é o domingo da semana seguinte.</para>
        /// <code>
        /// X	Data
        /// 1	14 de Abril ou Domingo seguinte
        /// 2	3 de Abril ou Domingo seguinte
        /// 3	23 de Março ou Domingo seguinte
        /// 4	11 de Abril ou Domingo seguinte
        /// 5	31 de Março ou Domingo seguinte
        /// 6	18 de Abril ou Domingo seguinte
        /// 7	8 de Abril ou Domingo seguinte
        /// 8	28 de Março ou Domingo seguinte
        /// 9	16 de Abril ou Domingo seguinte
        /// 10	5 de Abril ou Domingo seguinte
        /// 11	25 de Março ou Domingo seguinte
        /// 12	13 de Abril ou Domingo seguinte
        /// 13	2 de Abril ou Domingo seguinte
        /// 14	22 de Março ou Domingo seguinte
        /// 15	10 de Abril ou Domingo seguinte
        /// 16	30 de Março ou Domingo seguinte
        /// 17	17 de Abril ou Domingo seguinte
        /// 18	7 de Abril ou Domingo seguinte
        /// 19	27 de Março ou Domingo seguinte
        /// </code>
        /// </summary>
        /// <param name="ano"><para>Número do ano</para></param>
        /// <returns><para>Data da páscoa para o ano informado.</para></returns>
        public static DateTime CalcularUsandoTabelaSimples(int ano)
        {

            int x = (ano % 19) + 1;
            DateTime pascoa;

            switch (x)
            {
                case 1: pascoa = new System.DateTime(ano, 4, 14); break;
                case 2: pascoa = new System.DateTime(ano, 4, 3); break;
                case 3: pascoa = new System.DateTime(ano, 3, 23); break;
                case 4: pascoa = new System.DateTime(ano, 4, 11); break;
                case 5: pascoa = new System.DateTime(ano, 3, 31); break;
                case 6: pascoa = new System.DateTime(ano, 4, 18); break;
                case 7: pascoa = new System.DateTime(ano, 4, 8); break;
                case 8: pascoa = new System.DateTime(ano, 3, 28); break;
                case 9: pascoa = new System.DateTime(ano, 4, 16); break;
                case 10: pascoa = new System.DateTime(ano, 4, 5); break;
                case 11: pascoa = new System.DateTime(ano, 3, 25); break;
                case 12: pascoa = new System.DateTime(ano, 4, 13); break;
                case 13: pascoa = new System.DateTime(ano, 4, 2); break;
                case 14: pascoa = new System.DateTime(ano, 3, 22); break;
                case 15: pascoa = new System.DateTime(ano, 4, 10); break;
                case 16: pascoa = new System.DateTime(ano, 3, 30); break;
                case 17: pascoa = new System.DateTime(ano, 4, 17); break;
                case 18: pascoa = new System.DateTime(ano, 4, 7); break;
                case 19: pascoa = new System.DateTime(ano, 3, 27); break;
                default: throw new Exception(string.Format(msgErroNoAlgoritmo, "Algoritmo usando Tabela Simples"));
            }

            int diasParaOProximoDomingo = 7 - (int)pascoa.DayOfWeek;

            pascoa = pascoa.AddDays(diasParaOProximoDomingo);

            return pascoa;
        }


        /// <summary>
        /// Padrão de expressão regular para capturar os anos da lista <see cref="IntervaloDeAnosParaAlgoritmoDeGauss"/>.
        /// </summary>
        private static string padraoRegExpParaCapturaOsAnos = @"[0-9]{4}";

        /// <summary>
        /// <para>Intervalo de anos usados para ajustar o resultado do 
        /// cálculo da páscoa pelo algoritmo de Gauss</para>
        /// <para>O valor para cada item corresponde ao X e Y usado no algoritmo.
        /// O número é composto de 4 dígitos. Os dois primeiros correspondem ao X e
        /// os dois últimos ao Y. Por exemplo, 2303 indica X=23 e Y=3</para>
        /// </summary>
        public enum IntervaloDeAnosParaAlgoritmoDeGauss : int
        {
            /// <summary>
            /// Período de 1582 até 1599.
            /// </summary>
            De1582Ate1599 = 2202,
            /// <summary>
            /// Período de 1600 até 1699.
            /// </summary>
            De1600Ate1699 = 2202,
            /// <summary>
            /// Período de 1700 até 1799.
            /// </summary>
            De1700Ate1799 = 2303,
            /// <summary>
            /// Período de 1800 até 1899.
            /// </summary>
            De1800Ate1899 = 2304,
            /// <summary>
            /// Período de 1900 até 1999.
            /// </summary>
            De1900Ate1999 = 2405,
            /// <summary>
            /// Período de 2000 até 2099.
            /// </summary>
            De2000Ate2099 = 2405,
            /// <summary>
            /// Período de 2100 até 2199.
            /// </summary>
            De2100Ate2199 = 2406,
            /// <summary>
            /// Período de 2200 até 2299.
            /// </summary>
            De2200Ate2299 = 2507
        }

        /// <summary>
        /// <para>Calcula a data da Páscoa para uma determinado ano.</para>
        /// <para>Método: Usando Algoritmo de Gauss</para>
        /// <para>
        /// Para calcular o dia da Páscoa (Domingo), usa-se a fórmula abaixo, 
        /// onde o "ANO" deve ser introduzido com 4 dígitos. 
        /// O operador MOD é o resto da divisão.</para>
        /// <code>
        /// a=MOD(ANO;19)
        /// b=MOD(ANO;4)
        /// c=MOD(ANO;7)
        /// d=MOD((19*a)+X;30)
        /// e=MOD(((2*b)+(4*c)+(6*d)+Y);7)
        /// se: (d+e)&lt;10 então o dia = (d+e+22) e mês=Março
        /// senão: dia=(d+e-9) e mês=Abril
        /// </code>
        /// <para>A fórmula vale para anos entre 1901 e 2099.
        /// A fórmula pode ser estendida para outros anos, 
        /// alterando X e Y conforme a tabela a seguir:</para>
        /// <code>
        /// Faixa de anos    X      Y 
        /// 1582 -> 1599     22     2 
        /// 1600 -> 1699     22     2 
        /// 1700 -> 1799     23     3 
        /// 1800 -> 1899     23     4 
        /// 1900 -> 1999     24     5 
        /// 2000 -> 2099     24     5 
        /// 2100 -> 2199     24     6 
        /// 2200 -> 2299     25     7 
        /// </code>
        /// <para>Exceções:
        /// # Quando o domingo de Páscoa calculado for em 26 de Abril,
        /// corrige-se para uma semana antes, ou seja, 19 de Abril.
        /// # Quando o domingo de Páscoa calculado for em 25 de Abril e d=28 e a&gt;10,
        /// então a Páscoa é em 18 de Abril.</para>
        /// </summary>
        /// <param name="ano"><para>Número do ano</para></param>
        /// <param name="intervaloDeAnosPretendido"><para>Intervalo de anos pretendido para o cálculo.</para></param>
        /// <returns><para>Data da páscoa para o ano informado.</para></returns>
        public static DateTime CalcularUsandoAlgoritmoDeGauss(int ano, IntervaloDeAnosParaAlgoritmoDeGauss intervaloDeAnosPretendido)
        {
            int anoInicial = int.Parse(Regex.Matches(intervaloDeAnosPretendido.ToString(), padraoRegExpParaCapturaOsAnos)[0].Value);
            int anoFinal = int.Parse(Regex.Matches(intervaloDeAnosPretendido.ToString(), padraoRegExpParaCapturaOsAnos)[1].Value);
            if (!(anoInicial <= ano && ano <= anoFinal))
            {
                throw new NotImplementedException(string.Format("O ano {0} informado para o algoritmo de Gauss calcular a Páscoa não corresponde ao intervalo informado {1}.", ano, intervaloDeAnosPretendido));
            }

            DateTime pascoa;

            int x = int.Parse(((int)intervaloDeAnosPretendido).ToString().Substring(0, 2));
            int y = int.Parse(((int)intervaloDeAnosPretendido).ToString().Substring(2));

            int a = ano % 19;
            int b = ano % 4;
            int c = ano % 7;
            int d = ((19 * a) + x) % 30;
            int e = (((2 * b) + (4 * c) + (6 * d) + y)) % 7;
            if ((d + e) < 10)
            {
                pascoa = new DateTime(ano, 3, d + e + 22);
            }
            else
            {
                pascoa = new DateTime(ano, 4, d + e - 9);
            }

            //Exceções
            if (pascoa.Month == 4 && pascoa.Day == 26)
            {
                pascoa = pascoa.AddDays(-7);
            }
            else if (pascoa.Month == 4 && pascoa.Day == 25 && d==28 && a <10)
            {
                pascoa = new DateTime(ano, 4, 18);
            }

            return pascoa;
        }
        
        /// <summary>
        /// <para>Calcula a data da Páscoa para uma determinado ano.</para>
        /// <para>Método: Usando Algoritmo de Gauss</para>
        /// <para>
        /// Para calcular o dia da Páscoa (Domingo), usa-se a fórmula abaixo, 
        /// onde o "ANO" deve ser introduzido com 4 dígitos. 
        /// O operador MOD é o resto da divisão.</para>
        /// <code>
        /// a=MOD(ANO;19)
        /// b=MOD(ANO;4)
        /// c=MOD(ANO;7)
        /// d=MOD((19*a)+X;30)
        /// e=MOD(((2*b)+(4*c)+(6*d)+Y);7)
        /// se: (d+e)&lt;10 então o dia = (d+e+22) e mês=Março
        /// senão: dia=(d+e-9) e mês=Abril
        /// </code>
        /// <para>A fórmula vale para anos entre 1901 e 2099.
        /// A fórmula pode ser estendida para outros anos, 
        /// alterando X e Y conforme a tabela a seguir:</para>
        /// <code>
        /// Faixa de anos    X      Y 
        /// 1582 -> 1599     22     2 
        /// 1600 -> 1699     22     2 
        /// 1700 -> 1799     23     3 
        /// 1800 -> 1899     23     4 
        /// 1900 -> 1999     24     5 
        /// 2000 -> 2099     24     5 
        /// 2100 -> 2199     24     6 
        /// 2200 -> 2299     25     7 
        /// </code>
        /// <para>Exceções:
        /// # Quando o domingo de Páscoa calculado for em 26 de Abril,
        /// corrige-se para uma semana antes, ou seja, 19 de Abril.
        /// # Quando o domingo de Páscoa calculado for em 25 de Abril e d=28 e a&gt;10,
        /// então a Páscoa é em 18 de Abril.</para>
        /// </summary>
        /// <param name="ano"><para>Número do ano</para></param>
        /// <returns><para>Data da páscoa para o ano informado.</para></returns>
        public static DateTime CalcularUsandoAlgoritmoDeGauss(int ano)
        {
            foreach (string intervalo in Enum.GetNames(typeof(IntervaloDeAnosParaAlgoritmoDeGauss)))
            {                
                int anoInicial = int.Parse(Regex.Matches(intervalo, padraoRegExpParaCapturaOsAnos)[0].Value);
                int anoFinal = int.Parse(Regex.Matches(intervalo, padraoRegExpParaCapturaOsAnos)[1].Value);
                if (anoInicial <= ano && ano <= anoFinal)
                {
                    return CalcularUsandoAlgoritmoDeGauss(ano, (IntervaloDeAnosParaAlgoritmoDeGauss)Enum.Parse(typeof(IntervaloDeAnosParaAlgoritmoDeGauss), intervalo));
                }
            }
            throw new NotImplementedException(string.Format("O algoritmo de Gauss para cálculo da Páscoa não contempla o ano {0}.", ano));
        }

        
        /// <summary>
        /// <para>Calcula a data da Páscoa para uma determinado ano.</para>
        /// <para>Método: Usando o Algoritmo de Meeus/Jones/Butcher</para>
        /// <para>
        /// Este algoritmo é válido para o calendário gregoriano (atualmente usado)
        /// exposto por Jean Meeus em Astronomical Algorithms (1991).</para>
        /// <code>
        /// a = MOD(ANO;19)
        /// b = ANO \ 100
        /// c = MOD(ANO;100)
        /// d = b \ 4
        /// e = MOD(b;4)
        /// f = (b + 8) \ 25
        /// g = (b - f + 1) \ 3
        /// h = MOD((19 × a + b - d - g + 15);30)
        /// i = c \ 4
        /// k = MOD(c;4)
        /// L = MOD((32 + 2 × e + 2 × i - h - k);7)
        /// m = (a + 11 × h + 22 × L) \ 451
        /// MÊS = (h + L - 7 × m + 114) \ 31
        /// DIA = MOD((h + L - 7 × m + 114);31) + 1
        /// </code>
        /// <para>Obs.: O sinal "\" refere-se à divisão obtendo o inteiro, 
        /// por exemplo: 7\3 é igual a 2 e não a 2,333</para>
        /// </summary>
        /// <param name="ano"><para>Número do ano</para></param>
        /// <returns><para>Data da páscoa para o ano informado.</para></returns>
        public static DateTime CalcularUsandoAlgoritmoDeJeanMeeus(int ano)
        {
            int a = ano % 19;
            int b = ano / 100;
            int c = ano % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = ((19 * a + b - d - g + 15)) % 30;
            int i = c / 4;
            int k = c % 4;
            int L = ((32 + 2 * e + 2 * i - h - k)) % 7;
            int m = (a + 11 * h + 22 * L) / 451;
            
            int mes = (h + L - 7 * m + 114) / 31;
            int dia = (((h + L - 7 * m + 114)) % 31) + 1;

            return new DateTime(ano, mes, dia);
        }
    }
}
