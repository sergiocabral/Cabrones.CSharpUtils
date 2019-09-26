using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Suporte.Criptografia
{
    /// <summary>
    /// <para>Contem informações sobre algorítmos de criptografia simétrica.</para>
    /// </summary>
    public class InformacoesSobreAlgoritmoDeCriptografiaSimetrica
    {
        private Dictionary<string, InformacaoSobreAlgoritmoDeCriptografiaSimetrica> algoritmos = new Dictionary<string, InformacaoSobreAlgoritmoDeCriptografiaSimetrica>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista de algorítmos de criptografia simétrica.</para>
        /// </summary>
        public Dictionary<string, InformacaoSobreAlgoritmoDeCriptografiaSimetrica> Algoritmos
        {
            get { return new Dictionary<string, InformacaoSobreAlgoritmoDeCriptografiaSimetrica>(algoritmos); }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre o algorítmo de criptografia simétrica especificado.</para>
        /// </summary>
        /// <param name="criptografia">
        /// <para>Tipo do algorítmo de criptografia simétrica.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a estrutura <see cref="InformacaoSobreAlgoritmoDeCriptografiaSimetrica"/>
        /// preenchida com as informações do algorítmo de criptografia simétrica.</para>
        /// </returns>
        public InformacaoSobreAlgoritmoDeCriptografiaSimetrica this[Type criptografia]
        {
            get
            {
                Type baseType;
                do
                {
                    baseType = criptografia.BaseType;
                } while (baseType != typeof(SymmetricAlgorithm) && baseType != typeof(object));
                if (baseType == typeof(object))
                {
                    throw new FormatException("Não é um SymmetricAlgorithm.");
                }
                return Algoritmos[criptografia.Name];
            }
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Informações sobre o algorítmo de criptografia simétrica especificado.</para>
        /// </summary>
        /// <param name="criptografia">
        /// <para>Tipo do algorítmo de criptografia simétrica.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna a estrutura <see cref="InformacaoSobreAlgoritmoDeCriptografiaSimetrica"/>
        /// preenchida com as informações do algorítmo de criptografia simétrica.</para>
        /// </returns>
        public InformacaoSobreAlgoritmoDeCriptografiaSimetrica this[string criptografia]
        {
            get { return Algoritmos[criptografia]; }
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// </summary>
        public InformacoesSobreAlgoritmoDeCriptografiaSimetrica()
        {
            AdicionarAlgoritmo(
                typeof(DES),
                8,  // Chave de 64 bits
                8); // Vetor de Inicialização (IV) de 64 bits para bloco de 64 bits
            AdicionarAlgoritmo(
                typeof(TripleDES),
                24, // Chave de 192 bits
                8); // Vetor de Inicialização (IV) de 64 bits para bloco de 64 bits
            AdicionarAlgoritmo(
                typeof(RC2),
                16, // Chave de 128 bits
                8); // Vetor de Inicialização (IV) de 64 bits para bloco de 64 bits
            AdicionarAlgoritmo(
                typeof(Rijndael),
                32,  // Chave de 256 bits
                16); // Vetor de Inicialização (IV) de 64 bits para bloco de 128 bits
        }

        /// <summary>
        /// <para>Adiciona na propriedade <see cref="Algoritmos"/> um algorítmo de criptografia simétrica.</para>
        /// </summary>
        /// <param name="tipo">
        /// <para>Tipo da classe que representa o algorítmo de criptografia.</para>
        /// </param>
        /// <param name="bytesKey">
        /// <para>Quantidade de bytes usados na chave de criptografia.</para>
        /// </param>
        /// <param name="bytesIV">
        /// <para>Quantidade de bytes usados no Vetor de Inicialização (IV).</para>
        /// </param>
        private void AdicionarAlgoritmo(Type tipo, int bytesKey, int bytesIV)
        {
            InformacaoSobreAlgoritmoDeCriptografiaSimetrica algoritmo =
                new InformacaoSobreAlgoritmoDeCriptografiaSimetrica(tipo, tipo.Name, bytesKey, bytesIV);
            algoritmos.Add(algoritmo.Nome, algoritmo);
        }

    }
}
