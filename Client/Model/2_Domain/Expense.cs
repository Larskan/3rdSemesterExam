using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model._2_Domain
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public double ExpenseAmount { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }

    }
}
