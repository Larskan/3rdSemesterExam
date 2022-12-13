using Admin_Client.Model.DB.EF_Test;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Document = Aspose.Pdf.Document;
using Aspose.Pdf.Text;
using DataTable = System.Data.DataTable;
using Admin_Client.Model.DB;
using System.ComponentModel;
using DocumentFormat.OpenXml.Wordprocessing;
using Admin_Client.Model.FileIO.PDF;

namespace Admin_Client.Model.FileIO
{
    /// <summary>
    /// Header = Logo and general info
    /// Address = Info about Fair Share and Customer
    /// Grid = Table of ReceiptPDF Data and totals subsection
    /// Terms = List of strings for additional
    /// Footer = One string at bottom of page
    /// </summary>
    public class GeneratePDF
    {
        
        public void test()
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\Exam\Receipt_" + datetime.ToLongDateString() + ".pdf";
            Aspose.Pdf.Document document = new Document();
            document.PageInfo.Width = 612.0;
            document.PageInfo.Height = 792.0;
            document.PageInfo.Margin = new MarginInfo(72,72,72,72);
            Aspose.Pdf.Page page = document.Pages.Add();
            page.PageInfo.Width = 612.0;
            page.PageInfo.Height = 792.0;
            page.PageInfo.Margin = new MarginInfo(72, 72, 72, 72);



            Aspose.Pdf.FloatingBox floatbox = new FloatingBox();
            floatbox.Margin = page.PageInfo.Margin;

            page.Paragraphs.Add(floatbox);
            TextFragment textFragment = new TextFragment();
            
            //page.Paragraphs.Add(new TextFragment("Person"));
            //page.Paragraphs.Add(new TextFragment("Activity"));
            //page.Paragraphs.Add(new TextFragment("Expense"));
           // page.Paragraphs.Add(new TextFragment("Test"));
            //page.Paragraphs.Add(new HeaderArtifact("Fair Share"));
            
            TextSegment segment = new TextSegment();

            Aspose.Pdf.Heading headingUser = new Heading(1);
            headingUser.IsInList = true;
            headingUser.StartNumber = 1;
            headingUser.Text = "Person";
            headingUser.IsAutoSequence = true;

            floatbox.Paragraphs.Add(headingUser);

            Aspose.Pdf.Heading headingActivity = new Heading(1);
            headingActivity.IsInList = true;
            headingActivity.StartNumber = 13;
            headingActivity.Text = "Activity";
            headingActivity.IsAutoSequence = true;

            floatbox.Paragraphs.Add(headingActivity);

            Aspose.Pdf.Heading headingExpense = new Heading(1);
            headingActivity.IsInList = true;
            headingExpense.StartNumber = 1;
            headingExpense.Text = "Expenses";
            headingExpense.Style = NumberingStyle.NumeralsRomanLowercase;
            headingExpense.IsAutoSequence = true;

            floatbox.Paragraphs.Add(headingExpense);

            tblReceipt receipt = new tblReceipt();

            List<tblReceipt> bup = new List<tblReceipt>();
            //HttpClientHandler.GetReceiptsFromUser(id);

            DataTable dt = new DataTable("ReceiptPDF");
            dt.Columns.Add("Person",typeof(string));
            dt.Columns.Add("Activity", typeof(string));
            dt.Columns.Add("Expense", typeof(Int32));
            dt.Columns.Add("Test", typeof(string));
            DataRow dr = dt.NewRow();
            dr[0] = "Bob";
            dr[1] = "Testing"; //receipt.fldReceiptID;
            dr[2] = 200; //receipt.fldAmountPaid;
            dr[3] = "{bup}"; //bup.ToArray();
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "Steve";
            dr[1] = "Testing Again";
            dr[2] = 500;
            dr[3] = "lala";
            dt.Rows.Add(dr);

            document.Pages.Add();


            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.ColumnWidths = "40 100 100 100";
            table.Border = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightBlue));
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightBlue));
            table.ImportDataTable(dt, true,0,0,4,10);

            document.Pages[1].Paragraphs.Add(table);

            dataDir = dataDir + "TestingPdf.pdf";
            document.Save(dataDir);
            Console.WriteLine("\nDatabase integrated successfully.\nFile saved at " + dataDir);
            
        }

        public void text2()
        {
            
        }
  

    }


   

    

}
