using Aspose.Pdf.Text;
using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using System.Diagnostics;

namespace Admin_Client.Model.FileIO
{
    public class ReceiptPDF : IDisposable
    {
        #region Private Members
        private Color _textColor, _backColor;
        private readonly Font _timeNewRomanFont;
        private readonly TextBuilder _builder;
        private readonly Page _pdfPage;
        private readonly Document _pdfDocument;
        #endregion

        #region Get and Set
        public string ForegroundColor
        {
            get { return _textColor.ToString(); }
            set { _textColor = Color.Parse(value); }
        }
        public string BackgroundColor
        {
            get { return _backColor.ToString(); }
            set { _backColor = Color.Parse(value); }
        }
        #endregion

        #region Public Members
        public string ReceiptTitle;
        public List<string> ReceiptFrom;
        public List<string> ReceiptTo;
        public List<PersonPDF> People;
        public List<string> Details;
        public string Footer;
        #endregion

        public ReceiptPDF()
        {
            _pdfDocument = new Document();
            _pdfDocument.PageInfo.Margin.Left = 36;
            _pdfDocument.PageInfo.Margin.Right = 36;
            _pdfPage = _pdfDocument.Pages.Add();
            _textColor = Color.Black;
            _backColor = Color.Transparent;
            _timeNewRomanFont = FontRepository.FindFont("Times New Roman");
            _builder = new TextBuilder(_pdfPage);     
        }

        /// <summary>
        /// Loads all the sections for the PDF Page
        /// </summary>
        /// <param name="stream">Current active stream</param>
        public void Save(Stream stream)
        {
            HeaderSection(); //Title and Date
            AddressSection(); //TO and FROM
            GridSection(); //The table and Data
            TermsSection(); //Whatever message we want
            FooterSection(); //Link to website, for now I just put a google.com
            _pdfDocument.Save(stream);
        }
    
        /// <summary>
        /// Creates the default PDF Page
        /// </summary>
        /// <param name="trip">Chosen trip</param>
        public void GrabData(tblTrip trip)
        {
            DateTime datetime = DateTime.Now;
            string dataDir = @"C:\Users\Lars\Desktop\" + datetime.ToLongDateString() + ".pdf";

            // Get data to use
            List<PersonPDF> people = GetData(trip);

            var receiptPDF = new ReceiptPDF
            {
                ForegroundColor = "#4a7e79",
                BackgroundColor = "#FFFFFF",
                ReceiptTitle = "Dinner with Coworkers",
                ReceiptFrom = new List<string> { "Fair Share HQ", "Eastern Sønderborg", "Alsgade 44", "Denmark" },
                ReceiptTo = new List<string> { "Western Berlin", "Alsgade 0", "Germany" },
                People = people,
                Details = new List<string>
                {
                    "Final Message",
                    "Thanks for using Fair Share!",
                    string.Empty,
                    "If you have any questions regarding this receipt, please contact 74 44 85 85.\nPlease come again!",
                },
                Footer = "https://www.FairShare.com/"

            };
            var fileStream = new FileStream(dataDir, FileMode.OpenOrCreate);
            receiptPDF.Save(fileStream);
            fileStream.Close();
        }

        public List<PersonPDF> GetData(tblTrip trip)
        {
			Debug.WriteLine("Start");

			double total = 0;

			List<tblUserExpense> userExpenses = HttpClientHandler.GetUserExpensesFromTrip(trip);

			List<PersonPDF> people = new List<PersonPDF>();

			bool exists;
			foreach (var userExpense in userExpenses)
			{
                Debug.WriteLine("UserExpense: " + userExpense.fldNote);

				exists = false;

				tblUser user = HttpClientHandler.GetUser(userExpense.fldUserID);

				total += userExpense.fldExpense;

				// Check if user already has a record
				foreach (var userPerson in people)
				{
					Debug.WriteLine("UserPerson: " + userPerson.FirstName);
					if (userPerson.ID == userExpense.fldUserID)
					{
						Debug.WriteLine("Exists");
						userPerson.Expenses += userExpense.fldExpense;
						exists = true;
                    }
                }
				if (!exists)
				{
					Debug.WriteLine("Don't Exists");
					people.Add(new PersonPDF()
					{
						ID = user.fldUserID,
						FirstName = user.fldFirstName,
						LastName = user.fldLastName,
						Note = userExpense.fldNote,
						Expenses = userExpense.fldExpense
					});
				}
			}

			// Set total for all
			foreach (var person in people)
			{
				person.Total = total;
			}

			return people;
		}

        #region Sections
        private void HeaderSection()
        {
            var lines = new TextFragment[3];
            lines[0] = new TextFragment($"RECEIPT FOR {ReceiptTitle}");
            lines[0].TextState.FontSize = 20;
            lines[0].TextState.ForegroundColor = _textColor;
            lines[0].HorizontalAlignment = HorizontalAlignment.Left;

            _pdfPage.Paragraphs.Add(lines[0]);

            lines[1] = new TextFragment($"Date: {DateTime.Today:dd/MM/yyyy}");
            lines[1].TextState.Font = _timeNewRomanFont;
            lines[1].TextState.FontSize = 12;
            lines[1].HorizontalAlignment = HorizontalAlignment.Right;
            //_pdfPage.Paragraphs.Add(lines[1]);
            
            lines[2] = new TextFragment($"Contact: 74 44 85 85");
            for (var i = 1; i < lines.Length; i++)
            {
                //text properties
                lines[i].TextState.Font = _timeNewRomanFont;
                lines[i].TextState.FontSize = 12;
                lines[i].HorizontalAlignment = HorizontalAlignment.Right;

                //fragment to paragraph
                _pdfPage.Paragraphs.Add(lines[i]);
            }
            
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
                fragment.TextState.FontStyle = FontStyles.Bold;
                box.Paragraphs.Add(fragment);
            }

            fragment = new TextFragment("RECEIPT TO:") { IsFirstParagraphInColumn = true };
            fragment.TextState.Font = _timeNewRomanFont;
            fragment.TextState.FontSize = 12;
            fragment.TextState.FontStyle = FontStyles.Bold;
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;
            box.Paragraphs.Add(fragment);

            foreach (var str in ReceiptTo)
            {
                fragment = new TextFragment(str);
                fragment.TextState.Font = _timeNewRomanFont;
                fragment.TextState.FontSize = 12;
                fragment.TextState.FontStyle = FontStyles.Bold;
                fragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;
                box.Paragraphs.Add(fragment);
            }
            _pdfPage.Paragraphs.Add(box);
        }
        private void GridSection()
        {
            var table = new Table
            {
                //How large the columns are
                ColumnWidths = "26 78 78 78 78 78",
                Border = new BorderInfo(BorderSide.Box, 1f, _textColor),
                DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.5f, _textColor),
                DefaultCellPadding = new MarginInfo(4.5, 4.5, 4.5, 4.5),
                Margin = { Bottom = 10 },
                HorizontalAlignment = HorizontalAlignment.Center,
                DefaultCellTextState = { Font = _timeNewRomanFont }
            };

            var headerRow = table.Rows.Add();
            var cell = headerRow.Cells.Add("#");
            cell.Alignment = HorizontalAlignment.Center;
            headerRow.Cells.Add("First Name");
            headerRow.Cells.Add("Last Name");
            headerRow.Cells.Add("Bought");
            headerRow.Cells.Add("Paid");
            headerRow.Cells.Add("Group Total");

            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = _textColor;
                headerRowCell.DefaultCellTextState.ForegroundColor = _backColor;
                headerRowCell.Alignment= HorizontalAlignment.Center;
            }
            foreach (var user in People) 
            {
                //Adds the Data from Database to populate the rows in the table
                var row = table.Rows.Add();
                cell = row.Cells.Add(user.ID.ToString());
                cell.Alignment = HorizontalAlignment.Center;
                cell = row.Cells.Add(user.FirstName);
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.LastName);
                cell.Alignment = HorizontalAlignment.Right;
				cell = row.Cells.Add(user.Note);
				cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.Expenses.ToString());
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(user.Total.ToString());
                cell.Alignment = HorizontalAlignment.Right;
            }
            _pdfPage.Paragraphs.Add(table);

        }
        private void TermsSection()
        {
            foreach (var detail in Details)
            {
                var fragment = new TextFragment(detail);
                fragment.TextState.Font = _timeNewRomanFont;
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Color.CadetBlue;
                fragment.HorizontalAlignment= HorizontalAlignment.Center;
                _pdfPage.Paragraphs.Add(fragment);
            }
        }
        private void FooterSection()
        {
            var fragment = new TextFragment(Footer);
            var len = fragment.TextState.MeasureString(fragment.Text);
            fragment.Position = new Aspose.Pdf.Text.Position(_pdfPage.PageInfo.Width / 2 - len / 2, 10);
            fragment.Hyperlink = new WebHyperlink(Footer);
            var builder = new TextBuilder(_pdfPage);
            builder.AppendText(fragment);
        }
        #endregion

       
        #region IDisposable Support
        private bool disposedValue = false; //Detect redundant cells
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
