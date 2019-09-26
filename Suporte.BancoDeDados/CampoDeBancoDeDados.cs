using System;
using System.Data;
using System.Reflection;

namespace Suporte.BancoDeDados
{
    /// <summary>
    /// <para>Campo de banco de dados com valor atribuido.</para>
    /// <para>Agrupa atributos genéricos para qualquer tipo de campo.</para>
    /// </summary>
    /// <typeparam name="T">
    /// <para>Tipo do dado armazenado pelo campo.</para>
    /// </typeparam>
    public class CampoDeBancoDeDados<T> : ICampoDeBancoDeDados
    {
        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public CampoDeBancoDeDados()
        {
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor do campo.</para>
        /// </param>
        public CampoDeBancoDeDados(T valor)
            : this()
        {
            Valor = valor;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="nome">
        /// <para>Nome do campo.</para>
        /// </param>
        /// <param name="notNull">
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </param>
        /// <param name="chavePrimaria">
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </param>
        /// <param name="chaveUnica">
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </param>
        public CampoDeBancoDeDados(string nome, bool notNull, bool chavePrimaria, bool chaveUnica)
            : this()
        {
            Nome = nome;
            NotNull = notNull;
            ChavePrimaria = chavePrimaria;
            ChaveUnica = chaveUnica;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="nome">
        /// <para>Nome do campo.</para>
        /// </param>
        /// <param name="notNull">
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </param>
        /// <param name="chavePrimaria">
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </param>
        /// <param name="chaveUnica">
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </param>
        /// <param name="tamanho">
        /// <para>Tamanho do campo.</para>
        /// </param>
        /// <param name="precisao">
        /// <para>Precisão do campo.</para>
        /// </param>
        public CampoDeBancoDeDados(string nome, bool notNull, bool chavePrimaria, bool chaveUnica, int tamanho, int precisao)
            : this(nome, notNull, chavePrimaria, chaveUnica)
        {
            Tamanho = tamanho;
            Precisao = precisao;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="nome">
        /// <para>Nome do campo.</para>
        /// </param>
        /// <param name="notNull">
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </param>
        /// <param name="chavePrimaria">
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </param>
        /// <param name="chaveUnica">
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </param>
        /// <param name="tamanho">
        /// <para>Tamanho do campo.</para>
        /// </param>
        /// <param name="precisao">
        /// <para>Precisão do campo.</para>
        /// </param>
        /// <param name="extra">
        /// <para>Informação extra relacioada ao campo.</para>
        /// </param>
        public CampoDeBancoDeDados(string nome, bool notNull, bool chavePrimaria, bool chaveUnica, int tamanho, int precisao, object extra)
            : this(nome, notNull, chavePrimaria, chaveUnica, tamanho, precisao)
        {
            Extra = extra;
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="nome">
        /// <para>Nome do campo.</para>
        /// </param>
        /// <param name="notNull">
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </param>
        /// <param name="chavePrimaria">
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </param>
        /// <param name="chaveUnica">
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </param>
        /// <param name="tamanho">
        /// <para>Tamanho do campo.</para>
        /// </param>
        /// <param name="precisao">
        /// <para>Precisão do campo.</para>
        /// </param>
        /// <param name="valor">
        /// <para>Valor do campo.</para>
        /// </param>
        /// <param name="extra">
        /// <para>Informação extra relacioada ao campo.</para>
        /// </param>
        public CampoDeBancoDeDados(string nome, bool notNull, bool chavePrimaria, bool chaveUnica, int tamanho, int precisao, object extra, T valor)
            : this(nome, notNull, chavePrimaria, chaveUnica, tamanho, precisao, extra)
        {
            Valor = valor;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor do campo.</para>
        /// </summary>
        public T Valor
        {
            get;
            set;
        }

        #region ICampoDeBancoDeDados Members

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Nome do campo.</para>
        /// </summary>
        public string Nome
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo não permite valor NULL.</para>
        /// </summary>
        public bool NotNull
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo é uma chave primária.</para>
        /// </summary>
        public bool ChavePrimaria
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Indicativo se o campo é uma chave única.</para>
        /// </summary>
        public bool ChaveUnica
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Valor puro do campo.</para>
        /// </summary>
        public object ValorRaw
        {
            get
            {
                return Valor;
            }
            set
            {
                Valor = (T)value;
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Tamanho do campo.</para>
        /// </summary>
        public int Tamanho
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Precisão do campo.</para>
        /// </summary>
        public int Precisao
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Tipo de dados deste campo.</para>
        /// </summary>
        public Type Tipo
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Informação extra relacioada ao campo.</para>
        /// </summary>
        public object Extra
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// <para>Compara se um objeto é igual ao atual.</para>
        /// </summary>
        /// <param name="obj">
        /// <para>Objeto que será comparado.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna <c>true</c> quando o objeto é igual.</para>
        /// </returns>
        public override bool Equals(object obj)
        {
            return ((ICampoDeBancoDeDados)obj).Nome == Nome &&
                   ((ICampoDeBancoDeDados)obj).NotNull == NotNull &&
                   ((ICampoDeBancoDeDados)obj).ChavePrimaria == ChavePrimaria &&
                   ((ICampoDeBancoDeDados)obj).ChaveUnica == ChaveUnica &&
                   ((ICampoDeBancoDeDados)obj).Tamanho == Tamanho &&
                   ((ICampoDeBancoDeDados)obj).Precisao == Precisao;
        }

        /// <summary>
        /// <para>Obtem o código de hash da instância atual.</para>
        /// </summary>
        /// <returns>
        /// <apar>Código de hash da instância atual.</apar>
        /// </returns>
        public override int GetHashCode()
        {
            return (Nome + NotNull.ToString() + ChavePrimaria.ToString() + ChaveUnica.ToString() + Tamanho.ToString() + Precisao.ToString()).GetHashCode();
        }

        /// <summary>
        /// <para>A partir de um tipo de dados genérico retorna o 
        /// tipo de dados correspondente para uso em um banco de dados.</para>
        /// </summary>
        /// <returns>
        /// <para>Tipo de dados correspondente para uso em um banco de dados.</para>
        /// </returns>
        public static DbType ObterTipoParaBancoDeDados()
        {
            if (typeof(T) == typeof(bool)) { return DbType.Boolean; }
            else if (typeof(T) == typeof(byte)) { return DbType.Byte; }
            else if (typeof(T) == typeof(sbyte)) { return DbType.SByte; }
            else if (typeof(T) == typeof(short)) { return DbType.Int16; }
            else if (typeof(T) == typeof(int)) { return DbType.Int32; }
            else if (typeof(T) == typeof(ushort)) { return DbType.UInt16; }
            else if (typeof(T) == typeof(long)) { return DbType.Int64; }
            else if (typeof(T) == typeof(double)) { return DbType.Double; }
            else if (typeof(T) == typeof(float)) { return DbType.Decimal; }
            else if (typeof(T) == typeof(uint)) { return DbType.UInt32; }
            else if (typeof(T) == typeof(ulong)) { return DbType.UInt64; }
            else if (typeof(T) == typeof(char)) { return DbType.String; }
            else if (typeof(T) == typeof(string)) { return DbType.String; }
            else if (typeof(T) == typeof(DateTime)) { return DbType.DateTime; }
            else { return DbType.Binary; }
        }

        /// <summary>
        /// <para>A partir de um tipo de dados genérico retorna o 
        /// tipo de dados correspondente para uso em um banco de dados.</para>
        /// </summary>
        /// <param name="tipo">
        /// <para>Tipo de dados.</para>
        /// </param>
        /// <returns>
        /// <para>Tipo de dados correspondente para uso em um banco de dados.</para>
        /// </returns>
        public static DbType ObterTipoParaBancoDeDados(Type tipo)
        {
            object campoDeBancoDeDados = Activator.CreateInstance(typeof(CampoDeBancoDeDados<>).MakeGenericType(new Type[] { tipo }));
            foreach (MethodInfo m in campoDeBancoDeDados.GetType().GetMethods())
            {
                if (m.Name == "ObterTipoParaBancoDeDados" && m.GetParameters().Length == 0)
                {
                    return (DbType)m.Invoke(campoDeBancoDeDados, null);
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// <para>A partir de um valor retorna o 
        /// tipo de dados correspondente para uso em um banco de dados.</para>
        /// </summary>
        /// <param name="valor">
        /// <para>Valor.</para>
        /// </param>
        /// <returns>
        /// <para>Tipo de dados correspondente para uso em um banco de dados.</para>
        /// </returns>
        public static DbType ObterTipoParaBancoDeDados(object valor)
        {
            return ObterTipoParaBancoDeDados(valor.GetType());
        }
    }
}
