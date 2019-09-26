using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Suporte.Criptografia
{
    /// <summary>
    /// <para>Disponibiliza funcionalidades relacionadas a criptografia de dados 
    /// com algorítimos de criptografia simétrica.</para>
    /// </summary>
    /// <typeparam name="TSymmetricAlgorithmProvider">Tipo do algoritimo de criptografia simétrica.</typeparam>
    public class CriptografiaSimetrica<TSymmetricAlgorithmProvider> where TSymmetricAlgorithmProvider : SymmetricAlgorithm
    {
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Armazena o total de bytes usado na palavra chave (Key).</para>
        /// </summary>
        public int BytesKey
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Armazena o total de bytes usado no vetor de inicialização (IV).</para>
        /// </summary>
        public int BytesIV
        {
            get;
            private set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita></para>
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </summary>
        public string Senha
        {
            get;
            set;
        }

        /// <summary>
        /// <para>(Leitura/Escrita></para>
        /// <para>Bytes usados na derivação da chave de criptografia.</para>
        /// </summary>
        public byte[] BytesSalt
        {
            get;
            set;
        }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        public CriptografiaSimetrica()
            : this(
            string.Empty,
            new byte[] { })
        { }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        public CriptografiaSimetrica(string senha, byte[] bytesSalt)
            : this(
            senha, 
            bytesSalt,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesKey,
            (new InformacoesSobreAlgoritmoDeCriptografiaSimetrica())[typeof(TSymmetricAlgorithmProvider)].BytesIV)
        { }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// <para>Informa o comprimento de bytes usado na criptografia.</para>
        /// </summary>
        /// <param name="bytesKey">
        /// <para>Total de bytes usados na palavra chave (Key) do algorítmo de criptografia.</para>
        /// </param>
        /// <param name="bytesIV">
        /// <para>Total de bytes usados no vetor de inicialização (IV) do algorítmo de criptografia.</para>
        /// </param>
        public CriptografiaSimetrica(int bytesKey, int bytesIV)
            : this(string.Empty, new byte[] { }, bytesKey, bytesIV) { }

        /// <summary>
        /// <para>Construtor padrão.</para>
        /// <para>Informa o comprimento de bytes usado na criptografia.</para>
        /// </summary>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia que foi informada pelo usuário.</para>
        /// </param>
        /// <param name="bytesKey">
        /// <para>Total de bytes usados na palavra chave (Key) do algorítmo de criptografia.</para>
        /// </param>
        /// <param name="bytesIV">
        /// <para>Total de bytes usados no vetor de inicialização (IV) do algorítmo de criptografia.</para>
        /// </param>
        public CriptografiaSimetrica(string senha, byte[] bytesSalt, int bytesKey, int bytesIV)
        {
            Senha = senha;
            BytesSalt = bytesSalt;
            BytesKey = bytesKey;
            BytesIV = bytesIV;
        }

        /// <summary>
        /// <para>Criptografa ou descriptografa uma sequencia de texto.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <param name="texto">
        /// <para>Texto de entrada.</para>
        /// </param>
        /// <returns>
        /// <para>Resulta no mesmo texto de entrada, porém, criptografado.</para>
        /// </returns>
        public string Aplicar(bool paraEntrada, string texto)
        {
            return Aplicar(paraEntrada, texto, Senha, BytesSalt);
        }

        /// <summary>
        /// <para>Criptografa ou descriptografa uma sequencia de texto.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <param name="texto">
        /// <para>Texto de entrada.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <returns>
        /// <para>Resulta no mesmo texto de entrada, porém, criptografado.</para>
        /// </returns>
        public string Aplicar(bool paraEntrada, string texto, string senha)
        {
            return Aplicar(paraEntrada, texto, senha, BytesSalt);
        }

        /// <summary>
        /// <para>Criptografa ou descriptografa uma sequencia de texto.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <param name="texto">
        /// <para>Texto de entrada.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia.</para>
        /// </param>
        /// <returns>
        /// <para>Resulta no mesmo texto de entrada, porém, criptografado.</para>
        /// </returns>
        public string Aplicar(bool paraEntrada, string texto, string senha, byte[] bytesSalt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ICryptoTransform cryptoTransform = ObterCryptoTransform(paraEntrada, senha, bytesSalt);
                CryptoStream cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

                if (paraEntrada)
                {
                    cryptoStream.Write(Encoding.Default.GetBytes(texto), 0, texto.Length);
                }
                else
                {
                    byte[] arrayTexto = Convert.FromBase64String(texto);
                    cryptoStream.Write(arrayTexto, 0, arrayTexto.Length);
                }

                cryptoStream.FlushFinalBlock();

                if (paraEntrada)
                {
                    return Convert.ToBase64String(ms.ToArray());
                }
                else
                {
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// <para>Obtem uma instância de uma classe devidamente configurada
        /// para realizar a des/criptografia.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna uma classe que implementa a interface <see cref="ICryptoTransform"/>.</para>
        /// </returns>
        public ICryptoTransform ObterCryptoTransform(bool paraEntrada)
        {
            return ObterCryptoTransform(paraEntrada, Senha, BytesSalt);
        }

        /// <summary>
        /// <para>Obtem uma instância de uma classe devidamente configurada
        /// para realizar a des/criptografia.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna uma classe que implementa a interface <see cref="ICryptoTransform"/>.</para>
        /// </returns>
        public ICryptoTransform ObterCryptoTransform(bool paraEntrada, string senha)
        {
            return ObterCryptoTransform(paraEntrada, senha, BytesSalt);
        }

        /// <summary>
        /// <para>Obtem uma instância de uma classe devidamente configurada
        /// para realizar a des/criptografia.</para>
        /// </summary>
        /// <param name="paraEntrada">
        /// <para>Quando igual a <c>true</c>, define o processo como Criptografia.
        /// Mas se for igual a <c>false</c>, define como Descriptografia.</para>
        /// </param>
        /// <param name="senha">
        /// <para>Senha, ou chave de criptografia, usada para des/criptografar.</para>
        /// </param>
        /// <param name="bytesSalt">
        /// <para>Bytes usados na derivação da chave de criptografia.</para>
        /// </param>
        /// <returns>
        /// <para>Retorna uma classe que implementa a interface <see cref="ICryptoTransform"/>.</para>
        /// </returns>
        public ICryptoTransform ObterCryptoTransform(bool paraEntrada, string senha, byte[] bytesSalt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(senha, bytesSalt);

            System.Reflection.MethodInfo method = typeof(TSymmetricAlgorithmProvider).GetMethod("Create", new Type[] { });
            TSymmetricAlgorithmProvider algoritmo = (TSymmetricAlgorithmProvider)method.Invoke(typeof(TSymmetricAlgorithmProvider), new object[] { });
            algoritmo.Key = pdb.GetBytes(BytesKey);
            algoritmo.IV = pdb.GetBytes(BytesIV);
            if (paraEntrada)
            {
                return algoritmo.CreateEncryptor();
            }
            else
            {
                return algoritmo.CreateDecryptor();
            }
        }
    }
}
