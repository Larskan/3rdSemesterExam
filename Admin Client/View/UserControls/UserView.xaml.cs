﻿using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.ContentControlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin_Client.View.UserControls
{
	/// <summary>
	/// Interaction logic for UserView.xaml
	/// </summary>
	public partial class UserView : UserControl
	{
		UserViewModel viewModel = new UserViewModel();
		public UserView()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Changing CC to UserView"));
			InitializeComponent();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "UserView is shown as content"));

			this.DataContext = viewModel;
		}
	}
}