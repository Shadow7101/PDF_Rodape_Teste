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
                cliente.Encoding = Encoding.UTF8;
                var uri = new Uri(url);
                return cliente.DownloadString(uri);
            }
        }

        internal override void Documento()
        {

           // string htmlAntigo = RecuperaTexto("https://h-solicitacoesapi.educacao.intragov/demoImages/9312d0b8-8376-49f1-96c6-aae36529f9d6.html");
            string htmlNovo = RecuperaTexto("http://h-solicitacoesapi.educacao.intragov/demoImages/8d3cf72f-6f70-4064-b6ab-67005034dde1.html");

            

            //gravando texto no documento
            using (var reader = new StringReader(htmlNovo))
            {
                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDoc, reader);
            }
        }
    }
}
