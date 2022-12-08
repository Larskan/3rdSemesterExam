using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

			// FOR EASY USE -----------------
			bool loginSuccess = false;
			if (username.Equals(".") && password.Equals("."))
			{
				HttpClientHandler.currentUser = new tblUser() { fldEmail = "No@Email.dk", fldFirstName = "Local", fldLastName = "Admin",  fldIsAdmin = true, fldPassword = ".", fldPhonenumber = "00000000" };
				loginSuccess = true;
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Login == Success"));
				MainWindowModelSingleton.Instance.SetMainContent(new OverviewView(), false, true);
			}
			//-------------------------------

			// EMAIL NOT-VALID
			Regex regex = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*))");
			if (username.Length == 0 && !regex.IsMatch(username))
			{
				return;
			}
			if (password.Length == 0)
			{
				return;
			}

			// Auth
			//bool loginSuccess = false;
			List<tblUser> users = HttpClientHandler.GetUsers();
			foreach (tblUser user in users) 
			{ 
				if (user.fldIsAdmin.Value == true && user.fldEmail.Equals(username) && user.fldPassword.Equals(password))
				{
					HttpClientHandler.currentUser = user;
					loginSuccess = true;
					break;
				}
			}

			if (loginSuccess)
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Login == Success"));

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
