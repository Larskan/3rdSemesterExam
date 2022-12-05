using Admin_Client.Model;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.WindowModels.Popout
{
	public class PopoutLogWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		Window currentWindow;
		CancellationTokenSource tokenSource;

		ListBox logContainer;
		CheckBox autoScrollCheckBox;

		#endregion

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

		public PopoutLogWindowModel(DateTime dateTime, ListBox logContainer, CheckBox autoScrollCheckBox, Window currentWindow)
		{
			this.logContainer = logContainer;
			this.autoScrollCheckBox = autoScrollCheckBox;
			this.currentWindow = currentWindow;

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popout LogWindow")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "LogWindow is shown"));
			}

			LogFileName = dateTime.ToString();

			// Start the update thread
			this.tokenSource = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateLogsThread, new object[] { this.tokenSource.Token });
		}

		#endregion

		#region Public Methods

		public void Closed()
		{
			tokenSource.Cancel();

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Log Close Click"));
		}

		public void ToText()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popout LogTextWindow"));

			string content = "";
			string spacingItem = "";
			foreach (var item in Logs)
			{
				switch (item.LogType)
				{
					case LogType.Success: spacingItem = "\t\t\t\t\t\t\t"; break;
					case LogType.Information: spacingItem = "\t\t\t\t\t\t\t"; break;
					case LogType.UserAction: spacingItem = "\t"; break;
					case LogType.Warning: spacingItem = "\t\t\t\t\t\t\t\t"; break;
					case LogType.FatalError: spacingItem = "\t\t\t\t\t\t\t"; break;
				}
				content += "[" + item.DateTime + "] {" + item.LogType + "}" + spacingItem + " " + item.LogTxt + "\n";
			}

			new PopoutLogTextWindow(currentWindow, LogFileName, content).Show();
		}

		#endregion

		#region UpdateThread

		private void UpdateLogsThread(object o)
		{
			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			List<Log> logs = LogHandlerSingleton.Instance.ReadLogFile(DateTime.Parse(LogFileName), true);

			bool found;
			while (!token.IsCancellationRequested)
			{
				Debug.WriteLine(Thread.CurrentThread + " : Updating now");
				// Update
				logs = LogHandlerSingleton.Instance.ReadLogFile(DateTime.Parse(LogFileName), false);
				foreach (var itemNew in logs)
				{
					found = false;
					foreach (var itemOld in Logs)
					{
						if (DateTime.Compare(itemOld.DateTime, itemNew.DateTime) == 0 && itemNew.LogTxt.Equals(itemOld.LogTxt))
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { Logs.Add(itemNew); }));
					}
				}
				// AutoScroll
				App.Current.Dispatcher.BeginInvoke(new Action(() => {
					if (autoScrollCheckBox.IsChecked.Value)
					{
						logContainer.ScrollIntoView(logs.Last());
					}
				}));
				// Sleep
				Debug.WriteLine(Thread.CurrentThread + " : Sleeping for 5 sec");
				Thread.Sleep(2500);
			}
			Debug.WriteLine(Thread.CurrentThread + " : Closing");
		}

		#endregion

	}
}
