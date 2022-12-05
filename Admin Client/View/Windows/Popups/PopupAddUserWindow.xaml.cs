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
        
        public PopupAddUserWindow(Window owner, object o)
        {
            this.windowModel = new PopupAddUserWindowModel(this, o);
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
            CollectionView groupView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox_Parameters.ItemsSource);
            groupView.Filter = FilterList;
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            windowModel.Add(ListBox_Parameters);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            windowModel.Cancel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //CollectionViewSource.GetDefaultView(ListBox_Parameters.ItemsSource).Refresh();

            
        }

        private void ListBox_Parameters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // CollectionViewSource.GetDefaultView(ListBox_Parameters.ItemsSource).Refresh();
        }
        private bool FilterList(object item)
        {
            if (String.IsNullOrEmpty(Searchbar.Text))
                return true;
            else
                Console.WriteLine((item.ToString().IndexOf(Searchbar.Text) >= 0));
                return (item.ToString().IndexOf(Searchbar.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            
        }
        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {

            windowModel.Update();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            windowModel.Search();
        }
    }
}
