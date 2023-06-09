﻿using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
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

namespace Admin_Client.View.UserControls.Special
{
	/// <summary>
	/// Interaction logic for BackMenuView.xaml
	/// </summary>
	public partial class BackMenuView : UserControl
	{
		BackMenuViewModel viewModel;
		
		public BackMenuView(UserControl lastUControl)
		{
			this.viewModel = new BackMenuViewModel(lastUControl);
			this.DataContext = viewModel;

			InitializeComponent();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Back();
		}
	}
}
