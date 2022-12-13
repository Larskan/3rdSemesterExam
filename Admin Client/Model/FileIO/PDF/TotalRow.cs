using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.PDF
{
    //Render row in totals subsection
    public class TotalRow
    {
        public string Text;
        public decimal Value;

        public TotalRow(string text, decimal value)
        {
            Text = text;
            Value = value;
        }
    }
}
