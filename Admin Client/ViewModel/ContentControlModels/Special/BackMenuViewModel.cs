using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class BackMenuViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		UserControl userControl;

		#endregion

		#region Constructor

		public BackMenuViewModel(UserControl userControl)
		{
			this.userControl = userControl;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns the user to the previous content, with or without the menu
		/// </summary>
		public void Back()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Back To -->"));
			if (userControl.GetType().Name.Equals("OverviewView"))
			{
				MainWindowModelSingleton.Instance.SetMainContent(userControl, false, true);
			} else
			{
				MainWindowModelSingleton.Instance.SetMainContent(userControl, true, true);
			}
		}

		#endregion

	}
}
