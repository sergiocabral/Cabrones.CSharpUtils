using System.Collections.Generic;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Disponibiliza funcionalidades referentes aos mime types da web.</para>
    /// </summary>
    public static class MimeTypes
    {
        /// <summary>
        /// <para>Retorna o nome do mime type para texto puro: <c>text/plain</c>.</para>
        /// </summary>
        public static string MimeTypeTextPlain
        {
            get
            {
                return "text/plain";
            }
        }

        /// <summary>
        /// <para>Retorna uma lista com vários mime types e a extensão de arquivo associada.</para>
        /// </summary>
        /// <returns>
        /// <para>Dicionário de strings, contendo como chave o mime type e 
        /// como valor a extensão de arquivo.</para>
        /// </returns>
        public static IList<KeyValuePair<string, string>> ListaAssociativa()
        {
            IList<KeyValuePair<string, string>> lista = new List<KeyValuePair<string, string>>();
            lista.Add(new KeyValuePair<string, string>(MimeTypeTextPlain, string.Empty));
            lista.Add(new KeyValuePair<string, string>(MimeTypeTextPlain, "txt"));
            lista.Add(new KeyValuePair<string, string>("application/msword", "doc"));
            lista.Add(new KeyValuePair<string, string>("application/mswrite", "wri"));
            lista.Add(new KeyValuePair<string, string>("application/msaccess", "mdb"));
            lista.Add(new KeyValuePair<string, string>("application/ms-project", "mpp"));
            lista.Add(new KeyValuePair<string, string>("application/x-excel", "xls"));
            lista.Add(new KeyValuePair<string, string>("application/ms-powerpoint", "ppt"));
            lista.Add(new KeyValuePair<string, string>("application/ms-powerpoint", "pps"));
            lista.Add(new KeyValuePair<string, string>("application/zip", "zip"));
            lista.Add(new KeyValuePair<string, string>("application/rtf", "rtf"));
            lista.Add(new KeyValuePair<string, string>("application/pdf", "pdf"));
            lista.Add(new KeyValuePair<string, string>("image/gif", "gif"));
            lista.Add(new KeyValuePair<string, string>("image/jpg", "jpg"));
            lista.Add(new KeyValuePair<string, string>("image/jpeg", "jpeg"));
            return lista;
        }

        /// <summary>
        /// <para>Obtem os mime types de uma extensão de arquivo especificada.</para>
        /// </summary>
        /// <param name="extensao">
        /// <para>Extensão de arquivo.</para>
        /// </param>
        /// <returns>
        /// <para>Lista com os mime types associados.</para>
        /// </returns>
        public static IList<string> ObterMimeType(string extensao)
        {
            return ObterMimeType(extensao, false);
        }

        /// <summary>
        /// <para>Obtem os mime types de uma extensão de arquivo especificada.</para>
        /// </summary>
        /// <param name="extensao">
        /// <para>Extensão de arquivo.</para>
        /// </param>
        /// <param name="padraoTextPlain"><para>Quando igual a
        /// <c>true</c>, sempre retornará o valor de <see cref="MimeTypeTextPlain"/> 
        /// quando nenhum mime type correspondente for encontrado.</para></param>
        /// <returns>
        /// <para>Lista com os mime types associados.</para>
        /// </returns>
        public static IList<string> ObterMimeType(string extensao, bool padraoTextPlain)
        {
            IList<string> resultado = new List<string>();
            IList<KeyValuePair<string, string>> lista = ListaAssociativa();
            foreach (KeyValuePair<string, string> valor in lista)
            {
                if (valor.Value.ToLower() == extensao.ToLower())
                {
                    resultado.Add(valor.Key);
                }
            }
            if (resultado.Count == 0 && padraoTextPlain)
            {
                resultado.Add(MimeTypeTextPlain);
            }
            return resultado;
        }

        /// <summary>
        /// <para>Obtem as extensões de arquivo associadas a um mime type.</para>
        /// </summary>
        /// <param name="mimeType">
        /// <para>Mime type.</para>
        /// </param>
        /// <returns>
        /// <para>Lista com as extensões de arquivo associadas.</para>
        /// </returns>
        public static IList<string> ObterExtensao(string mimeType)
        {
            IList<string> resultado = new List<string>();
            IList<KeyValuePair<string, string>> lista = ListaAssociativa();
            foreach (KeyValuePair<string, string> valor in lista)
            {
                if (valor.Key.ToLower() == mimeType.ToLower())
                {
                    resultado.Add(valor.Value);
                }
            }
            return resultado;
        }
    }
}
