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

    //Prototype
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

            DataTable dt = new DataTable("TemplateReceiptPDF");
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
    } 
}
