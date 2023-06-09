﻿using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.ContentControlModels;
using Admin_Client.ViewModel.WindowModels.Popout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Admin_Client.View.Windows.Popout
{
	/// <summary>
	/// Interaction logic for LogWindow.xaml
	/// </summary>
	public partial class PopoutLogWindow : Window
	{
		PopoutLogWindowModel windowModel;
		public PopoutLogWindow(DateTime dateTime)
		{
			InitializeComponent();

			windowModel = new PopoutLogWindowModel(dateTime, ListBox_Logs, CheckBox_AutoScroll, this);
			this.DataContext = windowModel;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			windowModel.Closed();
		}

		private void ToText_Click(object sender, RoutedEventArgs e)
		{
			windowModel.ToText();
		}
	}
}
