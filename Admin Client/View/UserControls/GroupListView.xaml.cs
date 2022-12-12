using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using Admin_Client.ViewModel.ContentControlModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// Interaction logic for GroupListView.xaml
	/// </summary>
	public partial class GroupListView : UserControl
	{
		OverviewSingleton Overview = OverviewSingleton.getInstance();
		GroupListViewModel viewModel = new GroupListViewModel();
		public GroupListView()
		{
			this.DataContext = viewModel;

			InitializeComponent();

			CollectionView groupView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Groups.ItemsSource);
			groupView.Filter = FilterList;
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
			if (ListBox_Groups.SelectedItem != null)
			{
				viewModel.Edit((tblGroup)ListBox_Groups.SelectedItem);
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (ListBox_Groups.SelectedItem != null)
			{
				viewModel.Delete((tblGroup)ListBox_Groups.SelectedItem);
			}
		}
		private void OnPageLoaded(object sender, RoutedEventArgs e)
		{

			viewModel.Update();
		}

		#region Filtering

		private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListBox_Groups.ItemsSource).Refresh();
		}

		private bool FilterList(object item)
		{
			if (String.IsNullOrEmpty(TextBox_Search.Text))
				return true;
			else if (((tblGroup)item).fldGroupName.IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblGroup)item).fldGroupID.ToString().IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else
				return (((tblGroup)item).fldIsTemporary.ToString().IndexOf(TextBox_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		#endregion


	}
}
