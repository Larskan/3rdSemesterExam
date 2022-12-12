﻿using Admin_Client.Model.DB.EF_Test;
using Admin_Client.ViewModel.ContentControlModels;
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
    /// Interaction logic for PopupAddUserWindow.xaml
    /// </summary>
    public partial class PopupAddUserWindow : Window
    {
        PopupAddUserWindowModel windowModel;
        
        public PopupAddUserWindow(Window owner, tblGroup group)
        {
            this.windowModel = new PopupAddUserWindowModel(this, group);
            this.DataContext = windowModel;

            this.Owner = owner;

            InitializeComponent();

            // Minimum size
            if (owner.Height > 300)
            {
                this.Height = owner.Height / 1.33;

            }
            if (owner.Width > 600)
            {
                this.Width = owner.Width / 1.5;
            }

            
        }

		private void OnPageLoaded(object sender, RoutedEventArgs e)
		{
            windowModel.Update();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Users.SelectedItem != null)
            {
                windowModel.Add((tblUser)ListBox_Users.SelectedItem);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            windowModel.Cancel();
        }

    }
}
