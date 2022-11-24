using Admin_Client.Model.Domain;
using Admin_Client.Model.FileIO;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.WindowModels
{
	public class MainWindowModel
	{

		#region Variables


		#endregion

		#region Parameters

		private bool isloggetIn = false;

		public bool IsloggetIn
		{
			get { return isloggetIn; }
			set { isloggetIn = value; }
		}

		#endregion

		public MainWindowModel()
		{

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("MainWindow is starting")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "MainWindow is shown"));
			}

		}

	}
}
