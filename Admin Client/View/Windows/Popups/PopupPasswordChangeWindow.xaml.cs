﻿using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.WindowModels.Popup;
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
using System.Windows.Shapes;

namespace Admin_Client.View.Windows.Popups
{
	/// <summary>
	/// Interaction logic for PopupPasswordChangeWindow.xaml
	/// </summary>
	public partial class PopupPasswordChangeWindow : Window
	{
		PopupPasswordChangeWindowModel windowModel = new PopupPasswordChangeWindowModel();
		public PopupPasswordChangeWindow()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Poping up PopupPasswordChangeWindow"));
			InitializeComponent();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "PopupPasswordChangeWindow is shown"));

			this.DataContext = windowModel;
		}
	}
}
