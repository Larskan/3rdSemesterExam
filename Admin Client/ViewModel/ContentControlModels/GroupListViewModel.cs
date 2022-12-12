using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class GroupListViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private ObservableCollection<tblGroup> groups = new ObservableCollection<tblGroup>();
		public ObservableCollection<tblGroup> Groups
		{
			get { return groups; }
			set { groups = value; }
		}


		#endregion

		#region Constructor

		public GroupListViewModel()
		{

		}

		#endregion

		#region Public Methods

		CancellationTokenSource tokenSource;
		public void Update()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Groups Click"));
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			tokenSource = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { tokenSource.Token });
		}

		public void Create()
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(new tblGroup(), PopupMethod.Create);
			Thread.Sleep(500);
			Update();
		}

		public void Edit(tblGroup group)
		{

			MainWindowModelSingleton.Instance.StartPopupConfirm(group, PopupMethod.Edit);
		}

		public void Delete(tblGroup group)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(group, PopupMethod.Delete);
			Thread.Sleep(1000);
			Update();
		}

		#endregion

		#region Private Methods

		private void UpdateGroupsListThread(object o)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));
			App.Current.Dispatcher.BeginInvoke(new Action(() => { Groups.Clear(); }));
			Thread.Sleep(500);

			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				List<tblGroup> groups = HttpClientHandler.GetGroups();

				bool found;
				foreach (var groupItem in groups)
				{
					found = false;
					foreach (var GroupItem in Groups)
					{
						if (groupItem.fldGroupID == GroupItem.fldGroupID)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { Groups.Add(groupItem); }));
					}
				}
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));
				break;
			}
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		#endregion

	}
}
