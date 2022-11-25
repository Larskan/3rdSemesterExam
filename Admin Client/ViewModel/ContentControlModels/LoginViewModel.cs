using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
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

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Changing CC to LoginView")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "LoginView is shown"));
			}

		}

		#endregion

		#region Public Methods

		public void Login()
		{
			// LOGIN AUTHENTICATION MISSING
			if (true)
			{
				//CREATE USER

			} else
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "Login failed"));
			}

		}

		#endregion

		#region Private Methods

		#endregion

	}
}
