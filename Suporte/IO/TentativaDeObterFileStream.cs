using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Suporte.IO
{
    /// <summary>
    /// <para>Classe que realiza uma ou mais tentativas 
    /// para obter a abertura de uma <see cref="FileStream"/>.</para>
    /// </summary>
    class TentativaDeObterFileStream : ITentativaDeObterStream<FileStream>
    {
        IList<object> parametrosParaContrutorStream = new List<object>();

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Prepara a abertura de uma <see cref="FileStream"/>.</para>
        /// </summary>
        /// <param name="path">
        /// <para>Caminho físico do arquivo que será aberto.</para>
        /// </param>
        /// <param name="mode">
        /// <para>Modo de abertura do arquivo.</para>
        /// <para>Determina se deve ser criado, ou apenas acessado, etc.</para>
        /// </param>
        public TentativaDeObterFileStream(string path, FileMode mode)
        {
            parametrosParaContrutorStream.Add(1);
            parametrosParaContrutorStream.Add(path);
            parametrosParaContrutorStream.Add(mode);
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Prepara a abertura de uma <see cref="FileStream"/>.</para>
        /// </summary>
        /// <param name="path">
        /// <para>Caminho físico do arquivo que será aberto.</para>
        /// </param>
        /// <param name="mode">
        /// <para>Modo de abertura do arquivo.</para>
        /// <para>Determina se deve ser criado, ou apenas acessado, etc.</para>
        /// </param>
        /// <param name="access">
        /// <para>Modo de acesso ao arquivo.</para>
        /// <para>Determina se deve ser usado para leitura, escrita, etc.</para>
        /// </param>
        public TentativaDeObterFileStream(string path, FileMode mode, FileAccess access)
        {
            parametrosParaContrutorStream.Add(2);
            parametrosParaContrutorStream.Add(path);
            parametrosParaContrutorStream.Add(mode);
            parametrosParaContrutorStream.Add(access);
        }

        /// <summary>
        /// <para>Construtor.</para>
        /// <para>Prepara a abertura de uma <see cref="FileStream"/>.</para>
        /// </summary>
        /// <param name="path">
        /// <para>Caminho físico do arquivo que será aberto.</para>
        /// </param>
        /// <param name="mode">
        /// <para>Modo de abertura do arquivo.</para>
        /// <para>Determina se deve ser criado, ou apenas acessado, etc.</para>
        /// </param>
        /// <param name="access">
        /// <para>Modo de acesso ao arquivo.</para>
        /// <para>Determina se deve ser usado para leitura, escrita, etc.</para>
        /// </param>
        /// <param name="share">
        /// <para>Modo de compartilhamento do arquivo.</para>
        /// <para>Determina se deve ser compartilhado para para leitura, escrita, etc.</para>
        /// </param>
        public TentativaDeObterFileStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            parametrosParaContrutorStream.Add(3);
            parametrosParaContrutorStream.Add(path);
            parametrosParaContrutorStream.Add(mode);
            parametrosParaContrutorStream.Add(access);
            parametrosParaContrutorStream.Add(share);
        }

        #region ITentativaDeStream<FileStream> Members

        private int maximoDeTentativas = 3;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número máximo de tentativas para se obter a <see cref="Stream"/>.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        public int MaximoDeTentativas
        {
            get
            {
                return maximoDeTentativas;
            }
            set
            {
                maximoDeTentativas = value;
            }
        }

        private int tempoEntreAsTentativas = 300;
        /// <summary>
        /// <para>(Leitura/Escrita)</para>
        /// <para>Número correspondente ao tempo em milisegundos entre as tentativas
        /// para se obter a <see cref="Stream"/>.</para>
        /// <para>Todo valor menor que 1 (um) será considerado 1 (um).</para>
        /// </summary>
        public int TempoEntreAsTentativas
        {
            get
            {
                return tempoEntreAsTentativas;
            }
            set
            {
                tempoEntreAsTentativas = value;
            }
        }

        private IList<Exception> listaDasExceptions = new List<Exception>();
        /// <summary>
        /// <para>(Leitura)</para>
        /// <para>Lista das exceptions disparadas enquanto se 
        /// tentava obter a <see cref="Stream"/>.</para>
        /// </summary>
        public IList<Exception> ListaDasExceptions
        {
            get
            {
                return listaDasExceptions;
            }
        }

        /// <summary>
        /// <para>Tenta obter a <see cref="Stream"/> conforme possível.</para>
        /// </summary>
        /// <returns>
        /// <para>Sempre retornará a <see cref="Stream"/>. 
        /// Caso contrário, será disparada uma <see cref="Exception" />.
        /// Se necessário consulta a propriedade <c>InnerException</c> da <see cref="Exception"/>,
        /// dê preferência a propriedade <see cref="ListaDasExceptions"/> desta classe.</para>
        /// </returns>
        public Stream TentarObterStream()
        {
            int tentativas = 0;
            do
            {                
                try
                {
                    tentativas++;
                    switch ((int)parametrosParaContrutorStream[0])
                    {
                        case 1:
                            return File.Open(
                                (string)parametrosParaContrutorStream[1],
                                (FileMode)parametrosParaContrutorStream[2]);
                        case 2:
                            return File.Open(
                                (string)parametrosParaContrutorStream[1],
                                (FileMode)parametrosParaContrutorStream[2],
                                (FileAccess)parametrosParaContrutorStream[3]);
                        case 3:
                            return File.Open(
                                (string)parametrosParaContrutorStream[1],
                                (FileMode)parametrosParaContrutorStream[2],
                                (FileAccess)parametrosParaContrutorStream[3],
                                (FileShare)parametrosParaContrutorStream[4]);
                        default:
                            throw new NotImplementedException("Há um problema na implementação desta classe. Comunique ao desenvolvedor.");
                    }
                }
                catch (Exception ex)
                {
                    ListaDasExceptions.Add(ex);
                    Thread.Sleep(TempoEntreAsTentativas);
                }
            } while (tentativas < MaximoDeTentativas);

            throw new Exception(string.Format(
                "Erro de acesso ao arquivo '{0}'. Total de tentativas: {1}. Intervalo entre as tentativas: {2} ms.",
                (string)parametrosParaContrutorStream[1],
                tentativas, 
                TempoEntreAsTentativas));
        }

        #endregion

    }
}
