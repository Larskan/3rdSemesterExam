using Admin_Client.Model.DB.EF_Test;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Document = Aspose.Pdf.Document;

namespace Admin_Client.Model.FileIO
{
    public class GeneratePDF
    {
         /*
        public void GenerateReceiptPDF()
        {
            DateTime datetime = DateTime.Now;
            string route = @"C:\Users\Lars\Desktop\Exam\Receipt_"+datetime.ToShortDateString()+".pdf";
            // @"\Source\Repos\SplitBillsIntoFairShares\Admin Client
            //Uri route = new Uri("..");
            //Error: Cant access my file path, try to split it up
            //PdfDocument pdf = new PdfDocument(new PdfWriter(new FileStream(route,FileMode.Create,FileAccess.ReadWrite)));
            //PdfWriter writer = new PdfWriter(route);
            //FileStream fs = new FileStream(route,FileMode.Create);

            //PdfPage page = new PdfPage()

            //MemoryStream ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(route);
            PdfDocument pdf = new PdfDocument(writer);

           // PdfPage page = new PdfPage();


            Document document = new Document(pdf);
            //document.
            

            Paragraph header = new Paragraph("Fair Share").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            Paragraph subheader = new Paragraph("Receipt").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
            LineSeparator ls = new LineSeparator(new SolidLine());
            
            //Current throw: wont add header
            document.Add(header);
            document.Add(subheader);
            document.Add(ls);
           // document.Add()
            Console.WriteLine("PDF Created");

            tblReceipt tblReceipt = new tblReceipt();
            var name = tblReceipt.tblUser.fldFirstName; //NullReferenceException
            Debug.Write("Name: " + name);
            var activity = tblReceipt.tblTrip.fldTripName;
            Debug.Write("Activity: " + activity);
            var expense = tblReceipt.tblTrip.fldSum.ToString();
            Debug.Write("Expense: " + expense);

            //Adding the table where Receipt info will be shown
            //We tell it that the table contains 3 columns(Person, Activity, Expense)
            Table table = new Table(3, false);
            Cell cellUsers = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Person"));
            Cell cellTrips = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Activity"));
            Cell cellExpenses = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Expense"));

            table.AddCell(cellUsers);
            table.AddCell(cellTrips);
            table.AddCell(cellExpenses);

            Cell cellShowUsers = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(name));
            Cell cellShowTrip = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(activity));
            Cell cellShowExpenses = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(expense));


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

         */
        public void test()
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\Exam\Receipt_" + datetime.ToShortDateString() + ".pdf";
            Document document = new Document();
            Page page = document.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Ohai"));
            document.Save(dataDir);
        }
    }
}
