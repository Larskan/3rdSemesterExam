using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class MenuViewModel : NotifyPropertyChangedHandler
	{

		public MenuViewModel() 
		{
			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Menu is starting")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Menu is shown"));
			}
		}

	}
}
