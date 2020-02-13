using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Teste01
{
    public abstract class GeraPdfBase
    {
        protected Document pdfDoc;
        protected PdfWriter pdfWriter;

        public void Gerar()
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            //string fileName = System.Reflection.Assembly.GetEntryAssembly().Location + "\\" + string.Format("{0}.pdf", DateTime.Now.ToString(@"yyyyMMdd") + "_" + DateTime.Now.ToString(@"HHmmss"));
            using (FileStream msReport = new FileStream(fileName, FileMode.Create))
            {
                //step 1
                using (pdfDoc = new Document(PageSize.A4, 10f, 10f, 140f, 10f))
                {
                    try
                    {
                        // step 2
                        pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new ITextEvents();

                        //open the stream 
                        pdfDoc.Open();

                        //Corpo do documento
                        Documento();

                        //fecha pdf
                        if (pdfDoc.IsOpen()) pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                       
                    }
                }
            }
            //Thread.Sleep(3000);
            if (File.Exists(fileName)) System.Diagnostics.Process.Start(fileName);
        }

        internal abstract void Documento();
    }
}
