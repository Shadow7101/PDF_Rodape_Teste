﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace Teste01
{
    public class CabecalhosDocumentoAssinado : PdfPageEventHelper
    {
        #region | Construtor
        public CabecalhosDocumentoAssinado(string tituloDoDocumento, string nome, string cpf, string hash)
        {
            this.TituloDoDocumento = tituloDoDocumento;
            this.Nome = nome;
            this.CPF = cpf;
            this.Hash = hash;
        }
        #endregion

        #region | Variáveis privadas
        /// <summary>
        /// título do documento
        /// </summary>
        private string TituloDoDocumento;
        /// <summary>
        /// título do documento
        /// </summary>
        private string Nome;
        /// <summary>
        /// título do documento
        /// </summary>
        private string CPF;
        /// <summary>
        /// título do documento
        /// </summary>
        private string Hash;
        /// <summary>
        /// Content - corpo do documento
        /// </summary>
        private PdfContentByte cb;

        /// <summary>
        /// template de cabeçalho do documento
        /// </summary>
        private PdfTemplate headerTemplate;

        /// <summary>
        /// template do rodapé do documento
        /// </summary>
        private PdfTemplate footerTemplate;

        /// <summary>
        /// Fonte padrão do documento
        /// </summary>
        private BaseFont bf;

        /// <summary>
        /// data de impressão do documento
        /// </summary>
        private DateTime PrintTime;
        #endregion

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontSmall = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            string text = "Página " + writer.PageNumber + " de ";
            float right1 = 125;

            //Add paging to header
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetLeft(40), document.PageSize.GetTop(45));
                cb.ShowText(this.TituloDoDocumento);
                cb.SetTextMatrix(document.PageSize.GetRight(right1), document.PageSize.GetTop(45));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                //adcionando o total de páginas
                cb.AddTemplate(headerTemplate, document.PageSize.GetRight(right1) + len, document.PageSize.GetTop(45));
            }

            //Add paging to footer
            //{
            //    cb.BeginText();
            //    cb.SetFontAndSize(bf, 12);
            //    cb.SetTextMatrix(document.PageSize.GetRight(right1), document.PageSize.GetBottom(30));
            //    cb.ShowText(text);
            //    cb.EndText();
            //    float len = bf.GetWidthPoint(text, 12);
            //    //adcionando o total de páginas
            //    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(right1) + len, document.PageSize.GetBottom(30));
            //}

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.GetTop(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetTop(50));
            cb.Stroke();

            ////Move the pointer and draw line to separate footer section from rest of page
            //cb.MoveTo(40, document.PageSize.GetBottom(50));
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            //cb.Stroke();

            //colocando hash da assinatura
            PdfPTable tab1 = new PdfPTable(1);
            tab1.TotalWidth = document.PageSize.Width - 80f;
            tab1.WidthPercentage = 70;
            var frase = new Phrase();
            frase.Add(new Chunk("Hash: ", baseFontNormal));
            frase.Add(new Chunk(this.Hash, baseFontSmall));
            var celula = new PdfPCell(frase);
            celula.HorizontalAlignment = Element.ALIGN_LEFT;
            celula.VerticalAlignment = Element.ALIGN_MIDDLE;
            tab1.AddCell(celula);
            tab1.WriteSelectedRows(0, -1, 40, 70, writer.DirectContent);
        }

        #region | outros eventos
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(100, 100);
            }
            catch (DocumentException de)
            {
                Console.WriteLine(de.Message);
            }
            catch (System.IO.IOException io)
            {
                Console.WriteLine(io.Message);
            }
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            try
            {
                base.OnCloseDocument(writer, document);

                headerTemplate.BeginText();
                headerTemplate.SetFontAndSize(bf, 12);
                headerTemplate.SetTextMatrix(0, 0);
                headerTemplate.ShowText((writer.PageNumber).ToString());
                headerTemplate.EndText();

                //footerTemplate.BeginText();
                //footerTemplate.SetFontAndSize(bf, 12);
                //footerTemplate.SetTextMatrix(0, 0);
                //footerTemplate.ShowText((writer.PageNumber).ToString());
                //footerTemplate.EndText();
            }
            catch (DocumentException de)
            {
                Console.WriteLine(de.Message);
            }
            catch (System.IO.IOException io)
            {
                Console.WriteLine(io.Message);
            }
        }
        #endregion
    }
}
