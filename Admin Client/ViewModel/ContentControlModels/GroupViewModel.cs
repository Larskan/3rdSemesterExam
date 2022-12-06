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
using System.Windows;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class GroupViewModel : NotifyPropertyChangedHandler
	{
        //fix sorting groups by ID, need to change update method / add functionality to addremove member and trip buttons / also probably a better update method lol

        private ObservableCollection<tblUser> users = new ObservableCollection<tblUser>();
        public ObservableCollection<tblUser> Users
        {
            get { return users; }
            set { users = value; }
        }
        private ObservableCollection<tblTrip> trips = new ObservableCollection<tblTrip>();
        public ObservableCollection<tblTrip> Trips
        {
            get { return trips; }
            set { trips = value; }
        }

        public GroupViewModel()
        {

        }
        public void Add()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new tblUser(), PopupMethod.Add);
        }
        public void Create()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new tblTrip(), PopupMethod.Create);
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
                List<tblUser> users = FAKEDATABASE.GetUsers();  

                bool found;
                    foreach (var userItem in users)
                    {
                        found = false;
                        foreach (var UserItem in Users)
                        {
                            if (userItem.fldUserID == UserItem.fldUserID)
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
                //List<tblTrip> trips = FAKEDATABASE.getTrips();

                bool found;
                foreach (var userItem in trips)
                {
                    found = false;
                    foreach (var UserItem in Trips)
                    {
                        if (userItem.fldTripID == UserItem.fldTripID)
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
