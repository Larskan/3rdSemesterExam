using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class AccountTabViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public AccountTabViewModel() 
		{

		}

		#endregion

		#region Public Methods

		public void Logout()
		{
			HttpClientHandler.currentUser = null;

			MainWindowModelSingleton.Instance.SetMainContent(new LoginView(), false, false);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Logout == Success"));
		}
		public void ChangeContentToAccount()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new AccountView(), MainWindowModelSingleton.Instance.GetMainContent(), false);
		}

		#endregion

		#region Private Methods

		#endregion

	}
}
