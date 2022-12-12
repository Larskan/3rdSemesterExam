using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class UserListViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private ObservableCollection<tblUser> users = new ObservableCollection<tblUser>();
		public ObservableCollection<tblUser> Users
		{
			get { return users; }
			set { users = value; }
		}


		#endregion

		#region Constructor

		public UserListViewModel()
		{

		}

		#endregion

		#region Public Methods

		CancellationTokenSource tokenSource;
		public void Update()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Users Click"));
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			tokenSource = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
		}

		public void Create()
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(new tblUser() { fldPassword = "x" }, PopupMethod.Create);
			Thread.Sleep(500);
			Update();
		}

		public void Edit(tblUser user)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Edit);
		}

		public void Delete(tblUser user)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Delete);
			Thread.Sleep(500);
			Update();
		}

		#endregion

		#region Private Methods

		private void UpdateUsersListThread(object o)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));
			App.Current.Dispatcher.BeginInvoke(new Action(() => { Users.Clear(); }));
			Thread.Sleep(500);

			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				List<tblUser> users = HttpClientHandler.GetUsers();

				bool found;
				foreach (var userItem in users)
				{
					found = false;
					foreach (var UserItem in Users)
					{
						if (userItem.fldUserID == UserItem.fldUserID)
						{
							found = true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { Users.Add(userItem); }));
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
