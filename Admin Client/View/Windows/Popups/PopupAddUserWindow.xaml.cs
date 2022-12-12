using Admin_Client.Model.DB.EF_Test;
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
        
        public PopupAddUserWindow(Window owner, tblGroup group, List<tblUser> members)
        {
            this.windowModel = new PopupAddUserWindowModel(this, group, members);
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

			CollectionView userView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Users.ItemsSource);
			userView.Filter = FilterList;
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

        #region Filtering

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListBox_Users.ItemsSource).Refresh();
		}

		private bool FilterList(object item)
		{
			if (String.IsNullOrEmpty(TextBox_Search.Text))
				return true;
			else if (((string)((tblUser)item).fldFirstName + " " + ((tblUser)item).fldLastName).IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldLastName.IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldEmail.IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldPhonenumber.IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldUserID.ToString().IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else
				return (((tblUser)item).fldIsAdmin.ToString().IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		#endregion

	}
}
