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
        private CustomPageState pageState;

        public void Gerar(string tituloDoDocumento, string nome = null, string cpf = null, string hash = null)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            this.pageState = new CustomPageState();
            //string fileName = System.Reflection.Assembly.GetEntryAssembly().Location + "\\" + string.Format("{0}.pdf", DateTime.Now.ToString(@"yyyyMMdd") + "_" + DateTime.Now.ToString(@"HHmmss"));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                //criando e estipulando o tipo da folha usada
                using (pdfDoc = new Document(PageSize.A4))
                {
                    try
                    {
                        //adcionando data de criação
                        pdfDoc.AddCreationDate();

                        //criando pdf em branco no disco
                        pdfWriter = PdfWriter.GetInstance(pdfDoc, fs);

                        //atrelando a classe de eventos da pagina
                        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(hash))
                        {
                            //estibulando o espaçamento das margens que queremos
                            pdfDoc.SetMargins(40, 40, 60, 50);
                            //para documentos não assinados
                            pdfWriter.PageEvent = new CabecalhoDocumentoNaoAssinado(tituloDoDocumento);
                        }
                        else
                        {
                            //estibulando o espaçamento das margens que queremos
                            pdfDoc.SetMargins(40, 40, 60, 80);
                            //para documentos assinados
                            pdfWriter.PageEvent = new CabecalhosDocumentoAssinado(tituloDoDocumento, nome, cpf, hash, pageState);
                        }
                        //ABRINDO DOCUMENTO PARA ALTERAÇÕES
                        pdfDoc.Open();

                        //Corpo do documento
                        Documento();

                        //identificando a última página
                        this.pageState.IsLastPage = true;

                        //fecha pdf
                        if (pdfDoc.IsOpen())
                            pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            if (File.Exists(fileName))
                System.Diagnostics.Process.Start(fileName);
        }

        internal abstract void Documento();
    }
}
