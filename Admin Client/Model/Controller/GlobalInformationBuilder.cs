using Admin_Client.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Controller
{
	public class GlobalInformationBuilder : AbstractGlobalInformationBuilder
	{

		private GlobalInformation globalInformation;

		public GlobalInformationBuilder() 
		{
			globalInformation= new GlobalInformation();
		}

		public override void BuildCurrentUser()
		{
			throw new NotImplementedException();
		}

		public override void BuildGroupList()
		{
			throw new NotImplementedException();
		}

		public override void BuildUserList()
		{
			throw new NotImplementedException();
		}

		public override GlobalInformation GetGlobalInformation()
		{
			throw new NotImplementedException();
		}
	}
}
