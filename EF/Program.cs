using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing if I can create a new Group
            var context = new FairShareDBEntities();
            var group = new tblGroup()
            {
                fldGroupID = 1,
                fldGroupName = "Lars Test",
                fldTempBool = true
            };
            context.tblGroup.Add(group);
            context.SaveChanges();
            //Added to DB successfully
            
        }
    }
}
