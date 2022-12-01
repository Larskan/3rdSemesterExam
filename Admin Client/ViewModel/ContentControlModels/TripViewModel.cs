using Admin_Client.Model;
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
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class TripViewModel : NotifyPropertyChangedHandler
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

		private ObservableCollection<TblTrip> receipts = new ObservableCollection<TblTrip>();

		public ObservableCollection<TblTrip> Receipts
		{
			get { return receipts; }
			set { receipts = value; }
		}


		#endregion

		#region Constructor

		public TripViewModel(TblGroup group)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get Trips for Group: " + group.FldGroupId + " " + group.FldGroupName));

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThread, new object[] { group });
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
			List<TblReceipt> receipts = FAKEDATABASE.GetReceipts(user);

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
