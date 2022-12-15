using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class AccountTabViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private string currentUserName;

		public string CurrentUserName
		{
			get { return currentUserName; }
			set { currentUserName = value; NotifyPropertyChanged(); }
		}


		#endregion

		#region Constructor

		/// <summary>
		/// Starts up with the currentUsers name
		/// </summary>
		public AccountTabViewModel() 
		{
			CurrentUserName = HttpClientHandler.currentUser.fldFirstName + " " + HttpClientHandler.currentUser.fldLastName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the currentUser to null and sets the view to LoginView
		/// </summary>
		public void Logout()
		{
			HttpClientHandler.currentUser = null;

			MainWindowModelSingleton.Instance.SetMainContent(new LoginView(), false, false);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Logout == Success"));
		}

		/// <summary>
		/// Changes the content of the MainWindows ContentControls content to AccountView
		/// </summary>
		public void ChangeContentToAccount()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new AccountView(), MainWindowModelSingleton.Instance.GetMainContent(), false);
		}

		#endregion

	}
}
