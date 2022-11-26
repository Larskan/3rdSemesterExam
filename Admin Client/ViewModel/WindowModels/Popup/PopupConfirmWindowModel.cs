using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupConfirmWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private object target;
		private Action<object> action;

		private Window currentWindow;

		#endregion

		#region Constructor

		public PopupConfirmWindowModel(Window currentWindow, TblUser user, Action<object> action)
		{
			this.target = user;
			this.action = action;

			this.currentWindow = currentWindow;
		}

		public PopupConfirmWindowModel(Window currentWindow, TblGroup group, Action<object> action)
		{
			this.target = group;
			this.action = action;

			this.currentWindow = currentWindow;
		}

		#endregion

		#region Public Methods

		public void Confirm()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(target.GetType().Name + "." + action.GetMethodInfo().Name + "() --> Invoke"));
			switch (target.GetType().Name)
			{
				case "TblUser":
					{
						switch (action.GetType().Name)
						{
							case "Create": /*CREATE USER*/ break;
							case "Edit": /*EDIT USER*/ break;
							case "Delete": /*DELETE USER*/ break;
						}
						break;
					}
				case "TblGroup":
					{
						switch (action.GetType().Name)
						{
							case "Create": /*CREATE GROUP*/ break;
							case "Edit": /*EDIT GROUP*/ break;
							case "Delete": /*DELETE GROUP*/ break;
						}
						break;
					}
				default: LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.FatalError, "Could not confirm action")); break;
			}
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(target.GetType().Name + "." + action.GetMethodInfo().Name + "() == Invoked"));

			currentWindow.Close();
		}

		public void Cancel()
		{
			currentWindow.Close();
		}

		#endregion

		#region Private Methods

		#endregion

	}
}
