using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
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

		#region Variables

		private ObservableCollection<TblGroup> groups = new ObservableCollection<TblGroup>();
		public ObservableCollection<TblGroup> Groups
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
		public void Update()
		{
			if (tokenSource != null && tokenSource.Token.CanBeCanceled)
			{
				tokenSource.Cancel();
			}
			tokenSource= new CancellationTokenSource();

			ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { tokenSource.Token });
		}

		public void Create()
		{
			new PopupConfirmWindow(new TblGroup(), PopupTarget.Create).ShowDialog();
		}

		public void Edit(TblGroup group)
		{
			new PopupConfirmWindow(group, PopupTarget.Create).ShowDialog();
		}

		public void Delete(TblGroup group)
		{
			new PopupConfirmWindow(group, PopupTarget.Create).ShowDialog();
		}

		#endregion

		#region Private Methods

		private void UpdateGroupsListThread(object o)
		{
			object[] array = o as object[];
			CancellationToken token = (CancellationToken)array[0];

			while (!token.IsCancellationRequested)
			{
				// CHANGE THE FAKEDATEBASE.GETGROUPS() - TODO
				List<TblGroup> groups = FAKEDATABASE.GetGroups();

				bool found;
				foreach (var groupItem in groups)
				{
					found= false;
					foreach (var GroupItem in Groups)
					{
						Debug.WriteLine(GroupItem.FldGroupId);
						if (groupItem.FldGroupId == GroupItem.FldGroupId)
						{
							found= true;
							break;
						}
					}
					if (!found)
					{
						App.Current.Dispatcher.BeginInvoke(new Action(() => { Groups.Add(groupItem); }));
					}
				}
				break;
			}
		}

		#endregion

	}
}
