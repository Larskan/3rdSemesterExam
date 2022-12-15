using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using DocumentFormat.OpenXml.Presentation;
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
		#region Variables

		private Window currentWindow;
        private tblGroup currentGroup;
        private List<tblUser> currentMembers;

		#endregion

		#region Properties

		private ObservableCollection<tblUser> users = new ObservableCollection<tblUser>();
        public ObservableCollection<tblUser> Users
        {
            get { return users; }
            set { users = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a PopupAddUserWindowModel and creates the relations to some of the views elements
        /// </summary>
        /// <param name="currentWindow">The Window</param>
        /// <param name="group">The current group</param>
        /// <param name="members">The current members</param>
        public PopupAddUserWindowModel(Window currentWindow, tblGroup group, List<tblUser> members)
        {
            this.currentWindow = currentWindow;
            this.currentGroup = group;
            this.currentMembers = members;
        }

        #endregion

        #region Public Methods
 
        /// <summary>
        /// Create the relation between the currentGroup and the targetet user
        /// </summary>
        /// <param name="user">The target</param>
        public void Add(tblUser user)
        {
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
            HttpClientHandler.Post(new tblUserToGroup() { fldUserID = user.fldUserID, fldGroupID = currentGroup.fldGroupID});
			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

        /// <summary>
        /// Cancel the relation creation for currentGroup
        /// </summary>
        public void Cancel()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
            currentWindow.Close();
            MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
        }


        CancellationTokenSource tokenSource;
        /// <summary>
        /// Starts an update on the user list
        /// </summary>
        public void Update()
        {
            Users.Clear();
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Users Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the user list
        /// </summary>
        /// <param name="o"></param>
        private void UpdateUsersListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
				List<tblUser> users = HttpClientHandler.GetUsers();

				bool isfound;
                bool isMember;
                foreach (var userItem in users)
                {
                    // Check if they already are a member
					isMember = false;
					isfound = false;
					foreach (var member in currentMembers)
                    {
						if (member.fldUserID == userItem.fldUserID)
						{
							isMember= true;
							break;
						} else
						{ // Check if they already exist in the list
							foreach (var UserItem in Users)
							{
								if (userItem.fldUserID == UserItem.fldUserID)
								{
									isfound = true;
									break;
								}
							}
						}
					}
                    
                    if (!isfound && !isMember)
                    {
                        App.Current.Dispatcher.BeginInvoke(new Action(() => { Users.Add(userItem); }));
                    }
                }
                break;
            }
        }

        #endregion
    }

}
