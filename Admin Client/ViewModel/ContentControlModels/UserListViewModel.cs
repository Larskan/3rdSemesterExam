using Admin_Client.Model;
using Admin_Client.Model.DB;
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

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class UserListViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private ObservableCollection<TblUser> users = new ObservableCollection<TblUser>();
		public ObservableCollection<TblUser> Users
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
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			tokenSource = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
		}

		public void Create()
		{
			new PopupConfirmWindow(new TblUser(), PopupTarget.Create).ShowDialog();
		}

		public void Edit(TblUser user)
		{
			new PopupConfirmWindow(user, PopupTarget.Create).ShowDialog();
		}

		public void Delete(TblUser user)
		{
			new PopupConfirmWindow(user, PopupTarget.Create).ShowDialog();
		}

		#endregion

		#region Private Methods

		private void UpdateUsersListThread(object o)
		{
			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				// CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
				List<TblUser> users = FAKEDATABASE.GetUsers();

				bool found;
				foreach (var userItem in users)
				{
					found = false;
					foreach (var UserItem in Users)
					{
						if (userItem.FldUserId == UserItem.FldUserId)
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
				break;
			}
		}

		#endregion

	}
}
