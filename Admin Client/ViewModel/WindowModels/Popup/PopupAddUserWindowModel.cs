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
using System.Windows;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
    public class PopupAddUserWindowModel : NotifyPropertyChangedHandler
    {
        private Window currentWindow;


        private ObservableCollection<TblUser> users = new ObservableCollection<TblUser>();
        public ObservableCollection<TblUser> Users
        {
            get { return users; }
            set { users = value; }
        }
        public string Searchbar { get; set; }
        

        public PopupAddUserWindowModel(Window currentWindow, object o)
        {
            this.currentWindow = currentWindow;
        }
        List<TblUser> userList = FAKEDATABASE.GetUsers();
        public void Add(ListBox listBox)
        {
            Console.WriteLine("ayy");
        }
        public void Search()
        {
            
            Console.WriteLine(Searchbar);
            userList = new List<TblUser>(userList.Where(x => x.FldFirstName.IndexOf(Searchbar, StringComparison.InvariantCultureIgnoreCase) >= 0));
            //Users = new ObservableCollection<TblUser>(userList.Where(x => x.FldFirstName.IndexOf(Searchbar, StringComparison.InvariantCultureIgnoreCase) >= 0));
            Update();
            Console.WriteLine(userList.Count);
            
        }

        public void Cancel()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
            currentWindow.Close();
            MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
        }


        CancellationTokenSource tokenSource;
        public void Update()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
        }
        private void UpdateUsersListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                // CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
                

                bool found;
                foreach (var userItem in userList)
                {
                    Console.WriteLine(userList.Count);
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
    }

}
