using System;

namespace Suporte.Criptografia
{
    /// <summary>
    /// <para>Agrupa informações sobre um algorítmo de criptografia.</para>
    /// </summary>
    public struct InformacaoSobreAlgoritmoDeCriptografiaSimetrica
    {
        Type tipo;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Tipo da classe que representa o algorítmo de criptografia.</para>
        /// </summary>
        public Type Tipo
        {
            get { return tipo; }
        }

        string nome;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Nome que representa o algorítmo de criptografia.</para>
        /// </summary>
        public string Nome
        {
            get { return nome; }
        }

        int bytesKey;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Quantidade de bytes usado na chave de criptografia.</para>
        /// </summary>
        public int BytesKey
        {
            get { return bytesKey; }
        }

        int bytesIV;
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Quantidade de bytes usado no Vetor de Inicialização (IV).</para>
        /// </summary>
        public int BytesIV
        {
            get { return bytesIV; }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        /// <param name="tipo"><para>Tipo da classe que representa o algorítmo
        /// de criptografia.</para></param>
        /// <param name="nome"><para>Nome que representa o algorítmo
        /// de criptografia.</para></param>
        /// <param name="bytesKey"><para>Quantidade de bytes usado na
        /// chave de criptografia.</para></param>
        /// <param name="bytesIV"><para>Quantidade de bytes usado no
        /// Vetor de Inicialização (IV).</para></param>
        public InformacaoSobreAlgoritmoDeCriptografiaSimetrica(Type tipo, string nome, int bytesKey, int bytesIV)
        {
            this.tipo = tipo;
            this.nome = nome;
            this.bytesKey = bytesKey;
            this.bytesIV = bytesIV;
        }
    }
}
