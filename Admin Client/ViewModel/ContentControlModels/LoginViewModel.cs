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

		public void Login()
		{
			// LOGIN AUTHENTICATION MISSING
			if (true)
			{
				//CREATE USER OBJECT FOR LATER USE - (USE THE SESSION INFORMATION BUILDER TO DO SO)

				MainWindowModelSingleton.Instance.IsAccountTabActive(true);
				MainWindowModelSingleton.Instance.SetMainContent(new OverviewView());

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
