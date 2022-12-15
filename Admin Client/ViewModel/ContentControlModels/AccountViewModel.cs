using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
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

		private string firstname;

		public string Firstname
		{
			get { return firstname; }
			set { firstname = value; NotifyPropertyChanged(); }
		}

		private string lastname;

		public string Lastname
		{
			get { return lastname; }
			set { lastname = value; NotifyPropertyChanged(); }
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

		/// <summary>
		/// Creates a accountViewModel with information from the currentUser
		/// </summary>
		public AccountViewModel()
		{
			tblUser currentUser = HttpClientHandler.currentUser;

			this.Firstname = currentUser.fldFirstName;
			this.Lastname = currentUser.fldLastName;
			this.Initials = "" + Firstname.First() + Lastname.First();
			this.Username = this.Firstname + " " + this.Lastname;
			this.Email = currentUser.fldEmail;
			this.Phonenumber = currentUser.fldPhonenumber;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Edit the currentUsers information via PopupParameterChange
		/// </summary>
		public void Edit()
		{
			MainWindowModelSingleton.Instance.StartPopupParameterChange(HttpClientHandler.currentUser);
		}

		/// <summary>
		/// Edit the currentUseres password via PopupPasswordChange
		/// </summary>
		public void EditPassword()
		{
			MainWindowModelSingleton.Instance.StartPopupPasswordChange(HttpClientHandler.currentUser);
		}

		/// <summary>
		/// Start the logtool
		/// </summary>
		public void LogTool()
		{
			MainWindowModelSingleton.Instance.StartPopoutLogTool();
		}

		#endregion

	}
}
