using Admin_Client.Model;
using Admin_Client.Model.Controller;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.Model.Foundation;
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
			}
			else
			{
				viewModel.Login(TextBox_Username.Text, TextBox_Password.Text);
			}
		}

		private void TextBox_Password_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBox_Password.IsFocused)
			{
				PasswordBox_Password.Password = TextBox_Password.Text;
				PasswordBox_Password.SelectAll();
			}
		}

		private void PasswordBox_Password_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PasswordBox_Password.IsFocused)
			{
				TextBox_Password.Text = PasswordBox_Password.Password;
				TextBox_Password.Select(TextBox_Password.Text.Length, 0);
			}
		}

		private void TogglePasswordView_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox_Password.Visibility = Visibility.Visible;
			TextBox_Password.Visibility = Visibility.Hidden;

			PasswordBox_Password.Focus();
		}

		private void TogglePasswordView_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox_Password.Visibility = Visibility.Hidden;
			TextBox_Password.Visibility = Visibility.Visible;

			TextBox_Password.Focus();
		}

	}
}
