﻿using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.ContentControlModels;
using Admin_Client.ViewModel.ContentControlModels.Special;
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
	/// Interaction logic for AccountView.xaml
	/// </summary>
	public partial class AccountView : UserControl
	{
		AccountViewModel viewModel = new AccountViewModel();
		public AccountView()
		{
			this.DataContext = viewModel;

			InitializeComponent();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Edit();
        }

		private void EditPassword_Click(object sender, RoutedEventArgs e)
		{
			viewModel.EditPassword();
		}

		private void LogTool_Click(object sender, RoutedEventArgs e)
		{
			viewModel.LogTool();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
