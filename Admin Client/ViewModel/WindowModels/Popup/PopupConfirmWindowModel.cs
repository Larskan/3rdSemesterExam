using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupConfirmWindowModel : NotifyPropertyChangedHandler
	{

        #region Variables

        private object target;
		private Action action;
		private object linkedObject;

		private Window currentWindow;

		#endregion

		#region Properties

		private string actionText;

		public string ActionText
		{
			get { return actionText; }
			set { actionText = value; NotifyPropertyChanged(); }
		}

		private string targetText;

		public string TargetText
		{
			get { return targetText; }
			set { targetText = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region Constructor

		public PopupConfirmWindowModel(Window currentWindow, object target, PopupMethod popupMethod, object linkedObject)
		{
            // Set the methods and other object related info
            switch (popupMethod)
			{
				case PopupMethod.Edit: this.action = Edit; break;
				case PopupMethod.Create: this.action = Create; break;
				case PopupMethod.Delete: this.action = Delete; break;
				case PopupMethod.Add: this.action = Add; break;
				case PopupMethod.Remove: this.action = Remove; break;
			}
			this.target = target;
			this.currentWindow = currentWindow;
			this.linkedObject = linkedObject;

			ActionText = action.GetMethodInfo().Name;
			if (action.GetMethodInfo().Name == "Create")
			{
				TargetText = target.GetType().Name;
			}
			else
			{
				switch (this.target.GetType().Name)
				{
					case "tblUser":
						{
							tblUser user = (tblUser)target;
							TargetText = user.fldUserID + " - " + user.fldFirstName + " " + user.fldLastName;
							break;
						}
					case "tblGroup":
						{
							tblGroup group = (tblGroup)target;
							TargetText = group.fldGroupID + " - " + group.fldGroupName;
							break;
						}
					default: TargetText = ActionText; ActionText = ""; break;
				}
			}
		}

		#endregion

		#region Public Methods

		public void Confirm()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Confirm Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(target.GetType().Name + "." + action.GetMethodInfo().Name + "() --> Invoke"));
			action.Invoke();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, target.GetType().Name + "." + action.GetMethodInfo().Name + "() == Success"));

			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		public void Cancel()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		#endregion

		#region Private Methods

		private void Edit()
		{
			switch (target.GetType().Name)
			{
				case "tblUser":
					{
						tblUser user = (tblUser)target;

                        MainWindowModelSingleton.Instance.SetMainContent(new UserView(user));
                        
                        LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + user.fldUserID + " - " + user.fldFirstName + " " + user.fldLastName));
						break;
					}
				case "tblGroup":
					{
						tblGroup group = (tblGroup)target;

                        MainWindowModelSingleton.Instance.SetMainContent(new GroupView(group));

                        LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + group.fldGroupID + " - " + group.fldGroupName));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name + "()");
			}
		}

		private void Create()
		{
			switch (target.GetType().Name)
			{
				case "tblUser":
					{
						tblUser user = (tblUser)target;

						MainWindowModelSingleton.Instance.StartPopupParameterChange(user);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "New: ID " + user.fldUserID + " - " + user.fldFirstName + " " + user.fldLastName));
						break;
					}
				case "tblGroup":
					{
						tblGroup group = (tblGroup)target;

                        MainWindowModelSingleton.Instance.StartPopupParameterChange(group);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "New: ID " + group.fldGroupID + " - " + group.fldGroupName));
						break;
					}
				case "tblTrip":
					{
						tblTrip trip = (tblTrip)target;

						MainWindowModelSingleton.Instance.StartPopupParameterChange(trip);

						while (true)
						{
							bool isFound = false;
							foreach (var tempTrip in HttpClientHandler.GetTrips())
							{
								if (tempTrip.fldTripName.Equals(trip.fldTripName) && tempTrip.fldTripDate.Equals(trip.fldTripDate))
								{
									trip.fldTripID = tempTrip.fldTripID;
									isFound= true;
								}
							}
							if (isFound)
							{
								Thread.Sleep(250);
								break;
							}
						}

						HttpClientHandler.Post(new tblGroupToTrip() { fldGroupID = ((tblGroup)this.linkedObject).fldGroupID, fldTripID = trip.fldTripID });

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "New: ID " + trip.fldTripID + " - " + trip.fldTripDate));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name + "()");
			}
		}

		private void Delete()
		{
			switch (target.GetType().Name)
			{
				case "tblUser":
					{
						tblUser user = (tblUser)target;

						HttpClientHandler.Delete(SqlObjectType.tblUser, user.fldUserID);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + user.fldUserID + " - " + user.fldFirstName + " " + user.fldLastName));
						break;
					}
				case "tblGroup":
					{
						tblGroup group = (tblGroup)target;

						HttpClientHandler.Delete(SqlObjectType.tblGroup, group.fldGroupID);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + group.fldGroupID + " - " + group.fldGroupName));
						break;
					}
				case "tblReceipt":
					{
						tblReceipt receipt = (tblReceipt)target;

						HttpClientHandler.Delete(SqlObjectType.tblReceipt, receipt.fldReceiptID);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + receipt.fldReceiptID + " - " + receipt.fldUserID + " " + receipt.tblUser.fldFirstName + " " + receipt.tblUser.fldLastName));
						break;
					}
				case "tblTrip":
					{
						tblTrip trip = (tblTrip)target;

						HttpClientHandler.Delete(SqlObjectType.tblTrip, trip.fldTripID);

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + trip.fldTripID));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name + "()");
			}
		}
        private void Add()
        {
            switch (target.GetType().Name)
            {
                case "tblUser":
                    {
                        tblUser user = (tblUser)target;

                        

                        break;
                    }
                default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name + "()");
            }
        }

		private void Remove()
		{
			switch (target.GetType().Name)
			{
				case "tblUser":
					{
						tblUser user = (tblUser)target;

						List<tblUserToGroup> list = HttpClientHandler.GetUserToGroups();

						foreach (var relation in list)
						{
							if (relation.fldUserID == user.fldUserID)
							{
								HttpClientHandler.Delete(SqlObjectType.tblUserToGroup, relation.fldUserToGroupID);
								break;
							}
						}

						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name + "()");
			}
		}
		#endregion

	}
}
