using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model._2_Domain
{
    public class Trip
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Sum { get; set; }
        public ICollection<Receipt> Receipts { get; set; }

    }
}
