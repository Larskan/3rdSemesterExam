using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
using Admin_Client.Model.Domain;
using Admin_Client.Model.Foundation;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupPasswordChangeWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private Window currentWindow;
		private tblUser user;

		#endregion

		#region Properties

		private string objectName;

		public string ObjectName
		{
			get { return objectName; }
			set { objectName = value; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Create a PopupPasswordChangeWindowModel and set the user
		/// </summary>
		/// <param name="currentWindow"></param>
		/// <param name="user"></param>
		public PopupPasswordChangeWindowModel(Window currentWindow, tblUser user)
		{
			this.currentWindow = currentWindow;
			this.user = user;

			ObjectName = this.user.fldUserID + ": " + this.user.fldFirstName + " " + this.user.fldLastName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Validate the passwords and send them to the API
		/// </summary>
		/// <param name="password"></param>
		/// <param name="newPassword"></param>
		public void Confirm(string password, string newPassword)
		{
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Change Click"));

			// Authentication, if they don't match then return
			if (!Encryption.Encrypt_Password(password, Encryption.Salt_Password(password)).Equals(HttpClientHandler.GetUser(user.fldUserID).fldPassword))
            {
               return;
            }

            // ENCRYPT AND SEND TO DATABASE
            string EncryptedPW = Encryption.Encrypt_Password(newPassword, Encryption.Salt_Password(newPassword));
            user.fldPassword = EncryptedPW;
			HttpClientHandler.Put(user, user.fldUserID);

			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		/// <summary>
		/// Cancel the change
		/// </summary>
		public void Cancel()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		#endregion

	}
}
