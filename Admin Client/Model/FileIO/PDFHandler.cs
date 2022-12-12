using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using HttpClientHandler = Admin_Client.Model.DB.HttpClientHandler;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

namespace Admin_Client.Model.FileIO
{
    public class PDFHandler
    {
        //PdfWriter = Pass file name and write content to document
        //PdfDocument = In memory representation of the Pdf Document. Opens PDF doc in writing mode
        //Document = Creates a document from in-memory PdfDocument
        //Paragraph = Creates a paragraph, initialized with text
        //"C:\Users\Lars\Desktop\Exam"

        public PDFHandler()
        {
            PdfSharp();
        }


        #region iText 7

        /*
        public async Task<IActionResult> OnGet()
        {
            //Construct in memory and then stream it down to user and then user can decide if they want to save it or view
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc, PageSize.A4, false);
            //To prevent it automatically closing
            writer.SetCloseStream(false);

            Paragraph header = new Paragraph("Fair Share Receipt")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);

            Paragraph subheader = new Paragraph(DateTime.Now.ToShortDateString())
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15);
            document.Add(subheader);

            //empty line
            document.Add(new Paragraph(""));

            //line seperator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            //empty line
            document.Add(new Paragraph(""));

            //Add table containing data
            document.Add(await GetPdfTable());

            //page numbers
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 0; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                    .Format("Page" + i + " of " + n)), 559, 806, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);

            }
            document.Close();
            //Sets document as a stream and streams it down to browser using the content type
            byte[] byteInfo = ms.ToArray();
            ms.Write(byteInfo,0, byteInfo.Length);
            ms.Position= 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            //Uncomment this to return file as a download
            //fileStreamResult.FileDownloadName = "FairShareReceipt.pdf";

            return fileStreamResult;
        }

       
        public PDFHandler(tblReceipt receipt)
        {

            string receiptTripName = receipt.tblTrip.fldTripName.ToString();
            string receiptUserName = receipt.tblUser.fldFirstName.ToString();
            string receiptExpenses = receipt.tblTrip.fldSum.ToString();

            PdfWriter writer = new PdfWriter("C:\\Users\\Lars\\Desktop\\Exam");
            PdfDocument pdf = new PdfDocument(writer: writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Fair Share Receipt").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            Paragraph subheader = new Paragraph("Receipt for: "+receiptTripName).SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
            LineSeparator ls = new LineSeparator(new SolidLine());

            document.Add(header);
            document.Add(subheader);
            document.Add(ls);

            //Adding the table where Receipt info will be shown
            //We tell it that the table contains 3 columns(Person, Activity, Expense)
            Table table = new Table(3, false);
            Cell cellUsers = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Person"));
            Cell cellTrips = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Activity"));
            Cell cellExpenses = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Expense"));

            table.AddCell(cellUsers);
            table.AddCell(cellTrips);
            table.AddCell(cellExpenses);

            Cell cellShowUsers = new Cell(1,1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(receiptTripName));
            Cell cellShowTrip = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(receiptUserName));
            Cell cellShowExpenses = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(receiptExpenses));


            table.AddCell(cellShowUsers);
            table.AddCell(cellShowTrip);
            table.AddCell(cellShowExpenses);

            //Page Number
            int n = pdf.GetNumberOfPages();
            for (int i = 0; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String.Format("Page" + i + " of " + n)), 559, 806, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);

            }

            document.Close();

        }
        

        private async Task<Table> GetPdfTable()
       {
            Table table = new Table(3, false);
            //Headings
            Cell cellUsers = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Person"));
            Cell cellTrips = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Activity"));
            Cell cellExpenses = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Expense"));

            table.AddCell(cellUsers);
            table.AddCell(cellTrips);
            table.AddCell(cellExpenses);

            tblReceipt[] receipts = GetReceipt();

            foreach (var item in receipts)
            {
                Cell cName = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.tblUser.fldFirstName.ToString()));
                Cell cActivity = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.tblTrip.fldTripName.ToString()));
                Cell cExpenses = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(String.Format("{0:C2}", item.tblTrip.fldSum.ToString())));
            }
       }
        private tblReceipt GetReceipt()
        {
            //HttpClient client = new HttpClient();
            //var stream = await client.GetStreamAsync(HttpClientHandler.GetReceipts());
            //var stream = client.GetStreamAsync("https://localhost:7002/tblReceipts");
            var receipts = HttpClientHandler.GetReceipts();

            return receipts;
        }
        */

        #endregion

        #region PdfSharp
        private void PdfSharp()
        {
            tblReceipt receipt= new tblReceipt();   
            try
            {
                DataSet ds = new DataSet();
                string name = receipt.tblUser.fldFirstName.ToString();  
                string activity = receipt.tblTrip.fldTripName.ToString();
                string expenses = receipt.tblTrip.fldSum.ToString();
                int yPoint = 0;

                var bup = HttpClientHandler.GetReceipts();
                bup.Capacity.ToString();

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Fair Share Receipt";
                PdfSharp.Pdf.PdfPage pdfpage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfpage);
                XFont font = new XFont("Verdana", 20, XFontStyle.Regular);
                yPoint = yPoint + 100;
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    name = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    activity = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    expenses = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                    graph.DrawString(name, font, XBrushes.Black, new XRect(40, yPoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormat.TopLeft);
                    graph.DrawString(activity, font, XBrushes.Black, new XRect(280, yPoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormat.TopLeft);
                    graph.DrawString(expenses, font, XBrushes.Black, new XRect(420, yPoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormat.TopLeft);

                    yPoint = yPoint + 40;
                }

                string pdfFilename = "FairShareReceipt.pdf";
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion

    }
}
