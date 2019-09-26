using System.Web;
using System.Collections.Generic;

namespace Suporte.Web
{
    /// <summary>
    /// <para>Disponibiliza em propriedades as definições dos tipos de mídia
    /// para arquivo de estilos CSS..</para>
    /// <para>Para maiores detalhes consulte 
    /// o site: http://www.w3.org/TR/CSS2/media.html.
    /// </para>
    /// </summary>
    public static class StyleCssMediaTypes
    {
        /// <summary>
        /// Lista com os tipos de mídias
        /// </summary>
        public enum MediaTypes
        {
            /// <summary>
            /// Adequado para qualquer dispositivo.
            /// </summary>
            all,
            /// <summary>
            /// Direcionado para dispositivos táteis com respostas em braile.
            /// </summary>
            braille,
            /// <summary>
            /// Direcionado para página impressas em braile.
            /// </summary>
            embossed,
            /// <summary>
            /// Direcionado para dispositivos móveis, smartphones, etc. Tipicamente com tela pequena.
            /// </summary>
            handheld,
            /// <summary>
            /// Direcionado para páginas e documentos é visualização para impressão.
            /// </summary>
            print,
            /// <summary>
            /// Direcionado para para projetores de apresentações.
            /// </summary>
            projection,
            /// <summary>
            /// Direcionado primariamente para computadores com tela colorida.
            /// </summary>
            screen,
            /// <summary>
            /// Direcionado para sintetizadores de voz.
            /// </summary>
            speech,
            /// <summary>
            /// Direcionado para mídias com tamanho fixo de caratcer. Por exemplo, terminais de consulta. O dispositivo talvez use a unidade de pixels como "tty".
            /// </summary>
            tty,
            /// <summary>
            /// Direcionado para dispositivos tipo televisores, com baixa resolução, a cores, limitações na rolagem da tela e som disponível.
            /// </summary>
            tv};

        private static Dictionary<MediaTypes, string> listaDescritiva = null;
        /// <summary>
        /// <para>Retorna uma lista com os tipos de mídias e sua respectiva descrição.</para>
        /// </summary>
        /// <returns>Lista com os tipos de mídias e sua respectiva descrição.</returns>
        public static IDictionary<MediaTypes, string> ListaDescritiva
        {
            get
            {
                if (listaDescritiva == null)
                {
                    listaDescritiva = new Dictionary<MediaTypes, string>();
                    listaDescritiva.Add(MediaTypes.all, "Adequado para qualquer dispositivo.");
                    listaDescritiva.Add(MediaTypes.braille, "Direcionado para dispositivos táteis com respostas em braile.");
                    listaDescritiva.Add(MediaTypes.embossed, "Direcionado para página impressas em braile.");
                    listaDescritiva.Add(MediaTypes.handheld, "Direcionado para dispositivos móveis, smartphones, etc. Tipicamente com tela pequena.");
                    listaDescritiva.Add(MediaTypes.print, "Direcionado para páginas e documentos é visualização para impressão.");
                    listaDescritiva.Add(MediaTypes.projection, "Direcionado para para projetores de apresentações.");
                    listaDescritiva.Add(MediaTypes.screen, "Direcionado primariamente para computadores com tela colorida.");
                    listaDescritiva.Add(MediaTypes.speech, "Direcionado para sintetizadores de voz.");
                    listaDescritiva.Add(MediaTypes.tty, "Direcionado para mídias com tamanho fixo de caratcer. Por exemplo, terminais de consulta. O dispositivo talvez use a unidade de pixels como \"tty\".");
                    listaDescritiva.Add(MediaTypes.tv, "Direcionado para dispositivos tipo televisores, com baixa resolução, a cores, limitações na rolagem da tela e som disponível.");
                }
                return new Dictionary<MediaTypes, string>(listaDescritiva);
            }
        }

        private static List<MediaTypes> lista = null;
        /// <summary>
        /// <para>Retorna uma lista com os tipos de mídias.</para>
        /// </summary>
        /// <returns>Lista com os tipos de mídias.</returns>
        public static IList<MediaTypes> Lista
        {
            get
            {
                if (lista == null)
                {
                    lista = new List<MediaTypes>(ListaDescritiva.Keys);
                }
                return lista;
            }
        }
    }
}
