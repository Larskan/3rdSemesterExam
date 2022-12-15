using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;    
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        /// <summary>
        /// Creates a GroupViewModel with a targetet group
        /// </summary>
        /// <param name="group">The target</param>
		public GroupViewModel(tblGroup group)
        {
            this.group = group;
            this.groupName = this.group.fldGroupName;
        }

		#endregion

		#region Public Methods

        /// <summary>
        /// Add a member to the currentGroup and update member list
        /// </summary>
		public void MemberAdd()
        {
            List<tblUser> tempMembers = new List<tblUser>();

            foreach (var item in Members)
            {
				tempMembers.Add(item);

			}

			MainWindowModelSingleton.Instance.StartPopupAddUser(group, tempMembers);
			Thread.Sleep(500);
			UpdateMembers();
		}

        /// <summary>
        /// Remove targetet user from the currentGroup and update member list
        /// </summary>
        /// <param name="user">The target</param>
		public void MemberRemove(tblUser user)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Remove);
			Thread.Sleep(500);
			Members.Clear();
			UpdateMembers();
		}

        /// <summary>
        /// Create a trip on the currentGroup
        /// </summary>
		public void TripCreate()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new tblTrip(), PopupMethod.Create, group);
			Thread.Sleep(500);
			UpdateTrips();
		}

        /// <summary>
        /// Delete a trip from the currentGruop
        /// </summary>
        /// <param name="trip"></param>
        public void TripDelete(tblTrip trip)
        {
			MainWindowModelSingleton.Instance.StartPopupConfirm(trip, PopupMethod.Delete);
			Thread.Sleep(500);
            Trips.Clear();
			UpdateTrips();
		}

        /// <summary>
        /// Open a UserExpenseListView with the targetet trip
        /// </summary>
        /// <param name="trip">The target</param>
		public void TripEdit(tblTrip trip)
		{
			MainWindowModelSingleton.Instance.SetMainContent(new UserExpenseListView(trip));
		}

		#region Update

		CancellationTokenSource tokenSourceMembers;
        /// <summary>
        /// Start a update on the members list
        /// </summary>
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
        /// <summary>
        /// Start a update on the trips list
        /// </summary>
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

        /// <summary>
        /// Update the members list
        /// </summary>
        /// <param name="o">The parameters [CancellationToken]</param>
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

		/// <summary>
		/// Update the trips list
		/// </summary>
		/// <param name="o">The parameters [CancellationToken]</param>
		private void UpdateTripsListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                List<tblTrip> trips = HttpClientHandler.GetTripsFromGroup(new tblGroup() { fldGroupID = group.fldGroupID });

				bool found;
                foreach (var tripItem in trips)
                {
                    found = false;
                    foreach (var TripItem in Trips)
                    {
                        if (tripItem.fldTripID == TripItem.fldTripID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                      App.Current.Dispatcher.BeginInvoke(new Action(() => { Trips.Add(tripItem); }));
                    }
                }
                break;
            }
        }

		#endregion

	}
}
