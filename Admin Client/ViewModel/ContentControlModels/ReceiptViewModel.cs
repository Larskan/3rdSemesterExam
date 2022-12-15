using Admin_Client.Model;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class ReceiptViewModel : NotifyPropertyChangedHandler
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

		private ObservableCollection<tblReceipt> receipts = new ObservableCollection<tblReceipt>();

		public ObservableCollection<tblReceipt> Receipts
		{
			get { return receipts; }
			set { receipts = value; }
		}


		#endregion

		#region Constructor

		/// <summary>
		/// Creates a ReceiptViewModel with a targetet user and starts a update on the receipt list
		/// </summary>
		/// <param name="user">The target</param>
		public ReceiptViewModel(tblUser user)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get TemplateReceiptPDF for User: " + user.fldUserID + " " + user.fldFirstName + " " + user.fldFirstName));

			ThreadPool.QueueUserWorkItem(UpdateReceiptListThread, new object[] { user });
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Update the receipt list
		/// </summary>
		/// <param name="o">The parameters [tblUser]</param>
		private void UpdateReceiptListThread(object o)
		{
			Thread.Sleep(startupDelay);

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = o as object[];
			tblUser user = (tblUser)array[0];

			List<tblReceipt> receipts = HttpClientHandler.GetReceiptsFromUser(user);

			bool found;
			foreach (var receiptItem in receipts)
			{
				found = false;
				foreach (var ReceiptItem in Receipts)
				{
					if (receiptItem.fldReceiptID == ReceiptItem.fldReceiptID)
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
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		public void Delete(tblReceipt receipt)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(receipt, PopupMethod.Delete);
		}

		#endregion

	}
}
