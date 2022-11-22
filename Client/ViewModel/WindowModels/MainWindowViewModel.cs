using Client.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.WindowModels
{
	public class MainWindowViewModel
	{

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
