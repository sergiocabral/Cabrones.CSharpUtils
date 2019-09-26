using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Suporte.IO.PastasEArquivos
{
    /// <summary>
    /// <para>Funcionalidades utilitários para manipulação de arquivos.</para>
    /// </summary>
    public static class ArquivoUtil
    {
        /// <summary>
        /// <para>Retorna o caminho físico de um caminho virtual (ou físico).</para>
        /// <para>Substitui o prefixo <c>~/</c> ou <c>~\</c> pelo caminho base
        /// do projeto.</para>
        /// </summary>
        /// <param name="caminho"><para>Caminho virtual.</para></param>
        /// <returns><para>Caminho físico.</para></returns>
        public static string MapPath(string caminho)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(caminho);
            }
            else
            {
                if (caminho.IndexOf("~") == 0)
                {
                    string caminhoDoProjeto = AppDomain.CurrentDomain.BaseDirectory + "\\";
                    caminho = (caminhoDoProjeto + caminho.Substring(1)).Replace("/", "\\");
                    while (caminho.IndexOf("\\\\") >= 0)
                    {
                        caminho = caminho.Replace("\\\\", "\\");
                    }
                }
                return caminho;
            }
        }

        /// <summary>
        /// <para>Grava dados como arquivo.</para>
        /// </summary>
        /// <param name="dados"><para>Dados para gravação.</para></param>
        /// <param name="caminho"><para>Caminho do arquivo.</para></param>
        /// <returns><para>Referência ao arquivo gravados.</para></returns>
        public static FileInfo GravarComoArquivo(byte[] dados, string caminho)
        {
            FileInfo arquivo = new FileInfo(MapPath(caminho));
            using (FileStream stream = arquivo.OpenWrite())
            {
                stream.Write(dados, 0, dados.Length);
            }
            return arquivo;
        }

        /// <summary>
        /// <para>Grava dados como arquivo.</para>
        /// </summary>
        /// <param name="dados"><para>Dados para gravação.</para></param>
        /// <param name="caminho"><para>Caminho do arquivo.</para></param>
        /// <returns><para>Referência ao arquivo gravados.</para></returns>
        public static FileInfo GravarComoArquivo(string dados, string caminho)
        {
            FileInfo arquivo = new FileInfo(MapPath(caminho));
            using (StreamWriter stream = new StreamWriter(arquivo.FullName))
            {
                stream.Write(dados);
            }
            return arquivo;
        }

        /// <summary>
        /// <para>Lê dados do arquivo.</para>
        /// </summary>
        /// <param name="caminho"><para>Caminho do arquivo.</para></param>
        /// <returns><para>Dados do arquivo.</para></returns>
        public static string LerDoArquivoComoTexto(string caminho)
        {
            using (StreamReader stream = new StreamReader(MapPath(caminho)))
            {
                return stream.ReadToEnd();
            }
        }

        /// <summary>
        /// <para>Lê dados do arquivo.</para>
        /// </summary>
        /// <param name="caminho"><para>Caminho do arquivo.</para></param>
        /// <returns><para>Dados do arquivo.</para></returns>
        public static byte[] LerDoArquivoComoBytes(string caminho)
        {
            FileInfo arquivo = new FileInfo(MapPath(caminho));
            using (FileStream stream = arquivo.OpenRead())
            {
                byte[] dados = new byte[stream.Length];
                stream.Read(dados, 0, dados.Length);
                return dados;
            }            
        }
    }
}
