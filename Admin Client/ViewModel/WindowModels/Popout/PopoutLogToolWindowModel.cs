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
using System.Windows.Documents;

namespace Admin_Client.ViewModel.WindowModels.Popout
{
	public class PopoutLogToolWindowModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private ObservableCollection<string> logFiles = new ObservableCollection<string>();

		public ObservableCollection<string> LogFiles
		{
			get { return logFiles; }
			set { logFiles = value; }
		}

		#endregion

		#region Constructor

		public PopoutLogToolWindowModel()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popout LogToolWindow"));

			List<string> localLogFiles = new List<string>();
			foreach (var item in LogHandlerSingleton.Instance.GetLocalLogFiles())
			{
				localLogFiles.Add(item.ToString());
			}

			localLogFiles.Reverse();

			foreach (var item in localLogFiles)
			{
				LogFiles.Add(item);
			}

			LogFiles[0] = LogFiles[0] + " ( Current )";

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "LogToolWindow is shown"));
		}

		#endregion

		#region Public Methods

		public void OpenLogFile(DateTime dateTime)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "LogTool Click --> " + dateTime.ToString()));
			MainWindowModelSingleton.Instance.StartPopoutLog(dateTime);
		}

		public void Closed()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "LogTool Close Click"));
			MainWindowModelSingleton.Instance.GetMainWindow().Focus();
		}

		#endregion

	}
}
