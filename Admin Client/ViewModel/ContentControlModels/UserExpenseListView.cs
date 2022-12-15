﻿using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
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

		private tblTrip currentTrip;

		#endregion

		#region Properties

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; NotifyPropertyChanged(); }
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

			this.Name= user.fldFirstName;

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThreadViaUser, new object[] { user });
		}

		public UserExpenseListViewModel(tblTrip trip)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get UserExpenses for Trip: " + trip.fldTripID + " " + trip.fldTripName));

			this.Name = trip.fldTripName;
			this.currentTrip = trip;

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThreadViaTrip, new object[] { trip });
		}

		#endregion

		#region Public Methods

		public void Delete(tblUserExpense userExpense)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(userExpense, PopupMethod.Delete);
		}

		public void PDF()
		{
			// Du kan bruge dette trip til at få fat i dataen
			// this.currentTrip;

		}

		#endregion

		#region Private Methods

		private void UpdateReceiptListThreadViaUser(object o)
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

		private void UpdateReceiptListThreadViaTrip(object o)
		{
			Thread.Sleep(startupDelay);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = o as object[];
			tblTrip trip = (tblTrip)array[0];

			List<tblUserExpense> userExpenses = HttpClientHandler.GetUserExpensesFromTrip(trip);

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

		#endregion

	}
}
