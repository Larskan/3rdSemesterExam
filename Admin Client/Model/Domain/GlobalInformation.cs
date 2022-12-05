using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	public class GlobalInformation
	{

		private tblUser currentUser;

		private List<tblUser> userList;
		private List<tblGroup> groupList;

		public GlobalInformation() { }

		#region CurrentUser

		public void SetCurrentUser(tblUser user) { this.currentUser = user; }

		public tblUser GetCurrentUser() { return this.currentUser; }

		#endregion

		#region UserList

		public void SetUserList(List<tblUser> userList) { this.userList = userList; }

		public void AddUserList(tblUser user) { this.userList.Add(user); }

		public void RemoveUserList(tblUser user) { this.userList.Remove(user); }

		public List<tblUser> GetUserList() { return this.userList; }

		#endregion

		public void SetGroupList(List<tblGroup> groupList) { this.groupList = groupList; }

		public void AddGroupList(tblGroup group) { this.groupList.Add(group); }

		public void RemoveGroupList(tblGroup group) { this.groupList.Remove(group); }

		public List<tblGroup> GetGroupList() { return this.groupList; }

	}
}
