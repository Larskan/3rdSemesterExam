using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model._2_Domain
{
    public class Receipt
    {

        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Trip Trip { get; set; }
        public double ProjectedValue { get; set; }
        public double AmountPayed { get; set; }

    }
}
