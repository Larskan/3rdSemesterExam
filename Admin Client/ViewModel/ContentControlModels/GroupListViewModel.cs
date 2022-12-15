using Admin_Client.Model;
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
using System.Windows.Controls;
using System.Windows.Data;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class GroupListViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private ObservableCollection<tblGroup> groups = new ObservableCollection<tblGroup>();
		public ObservableCollection<tblGroup> Groups
		{
			get { return groups; }
			set { groups = value; }
		}

		#endregion

		#region Constructor

		public GroupListViewModel()
		{

		}

		#endregion

		#region Public Methods

		CancellationTokenSource tokenSource;

		/// <summary>
		/// Start a update of the group list
		/// </summary>
		public void Update()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Groups Click"));
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			tokenSource = new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { tokenSource.Token });
		}

		/// <summary>
		/// Create a new group and update the group list
		/// </summary>
		public void Create()
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(new tblGroup(), PopupMethod.Create);
			Thread.Sleep(250);
			Update();
		}

		/// <summary>
		/// Edit the targetet group and update the group list
		/// </summary>
		/// <param name="group"></param>
		public void Edit(tblGroup group)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(group, PopupMethod.Edit);
			Thread.Sleep(250);
			Groups.Clear();
			Update();
		}

		/// <summary>
		/// Delete the targetet group and update the group list
		/// </summary>
		/// <param name="group"></param>
		public void Delete(tblGroup group)
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(group, PopupMethod.Delete);
			Thread.Sleep(500);
			Groups.Clear();
			Update();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Update the group list
		/// </summary>
		/// <param name="o">The parameters [CancellationToken]</param>
		private void UpdateGroupsListThread(object o)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("ThreadID: " + Thread.CurrentThread.ManagedThreadId + " --> Starting"));

			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				List<tblGroup> groups = HttpClientHandler.GetGroups();

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
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Done"));
				break;
			}
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "ThreadID: " + Thread.CurrentThread.ManagedThreadId + " ==> Closed"));
		}

		#endregion

	}
}
