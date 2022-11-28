using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.WindowModels.Popout
{
	public class PopoutLogWindowModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private string logFileName;

		public string LogFileName
		{
			get { return logFileName; }
			set { logFileName = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<Log> logs = new ObservableCollection<Log>();

		public ObservableCollection<Log> Logs
		{
			get { return logs; }
			set { logs = value; }
		}

		#endregion

		#region Constructor

		public PopoutLogWindowModel(DateTime dateTime)
		{

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popout LogWindow")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "LogWindow is shown"));
			}

			LogFileName= dateTime.ToString();

			foreach (var item in LogHandlerSingleton.Instance.ReadLogFile(dateTime))
			{
				logs.Add(item);
			}
		}

		#endregion

		#region Public Methods

		public void Closed()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Log Close Click"));
		}

		#endregion

	}
}
