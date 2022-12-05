using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Controller
{
	public abstract class AbstractGlobalInformationBuilder
	{

		public abstract void BuildStartInfo();
		public abstract void BuildCurrentUser(tblUser user);

		// Build On Startup
		public abstract void BuildUserList();

		// Build On Startup
		public abstract void BuildGroupList();

		public abstract GlobalInformation GetGlobalInformation();

	}
}
