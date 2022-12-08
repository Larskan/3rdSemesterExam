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
	/// Interaction logic for LogView.xaml
	/// </summary>
	public partial class LogListView : UserControl
	{
		LogListViewModel viewModel;
		public LogListView(tblUser user)
		{
			viewModel = new LogListViewModel(user);
			this.DataContext = viewModel;

			InitializeComponent();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{

		}

	}
}
