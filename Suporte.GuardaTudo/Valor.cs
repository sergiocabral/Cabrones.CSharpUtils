using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>Lista com os tipos de valores possíveis.</para>
    /// </summary>
    public enum TipoDeValor
    {
        /// <summary>
        /// <para>Nenhum valor definido.</para>
        /// </summary>
        Nenhum,

        /// <summary>
        /// <para>O valor está armazenado em outra informação.</para>
        /// </summary>
        Referencia,

        /// <summary>
        /// <para>Valor tipo Texto.</para>
        /// </summary>
        Texto,

        /// <summary>
        /// <para>Valor tipo Numérico.</para>
        /// </summary>
        Numerico,

        /// <summary>
        /// <para>Valor tipo Data e Hora.</para>
        /// </summary>
        DataHora,

        /// <summary>
        /// <para>Valor tipo Booleano.</para>
        /// </summary>
        Booleano,

        /// <summary>
        /// <para>Valor tipo Dados Binários.</para>
        /// </summary>
        Binario
    }

    /// <summary>
    /// <para>Valor armazenado para uma <see cref="Informacao"/>.</para>
    /// </summary>
    public class Valor : IComparable
    {
        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        public Valor()
        {
            Limpar();
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(Valor valor)
            : this()
        {
            binario = valor.binario;
            booleano = valor.booleano;
            dataHora = valor.dataHora;
            informacao = valor.informacao;
            numerico = valor.numerico;
            texto = valor.texto;
            Extra = valor.Extra;
            Tipo = valor.Tipo;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(Informacao valor)
            : this()
        {
            Informacao = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(string valor)
            : this()
        {
            Texto = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(decimal valor)
            : this()
        {
            Numerico = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(double valor)
            : this()
        {
            Numerico = (decimal)valor;
        }
        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(DateTime valor)
            : this()
        {
            DataHora = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(bool valor)
            : this()
        {
            Booleano = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor.</para></param>
        public Valor(byte[] valor)
            : this()
        {
            Binario = valor;
        }

        /// <summary>
        /// <para>Tipo de valor armazenado.</para>
        /// </summary>
        public TipoDeValor Tipo { get; private set; }

        private Informacao informacao;
        /// <summary>
        /// <para>Referência para a <see cref="Informacao"/> que contem os valores.</para>
        /// </summary>
        public Informacao Informacao
        {
            get
            {
                return informacao;
            }
            set
            {
                Limpar();
                informacao = value;
                Tipo = TipoDeValor.Referencia;
            }
        }

        private string texto;
        /// <summary>
        /// <para>Valor como: Texto</para>
        /// </summary>
        public string Texto
        {
            get
            {
                if (Informacao != null)
                {
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao.Valor.Texto;
                }
                else
                {
                    return texto;
                }
            }
            set
            {
                Limpar();
                texto = value;
                Tipo = TipoDeValor.Texto;
            }
        }

        private decimal numerico;
        /// <summary>
        /// <para>Valor como: Numérico</para>
        /// </summary>
        public decimal Numerico
        {
            get
            {
                if (Informacao != null)
                {
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao.Valor.Numerico;
                }
                else
                {
                    return numerico;
                }
            }
            set
            {
                Limpar();
                numerico = value;
                Tipo = TipoDeValor.Numerico;
            }
        }

        private DateTime dataHora;
        /// <summary>
        /// <para>Valor como: Data e Hora</para>
        /// </summary>
        public DateTime DataHora
        {
            get
            {
                if (Informacao != null)
                {
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao.Valor.DataHora;
                }
                else
                {
                    return dataHora;
                }
            }
            set
            {
                Limpar();
                dataHora = value;
                Tipo = TipoDeValor.DataHora;
            }
        }

        private bool booleano;
        /// <summary>
        /// <para>Valor como: Booleano</para>
        /// </summary>
        public bool Booleano
        {
            get
            {
                if (Informacao != null)
                {
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao.Valor.Booleano;
                }
                else
                {
                    return booleano;
                }
            }
            set
            {
                Limpar();
                booleano = value;
                Tipo = TipoDeValor.Booleano;
            }
        }

        private byte[] binario;
        /// <summary>
        /// <para>Valor como: Binário</para>
        /// </summary>
        public byte[] Binario
        {
            get
            {
                if (Informacao != null)
                {
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao.Valor.Binario;
                }
                else
                {
                    return binario;
                }
            }
            set
            {
                Limpar();
                binario = value;
                Tipo = TipoDeValor.Binario;
            }
        }

        /// <summary>
        /// <para>Armazena qualquer informação adicional.</para>
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// <para>Limpar o valor armazenado.</para>
        /// </summary>
        public void Limpar()
        {
            informacao = null;
            texto = default(string);
            numerico = default(decimal);
            dataHora = default(DateTime);
            booleano = default(bool);
            binario = default(byte[]);
            Tipo = TipoDeValor.Nenhum;
        }

        /// <summary>
        /// <para>Exibição como <c>string</c>.</para>
        /// </summary>
        /// <returns><para>Exibição como <c>string</c>.</para></returns>
        public override string ToString()
        {
            switch (Tipo)
            {
                case TipoDeValor.Texto:
                    return Texto;
                case TipoDeValor.Booleano:
                    return Booleano.ToString();
                case TipoDeValor.DataHora:
                    return DataHora.ToString();
                case TipoDeValor.Numerico:
                    return Numerico.ToString();
                case TipoDeValor.Binario:
                    return Binario == null ? "null" : "byte[" + Binario.Length.ToString() + "]";
                case TipoDeValor.Referencia:
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return Informacao == null ? string.Empty : Convert.ToString(Informacao.Valor);
                case TipoDeValor.Nenhum:
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// <para>Verifica se um objeto é igual em valor a esta instância.</para>
        /// </summary>
        /// <param name="obj"><para>Objeto que será comparado.</para></param>
        /// <returns><para>Retorna <c>true</c> quando seus valores são iguais.</para></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            switch (Tipo)
            {
                case TipoDeValor.Nenhum:
                case TipoDeValor.Binario:
                    return false;
                case TipoDeValor.Referencia:
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return this.Informacao.Valor.Equals(obj);
                case TipoDeValor.Booleano:
                    return this.Booleano.Equals(Convert.ToBoolean(obj));
                case TipoDeValor.DataHora:
                    return this.DataHora.Equals(Convert.ToDateTime(obj));
                case TipoDeValor.Numerico:
                    return this.Numerico.Equals(Convert.ToDecimal(obj));
                case TipoDeValor.Texto:
                    return this.Texto.Equals(Convert.ToString(obj));
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// <para>Fornece o código de hash.</para>
        /// </summary>
        /// <returns><para>Código de hash.</para></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// <para>Compara a instância atual com outro objeto e retorna
        /// um inteiro indicando a posição relativa da instância atual 
        /// em relação ao outro objeto.
        /// </para>
        /// </summary>
        /// <param name="obj"><para>Objeto que será comparado.</para></param>
        /// <returns><para>Retorna zero quando possuem o mesmo valor,
        /// negativo quando a instância atual é menor e
        /// positivo quando a instância atual é maior.</para></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("Não é possível comparar um valor nulo.");
            }
            switch (Tipo)
            {
                case TipoDeValor.Nenhum:
                case TipoDeValor.Binario:
                    throw new ArgumentException("O valor não permite comparação: TipoDeValor." + Tipo.ToString());
                case TipoDeValor.Referencia:
                    //Isso pode resultar em loop quando o valor for uma referência. Propositalmente não foi tratado.
                    return this.Informacao.Valor.CompareTo(obj);
                case TipoDeValor.Booleano:
                    return this.Booleano.CompareTo(Convert.ToBoolean(obj));
                case TipoDeValor.DataHora:
                    return this.DataHora.CompareTo(Convert.ToDateTime(obj));
                case TipoDeValor.Numerico:
                    return this.Numerico.CompareTo(Convert.ToDecimal(obj));
                case TipoDeValor.Texto:
                    return this.Texto.CompareTo(Convert.ToString(obj));
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// <para>Obtem o tipo de um determinado valor.</para>
        /// </summary>
        /// <param name="valor"><para>Valor para verificação.</para></param>
        /// <returns><para>Tipo do valor indicado.</para></returns>
        public static TipoDeValor ObterTipo(object valor)
        {
            if (valor is Int16 ||
                valor is Int32 ||
                valor is Int64 ||
                valor is Double ||
                valor is Decimal ||
                valor is Byte)
            {
                return TipoDeValor.Numerico;
            }
            else if (valor is DateTime)
            {
                return TipoDeValor.DataHora;
            }
            else if (valor is Boolean)
            {
                return TipoDeValor.Booleano;
            }
            else if (valor is byte[])
            {
                return TipoDeValor.Binario;
            }
            else if (valor == null)
            {
                return TipoDeValor.Nenhum;
            }
            else
            {
                return TipoDeValor.Texto;
            }
        }

        /// <summary>
        /// <para>Obtem o tipo para banco de dados com base em um <see cref="TipoDeValor"/>.</para>
        /// </summary>
        /// <param name="tipoDeValor"><para><see cref="TipoDeValor"/></para></param>
        /// <returns><para>Tipo de banco de dados.</para></returns>
        public static System.Data.DbType ObterTipoDeBancoDeDados(TipoDeValor tipoDeValor)
        {
            switch (tipoDeValor)
            {
                case TipoDeValor.Binario:
                    return System.Data.DbType.Binary;
                case TipoDeValor.Booleano:
                    return System.Data.DbType.Boolean;
                case TipoDeValor.DataHora:
                    return System.Data.DbType.DateTime;
                case TipoDeValor.Numerico:
                    return System.Data.DbType.Decimal;
                case TipoDeValor.Texto:
                    return System.Data.DbType.String;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
