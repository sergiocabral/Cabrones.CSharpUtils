using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suporte.GuardaTudo
{
    /// <summary>
    /// <para>Informação sobre qualquer.</para>
    /// </summary>
    public class Informacao
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public Informacao()
        {
            Limpar();
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="nome"><para>Nome da informação.</para></param>
        public Informacao(string nome)
            : this()
        {
            Nome = nome;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="nome"><para>Nome da informação.</para></param>
        /// <param name="valor"><para>Valor da informação.</para></param>
        public Informacao(string nome, Valor valor)
            : this(nome)
        {
            Valor = valor;
        }

        /// <summary>
        /// <para>Construtor com inicialização de valor.</para>
        /// </summary>
        /// <param name="id"><para>Identificador numérico da informação.</para></param>
        /// <param name="nome"><para>Nome da informação.</para></param>
        /// <param name="valor"><para>Valor da informação.</para></param>
        public Informacao(long id, string nome, Valor valor)
            : this(nome, valor)
        {
            Id = id;
        }

        /// <summary>
        /// <para>Identificador numérico (e sequencial) da informação.</para>
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// <para>Informação pai que contem esta.</para>
        /// </summary>
        public Informacao Pai { get; set; }

        private List<Informacao> filhos = new List<Informacao>();
        /// <summary>
        /// <para>Informações contidas nesta.</para>
        /// </summary>
        public IList<Informacao> Filhos
        {
            get
            {
                return filhos;
            }
            set
            {
                filhos = value == null ? new List<Informacao>() : new List<Informacao>(value);
            }
        }

        private string nome = string.Empty;
        /// <summary>
        /// <para>Nome da informação.</para>
        /// </summary>
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value == null ? string.Empty : value;
            }
        }

        private Valor valor = new Valor();
        /// <summary>
        /// <para>Valor desta informação.</para>
        /// </summary>
        public Valor Valor
        {
            get
            {
                return valor;
            }
            set
            {
                valor = value == null ? new Valor() : value;
            }
        }


        /// <summary>
        /// <para>Limpar o valores armazenado.</para>
        /// </summary>
        public void Limpar()
        {
            Id = default(long);
            Pai = default(Informacao);
            Filhos = default(IList<Informacao>);
            Nome = default(string);
            Valor.Limpar();
        }

        /// <summary>
        /// <para>Substitui os valores das propriedades pelos valores
        /// presentes em outra informação. Trata-se de uma clonagem 
        /// dos dados.</para>
        /// </summary>
        /// <param name="informacao"><para><see cref="Informacao"/> que será copiada.</para></param>
        public void SubstituirConteudo(Informacao informacao)
        {
            if (informacao == null)
            {
                throw new NullReferenceException("A informação passada não pode ser nula.");
            }

            Id = informacao.Id;
            Pai = informacao.Pai;
            Filhos = new List<Informacao>(informacao.Filhos);
            Nome = informacao.Nome;
            Valor = new Valor(informacao.Valor);
        }


        /// <summary>
        /// <para>Exibição como <c>string</c>.</para>
        /// </summary>
        /// <returns><para>Exibição como <c>string</c>.</para></returns>
        public override string ToString()
        {
            return Nome + " = " + Convert.ToString(Valor);
        }
    }
}
