using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class UserViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		tblUser user;

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public UserViewModel(tblUser user)
		{
			this.user = user;
		}

		#endregion

		#region Public Methods

		public void EditUser()
		{
			MainWindowModelSingleton.Instance.StartPopupConfirm(user, PopupMethod.Edit);
		}

		public void DeleteReceipt()
		{

		}

		#endregion

		#region Private Methods

		#endregion

	}
}
