﻿using Admin_Client.Model.DB;
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

		private int phonenumber;

		public int Phonenumber
		{
			get { return phonenumber; }
			set { phonenumber = value; NotifyPropertyChanged(); }
		}


		#endregion

		#region Constructor

		public AccountViewModel()
		{
			// Get Current USER - TODO (DO NOT!!!!!!! USE TBLUSER AS PARAMETER)
			
			this.Firstname = "FirstName";
			this.Lastname = "LastName";
			this.Initials = "" + Firstname.First() + Lastname.First();
			this.Username = this.Firstname+ " " + this.Lastname;
			this.Email = "First@Last.com";
			this.Phonenumber = 42424242;
		}

		#endregion

		#region Public Methods

		public void Edit()
		{
			// EDIT POPUP WITH CURRENT USER - TODO
			MainWindowModelSingleton.Instance.StartPopupParameterChange(new TblUser() { FldUserId = 0, FldFirstName = Firstname, FldLastName = Lastname, FldEmail = Email, FldPhonenumber = Phonenumber });
		}

		public void LogTool()
		{
			MainWindowModelSingleton.Instance.StartPopoutLogTool();
		}

		#endregion

	}
}
