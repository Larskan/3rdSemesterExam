using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class LoginViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public LoginViewModel()
		{

		}

		#endregion

		#region Public Methods

		public void Login(string username, string password)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Login Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Username: " + username));
			

			// LOGIN AUTHENTICATION MISSING - TODO

			if (true)
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Login == Success"));

				// TODO
				GlobalInformationBuilderSingleton.Instance.BuildCurrentUser(new tblUser());

				MainWindowModelSingleton.Instance.SetMainContent(new OverviewView(), false, true);

			}
			else
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "Login --> Failed"));
			}

		}

		#endregion

		#region Private Methods

		#endregion

	}
}
