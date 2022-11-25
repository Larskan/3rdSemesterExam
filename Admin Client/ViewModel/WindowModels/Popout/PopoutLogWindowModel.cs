using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.WindowModels.Popout
{
	public class PopoutLogWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public PopoutLogWindowModel()
		{

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popout LogWindow")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "LogWindow is shown"));
			}

		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion

	}
}
