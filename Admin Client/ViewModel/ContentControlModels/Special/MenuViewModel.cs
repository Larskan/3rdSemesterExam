using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class MenuViewModel : NotifyPropertyChangedHandler
	{

		#region Constructor

		public MenuViewModel() 
		{
			
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Change the MainWindows ContentControls content to GroupListView
		/// </summary>
		public void ChangeContentToGroupList()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new GroupListView());
		}

		/// <summary>
		/// Change the MainWindows ContentControls content to UserListView
		/// </summary>
		public void ChangeContentToUserList()
		{
			MainWindowModelSingleton.Instance.SetMainContent(new UserListView());
		}

		#endregion

	}
}
