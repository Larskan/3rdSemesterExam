using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popout;
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
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl
	{
		LoginViewModel viewModel = new LoginViewModel();
		public LoginView()
		{
			this.DataContext = viewModel;

			InitializeComponent();
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			if (PasswordBox_Password.Password.Length > 0)
			{
				viewModel.Login(TextBox_Username.Text, PasswordBox_Password.Password);
			} else
			{
				viewModel.Login(TextBox_Username.Text, TextBox_Password.Text);
			}
        }
	}
}
