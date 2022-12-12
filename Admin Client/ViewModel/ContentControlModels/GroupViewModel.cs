using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
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

        #region Variables

        private tblGroup group;

        #endregion

        #region Properties

        private string groupName = "";

        public string GroupName
		{
            get { return groupName; }
            set { groupName = value; }
        }

        private ObservableCollection<tblUser> members = new ObservableCollection<tblUser>();
        public ObservableCollection<tblUser> Members
        {
            get { return members; }
            set { members = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<tblTrip> trips = new ObservableCollection<tblTrip>();
        public ObservableCollection<tblTrip> Trips
        {
            get { return trips; }
            set { trips = value; NotifyPropertyChanged(); }
        }

		#endregion

		#region Constructor
		public GroupViewModel(tblGroup group)
        {
            this.group = group;
            this.groupName = this.group.fldGroupName;
        }
		#endregion

		#region Public Methods

		public void MemberAdd()
        {
			MainWindowModelSingleton.Instance.StartPopupConfirm(new tblUser(), PopupMethod.Add);
			Thread.Sleep(500);
			UpdateMembers();
		}

		public void MemberRemove(tblUser user)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Remove);
			Thread.Sleep(500);
			UpdateMembers();
		}

		public void TripCreate()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new tblTrip(), PopupMethod.Create, group);
			Thread.Sleep(500);
			UpdateTrips();
		}

        public void TripDelete(tblTrip trip)
        {
			MainWindowModelSingleton.Instance.StartPopupConfirm(trip, PopupMethod.Delete);
			Thread.Sleep(500);
			UpdateTrips();
		}

		public void TripEdit(tblTrip trip)
		{
			MainWindowModelSingleton.Instance.SetMainContent(new UserExpenseListView(trip));
		}

		#region Update

		CancellationTokenSource tokenSourceMembers;
        public void UpdateMembers()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Members Click"));
            if (tokenSourceMembers != null && tokenSourceMembers.Token.CanBeCanceled)
            {
				tokenSourceMembers.Cancel();
            }
			tokenSourceMembers = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateMembersListThread, new object[] { tokenSourceMembers.Token });
            
        }

		CancellationTokenSource tokenSourceTrips;
		public void UpdateTrips()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Trips Click"));
            if (tokenSourceTrips != null && tokenSourceTrips.Token.CanBeCanceled)
            {
				tokenSourceTrips.Cancel();
            }
			tokenSourceTrips = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(UpdateTripsListThread, new object[] { tokenSourceTrips.Token });

        }

		#endregion

		#endregion

		#region Private Methods

		private void UpdateMembersListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                List<tblUser> users = HttpClientHandler.GetUsersFromGroup(new tblGroup() { fldGroupID = group.fldGroupID });

                bool found;
                foreach (var userItem in users)
                {
                    found = false;
                    foreach (var UserItem in Members)
                    {
                        if (userItem.fldUserID == UserItem.fldUserID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        App.Current.Dispatcher.BeginInvoke(new Action(() => { Members.Add(userItem); }));
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
                List<tblTrip> trips = HttpClientHandler.GetTripsFromGroup(new tblGroup() { fldGroupID = group.fldGroupID });

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

		#endregion

	}
}
