using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using Admin_Client.ViewModel.ContentControlModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin_Client.View.UserControls
{
    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl
    {
        GroupViewModel viewModel;
        public GroupView(tblGroup group)
        {
            viewModel = new GroupViewModel(group);
			this.DataContext = viewModel;

            InitializeComponent();

			CollectionView tripView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Trips.ItemsSource);
			tripView.Filter = FilterTripList;
			CollectionView memberView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Members.ItemsSource);
			memberView.Filter = FilterMemberList;
		}

		private void OnPageLoaded(object sender, RoutedEventArgs e)
		{
			viewModel.UpdateMembers();
			viewModel.UpdateTrips();
		}

		private void MemberAdd_Click(object sender, RoutedEventArgs e)
		{
			// TODO
		}

		private void MemberRemove_Click(object sender, RoutedEventArgs e)
		{
			if (ListBox_Members.SelectedItem != null)
			{
				viewModel.MemberRemove((tblUser)ListBox_Members.SelectedItem);
			}
		}

		private void TripCreate_Click(object sender, RoutedEventArgs e)
		{
			viewModel.TripCreate();
		}

		private void TripEdit_Click(object sender, RoutedEventArgs e)
		{
			
			if (ListBox_Trips.SelectedItem != null)
			{
				viewModel.TripEdit((tblTrip)ListBox_Trips.SelectedItem);
			}
		}

		private void TripDelete_Click(object sender, RoutedEventArgs e)
		{
			if (ListBox_Trips.SelectedItem != null)
			{
				viewModel.TripDelete((tblTrip)ListBox_Trips.SelectedItem);
			}
		}

		#region Filtering

		private void TextBoxTrip_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListBox_Trips.ItemsSource).Refresh();
		}

		private bool FilterTripList(object item)
		{
			if (String.IsNullOrEmpty(TextBoxTrip_Search.Text))
				return true;
			else if (((tblTrip)item).fldTripID.ToString().IndexOf(TextBoxTrip_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblTrip)item).fldTripDate.IndexOf(TextBoxTrip_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else
				return (((tblTrip)item).fldSum.ToString().IndexOf(TextBoxTrip_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private void TextBoxMembers_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListBox_Members.ItemsSource).Refresh();
		}

		private bool FilterMemberList(object item)
		{
			if (String.IsNullOrEmpty(TextBoxMembers_Search.Text))
				return true;
			else if (((string)((tblUser)item).fldFirstName + " " + ((tblUser)item).fldLastName).IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldLastName.IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldEmail.IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldPhonenumber.IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else if (((tblUser)item).fldUserID.ToString().IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
			else
				return (((tblUser)item).fldIsAdmin.ToString().IndexOf(TextBoxMembers_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}


		#endregion

	}
}
