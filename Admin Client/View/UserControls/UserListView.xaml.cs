using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
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
	/// Interaction logic for UserListView.xaml
	/// </summary>
	public partial class UserListView : UserControl
	{
		UserListViewModel viewModel = new UserListViewModel();
		public UserListView()
		{
			this.DataContext = viewModel;

			InitializeComponent();

			CollectionView userView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Users.ItemsSource);
			userView.Filter = FilterList;
		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Update();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Create();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			if (ListBox_Users.SelectedItem != null)
			{
				viewModel.Edit((tblUser)ListBox_Users.SelectedItem);

				// NOPE
				//Userview.SetUserID(FAKEDATABASE.GetUserID((tblUser)ListBox_Users.SelectedItem));
				//MainWindowModelSingleton.Instance.SetMainContent(new UserView(), true);
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (ListBox_Users.SelectedItem != null)
			{
				viewModel.Delete((tblUser)ListBox_Users.SelectedItem);
			}
		}
		private void OnPageLoaded(object sender, RoutedEventArgs e)
		{

			viewModel.Update();
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
