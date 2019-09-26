using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Web;
using Suporte.Texto.FormatProvider;

namespace Suporte.Texto
{
    /// <summary>
    /// <para>Esta classe disponibiliza funcionalidades que permitem obter
    /// valores únicos dentro de determinadas circunstâncias.</para>
    /// <para>Tais valores podem ser usados para uso em chave primária em tabelas,
    /// ou gravação de arquivos, etc.</para>
    /// </summary>
    public class ValorUnico
    {
        private static int comprimentoValorMinimo = 8;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Retorna o comprimento mínimo permitido (<see cref="string.Length"/>) 
        /// para as sequências de textos geradas por esta classe.</para>
        /// </summary>
        public static int ComprimentoValorMinimo
        {
            get { return ValorUnico.comprimentoValorMinimo; }
        }

        private static int comprimentoValorPadrao = comprimentoValorMinimo;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Comprimento padrão para as sequências de textos (<see cref="string.Length"/>)
        /// geradas por instâncias desta classe.</para>
        /// <para>É importante ressaltar que quanto menor for o comprimento,
        /// mais chance há de se repetir valores.</para>
        /// <para>O valor inicial desta propriedade é o mesmo da
        /// propriedade estática <see cref="ValorUnico.ComprimentoValorMinimo"/>.</para>
        /// </summary>
        public static int ComprimentoValorPadrao
        {
            get { return ValorUnico.comprimentoValorPadrao; }
            set
            {
                if (value < ValorUnico.comprimentoValorMinimo)
                {
                    throw new Exception(string.Format("O comprimento deve ser maior ou igual a {0}.", ValorUnico.comprimentoValorMinimo));
                }
                ValorUnico.comprimentoValorPadrao = value;
            }
        }

        private int comprimento;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Comprimento (<see cref="string.Length"/>) das sequências de textos geradas.</para>
        /// </summary>
        public int Comprimento
        {
            get { return comprimento; }
            set
            {
                if (value < comprimentoValorMinimo)
                {
                    throw new Exception(string.Format("O comprimento deve ser maior ou igual a {0}.", comprimentoValorMinimo));
                }
                comprimento = value;
            }
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// <para>O comprimento (<see cref="string.Length"/>) das sequências de textos
        /// geradas é definido como sendo o mesmo valor da propriedade
        /// estática <see cref="ValorUnico.ComprimentoValorPadrao"/>.</para>
        /// </summary>
        public ValorUnico()
            : this(ValorUnico.ComprimentoValorPadrao)
        {
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="comprimento"><para>Informa o comprimento (<see cref="string.Length"/>) das 
        /// sequências de textos geradas.</para>
        /// <para>É importante ressaltar que quanto menor for o comprimento,
        /// mais chance há de se repetir valores.</para></param>
        public ValorUnico(int comprimento)
        {
            Comprimento = comprimento;
        }

        /// <summary>
        /// <para>Condifica um valor informado como valor único.</para>
        /// </summary>
        /// <param name="valor"><para>Valor de entrada.</para></param>
        /// <returns><para>Valor de entrada já codificado.</para></returns>
        public string Codificar(string valor)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(valor, new byte[] { });
            valor = Convert.ToBase64String(pdb.GetBytes(Comprimento));
            valor = string.Format(new FormatadorApenasAlfaNumerico(), "{0}", valor);
            while (valor.Length < Comprimento)
            {
                valor += valor;
            }
            return valor.ToUpper().Substring(0, Comprimento);
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retorna um valor único, não repetido nunca mais.</para>
        /// <para>Porém, quando esta propriedade é consultada seguidamente numa linha
        /// de código após a outra, pode haver repetição. Se necessário use a propriedade
        /// <see cref="UnicoAbsoluto"/>, mas veja sua descrição pois poderá haver perda
        /// de performance.</para>
        /// </summary>
        public string Unico
        {
            get
            {
                return Codificar((new Random(Environment.TickCount)).NextDouble().ToString() + PorHora + PorData + PorSessao);
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retorna um valor único, não repetido nunca mais.</para>
        /// <para>Para garantir que um valor nunca se repita, a consulta desta
        /// propriedade faz com que seja esperado pela conclusão de um 
        /// ciclo (<see cref="Environment.TickCount"/>) de processamento do 
        /// sistema operacional.</para>
        /// </summary>
        public string UnicoAbsoluto
        {
            get
            {
                int tick = Environment.TickCount;
                while (tick == Environment.TickCount) ;
                return Unico;
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retornará um valor único para cada usuário que
        /// acessar a aplicação. Cada usuário terá sempre o mesmo valor retornado, mas
        /// dois usuários diferentes terão valores retornados diferentes.</para>
        /// </summary>
        public string PorUsuario
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return Codificar(HttpContext.Current.Request.ServerVariables["REMOTE_USER"]);
                }
                else
                {
                    return Codificar(Environment.UserDomainName + "\\" + Environment.UserName);
                }
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retornará um valor único para cada instância
        /// do aplicativo em execução.</para>
        /// </summary>
        public string PorSessao
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Session != null)
                    {
                        return Codificar(HttpContext.Current.Session.SessionID);
                    }
                    else
                    {
                        throw new NullReferenceException("Não existe sessão no contexto atual.");
                    }
                }
                else
                {
                    return Codificar(Process.GetCurrentProcess().Id.ToString());
                }
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retornará um valor único com base 
        /// na data corrente.</para>
        /// </summary>
        public string PorData
        {
            get
            {
                return Codificar(DateTime.Now.ToString("yyyyMMdd"));
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retornará um valor único com base 
        /// na hora corrente.</para>
        /// </summary>
        public string PorHora
        {
            get
            {
                return Codificar(DateTime.Now.ToString("HHmmss"));
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Esta propriedade retornará um valor único com base 
        /// na data e hora corrente.</para>
        /// </summary>
        public string PorDataHora
        {
            get
            {
                return Codificar(DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
        }

    }
}
