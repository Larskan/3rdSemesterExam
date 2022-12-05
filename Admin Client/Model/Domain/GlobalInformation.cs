using Admin_Client.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	public class GlobalInformation
	{

		private TblUser currentUser;

		private List<TblUser> users;
		private List<TblGroup> groups;

		public GlobalInformation() { }

		public void SetCurrentUser(TblUser user) 
		{ 
			this.currentUser = user;
		}

		public void SetCurrentUser(List<TblUser> users)
		{
			this.users = users;
		}

		public void SetCurrentUser(List<TblGroup> groups)
		{
			this.groups = groups;
		}

	}
}
