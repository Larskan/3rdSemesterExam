using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
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
		private TblUser user;

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

		public PopupPasswordChangeWindowModel(Window currentWindow, TblUser user)
		{
			this.currentWindow = currentWindow;
			this.user = user;

			ObjectName = this.user.FldUserId + ": " + this.user.FldFirstName + " " + this.user.FldLastName;
		}

		#endregion

		#region Public Methods

		public void Change()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Change Click"));

			//DO STUFF TO LOGIN WITH USER ID
			// ENCRYPT AND SEND TO DATABASE


			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		public void Cancel()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		#endregion

	}
}
