using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.Model._2_Domain
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Temp { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Trip> Trips { get; set; }

    }
}
