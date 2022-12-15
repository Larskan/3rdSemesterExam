using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
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

namespace Admin_Client.ViewModel.ContentControlModels
{
    public class UserViewModel : NotifyPropertyChangedHandler
    {
        #region Variables

        tblUser user;
        private string initials;

		#endregion

		#region Properties

		private ObservableCollection<tblGroup> groups = new ObservableCollection<tblGroup>();
		public ObservableCollection<tblGroup> Groups
		{
			get { return groups; }
			set { groups = value; }
		}

		private ObservableCollection<tblReceipt> receipts = new ObservableCollection<tblReceipt>();
		public ObservableCollection<tblReceipt> Receipts
		{
			get { return receipts; }
			set { receipts = value; }
		}

		public string Initials
		{
			get { return initials; }
			set { initials = value; NotifyPropertyChanged(); }
		}

		private string firstname;

		public string Firstname
		{
			get { return firstname; }
			set { firstname = value; NotifyPropertyChanged(); }
		}

		private string lastname;

		public string Lastname
		{
			get { return lastname; }
			set { lastname = value; NotifyPropertyChanged(); }
		}

		private string username;

		public string Username
		{
			get { return username; }
			set { username = value; NotifyPropertyChanged(); }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; NotifyPropertyChanged(); }
		}

		private string phonenumber;

		public string Phonenumber
		{
			get { return phonenumber; }
			set { phonenumber = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region Constructor

        /// <summary>
        /// Creates a UserViewModel with a targetet user
        /// </summary>
        /// <param name="user">The target</param>
		public UserViewModel(tblUser user)
        {
            this.user = user;
            this.Firstname = user.fldFirstName;
            this.Lastname = user.fldLastName;
            this.Initials = "" + Firstname.First() + Lastname.First();
            this.Username = this.Firstname + " " + this.Lastname;
            this.Email = user.fldEmail;
            this.Phonenumber = user.fldPhonenumber;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Edit the currentUser
        /// </summary>
        public void EditUser()
        {
            MainWindowModelSingleton.Instance.StartPopupParameterChange(user);
        }

        /// <summary>
        /// Delete the targetet receipt and update the receipt list
        /// </summary>
        /// <param name="receipt">The target</param>
        public void DeleteReceipt(tblReceipt receipt)
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(receipt, PopupMethod.Delete);
            Thread.Sleep(500);
            Receipts.Clear();
            UpdateReceipts();
        }

        CancellationTokenSource tokenSourceGroups;
        /// <summary>
        /// Starts an update on the group list
        /// </summary>
        public void UpdateGroups()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Groups Click"));
            if (tokenSourceGroups != null && tokenSourceGroups.Token.CanBeCanceled)
            {
				tokenSourceGroups.Cancel();
            }
			tokenSourceGroups = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { tokenSourceGroups.Token });
            
        }

		CancellationTokenSource tokenSourceReceipts;
        /// <summary>
        /// Starts an update on the receitps
        /// </summary>
		public void UpdateReceipts()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Receipt Click"));
            if (tokenSourceReceipts != null && tokenSourceReceipts.Token.CanBeCanceled)
            {
				tokenSourceReceipts.Cancel();
            }
			tokenSourceReceipts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(UpdateReceiptsListThread, new object[] { tokenSourceReceipts.Token });
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the group list
        /// </summary>
        /// <param name="o">The parameters [CancellationToken]</param>
        private void UpdateGroupsListThread(object o)
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                List<tblGroup> groups = HttpClientHandler.GetGroupsFromUser(user);

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

				return;
			}
        }

		/// <summary>
		/// Updates the receipt list
		/// </summary>
		/// <param name="o">The parameters [CancellationToken]</param>
		private void UpdateReceiptsListThread(object o)
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
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

                return;
            }
        }
        #endregion
    }
}
