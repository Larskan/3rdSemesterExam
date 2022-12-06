using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
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

		public static List<tblGroup> tblGroups = new List<tblGroup> {
				new tblGroup { fldGroupID = 1, fldGroupName = "Group1", fldGroupBoolean = false},
				new tblGroup { fldGroupID = 2, fldGroupName = "Group2", fldGroupBoolean = false},
				new tblGroup { fldGroupID = 3, fldGroupName = "Group3", fldGroupBoolean = false},
				new tblGroup { fldGroupID = 4, fldGroupName = "Group4T", fldGroupBoolean = true},
				new tblGroup { fldGroupID = 5, fldGroupName = "Group5T", fldGroupBoolean = true}
		};

		public static List<tblGroup> GetGroups()
		{
			return tblGroups;
		}

		public static tblGroup GetGroup(int fldGroupID)
		{
			foreach (var item in tblGroups)
			{
				if (item.fldGroupID == fldGroupID)
				{
					return item;
				}
			}
			return null;
		}

		public static void EditGroup(tblGroup tblGroup)
		{
			foreach (var item in tblGroups)
			{
				if (item.fldGroupID == tblGroup.fldGroupID)
				{
					item.fldGroupName = tblGroup.fldGroupName;
					item.fldGroupBoolean = tblGroup.fldGroupBoolean;
				}
			}
		}

		public static void DeleteGroup(int fldGroupID)
		{
			foreach (var item in tblGroups)
			{
				if (item.fldGroupID == fldGroupID)
				{
					tblGroups.Remove(item);
				}
			}
		}
		public static int GetGroupID(tblGroup tblGroup)
		{
			foreach (var item in tblGroups)
			{
				if (item.fldGroupID == tblGroup.fldGroupID)
				{
					return item.fldGroupID;
				}
			}

			//fix return? although this return will never be reached
			return 0;
		}

		#endregion

		#region Users

		public static List<tblUser> tblUsers = new List<tblUser> {
				new tblUser { fldUserID = 1, fldEmail = "email1", fldFirstName = "firstname1", fldLastName = "lastname1", fldPhonenumber = 10101010, fldIsAdmin = false },
				new tblUser { fldUserID = 2, fldEmail = "email2", fldFirstName = "firstname2", fldLastName = "lastname2", fldPhonenumber = 20202020, fldIsAdmin = false },
				new tblUser { fldUserID = 3, fldEmail = "email3", fldFirstName = "firstname3", fldLastName = "lastname3", fldPhonenumber = 30303030, fldIsAdmin = false },
				new tblUser { fldUserID = 4, fldEmail = "email4", fldFirstName = "firstname4", fldLastName = "lastname4", fldPhonenumber = 40404040, fldIsAdmin = false },
				new tblUser { fldUserID = 5, fldEmail = "email5", fldFirstName = "firstname5", fldLastName = "lastname5", fldPhonenumber = 50505050, fldIsAdmin = true }
		};

		public static List<tblUser> GetUsers()
		{
			return tblUsers;
		}

		public static tblUser GetUser(int fldUserID)
		{
			foreach (var item in tblUsers)
			{
				if (item.fldUserID == fldUserID)
				{
					return item;
				}
			}
			return null;
		}

		public static void EditUser(tblUser tblUser)
		{
			foreach (var item in tblUsers)
			{
				if (item.fldUserID == tblUser.fldUserID)
				{
					item.fldFirstName = tblUser.fldFirstName;
					item.fldLastName = tblUser.fldLastName;

					item.fldPhonenumber = tblUser.fldPhonenumber;
					item.fldEmail = tblUser.fldEmail;
				}
			}
		}

		public static void DeleteUser(int fldUserID)
		{
			foreach (var item in tblUsers)
			{
				if (item.fldUserID == fldUserID)
				{
					tblUsers.Remove(item);
				}
			}
		}
		public static int GetUserID(tblUser tblUser)
		{
			foreach (var item in tblUsers)
			{
				if (item.fldUserID == tblUser.fldUserID)
				{
					return item.fldUserID;
				}
			}
			return 0;
		}

		#endregion

		#region Receipt

		public static List<tblReceipt> tblReceipts = new List<tblReceipt>
		{
			new tblReceipt{ fldReceiptID = 1, fldUserID = 1, fldAmountPaid = 500, fldTripID = 1 },
			new tblReceipt{ fldReceiptID = 1, fldUserID = 1, fldAmountPaid = 800, fldTripID = 1 },
			new tblReceipt{ fldReceiptID = 1, fldUserID = 1, fldAmountPaid = 1000, fldTripID = 2 }
		};

		public static List<tblReceipt> GetReceipts(tblUser user)
		{
			List<tblReceipt> userReceipts = new List<tblReceipt>();

			foreach (var item in tblReceipts)
			{
				if (user.fldUserID == item.fldUserID)
				{
					userReceipts.Add(item);
				}
			}
			return userReceipts;
		}

		public static tblUser GetReceipt(int fldReceiptId)
		{
			foreach (var item in tblUsers)
			{
				if (item.fldUserID == fldReceiptId)
				{
					return item;
				}
			}
			return null;
		}

		public static void EditReceipt(tblReceipt tblReceipt)
		{
			foreach (var item in tblReceipts)
			{
				if (item.fldReceiptID == tblReceipt.fldReceiptID) ;
				{
					item.fldUserID = tblReceipt.fldUserID;
					item.fldAmountPaid = tblReceipt.fldAmountPaid;

					item.fldProjectedValue = tblReceipt.fldProjectedValue;
					item.fldTripID = tblReceipt.fldTripID;
				}
			}
		}

		public static void DeleteReceipt(int fldReceiptId)
		{
			foreach (var item in tblReceipts)
			{
				if (item.fldReceiptID == fldReceiptId)
				{
					tblReceipts.Remove(item);
				}
			}
		}

		#endregion

	}
}
