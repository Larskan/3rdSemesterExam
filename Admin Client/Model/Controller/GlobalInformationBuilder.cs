using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.Model.Controller
{
	public class GlobalInformationBuilder : AbstractGlobalInformationBuilder
	{

		private GlobalInformation globalInformation;

		private List<tblUser> tblUsers;
		private List<tblGroup> tblGroups;

		public GlobalInformationBuilder() { }

		public override void BuildStartInfo()
		{
			globalInformation = new GlobalInformation();

			tblUsers = new List<tblUser>();
			BuildUserList();
			tblGroups = new List<tblGroup>();
			BuildGroupList();
		}

		public override void BuildCurrentUser(tblUser user)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "CurrentUser --> Setting"));
			this.globalInformation.SetCurrentUser(user);
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "CurrentUser == " + user.fldUserID + " " + user.fldFirstName + " " + user.fldLastName));
		}

		CancellationTokenSource tokenSourceGroupList;
		public override void BuildGroupList()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "GroupList --> Building"));
			if (tokenSourceGroupList != null && tokenSourceGroupList.Token.CanBeCanceled)
			{
				tokenSourceGroupList.Cancel();
			}
			tokenSourceGroupList = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateGroupListThread, new object[] { tokenSourceGroupList.Token });
		}

		CancellationTokenSource tokenSourceUserList;
		public override void BuildUserList()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "UserList --> Building"));
			if (tokenSourceUserList != null && tokenSourceUserList.Token.CanBeCanceled)
			{
				tokenSourceUserList.Cancel();
			}
			tokenSourceUserList = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateUserListThread, new object[] { tokenSourceUserList.Token });
		}

		public override GlobalInformation GetGlobalInformation()
		{
			return this.globalInformation;
		}


		#region Threading GroupList

		private void UpdateGroupListThread(object o)
		{
			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				// CHANGE THE FAKEDATEBASE.GETGROUPS() - TODO
				List<tblGroup> groups = FAKEDATABASE.GetGroups();

				bool found;
				foreach (var groupItem in groups)
				{
					found = false;
					foreach (var GroupItem in tblGroups)
					{
						if (groupItem.fldGroupID == GroupItem.fldGroupID)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { tblGroups.Add(groupItem); }));
					}
				}
				break;
			}

			globalInformation.SetGroupList(tblGroups);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "GroupList == Finished"));
			Debug.WriteLine("GroupList Build");
		}

		#endregion

		#region Threading UserList

		private void UpdateUserListThread(object o)
		{
			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				// CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
				List<tblUser> users = FAKEDATABASE.GetUsers();

				bool found;
				foreach (var userItem in users)
				{
					found = false;
					foreach (var UserItem in tblUsers)
					{
						if (userItem.fldUserID == UserItem.fldUserID)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { tblUsers.Add(userItem); }));
					}
				}
				break;
			}

			globalInformation.SetUserList(tblUsers);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "UserList == Finished"));
			Debug.WriteLine("UserList Build");
		}

		#endregion

	}
}
