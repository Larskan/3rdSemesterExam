using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class OverviewViewModel : NotifyPropertyChangedHandler
	{

		#region Constructor

		public OverviewViewModel() 
		{
			
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Set the MainWindows ContentControls content to GroupList
		/// </summary>
		public void SetContentToGroupList()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new GroupListView(), true);
		}

		/// <summary>
		/// Set the MainWindows ContentControls content to UserList
		/// </summary>
		public void SetContentToUserList()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new UserListView(), true);
		}

		#endregion

	}
}
