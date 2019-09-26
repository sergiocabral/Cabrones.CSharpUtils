using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPdf.HtmlToPdf;
using System.IO;

namespace Suporte.Relatorio.EvoHtmlToPdf
{
    /// <summary>
    /// <para>
    /// Classe estática que gerencia informações de licensa e outros referente
    /// a classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>.
    /// </para>
    /// </summary>
    public static class EvoHtmlToPdf
    {
        /// <summary>
        /// <para>Licensa de uso da classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>.</para>
        /// </summary>
        private static string LicenseKey
        {
            get
            {
                return Encoding.ASCII.GetString(new byte[] { 80, 82, 89, 80, 72, 81, 52, 79, 72, 81, 48, 100, 68, 104, 77, 78, 72, 81, 52, 77, 69, 119, 119, 80, 69, 119, 81, 69, 66, 65, 81, 61 });
            }
        }

        /// <summary>
        /// <para>Configura as informações de licensa de uma instância
        /// da classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>.</para>
        /// </summary>
        /// <param name="pdfConverter">
        /// <para>Instância da classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>.</para>
        /// </param>
        public static void ConfigurarLicensa(PdfConverter pdfConverter)
        {
            pdfConverter.LicenseKey = LicenseKey;
            pdfConverter.EvoInternalFileName = new FileInfo("evointernal.dll").FullName;
        }

        /// <summary>
        /// <para>Obtem uma instância da classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>
        /// com a licensa de uso configurada.</para>
        /// </summary>
        /// <returns><para>Instância da classe <see cref="EvoPdf.HtmlToPdf.PdfConverter"/>
        /// devidamente licensiada para uso.</para></returns>
        public static PdfConverter ObterPdfConverterLicensiado()
        {
            PdfConverter pdfConverter = new PdfConverter();
            ConfigurarLicensa(pdfConverter);
            return pdfConverter;
        }
    }
}
