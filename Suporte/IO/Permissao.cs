using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Suporte.IO
{
    /// <summary>
    /// <para>Verifica permissões em arquivo e pastas</para>
    /// </summary>
    public static class Permissao
    {
        #region Static Public

        /// <summary>
        /// Verifica se há permissão de Leitura no caminho informado.
        /// </summary>
        /// <param name="caminho">
        /// <para>Caminho.</para>
        /// </param>
        public static bool Leitura(string caminho)
        {
            DirectoryInfo pasta = new DirectoryInfo(caminho);
            FileInfo arquivo = new FileInfo(caminho);
            if (pasta.Exists)
            {
                return Leitura(pasta);
            }
            else if (arquivo.Exists)
            {
                return Leitura(arquivo);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se há permissão de Leitura no arquivo informado.
        /// </summary>
        /// <param name="arquivo">
        /// <para>Arquivo.</para>
        /// </param>
        public static bool Leitura(FileInfo arquivo)
        {
            if (arquivo != null && arquivo.Exists)
            {
                try
                {
                    arquivo.OpenRead().Close();
                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Verifica se há permissão de Leitura na pasta informada.
        /// </summary>
        /// <param name="pasta">
        /// <para>Pasta.</para>
        /// </param>
        public static bool Leitura(DirectoryInfo pasta)
        {
            if (pasta != null && pasta.Exists)
            {
                try
                {
                    pasta.GetFiles();
                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }


        /// <summary>
        /// Verifica se há permissão de Escrita no caminho informado.
        /// </summary>
        /// <param name="caminho">
        /// <para>Caminho.</para>
        /// </param>
        public static bool Escrita(string caminho)
        {
            DirectoryInfo pasta = new DirectoryInfo(caminho);
            FileInfo arquivo = new FileInfo(caminho);
            if (pasta.Exists)
            {
                return Escrita(pasta);
            }
            else if (arquivo.Exists)
            {
                return Escrita(arquivo);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se há permissão de Escrita no arquivo informado.
        /// </summary>
        /// <param name="arquivo">
        /// <para>Arquivo.</para>
        /// </param>
        public static bool Escrita(FileInfo arquivo)
        {
            if (arquivo != null && arquivo.Exists)
            {
                try
                {
                    arquivo.OpenWrite().Close();
                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Verifica se há permissão de Escrita na pasta informada.
        /// </summary>
        /// <param name="pasta">
        /// <para>Pasta.</para>
        /// </param>
        public static bool Escrita(DirectoryInfo pasta)
        {
            if (pasta != null && pasta.Exists)
            {
                FileInfo arquivo = new FileInfo(pasta.FullName + "\\tmp" + DateTime.Now.Ticks.ToString());
                bool resultado;
                try
                {
                    arquivo.Create();
                    resultado = true;
                    arquivo.Delete();
                }
                catch (Exception)
                {
                    resultado = false;
                }
                return resultado;
            }
            return false;
        }

        #endregion

    }
}
