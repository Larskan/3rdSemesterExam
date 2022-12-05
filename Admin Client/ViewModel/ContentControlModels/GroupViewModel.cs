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

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class GroupViewModel : NotifyPropertyChangedHandler
	{
        //fix sorting groups by ID, need to change update method / add functionality to addremove member and trip buttons / also probably a better update method lol

        private ObservableCollection<TblUser> users = new ObservableCollection<TblUser>();
        public ObservableCollection<TblUser> Users
        {
            get { return users; }
            set { users = value; }
        }
        private ObservableCollection<TblTrip> trips = new ObservableCollection<TblTrip>();
        public ObservableCollection<TblTrip> Trips
        {
            get { return trips; }
            set { trips = value; }
        }

        public GroupViewModel()
        {

        }
        public void Add()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new TblUser(), PopupMethod.Add);
        }
        public void Create()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new TblTrip(), PopupMethod.Create);
        }

        CancellationTokenSource tokenSource;
        public void Update(string methodtype)
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();

            switch (methodtype)
            {
                case "User":
                    {
                        ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
                        break;
                    }
                case "Trip":
                    {
                        ThreadPool.QueueUserWorkItem(UpdateTripsListThread, new object[] { tokenSource.Token });
                        break;
                    }
                default: throw new Exception("Has not been implemented");
            }
            
        }
     


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
        private void UpdateTripsListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                // CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
                List<TblTrip> trips = FAKEDATABASE.GetTrips();

                bool found;
                foreach (var userItem in trips)
                {
                    found = false;
                    foreach (var UserItem in Trips)
                    {
                        if (userItem.FldTripID == UserItem.FldTripID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {

                        App.Current.Dispatcher.BeginInvoke(new Action(() => { Trips.Add(userItem); }));

                    }
                }
                break;
            }
        }

    }
}
