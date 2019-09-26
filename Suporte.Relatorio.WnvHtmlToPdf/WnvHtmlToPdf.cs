using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winnovative.WnvHtmlConvert;

namespace Suporte.Relatorio.WnvHtmlToPdf
{
    /// <summary>
    /// <para>
    /// Classe estática que gerencia informações de licensa e outros referente
    /// a classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>.
    /// </para>
    /// </summary>
    public static class WnvHtmlToPdf
    {
        /// <summary>
        /// <para>Licensa de uso da classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>.</para>
        /// </summary>
        private static string LicenseKey
        {
            get
            {
                return Encoding.ASCII.GetString(new byte[] { 81, 71, 116, 120, 89, 72, 70, 103, 99, 88, 66, 48, 99, 71, 66, 52, 98, 110, 66, 103, 99, 51, 70, 117, 99, 88, 74, 117, 101, 88, 108, 53, 101, 81, 61, 61 });
            }
        }

        /// <summary>
        /// <para>Configura as informações de licensa de uma instância
        /// da classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>.</para>
        /// </summary>
        /// <param name="pdfConverter">
        /// <para>Instância da classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>.</para>
        /// </param>
        public static void ConfigurarLicensa(PdfConverter pdfConverter)
        {
            pdfConverter.LicenseKey = LicenseKey;
        }

        /// <summary>
        /// <para>Obtem uma instância da classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>
        /// com a licensa de uso configurada.</para>
        /// </summary>
        /// <returns><para>Instância da classe <see cref="Winnovative.WnvHtmlConvert.PdfConverter"/>
        /// devidamente licensiada para uso.</para></returns>
        public static PdfConverter ObterPdfConverterLicensiado()
        {
            PdfConverter pdfConverter = new PdfConverter();
            ConfigurarLicensa(pdfConverter);
            return pdfConverter;
        }
    }
}
