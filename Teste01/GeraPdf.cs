using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste01
{
    public class GeraPdf
    {
        public static void Gerar()
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            //string fileName = System.Reflection.Assembly.GetEntryAssembly().Location + "\\" + string.Format("{0}.pdf", DateTime.Now.ToString(@"yyyyMMdd") + "_" + DateTime.Now.ToString(@"HHmmss"));
            using (FileStream msReport = new FileStream(fileName, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 140f, 10f))
                {
                    try
                    {
                        // step 2
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new ITextEvents();

                        //open the stream 
                        pdfDoc.Open();

                        for (int i = 0; i < 5; i++)
                        {
                            Paragraph para = new Paragraph("Hello world. Checking Header Footer", new Font(Font.FontFamily.HELVETICA, 22));

                            para.Alignment = Element.ALIGN_CENTER;

                            pdfDoc.Add(para);

                            pdfDoc.NewPage();
                        }

                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (File.Exists(fileName)) System.Diagnostics.Process.Start(fileName);
                    }

                }

            }
        }
    }
}
