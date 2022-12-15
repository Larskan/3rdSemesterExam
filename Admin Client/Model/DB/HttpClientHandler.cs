using Admin_Client.Model.DB.EF;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shell;

namespace Admin_Client.Model.DB
{
	public static class HttpClientHandler
	{

		#region Variables

		public static tblUser currentUser = null;

		#endregion

		#region Standard Get Methods

		#region tblUser

		static bool userIsDone = false;
		static tblUser userObject = null;
		/// <summary>
		/// Gets the specified user from the api
		/// </summary>
		/// <param name="ID">The ID for the user</param>
		/// <returns>The user</returns>
		public static tblUser GetUser(int ID)
		{
			if (userObject != null || userIsDone)
			{
				userIsDone = false;
				userObject = null;
			}

			ThreadStart(SqlObjectType.tblUser,APIMethod.Get, targetID: ID);

			while (!userIsDone)
			{
				Thread.Sleep(500);
			}

			return userObject;
		}

		static bool usersIsDone = false;
		static List<tblUser> usersList = new List<tblUser>();
		/// <summary>
		/// Gets all users in the api
		/// </summary>
		/// <returns>The users</returns>
		public static List<tblUser> GetUsers()
		{
			if (usersList.Count > 0 || usersIsDone)
			{
				usersIsDone = false;
				usersList.Clear();
			}

			ThreadStart(SqlObjectType.tblUser, APIMethod.GetAll);

			while (!usersIsDone)
			{
				Thread.Sleep(500);
			}

			return usersList;
		}

		#endregion

		#region tblGroup

		static bool groupsIsDone = false;
		static List<tblGroup> groupsList = new List<tblGroup>();
		/// <summary>
		/// Gets all groups in the api
		/// </summary>
		/// <returns>The groups</returns>
		public static List<tblGroup> GetGroups()
		{
			if (groupsList.Count > 0 || groupsIsDone)
			{
				groupsIsDone = false;
				groupsList.Clear();
			}

			ThreadStart(SqlObjectType.tblGroup, APIMethod.GetAll);

			while (!groupsIsDone)
			{
				Thread.Sleep(500);
			}

			return groupsList;
		}

		#endregion

		#region tblReceipt

		static bool receiptsIsDone = false;
		static List<tblReceipt> receiptsList = new List<tblReceipt>();
		/// <summary>
		/// Gets all receipts in the api
		/// </summary>
		/// <returns>The receipts</returns>
		public static List<tblReceipt> GetReceipts()
		{
			if (receiptsList.Count > 0 || receiptsIsDone)
			{
				receiptsIsDone = false;
				receiptsList.Clear();
			}

			ThreadStart(SqlObjectType.tblReceipt, APIMethod.GetAll);

			while (!receiptsIsDone)
			{
				Thread.Sleep(500);
			}

			return receiptsList;
		}

		#endregion

		#region tblTrip

		static bool tripIsDone = false;
		static tblTrip tripObject = null;
		/// <summary>
		/// Gets a specified trip from the api
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static tblTrip GetTrip(int ID)
		{
			if (tripObject != null || tripIsDone)
			{
				tripIsDone = false;
				tripObject = null;
			}

			ThreadStart(SqlObjectType.tblTrip, APIMethod.Get, targetID: ID);

			while (!tripIsDone)
			{
				Thread.Sleep(500);
			}

			return tripObject;
		}

		static bool tripsIsDone = false;
		static List<tblTrip> tripsList = new List<tblTrip>();
		/// <summary>
		/// Gets all trips from the api
		/// </summary>
		/// <returns></returns>
		public static List<tblTrip> GetTrips()
		{
			if (tripsList.Count > 0 || tripsIsDone)
			{
				tripsIsDone = false;
				tripsList.Clear();
			}

			ThreadStart(SqlObjectType.tblTrip, APIMethod.GetAll);

			while (!tripsIsDone)
			{
				Thread.Sleep(500);
			}

			return tripsList;
		}

		#endregion

		#region tblUserExpense

		static bool userExpensesIsDone = false;
		static List<tblUserExpense> userExpensesList = new List<tblUserExpense>();
		/// <summary>
		/// Gets all userexpenses from the api
		/// </summary>
		/// <returns>The userexpenses</returns>
		public static List<tblUserExpense> GetUserExpenses()
		{
			if (userExpensesList.Count > 0 || userExpensesIsDone)
			{
				userExpensesIsDone = false;
				userExpensesList.Clear();
			}

			ThreadStart(SqlObjectType.tblUserExpense, APIMethod.GetAll);

			while (!userExpensesIsDone)
			{
				Thread.Sleep(500);
			}

			return userExpensesList;
		}

		#endregion

		#region tblUserToGroup

		static bool userToGroupIsDone = false;
		static List<tblUserToGroup> userToGroupList = new List<tblUserToGroup>();
		/// <summary>
		/// Gets all userttogroup relations from the api
		/// </summary>
		/// <returns>The relations usertogroup</returns>
		public static List<tblUserToGroup> GetUserToGroup()
		{
			if (userToGroupList.Count > 0 || userToGroupIsDone)
			{
				userToGroupIsDone = false;
				userToGroupList.Clear();
			}

			ThreadStart(SqlObjectType.tblUserToGroup, APIMethod.GetAll);

			while (!userToGroupIsDone)
			{
				Thread.Sleep(500);
			}

			return userToGroupList;
		}

		#endregion

		#region tblGroupToTrip

		static bool groupToTripIsDone = false;
		static List<tblGroupToTrip> groupToTripList = new List<tblGroupToTrip>();
		/// <summary>
		/// Gets all grouptotrip relations from the api
		/// </summary>
		/// <returns>The relations grouptotrip</returns>
		public static List<tblGroupToTrip> GetGroupToTrip()
		{
			if (groupToTripList.Count > 0 || groupToTripIsDone)
			{
				groupToTripIsDone = false;
				groupToTripList.Clear();
			}

			ThreadStart(SqlObjectType.tblGroupToTrip, APIMethod.GetAll);

			while (!groupToTripIsDone)
			{
				Thread.Sleep(500);
			}

			return groupToTripList;
		}

		#endregion

		#region tblTripToUserExpense

		static bool tripToUserExpenseIsDone = false;
		static List<tblTripToUserExpense> tripToUserExpenseList = new List<tblTripToUserExpense>();
		/// <summary>
		/// Gets all triptouserexpense relations from the api
		/// </summary>
		/// <returns>The relations triptouserexpense</returns>
		public static List<tblTripToUserExpense> GetTripToUserExpense()
		{
			if (tripToUserExpenseList.Count > 0 || tripToUserExpenseIsDone)
			{
				tripToUserExpenseIsDone = false;
				tripToUserExpenseList.Clear();
			}

			ThreadStart(SqlObjectType.tblTripToUserExpense, APIMethod.GetAll);

			while (!tripToUserExpenseIsDone)
			{
				Thread.Sleep(500);
			}

			return tripToUserExpenseList;
		}

		#endregion

		#endregion

		#region Post, Put, Delete Methods

		/// <summary>
		/// Posts a specific object via the HttpAPIClient
		/// </summary>
		/// <param name="o">The object to post</param>
		public static void Post(object o)
		{
			new HttpAPIClient().Post(o);
		}
		/// <summary>
		/// Put a specific object in the place of a target given by the ID
		/// </summary>
		/// <param name="o"></param>
		/// <param name="ID"></param>
		public static void Put(object o, int ID)
		{
			new HttpAPIClient().Put(o, ID);
		}
		/// <summary>
		/// Delete a specific object given via type and ID
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		public static void Delete(SqlObjectType type, int ID)
		{
			new HttpAPIClient().Delete(type, ID);
		}

		#endregion

		#region Relations Get Methods

		/// <summary>
		/// Get all groups from a specific user
		/// </summary>
		/// <param name="user">The specific user</param>
		/// <returns>The groups</returns>
		public static List<tblGroup> GetGroupsFromUser(tblUser user)
		{
			if (groupsIsDone || userToGroupIsDone)
			{
				groupsIsDone = false;
				userToGroupIsDone = false;
			}

			ThreadStart(SqlObjectType.tblGroup, APIMethod.GetAll);
			ThreadStart(SqlObjectType.tblUserToGroup, APIMethod.GetAll);

			while (!groupsIsDone || !userToGroupIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblGroup> filterGroups = new List<tblGroup>();
			foreach (var relation in userToGroupList)
			{
				if (relation.fldUserID == user.fldUserID)
				{
					foreach (var group in groupsList)
					{
						if (relation.fldGroupID == group.fldGroupID)
						{
							filterGroups.Add(group);
							break;
						}
					}
				}
			}

			return filterGroups;
		}

		/// <summary>
		/// Gets all receipts from a specific user
		/// </summary>
		/// <param name="user">The specific user</param>
		/// <returns>The receitps</returns>
		public static List<tblReceipt> GetReceiptsFromUser(tblUser user)
		{
			if (receiptsIsDone)
			{
				receiptsIsDone = false;
			}

			ThreadStart(SqlObjectType.tblReceipt, APIMethod.GetAll);

			while (!receiptsIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblReceipt> filterReceipts = new List<tblReceipt>();
			foreach (var receipt in receiptsList)
			{
				if (receipt.fldUserID == user.fldUserID)
				{
					filterReceipts.Add(receipt);
					break;
				}
			}

			return filterReceipts;
		}

		/// <summary>
		/// Gets all userExpenses from a specific user
		/// </summary>
		/// <param name="user">The specific user</param>
		/// <returns>The userExpenses</returns>
		public static List<tblUserExpense> GetUserExpensesFromUser(tblUser user)
		{
			if (userExpensesIsDone)
			{
				userExpensesIsDone = false;
			}

			ThreadStart(SqlObjectType.tblUserExpense, APIMethod.GetAll);

			while (!userExpensesIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblUserExpense> filterUserExpense = new List<tblUserExpense>();
			foreach (var userExpense in userExpensesList)
			{
				if (userExpense.fldUserID == user.fldUserID)
				{
					filterUserExpense.Add(userExpense);
					break;
				}
			}

			return filterUserExpense;
		}

		/// <summary>
		/// Gets all users from a specific group
		/// </summary>
		/// <param name="group">The specific group</param>
		/// <returns>The users</returns>
		public static List<tblUser> GetUsersFromGroup(tblGroup group)
		{
			if (userToGroupIsDone || usersIsDone)
			{
				usersIsDone = false;
				userToGroupIsDone = false;
			}

			ThreadStart(SqlObjectType.tblUser, APIMethod.GetAll);
			ThreadStart(SqlObjectType.tblUserToGroup, APIMethod.GetAll);

			while (!usersIsDone || !userToGroupIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblUser> filterUsers = new List<tblUser>();
			foreach (var relation in userToGroupList)
			{
				if (relation.fldGroupID == group.fldGroupID)
				{
					foreach (var user in usersList)
					{
						if (relation.fldUserID == user.fldUserID)
						{
							filterUsers.Add(user);
							break;
						}
					}
				}
			}

			return filterUsers;
		}

		/// <summary>
		/// Gets all trips from a specific group
		/// </summary>
		/// <param name="group">The specific group</param>
		/// <returns>The trips</returns>
		public static List<tblTrip> GetTripsFromGroup(tblGroup group)
		{
			if (tripsIsDone || groupToTripIsDone)
			{
				tripsIsDone = false;
				groupToTripIsDone = false;
			}

			ThreadStart(SqlObjectType.tblTrip, APIMethod.GetAll);
			ThreadStart(SqlObjectType.tblGroupToTrip, APIMethod.GetAll);

			while (!tripsIsDone || !groupToTripIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblTrip> filterTrips = new List<tblTrip>();
			foreach (var relation in groupToTripList)
			{
				if (relation.fldGroupID == group.fldGroupID)
				{
					foreach (var trip in tripsList)
					{
						if (relation.fldTripID == trip.fldTripID)
						{
							filterTrips.Add(trip);
							break;
						}
					}
				}
			}

			return filterTrips;
		}

		/// <summary>
		/// Gets all userExpenses from a specific trip
		/// </summary>
		/// <param name="trip">The specific trip</param>
		/// <returns>The userExpenses</returns>
		public static List<tblUserExpense> GetUserExpensesFromTrip(tblTrip trip)
		{
			if (userExpensesIsDone || tripToUserExpenseIsDone)
			{
				userExpensesIsDone = false;
				tripToUserExpenseIsDone = false;
			}

			ThreadStart(SqlObjectType.tblUserExpense, APIMethod.GetAll);
			ThreadStart(SqlObjectType.tblTripToUserExpense, APIMethod.GetAll);

			while (!userExpensesIsDone || !tripToUserExpenseIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblUserExpense> filterUserExpense = new List<tblUserExpense>();
			foreach (var relation in tripToUserExpenseList)
			{
				if (relation.fldTripID == trip.fldTripID)
				{
					foreach (var userExpense in userExpensesList)
					{
						if (relation.fldExpenseID == userExpense.fldExpenseID)
						{
							filterUserExpense.Add(userExpense);
							break;
						}
					}
				}
			}

			return filterUserExpense;
		}

		/// <summary>
		/// Gets all receipts from a specific trip
		/// </summary>
		/// <param name="trip">The specific trip</param>
		/// <returns>The receipts</returns>
		public static List<tblReceipt> GetReceiptsFromTrip(tblTrip trip)
		{
			if (receiptsIsDone)
			{
				receiptsIsDone = false;
			}

			ThreadStart(SqlObjectType.tblReceipt, APIMethod.GetAll);

			while (!receiptsIsDone)
			{
				Thread.Sleep(500);
			}

			List<tblReceipt> filterReceipts = new List<tblReceipt>();
			foreach (var receipt in receiptsList)
			{
				if (receipt.fldTripID == trip.fldTripID)
				{
					filterReceipts.Add(receipt);
					break;
				}
			}

			return filterReceipts;
		}


		#endregion

		#region Private

		/// <summary>
		/// Starts a specific API thread
		/// </summary>
		/// <param name="type">The object type</param>
		/// <param name="method">The API method</param>
		/// <param name="targetID">The targets ID, 0 by default</param>
		static private void ThreadStart(SqlObjectType type, APIMethod method, int targetID = 0)
		{
			switch (method)
			{
				case APIMethod.Get: ThreadPool.QueueUserWorkItem(GetThread, new object[] { type, targetID }); break;
				case APIMethod.GetAll: ThreadPool.QueueUserWorkItem(GetAllThread, new object[] { type }); break;
					default: throw new Exception(method + " is not usable in the current context");
			}
			
		}

		/// <summary>
		/// Gets a single object from a table via the API
		/// </summary>
		/// <param name="obj">The parameters [SqlObjectType, ID]</param>
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
						userObject = ((JObject)o).ToObject<tblUser>();
						userIsDone = true;
						break;
					}
				case SqlObjectType.tblTrip:
					{
						tripObject = ((JObject)o).ToObject<tblTrip>();
						tripIsDone = true;
						break;
					}
				default: throw new Exception(type + " is not implemented");
			}

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		/// <summary>
		/// Gets all objects from a table via the API
		/// </summary>
		/// <param name="obj">The parameters [SqlObjectType]</param>
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
						List<tblUser> tempList = new List<tblUser>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblUser user = ((JObject)item).ToObject<tblUser>();
								tempList.Add(user);
							}
							usersList = tempList;
						}
						usersIsDone = true;
						break;
					}
				case SqlObjectType.tblGroup: 
					{
						List<tblGroup> tempList = new List<tblGroup>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblGroup group = ((JObject)item).ToObject<tblGroup>();
								tempList.Add(group);
							}
							groupsList = tempList;
						}
						groupsIsDone = true; 
						break;
					}
				case SqlObjectType.tblReceipt:
					{
						List<tblReceipt> tempList = new List<tblReceipt>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblReceipt receipt = ((JObject)item).ToObject<tblReceipt>();
								tempList.Add(receipt);
							}
							receiptsList = tempList;
						}
						receiptsIsDone = true;
						break;
					}
				case SqlObjectType.tblTrip:
					{
						List<tblTrip> tempList = new List<tblTrip>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblTrip trip = ((JObject)item).ToObject<tblTrip>();
								tempList.Add(trip);
							}
							tripsList = tempList;
						}
						tripsIsDone = true;
						break;
					}
				case SqlObjectType.tblUserExpense:
					{
						List<tblUserExpense> tempList = new List<tblUserExpense>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblUserExpense userExpense = ((JObject)item).ToObject<tblUserExpense>();
								tempList.Add(userExpense);
							}
							userExpensesList = tempList;
						}
						userExpensesIsDone = true;

						break;
					}
				case SqlObjectType.tblUserToGroup:
					{
						List<tblUserToGroup> tempList = new List<tblUserToGroup>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblUserToGroup userToGroup = ((JObject)item).ToObject<tblUserToGroup>();
								tempList.Add(userToGroup);
							}
							userToGroupList = tempList;
						}
						userToGroupIsDone = true;

						break;
					}
				case SqlObjectType.tblGroupToTrip:
					{
						List<tblGroupToTrip> tempList = new List<tblGroupToTrip>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblGroupToTrip groupToTrip = ((JObject)item).ToObject<tblGroupToTrip>();
								tempList.Add(groupToTrip);
							}
							groupToTripList = tempList;
						}
						groupToTripIsDone = true;

						break;
					}
				case SqlObjectType.tblTripToUserExpense:
					{
						List<tblTripToUserExpense> tempList = new List<tblTripToUserExpense>();
						if (l != null)
						{
							foreach (var item in l)
							{
								tblTripToUserExpense tripToUserExpense = ((JObject)item).ToObject<tblTripToUserExpense>();
								tempList.Add(tripToUserExpense);
							}
							tripToUserExpenseList = tempList;
						}
						tripToUserExpenseIsDone = true;

						break;
					}
				default: throw new Exception(type + " is not implemented");
			}

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		#endregion

	}
}
