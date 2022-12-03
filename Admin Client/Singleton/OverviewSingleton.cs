using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
    public class OverviewSingleton
    {
        private static OverviewSingleton instance = new OverviewSingleton();
        private static int GroupID = 0;



        private OverviewSingleton()
        {

        }


        public void SetGroupID(int groupID)
        {
            GroupID = groupID;

        }
        public int GetGroupID()
        {
            return GroupID;
        }

        public static OverviewSingleton getInstance()
        {
            return instance;
        }

    }
}
