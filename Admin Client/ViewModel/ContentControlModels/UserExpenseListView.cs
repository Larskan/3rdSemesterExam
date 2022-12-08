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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class UserExpenseListViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private int startupDelay = 500;

		#endregion

		#region Properties

		private string groupname;

		public string Groupname
		{
			get { return groupname; }
			set { groupname = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<tblUserExpense> userExpenses = new ObservableCollection<tblUserExpense>();

		public ObservableCollection<tblUserExpense> UserExpenses
		{
			get { return userExpenses; }
			set { userExpenses = value; }
		}


		#endregion

		#region Constructor

		public UserExpenseListViewModel(tblUser user)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get UserExpenses for User: " + user.fldUserID + " " + user.fldFirstName + " " + user.fldLastName));

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThread, new object[] { user });
		}

		#endregion

		#region Public Methods

		private void UpdateReceiptListThread(object o)
		{
			Thread.Sleep(startupDelay);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = o as object[];
			tblUser user = (tblUser)array[0];

			List<tblUserExpense> userExpenses = HttpClientHandler.GetUserExpensesFromUser(user);

			bool found;
			foreach (var userExpenseItem in userExpenses)
			{
				found = false;
				foreach (var UserExpenseItem in UserExpenses)
				{
					if (userExpenseItem.fldExpenseID == userExpenseItem.fldExpenseID)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					App.Current.Dispatcher.BeginInvoke(new Action(() => { UserExpenses.Add(userExpenseItem); }));
				}
			}
			
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		public void Delete(tblTrip trip)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(trip, PopupMethod.Delete);
		}

		#endregion

	}
}
