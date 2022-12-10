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


        }

        private void Delete_Trip(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Trip(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Trip(object sender, RoutedEventArgs e)
        {
            viewModel.Create();
        }

        private void Remove_Member(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Member(object sender, RoutedEventArgs e)
        {
            viewModel.Add();

        }
        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {

            viewModel.Update("User");
            viewModel.Update("Trip");
        }
    }
}
