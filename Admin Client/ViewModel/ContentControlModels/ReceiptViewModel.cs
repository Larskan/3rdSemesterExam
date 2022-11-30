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
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class ReceiptViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private string username;

		public string Username
        {
			get { return username; }
			set { username = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<TblReceipt> receipts = new ObservableCollection<TblReceipt>();

		public ObservableCollection<TblReceipt> Receipts
        {
			get { return receipts; }
			set { receipts = value; }
		}


		#endregion

		#region Constructor

		public ReceiptViewModel()
		{

		}

		#endregion

		#region Public Methods

		public void Delete(TblReceipt receipt)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(receipt, PopupMethod.Delete);
		}

		#endregion

	}
}
