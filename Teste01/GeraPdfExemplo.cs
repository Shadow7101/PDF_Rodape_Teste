using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste01
{
    /// <summary>
    /// https://www.nilthakkar.com/2013/11/itextsharpadd-headerfooter-to-pdf.html
    /// </summary>
    public class GeraPdfExemplo : GeraPdfBase
    {
        internal override void Documento()
        {
            for (int i = 0; i < 5; i++)
            {
                Paragraph para = new Paragraph("Hello world. Checking Header Footer", new Font(Font.FontFamily.HELVETICA, 22));

                para.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(para);

                pdfDoc.NewPage();
            }
        }
    }
}
