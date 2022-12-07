using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.Model.DB
{
	public static class HttpClientHandler
	{

		#region Variables

		public static tblUser currentUser = null;

		public static tblUser loadedUser = null;
		public static tblGroup loadedGroup = null;
		public static tblTrip loadedTrip = null;
		public static tblUserExpense loadedUserExpense = null;
		public static tblReceipt loadedReceipt = null;

		public static List<tblUser> loadedUsers = new List<tblUser>();
		public static List<tblGroup> loadedGroups = new List<tblGroup>();
		public static List<tblTrip> loadedTrips = new List<tblTrip>();
		public static List<tblUserExpense> loadedUserExpenses = new List<tblUserExpense>();
		public static List<tblReceipt> loadedReceipts = new List<tblReceipt>();

		#endregion

		#region Login

		public static bool Login(string username, string password)
		{

			foreach (var item in GetUsers())
			{
				
			}

			return false;
		}

		#endregion

		#region Get

		#region tblUser

		static bool getUserIsDone = false;
		static object getUserObject = null;
		public static tblUser GetUser(int ID)
		{
			if (loadedUser != null)
			{
				loadedUser = null;
			}
			if (getUserObject != null || getUserIsDone)
			{
				getUserIsDone = false;
				getUserObject = null;
			}

			ThreadStart(SqlObjectType.tblUser,APIMethod.Get, targetID: ID);

			while (!getUserIsDone)
			{
				Thread.Sleep(500);
			}

			if (getUserObject != null && getUserIsDone)
			{
				loadedUser = ((JObject)getUserObject).ToObject<tblUser>();
			}
			return loadedUser;
		}

		static bool getUsersIsDone = false;
		static List<object> getUsersList = new List<object>();
		public static List<tblUser> GetUsers()
		{
			if (loadedUsers.Count > 0)
			{
				loadedUsers.Clear();
			}
			if (getUsersList.Count > 0 || getUsersIsDone)
			{
				getUsersIsDone= false;
				getUsersList.Clear();
			}

			ThreadStart(SqlObjectType.tblUser, APIMethod.GetAll);

			while (!getUsersIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblUser> tempList = new List<tblUser>();
			if (getUsersList != null && getUsersIsDone)
			{
				foreach (var item in getUsersList)
				{
					tblUser user = ((JObject)item).ToObject<tblUser>();
					tempList.Add(user);
				}
			}
			loadedUsers = tempList;

			return loadedUsers;
		}

		#endregion

		#region tblGroup

		public static tblGroup GetGroup()
		{
			return null;
		}

		static bool getGroupsIsDone = false;
		static List<object> getGroupsList = new List<object>();
		public static List<tblGroup> GetGroups()
		{
			if (loadedGroups.Count > 0)
			{
				loadedGroups.Clear();
			}
			if (getGroupsList.Count > 0 || getGroupsIsDone)
			{
				getGroupsIsDone= false;
				getGroupsList.Clear();
			}

			ThreadStart(SqlObjectType.tblGroup, APIMethod.GetAll);

			while (!getGroupsIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblGroup> tempList = new List<tblGroup>();
			if (getGroupsList != null && getGroupsIsDone)
			{
				foreach (var item in getGroupsList)
				{
					tblGroup group = ((JObject)item).ToObject<tblGroup>();
					tempList.Add(group);
				}
			}
			loadedGroups = tempList;

			return loadedGroups;
		}

		#endregion


		#endregion


		#region Private

		static private void ThreadStart(SqlObjectType type, APIMethod method, string targetInfo = "", int targetID = 0)
		{
			switch (method)
			{
				case APIMethod.Get: ThreadPool.QueueUserWorkItem(GetThread, new object[] { type, targetID }); break;
				case APIMethod.GetAll: ThreadPool.QueueUserWorkItem(GetAllThread, new object[] { type }); break;
				case APIMethod.Post: break;
				case APIMethod.Put: break;
				case APIMethod.Delete: break;
			}
			
		}

		static private void GetThread(object obj)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = obj as object[];
			SqlObjectType type = (SqlObjectType)array[0];
			int ID = (int)array[1];

			object o = null;
			o = new HttpAPIClient().GetById(type, ID).GetAwaiter().GetResult();

			// Getting ojects
			switch (type)
			{
				case SqlObjectType.tblUser:
					{
						getUserObject = o;
						getUserIsDone = true;
						break;
					}
				case SqlObjectType.tblGroup:
					{
						
						break;
					}
			}

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		static private void GetAllThread(object obj)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = obj as object[];
			SqlObjectType type = (SqlObjectType)array[0];

			List<object> l = null;
			l = new HttpAPIClient().GetAll(type).GetAwaiter().GetResult();

			// Getting ojects
			switch (type)
			{
				case SqlObjectType.tblUser: 
					{
						getUsersList = l;
						getUsersIsDone = true;
						break;
					}
				case SqlObjectType.tblGroup: 
					{
						getGroupsList = l;
						getGroupsIsDone = true; 
						break;
					}
			}

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		#endregion

	}
}
