using Admin_Client.Model.DB;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model
{
	public static class FAKEDATABASE
	{

		#region Groups

		public static List<TblGroup> tblGroups = new List<TblGroup> {
				new TblGroup { FldGroupId = 1, FldGroupName = "Group1", FldGroupBoolean = false},
				new TblGroup { FldGroupId = 2, FldGroupName = "Group2", FldGroupBoolean = false},
				new TblGroup { FldGroupId = 3, FldGroupName = "Group3", FldGroupBoolean = false},
				new TblGroup { FldGroupId = 4, FldGroupName = "Group4T", FldGroupBoolean = true},
				new TblGroup { FldGroupId = 5, FldGroupName = "Group5T", FldGroupBoolean = true}
		};

		public static List<TblGroup> GetGroups()
		{
			return tblGroups;
		}

		public static TblGroup GetGroup(int FldGroupId)
		{
			foreach (var item in tblGroups)
			{
				if (item.FldGroupId == FldGroupId)
				{
					return item;
				}
			}
			return null;
		}

		public static void EditGroup(TblGroup tblGroup)
		{
			foreach (var item in tblGroups)
			{
				if (item.FldGroupId == tblGroup.FldGroupId)
				{
					item.FldGroupName = tblGroup.FldGroupName;
					item.FldGroupBoolean = tblGroup.FldGroupBoolean;
				}
			}
		}

		public static void DeleteGroup(int FldGroupId)
		{
			foreach (var item in tblGroups)
			{
				if (item.FldGroupId == FldGroupId)
				{
					tblGroups.Remove(item);
				}
			}
		}
        public static int GetGroupID(TblGroup tblGroup)
        {
            foreach (var item in tblGroups)
            {
                if (item.FldGroupId == tblGroup.FldGroupId)
                {
                    return item.FldGroupId;	
                }
            }

            //fix return? although this return will never be reached
            return 0;
        }

        #endregion

        #region Users

        public static List<TblUser> tblUsers = new List<TblUser> {
				new TblUser { FldUserId = 1, FldEmail = "email1", FldFirstName = "firstname1", FldLastName = "lastname1", FldPhonenumber = 10101010, FldIsAdmin = false },
				new TblUser { FldUserId = 2, FldEmail = "email2", FldFirstName = "firstname2", FldLastName = "lastname2", FldPhonenumber = 20202020, FldIsAdmin = false },
				new TblUser { FldUserId = 3, FldEmail = "email3", FldFirstName = "firstname3", FldLastName = "lastname3", FldPhonenumber = 30303030, FldIsAdmin = false },
				new TblUser { FldUserId = 4, FldEmail = "email4", FldFirstName = "firstname4", FldLastName = "lastname4", FldPhonenumber = 40404040, FldIsAdmin = false },
				new TblUser { FldUserId = 5, FldEmail = "email5", FldFirstName = "firstname5", FldLastName = "lastname5", FldPhonenumber = 50505050, FldIsAdmin = true }
		};

		public static List<TblUser> GetUsers()
		{
			return tblUsers;
		}

		public static TblUser GetUser(int FldUserId)
		{
			foreach (var item in tblUsers)
			{
				if (item.FldUserId == FldUserId)
				{
					return item;
				}
			}
			return null;
		}

		

        public static void EditUser(TblUser tblUser)
		{
			foreach (var item in tblUsers)
			{
				if (item.FldUserId == tblUser.FldUserId)
				{
					item.FldFirstName = tblUser.FldFirstName;
					item.FldLastName = tblUser.FldLastName;

					item.FldPhonenumber = tblUser.FldPhonenumber;
					item.FldEmail = tblUser.FldEmail;
				}
			}
		}

		public static void DeleteUser(int FldUserId)
		{
			foreach (var item in tblUsers)
			{
				if (item.FldUserId == FldUserId)
				{
					tblUsers.Remove(item);
				}
			}
		}
        public static int GetUserID(TblUser tblUser)
        {
            foreach (var item in tblUsers)
            {
                if (item.FldUserId == tblUser.FldUserId)
                {
                    return item.FldUserId;
                }
            }
            return 0;
        }

        #endregion

        #region Receipt

        public static List<TblReceipt> tblReceipts = new List<TblReceipt>
		{
			new TblReceipt{ FldReceiptId = 1, FldUserId = 1, FldAmountPaid = 500, FldTripId = 1 },
			new TblReceipt{ FldReceiptId = 1, FldUserId = 1, FldAmountPaid = 800, FldTripId = 1 },
			new TblReceipt{ FldReceiptId = 1, FldUserId = 1, FldAmountPaid = 1000, FldTripId = 2 }
		};

        public static List<TblReceipt> GetReceipts(TblUser user)
        {
			List<TblReceipt> userReceipts = new List<TblReceipt>();

            foreach (var item in tblReceipts)
			{
				if (user.FldUserId == item.FldUserId)
				{
					userReceipts.Add(item);
				}
			}
            return userReceipts;
        }

        public static TblUser GetReceipt(int FldReceiptId)
        {
            foreach (var item in tblUsers)
            {
                if (item.FldUserId == FldReceiptId)
                {
                    return item;
                }
            }
            return null;
        }

        public static void EditReceipt(TblReceipt tblReceipt)
        {
            foreach (var item in tblReceipts)
            {
				if (item.FldReceiptId == tblReceipt.FldReceiptId);
                {
                    item.FldUserId = tblReceipt.FldUserId;
                    item.FldAmountPaid = tblReceipt.FldAmountPaid;

                    item.FldProjectedValue = tblReceipt.FldProjectedValue;
                    item.FldTripId = tblReceipt.FldTripId;
                }
            }
        }

        public static void DeleteReceipt(int FldReceiptId)
        {
            foreach (var item in tblReceipts)
            {
                if (item.FldReceiptId == FldReceiptId)
                {
                    tblReceipts.Remove(item);
                }
            }
        }

        #endregion

    }
}
