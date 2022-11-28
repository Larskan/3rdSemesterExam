using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class AccountViewModel : NotifyPropertyChangedHandler
	{

		#region Properties

		private string initials;

		public string Initials
		{
			get { return initials; }
			set { initials = value; NotifyPropertyChanged(); }
		}

		private string username;

		public string Username
		{
			get { return username; }
			set { username = value; NotifyPropertyChanged(); }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; NotifyPropertyChanged(); }
		}

		private string phonenumber;

		public string Phonenumber
		{
			get { return phonenumber; }
			set { phonenumber = value; NotifyPropertyChanged(); }
		}


		#endregion

		#region Constructor

		public AccountViewModel()
		{
			// Get Current USER
			this.initials = "FL";
			this.username = "FirstName LastName";
			this.email = "First@Last.com";
			this.phonenumber = "" + 42424242;
		}

		#endregion

		#region Public Methods

		public void Edit()
		{
			// EDIT POPUP WITH CURRENT USER
		}

		public void LogTool()
		{
			MainWindowModelSingleton.Instance.StartPopoutLogTool();
		}

		#endregion

	}
}
