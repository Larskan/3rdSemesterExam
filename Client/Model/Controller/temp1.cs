using Client.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model._1_Controller
{
	public class temp1
	{
        public void Main(string[] args)
        {
            AddingGroup(3, "LarsKan", true);
        }
        //simple test to see if it actually makes a new entry for DB in tblGroup

        public void AddingGroup(int ID, string name, bool temp)
        {
            var context = new FairShareDBEntities();
            var group = new tblGroup()
            {
                fldGroupID = ID,
                fldGroupName = name,
                fldTempBool = temp
            };
            context.tblGroup.Add(group);
            context.SaveChanges();

        }
    }
}
