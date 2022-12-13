using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.FileIO.TemplatePDF
{
    public class TemplateTotalRow
    {
        public string Text;
        public decimal Value;

        public TemplateTotalRow(string text, decimal value)
        {
            Text = text;
            Value = value;
        }
    }
}
