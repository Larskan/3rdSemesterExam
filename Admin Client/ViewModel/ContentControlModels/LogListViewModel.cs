﻿using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class LogListViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private int startupDelay = 500;

		#endregion

		#region Properties

		private string username;

		public string Username
		{
			get { return username; }
			set { username = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<TblTrip> logs = new ObservableCollection<TblTrip>();

		public ObservableCollection<TblTrip> Logs
		{
			get { return logs; }
			set { logs = value; }
		}

		#endregion

		#region Constructor

		public LogListViewModel(TblUser user)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get Logs for User: " + user.FldUserId + " " + user.FldFirstName + " " + user.FldFirstName));

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThread, new object[] { user });
		}

		#endregion

		#region Public Methods

		private void UpdateReceiptListThread(object o)
		{
			Thread.Sleep(startupDelay);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = o as object[];
			TblUser user = (TblUser)array[0];

			/*
			// CHANGE THE FAKEDATEBASE.GETGROUPS() - TODO
			List<TblReceipts> receipts = FAKEDATABASE.GetReceipts(user);

			bool found;
			foreach (var receiptItem in receipts)
			{
				found = false;
				foreach (var ReceiptItem in Receipts)
				{
					if (receiptItem.FldReceiptId == ReceiptItem.FldReceiptId)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					App.Current.Dispatcher.BeginInvoke(new Action(() => { Receipts.Add(receiptItem); }));
				}
			}
			*/
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		public void Delete(TblTrip trip)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(trip, PopupMethod.Delete);
		}

		#endregion

	}
}
