using Admin_Client.ViewModel.WindowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
	internal class MainWindowModelSingleton : MainWindowModel
	{
		private MainWindowModelSingleton() { }
		private static MainWindowModelSingleton instance = null;
		/// <summary>
		/// Gets a already existing instance or creates a new instance of the MainWindowModel
		/// </summary>
		public static MainWindowModelSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MainWindowModelSingleton();
				}
				return instance;
			}
		}
	}
}
