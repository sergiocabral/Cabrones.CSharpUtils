using System;
using System.Collections.Generic;
using System.Text;

namespace Suporte.Validador
{
    /// <summary>
    /// <para>Classe capaz de converter um número de qualquer base 
    /// numérica para qualquer outra base numérica.</para>
    /// </summary>
    public class ConversorDeBaseNumerica
    {
        #region Public Static

        /// <summary>
        /// <para>Base numérica: Binário: 01</para>
        /// </summary>
        public static readonly string BASE_BINARIO = "01";

        /// <summary>
        /// <para>Base numérica: Octal: 01234567</para>
        /// </summary>
        public static readonly string BASE_OCTAL = "01234567";

        /// <summary>
        /// <para>Base numérica: Decimal: 0123456789</para>
        /// </summary>
        public static readonly string BASE_DECIMAL = "0123456789";

        /// <summary>
        /// <para>Base numérica: Hexadecimal: 0123456789ABCDEF</para>
        /// </summary>
        public static readonly string BASE_HEXADECIMAL = "0123456789ABCDEF";

        /// <summary>
        /// <para>Base numérica: Todos números e letras: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ</para>
        /// </summary>
        public static readonly string BASE_ALFANUMERICO = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// <para>Base numérica: Todas as letras: ABCDEFGHIJKLMNOPQRSTUVWXYZ</para>
        /// </summary>
        public static readonly string BASE_ALFABETO = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// <para>Base numérica: Somente letras consoantes : BCDFGHJKLMNPQRSTVWXYZ</para>
        /// </summary>
        public static readonly string BASE_CONSOANTES = "BCDFGHJKLMNPQRSTVWXYZ";

        /// <summary>
        /// <para>Base numérica: Comente letras vogais: AEIOU</para>
        /// </summary>
        public static readonly string BASE_VOGAIS = "AEIOU";

        /// <summary>
        /// <para>Efetua a conversão de um número numa base numérica qualquer
        /// para outra base numérica qualquer.</para>
        /// </summary>
        /// <param name="numeroDeEntrada"><para>Número de entrada.</para></param>
        /// <param name="baseNumericaDeEntrada"><para>Base numérica de entrada.</para></param>
        /// <param name="baseNumericaDeSaida"><para>Base numérica de saída.</para></param>
        /// <returns><para>Número de saída convertido.</para></returns>
        public static string Converter(string numeroDeEntrada, string baseNumericaDeEntrada, string baseNumericaDeSaida)
        {
            ConversorDeBaseNumerica conv = new ConversorDeBaseNumerica();
            conv.BaseNumericaDeEntrada = baseNumericaDeEntrada;
            conv.BaseNumericaDeSaida = baseNumericaDeSaida;
            conv.NumeroDeEntrada = numeroDeEntrada;
            return conv.NumeroDeSaida;
        }

        /// <summary>
        /// <para>Efetua a conversão de um número decimal
        /// para outra base numérica qualquer.</para>
        /// </summary>
        /// <param name="numeroDecimalDeEntrada"><para>Número de entrada decimal.</para></param>
        /// <param name="baseNumericaDeSaida"><para>Base numérica de saída.</para></param>
        /// <returns><para>Número de saída convertido.</para></returns>
        public static string Converter(long numeroDecimalDeEntrada, string baseNumericaDeSaida)
        {
            return (numeroDecimalDeEntrada < 0 ? "-" : string.Empty) + Converter(Math.Abs(numeroDecimalDeEntrada).ToString(), BASE_DECIMAL, baseNumericaDeSaida);
        }

        /// <summary>
        /// <para>Efetua a conversão de um número numa base numérica qualquer
        /// para a base numérica decimal.</para>
        /// </summary>
        /// <param name="numeroDeEntrada"><para>Número de entrada.</para></param>
        /// <param name="baseNumericaDeEntrada"><para>Base numérica de entrada.</para></param>
        /// <returns><para>Número de saída convertido para base decimal.</para></returns>
        public static long Converter(string numeroDeEntrada, string baseNumericaDeEntrada)
        {
            return long.Parse(Converter(numeroDeEntrada, baseNumericaDeEntrada, BASE_DECIMAL));
        }

        #endregion

        #region Public

        private string baseNumericaDeEntrada;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Base decimal de entrada (Antes do cálculo de conversão).</para>
        /// </summary>
        public string BaseNumericaDeEntrada
        {
            get
            {
                return baseNumericaDeEntrada;
            }
            set
            {
                if (BaseNumericaEValida(value))
                {
                    baseNumericaDeEntrada = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Base numérica de entrada \"{0}\" é inválida.", value));
                }
            }
        }

        private string baseNumericaDeSaida;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Base decimal de saída (Após o cálculo de conversão).</para>
        /// </summary>
        public string BaseNumericaDeSaida
        {
            get
            {
                return baseNumericaDeSaida;
            }
            set
            {
                if (BaseNumericaEValida(value))
                {
                    baseNumericaDeSaida = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Base numérica de saída \"{0}\" é inválida.", value));
                }
            }
        }

        private string numeroDeEntrada;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número de entrada (Antes do cálculo de conversão).</para>
        /// </summary>
        public string NumeroDeEntrada
        {
            get
            {
                return numeroDeEntrada;
            }
            set
            {
                if (NumeroEValidoParaBaseNumerica(BaseNumericaDeEntrada, value))
                {
                    numeroDeEntrada = value;
                }
                else
                {
                    throw new Exception(string.Format("O número de entrada \"{0}\" não é válido para a base numérica de entrada \"{0}\".", value, BaseNumericaDeEntrada));
                }
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Número de saída (Após o cálculo de conversão).</para>
        /// </summary>
        public string NumeroDeSaida
        {
            get
            {
                return ConverterBaseNumericaDecimalParaQualquer(ConverterBaseNumericaQualquerParaDecimal(NumeroDeEntrada, BaseNumericaDeEntrada), BaseNumericaDeSaida);
            }
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        public ConversorDeBaseNumerica()
        {
            baseNumericaDeEntrada = BASE_DECIMAL;
            baseNumericaDeSaida = BASE_DECIMAL;
            numeroDeEntrada = baseNumericaDeEntrada.Substring(0, 1);
        }

        /// <summary>
        /// <para>Verifica se uma base numérica é Válida.</para>
        /// </summary>
        /// <param name="baseNumerica"><para>Base numérica.</para></param>
        /// <returns><para>Quando é válida retorna <c>true</c>, 
        /// se não retorna <c>false</c>.</para></returns>
        public bool BaseNumericaEValida(string baseNumerica)
        {
            for (int i = 0; i < baseNumerica.Length; i++)
                if (baseNumerica.IndexOf(baseNumerica[i]) != baseNumerica.LastIndexOf(baseNumerica[i]))
                    return false;

            return (baseNumerica.Length > 1);
        }

        /// <summary>
        /// <para>Verifica se um número é válido para uma base numérica.</para>
        /// </summary>
        /// <param name="baseNumerica"><para>Base numérica.</para></param>
        /// <param name="numero"><para>Número</para></param>
        /// <returns><para>Quando é válido retorna <c>true</c>, 
        /// se não retorna <c>false</c>.</para></returns>
        public bool NumeroEValidoParaBaseNumerica(string baseNumerica, string numero)
        {
            for (int i = 0; i < numero.Length; i++)
                if (baseNumerica.IndexOf(numero[i]) < 0)
                    return false;

            return (numero.Length > 0);
        }

        #endregion

        #region Private

        /// <summary>
        /// <para>Converte um número na base numérica decimal 
        /// para um outra base numérica qualquer.</para>
        /// </summary>
        /// <param name="numeroDecimal"><para>Número na base numérica decimal.</para></param>
        /// <param name="baseNumericaQualquer"><para>Base numérica qualquer do resultado.</para></param>
        /// <returns><para>Número convertido para a base numérica informada.</para></returns>
        private string ConverterBaseNumericaDecimalParaQualquer(string numeroDecimal, string baseNumericaQualquer)
        {
            if ((!BaseNumericaEValida(baseNumericaQualquer)) || (!NumeroEValidoParaBaseNumerica(BASE_DECIMAL, numeroDecimal))) return "";

            long numero = int.Parse(numeroDecimal);
            string result = "";

            do
            {
                result = Convert.ToString(baseNumericaQualquer[int.Parse(RealizarOperacaoMatematica(Convert.ToString(numero), Convert.ToString(baseNumericaQualquer.Length), OperacoesMatematicas.ExtrairRestoDaDivisao))]) + result;
                numero = (long)double.Parse(RealizarOperacaoMatematica(Convert.ToString(numero), Convert.ToString(baseNumericaQualquer.Length), OperacoesMatematicas.Dividir));
            } while (numero > 0);

            return result;
        }

        /// <summary>
        /// <para>Converte um número numa base numérica qualquer para um
        /// número na base numérica decimal.</para>
        /// </summary>
        /// <param name="numeroEmBaseNumericaQualquer"><para>Número de entrada numa 
        /// base numérica qualquer.</para></param>
        /// <param name="baseNumericaQualquer"><para>Base numérica qualquer do número
        /// de entrada.</para></param>
        /// <returns><para>Número convertido para a base numérica decimal.</para></returns>
        private string ConverterBaseNumericaQualquerParaDecimal(string numeroEmBaseNumericaQualquer, string baseNumericaQualquer)
        {
            if ((!BaseNumericaEValida(baseNumericaQualquer)) || (!NumeroEValidoParaBaseNumerica(baseNumericaQualquer, numeroEmBaseNumericaQualquer))) return "";

            string resultadoParcial;
            string result = "0";

            for (int i = 0; i < numeroEmBaseNumericaQualquer.Length; i++)
            {
                resultadoParcial = Convert.ToString(baseNumericaQualquer.IndexOf(numeroEmBaseNumericaQualquer[i]));
                for (int vInt_2 = 0; vInt_2 < (numeroEmBaseNumericaQualquer.Length - i - 1); vInt_2++)
                {
                    resultadoParcial = RealizarOperacaoMatematica(resultadoParcial, Convert.ToString(baseNumericaQualquer.Length), OperacoesMatematicas.Multiplicar);
                }
                result = RealizarOperacaoMatematica(result, resultadoParcial, OperacoesMatematicas.Somar);
            }

            return Convert.ToString(result);
        }

        /// <summary>
        /// <para>Lista com as operações matemáticas usadas por esta classe.</para>
        /// </summary>
        private enum OperacoesMatematicas
        {
            /// <summary>
            /// <para>Somar.</para>
            /// </summary>
            Somar,

            /// <summary>
            /// <para>Subtrair.</para>
            /// </summary>
            Subtrair,

            /// <summary>
            /// <para>Multiplicar.</para>
            /// </summary>
            Multiplicar,

            /// <summary>
            /// <para>Dividir.</para>
            /// </summary>
            Dividir,

            /// <summary>
            /// <para>Extrair resto da divisão.</para>
            /// </summary>
            ExtrairRestoDaDivisao
        }

        /// <summary>
        /// <para>Realiza uma operação matemática com dois números no tipo <see cref="String"/>.</para>
        /// </summary>
        /// <param name="numero1"><para>Número 1.</para></param>
        /// <param name="numero2"><para>Número 2.</para></param>
        /// <param name="operacao"><para>Operação que será realizada.</para></param>
        /// <returns><para>Resultado da operação.</para></returns>
        private string RealizarOperacaoMatematica(string numero1, string numero2, OperacoesMatematicas operacao)
        {
            double num1 = double.Parse(numero1);
            double num2 = double.Parse(numero2);

            switch (operacao)
            {
                case OperacoesMatematicas.Somar:
                    num1 += num2;
                    break;
                case OperacoesMatematicas.Subtrair:
                    num1 -= num2;
                    break;
                case OperacoesMatematicas.Multiplicar:
                    num1 *= num2;
                    break;
                case OperacoesMatematicas.Dividir:
                    num1 /= num2;
                    break;
                case OperacoesMatematicas.ExtrairRestoDaDivisao:
                    num1 %= num2;
                    break;
                default: throw new NotImplementedException(string.Format("Operação \"{0}\" não implementada por este método.", operacao));
            }


            string result = Convert.ToString(num1);
            if (result.Contains(".") && int.Parse(result.Substring(result.IndexOf(".") + 1)) == 0) result = result.Substring(0, result.Length - 2);
            return (result);
        }

        #endregion

    }
}