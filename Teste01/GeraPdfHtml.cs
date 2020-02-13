using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Teste01
{
    public class GeraPdfHtml: GeraPdfBase
    {
        public string RecuperaTexto(string url)
        {
            using (WebClient cliente = new WebClient())
            {
                cliente.UseDefaultCredentials = true;
                var uri = new Uri(url);
                return cliente.DownloadString(uri);
            }
        }

        internal override void Documento()
        {
            //recuperando conteudo do documento
            string html = RecuperaTexto("https://h-solicitacoesapi.educacao.intragov/demoImages/9312d0b8-8376-49f1-96c6-aae36529f9d6.html");

            //gravando texto no documento
            using (var reader = new StringReader(html))
            {
                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDoc, reader);
            }
        }
    }
}
