using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
    public class UserviewSingleton
    {
        private static UserviewSingleton instance = new UserviewSingleton();
        public static int userID = 0;

        public void SetUserID(int UserID)
        {
            userID = UserID;

        }
        public int GetUserID()
        {
            return userID;
        }





        private UserviewSingleton()
        {

        }
        public static UserviewSingleton getInstance()
        {
            return instance;
        }
    }
}
