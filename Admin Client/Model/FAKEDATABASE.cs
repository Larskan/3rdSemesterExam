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

		#endregion

		#region Users

		public static List<TblUser> tblUsers = new List<TblUser> {
				new TblUser { FldUserId = 1, FldEmail = "email1", FldFirstName = "firstname1", FldLastName = "lastname1", FldPhonenumber = 10101010 },
				new TblUser { FldUserId = 2, FldEmail = "email2", FldFirstName = "firstname2", FldLastName = "lastname2", FldPhonenumber = 20202020 },
				new TblUser { FldUserId = 3, FldEmail = "email3", FldFirstName = "firstname3", FldLastName = "lastname3", FldPhonenumber = 30303030 },
				new TblUser { FldUserId = 4, FldEmail = "email4", FldFirstName = "firstname4", FldLastName = "lastname4", FldPhonenumber = 40404040 },
				new TblUser { FldUserId = 5, FldEmail = "email5", FldFirstName = "firstname5", FldLastName = "lastname5", FldPhonenumber = 50505050 }
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

		public static void EditGroup(TblUser tblUser)
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

		#endregion

	}
}
