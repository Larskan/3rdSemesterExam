using Aspose.Pdf.Text;
using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;

namespace Admin_Client.Model.FileIO.PDF
{
    public class ReceiptPDF : IDisposable
    {
        #region private members
        private static readonly Aspose.Pdf.License License = new Aspose.Pdf.License();
        private Aspose.Pdf.Color _textColor, _backColor;
        private readonly Aspose.Pdf.Text.Font _timeNewRomanFont;
        private readonly TextBuilder _builder;
        private readonly Aspose.Pdf.Page _pdfPage;
        private readonly Aspose.Pdf.Document _pdfDocument;
        private readonly Aspose.Pdf.Rectangle _logoPlaceHolder;
        #endregion

        #region Get and Set
        public string ForegroundColor
        {
            get { return _textColor.ToString(); }
            set { _textColor = Aspose.Pdf.Color.Parse(value); }
        }
        public string BackgroundColor
        {
            get { return _backColor.ToString(); }
            set { _backColor = Aspose.Pdf.Color.Parse(value); }
        }
        #endregion

        #region Receipt details
        public string Number;
        public LogoImage Logo;
        public List<string> ReceiptFrom;
        public List<string> ReceiptTo;
        public List<UserPersonPDF> People;
        public List<TotalRow> Totals;
        public List<string> Details;
        public string Footer;
        #endregion

        public ReceiptPDF()
        {
            _pdfDocument = new Document();
            _pdfDocument.PageInfo.Margin.Left = 36;
            _pdfDocument.PageInfo.Margin.Right = 36;
            _pdfPage = _pdfDocument.Pages.Add();
            _textColor = Aspose.Pdf.Color.Black;
            _backColor = Aspose.Pdf.Color.Transparent;
            _logoPlaceHolder = new Rectangle(20, 700, 120, 800);
            _timeNewRomanFont = FontRepository.FindFont("Times New Roman");
            _builder = new TextBuilder(_pdfPage);     
        }

        public void Save(Stream stream)
        {
            HeaderSection(); //Logo and Title and Date
            AddressSection(); //TO and FROM
            //GridSection(); //The table and Data
            ReplaceGridSection(); //The table and Data
            TermsSection(); //Whatever message we want
            FooterSection(); //Link to website, for now I just put a google.com
            _pdfDocument.Save(stream);
        }

        

        //The current magic
        public void GrabData(tblTrip trip)
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\Exam\Receipt_" + datetime.ToLongDateString() + ".pdf";

            // Get data to use
            List<UserPersonPDF> userPeople = GetData(trip);

            var receiptPDF = new ReceiptPDF
            {
                ForegroundColor = "#0000CC",
                BackgroundColor = "#FFFFFF",
                Number = "1",
                Logo = new LogoImage(@"C:\Users\Lars\Desktop\Exam\FairShareLogo.png", 160, 120),
                ReceiptFrom = new List<string> { "Fair Share HQ", "Eastern Sønderborg", "Alsgade 44", "Denmark" },
                ReceiptTo = new List<string> { "Western Sønderborg", "Alsgade 0", "Germany" },
                People = userPeople,
                Details = new List<string>
                {
                    "Terms & Conditions",
                    "Thanks for using Fair Share",
                    string.Empty,
                    "If you have any questions regarding this receipt, you dun goofed","","Thx for using us."
                },
                Footer = "https://www.google.com/"

            };
            var fileStream = new FileStream(dataDir, FileMode.OpenOrCreate);
            receiptPDF.Save(fileStream);
            fileStream.Close();
        }

        public List<UserPersonPDF> GetData(tblTrip trip)
        {
			double total = 0;
			List<tblReceipt> receipts;
			List<tblUserExpense> userExpenses = HttpClientHandler.GetUserExpensesFromTrip(trip);
			List<UserPersonPDF> userPeople = new List<UserPersonPDF>();

			bool exists = false;
			foreach (var userExpense in userExpenses)
			{
				tblUser user = HttpClientHandler.GetUser((int)userExpense.fldUserID);
				receipts = HttpClientHandler.GetReceiptsFromUser(user);
				foreach (var receipt in receipts)
				{
					total += receipt.fldAmountPaid;

					//check if user already has a record
					foreach (var userPerson in userPeople)
					{
						if (userPerson.ID == receipt.fldUserID)
						{
							userPerson.Expenses += receipt.fldAmountPaid;
						}
					}
					if (!exists)
					{
						userPeople.Add(new UserPersonPDF()
						{
							ID = user.fldUserID,
							FirstName = user.fldFirstName,
							LastName = user.fldLastName,
							TripName = trip.fldTripName,
							Expenses = (double)userExpense.fldExpense
						});
					}
				}
				//total for all
				foreach (var userPerson in userPeople)
				{
					userPerson.Total = total;
				}
			}

			return userPeople;
		}

        #region Sections
        private void HeaderSection()
        {
            var lines = new TextFragment[3];
            //create text fragment
            lines[0] = new TextFragment($"RECEIPT #{Number}");
            lines[0].TextState.FontSize = 20;
            lines[0].TextState.ForegroundColor = _textColor;
            lines[0].HorizontalAlignment = HorizontalAlignment.Center;

            _pdfPage.Paragraphs.Add(lines[0]);

            lines[1] = new TextFragment($"DATE: {DateTime.Today:dd/MM/yyyy}");
            lines[2] = new TextFragment($"COMPLAIN DATE: {DateTime.Today.AddDays(7):dd/MM/yyyy}");
            for (var i = 1; i < lines.Length; i++)
            {
                //text properties
                lines[i].TextState.Font = _timeNewRomanFont;
                lines[i].TextState.FontSize = 12;
                lines[i].HorizontalAlignment = HorizontalAlignment.Right;

                //fragment to paragraph
                _pdfPage.Paragraphs.Add(lines[i]);
            }
            //Logo, set coords
            _logoPlaceHolder.URX = _logoPlaceHolder.LLX + Logo.Width;
            _logoPlaceHolder.URY = _logoPlaceHolder.LLY + Logo.Height;

            //load image into stream
            var imageStream = new FileStream(Logo.FileName, FileMode.Open);
            //add image to images collection of page resources
            _pdfPage.Resources.Images.Add(imageStream);

            #region needs to be fixed to make logo work
            //Use GSave operation: saves current graphics state
            //_pdfPage.Contents.Add(new Operator.GSave());
            //create rectangle and matrix objects
            //var matrix = new Matrix(new[] { _logoPlaceHolder.URX - _logoPlaceHolder.LLX, 0, 0, _logoPlaceHolder.URY - _logoPlaceHolder.LLY, _logoPlaceHolder.LLX, _logoPlaceHolder.LLY });
            //use concatenatematrix operator: defines how image should be placed
            // _pdfPage.Contents.Add(new Operator.ConcatenateMatrix(matrix));
            // var ximage = _pdfPage.Resources.Images[_pdfPage.Resources.Images.Count];
            // _pdfPage.Contents.Add(new Operator.Do(ximage.Name));
            // _pdfPage.Contents.Add(new Operator.GRestore());
            #endregion
        }
        private void AddressSection()
        {
            var box = new FloatingBox(524, 120)
            {
                ColumnInfo =
                {
                    ColumnCount = 2,
                    ColumnWidths = "252 252"
                },
                Padding = { Top = 20 }
            };
            TextFragment fragment;

            ReceiptFrom.Insert(0, "FROM:");
            foreach (var str in ReceiptFrom)
            {
                fragment = new TextFragment(str);
                fragment.TextState.Font = _timeNewRomanFont;
                fragment.TextState.FontSize = 12;
                box.Paragraphs.Add(fragment);
            }

            fragment = new TextFragment("RECEIPT TO:") { IsFirstParagraphInColumn = true };
            fragment.TextState.Font = _timeNewRomanFont;
            fragment.TextState.FontSize = 12;
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;
            box.Paragraphs.Add(fragment);

            foreach (var str in ReceiptTo)
            {
                fragment = new TextFragment(str);
                fragment.TextState.Font = _timeNewRomanFont;
                fragment.TextState.FontSize = 12;
                fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;
                box.Paragraphs.Add(fragment);
            }
            _pdfPage.Paragraphs.Add(box);
        }

        //Currently used GridSection
        private void ReplaceGridSection()
        {
            var table = new Table
            {
                //The height of the 5 sections
                ColumnWidths = "26 257 78 70 78",
                Border = new BorderInfo(BorderSide.Box, 1f, _textColor),
                DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.5f, _textColor),
                DefaultCellPadding = new MarginInfo(4.5, 4.5, 4.5, 4.5),
                Margin = { Bottom = 10 },
                DefaultCellTextState = { Font = _timeNewRomanFont }
            };

            var headerRow = table.Rows.Add();
            var cell = headerRow.Cells.Add("#");
            cell.Alignment = HorizontalAlignment.Center;
            headerRow.Cells.Add("First Name");
            headerRow.Cells.Add("Last Name");
            headerRow.Cells.Add("Trip Name");
            headerRow.Cells.Add("Expenses");
            headerRow.Cells.Add("Total");

            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = _textColor;
                headerRowCell.DefaultCellTextState.ForegroundColor = _backColor;
            }
            foreach (var user in People)
            {
                var row = table.Rows.Add();
                cell = row.Cells.Add(user.ID.ToString());
                cell.Alignment = HorizontalAlignment.Center;
                row.Cells.Add(user.FirstName);
                cell = row.Cells.Add(user.LastName);
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.TripName);
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.Expenses.ToString());
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.Total.ToString());
                cell.Alignment = HorizontalAlignment.Right;
            }


            /*
            foreach(var totalRow in Totals)
            {
                var row = table.Rows.Add();
                var nameCell = row.Cells.Add(totalRow.Text);
                nameCell.Alignment= HorizontalAlignment.Right;
                var textCell = row.Cells.Add(totalRow.Value.ToString());
                textCell.Alignment = HorizontalAlignment.Right;

            }
            */

            _pdfPage.Paragraphs.Add(table);

        }
        private void TermsSection()
        {
            foreach (var detail in Details)
            {
                var fragment = new TextFragment(detail);
                fragment.TextState.Font = _timeNewRomanFont;
                fragment.TextState.FontSize = 12;
                _pdfPage.Paragraphs.Add(fragment);
            }
        }
        private void FooterSection()
        {
            var fragment = new TextFragment(Footer);
            var len = fragment.TextState.MeasureString(fragment.Text);
            fragment.Position = new Aspose.Pdf.Text.Position(_pdfPage.PageInfo.Width / 2 - len / 2, 20);
            fragment.Hyperlink = new WebHyperlink(Footer);
            var builder = new TextBuilder(_pdfPage);
            builder.AppendText(fragment);
        }
        #endregion

        #region Unused Methods for reference
        //Unused
        private void GridSection()
        {
            var table = new Aspose.Pdf.Table
            {
                //The height of the 5 sections
                ColumnWidths = "26 257 78 78 78",
                Border = new BorderInfo(BorderSide.Box, 1f, _textColor),
                DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.5f, _textColor),
                DefaultCellPadding = new MarginInfo(4.5, 4.5, 4.5, 4.5),
                Margin = { Bottom = 10 },
                DefaultCellTextState = { Font = _timeNewRomanFont }

            };

            var headerRow = table.Rows.Add();
            var cell = headerRow.Cells.Add("First Name");
            cell.Alignment = HorizontalAlignment.Center;
            headerRow.Cells.Add("Last Name");
            headerRow.Cells.Add("Expenses");
            headerRow.Cells.Add("Sum");
            headerRow.Cells.Add("Rest");

            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = _textColor;
                headerRowCell.DefaultCellTextState.ForegroundColor = _backColor;
            }
            foreach (var peopleObject in People)
            {
                var row = table.Rows.Add();
                cell = row.Cells.Add(peopleObject.FirstName);
                cell.Alignment = HorizontalAlignment.Center;
                row.Cells.Add(peopleObject.LastName);
                cell = row.Cells.Add(peopleObject.Expenses.ToString("C2"));
                cell.Alignment = HorizontalAlignment.Right;
                //  cell = row.Cells.Add(peopleObject.Sum.ToString());
                // cell.Alignment = HorizontalAlignment.Right;
                // cell = row.Cells.Add(peopleObject.Rest.ToString("C2"));
                // cell.Alignment = HorizontalAlignment.Right;
            }

            foreach (var totalRow in Totals)
            {
                var row = table.Rows.Add();
                var nameCell = row.Cells.Add(totalRow.Text);
                nameCell.ColSpan = 4;
                var textCell = row.Cells.Add(totalRow.Value.ToString("C2"));
                textCell.Alignment = HorizontalAlignment.Right;
            }

            _pdfPage.Paragraphs.Add(table);

        }

        //Unused
        public void SecondPlaceHolder()
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\Exam\Receipt_" + datetime.ToLongDateString() + ".pdf";

            var persons = new List<Person>
            {
                new Person(1,"Bob","Bobsen",500,1600),
                new Person(2,"Steve", "Jeff", 300,1600),
                new Person(3,"Tim", "Timsen", 800,1600)
            };
            var receipt = new ReceiptPDF
            {
                ForegroundColor = "#0000CC",
                BackgroundColor = "#FFFFFF",
                Number = "1",
                Logo = new LogoImage(@"C:\Users\Lars\Desktop\Exam\FairShareLogo.png", 160, 120),
                ReceiptFrom = new List<string> { "Fair Share HQ", "Eastern Sønderborg", "Alsgade 44", "Denmark" },
                ReceiptTo = new List<string> { "Western Sønderborg", "Alsgade 0", "Germany" },
                // People = persons,
                Details = new List<string>
                {
                    "Terms & Conditions",
                    "Thanks for using Fair Share",
                    string.Empty,
                    "If you have any questions regarding this receipt, you dun goofed","","Thx for using us."
                },
                Footer = "https://www.google.com/"

            };
            var fileStream = new FileStream(dataDir, FileMode.OpenOrCreate);
            receipt.Save(fileStream);
            fileStream.Close();

        }
        //Unused
        public void PlaceholderData()
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\Exam\Receipt_" + datetime.ToLongDateString() + ".pdf";
            var persons = new List<Person>
            {
                new Person(1,"Bob","Bobsen",500,1600),
                new Person(2,"Steve", "Jeff", 300,1600),
                new Person(3,"Tim", "Timsen", 800,1600)
            };
            var subTotal = persons.Sum(i => i.Rest);
            var receipt = new ReceiptPDF
            {
                ForegroundColor = "#0000CC",
                BackgroundColor = "#FFFFFF",
                Number = "1",
                Logo = new LogoImage(@"C:\Users\Lars\Desktop\Exam\FairShareLogo.png", 160, 120),
                ReceiptFrom = new List<string> { "Fair Share HQ", "Eastern Sønderborg", "Alsgade 44", "Denmark" },
                ReceiptTo = new List<string> { "Western Sønderborg", "Alsgade 0", "Germany" },
                //People = persons,

                Totals = new List<TotalRow>
                {
                    new TotalRow("Sub Rest", subTotal),
                    new TotalRow("VAR @ 20%", subTotal * 0.2M),
                    new TotalRow("Rest", subTotal * 1.2M)
                },

                Details = new List<string>
                {
                    "Terms & Conditions",
                    "Thanks for using Fair Share",
                    string.Empty,
                    "If you have any questions regarding this receipt, you dun goofed","","Thx for using us."
                },
                Footer = "https://www.google.com/"

            };
            var fileStream = new FileStream(dataDir, FileMode.OpenOrCreate);
            receipt.Save(fileStream);
            fileStream.Close();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; //Detect reduncant cells
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _pdfPage.Dispose();
                    _pdfDocument.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
