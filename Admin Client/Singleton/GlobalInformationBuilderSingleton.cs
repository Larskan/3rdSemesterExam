using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
	public class GlobalInformationBuilderSingleton : GlobalInformation
	{
		private GlobalInformationBuilderSingleton() { }
		private static GlobalInformationBuilderSingleton instance = null;
		public static GlobalInformationBuilderSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GlobalInformationBuilderSingleton();
				}
				return instance;
			}
		}
	}
}
