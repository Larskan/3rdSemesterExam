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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
    public class UserViewModel : NotifyPropertyChangedHandler
    {
        UserviewSingleton Usersingleton = UserviewSingleton.getInstance();
        #region Variables

        tblUser user;
        private string initials;

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

        #region Properties

        #endregion

        #region Constructor

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

        public void EditUser()
        {
            MainWindowModelSingleton.Instance.StartPopupParameterChange(user);
            //MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Edit);
        }

        public void DeleteReceipt(tblReceipt receipt)
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(receipt, PopupMethod.Delete);
            Thread.Sleep(500);
            Receipts.Clear();
            UpdateReceipts();
        }

        CancellationTokenSource tokenSource;
        public void UpdateGroups()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Users Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { tokenSource.Token });
            
        }
        public void UpdateReceipts()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Users Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(UpdateReceiptsListThread, new object[] { tokenSource.Token });
        }

        #endregion

        #region Private Methods
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
            }
        }
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
                      Console.WriteLine("ahe");
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
            }
        }
        #endregion
    }
}
