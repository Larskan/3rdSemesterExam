using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Model.FileIO;
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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        UserViewModel viewModel;
        public UserView(tblUser user)
        {
			viewModel = new UserViewModel(user);
			this.DataContext = viewModel;

            InitializeComponent();
        }

        private void ChangeMobileNumberClick(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeEmailClick(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeNameClick(object sender, RoutedEventArgs e)
        {

        }

        private void SeeLogClick(object sender, RoutedEventArgs e)
        {
            //MainWindowModelSingleton.Instance.SetMainContent(new LogListView(), true);
        }

        private void CheckReceiptClick(object sender, RoutedEventArgs e)
        {
            GeneratePDF gen = new GeneratePDF();
            gen.test();
            //doesnt work, what do u want me to pass to the receiptview lol
           // MainWindowModelSingleton.Instance.SetMainContent(new ReceiptView(), true);
        }
    }
}
