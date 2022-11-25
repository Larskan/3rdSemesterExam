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
	public class UserListViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public UserListViewModel()
		{

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Changing CC to OverviewView")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "OverviewView is shown"));
			}

		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion

	}
}
