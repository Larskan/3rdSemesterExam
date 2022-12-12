using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	public class Receipt
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string GroupName { get; set; }

		public string ProjectedValue { get; set; }

		public string AmountPaid { get; set; }

		public Receipt(tblReceipt receipt)
		{
			// TODO
			this.Id = 1;
			this.Name = "Spain trip";
			this.GroupName = "Butiful girls";
			this.ProjectedValue = 23.34 + " DKK";
			this.AmountPaid = 18.44 + "DKK";

			PdfDocument document = new PdfDocument();
			PdfPage pageOne = document.AddPage();
			XGraphics gfx = XGraphics.FromPdfPage(pageOne);
			XFont font = new XFont("Verdana",20,XFontStyle.Bold);
			gfx.DrawString("Fair Share Receipt", font, XBrushes.Black, new XRect(0, 0, pageOne.Width, pageOne.Height), XStringFormat.Center);
			string filename = "{receipt}.pdf";
			document.Save(filename);
					}
	}
}
