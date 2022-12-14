using Admin_Client.Model;
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
using Admin_Client.Model.DB;
using Admin_Client.Model.FileIO;
using Admin_Client.Model.FileIO.PDF;
using Admin_Client.Model.FileIO.TemplatePDF;
using Admin_Client.Model.DB.EF_Test;

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

			
			ReceiptPDF rec = new ReceiptPDF();
			tblTrip trip = new tblTrip();
			var ins = new tblTrip() { fldTripID = 4 };
			rec.GrabData(ins);

			foreach (var item in rec.GetData(ins))
			{
                rec.GrabData(ins);
                Debug.WriteLine(item.FirstName + ": " + item.Expenses);
			}
			

			
			/*
			if (PasswordBox_Password.Password.Length > 0)
			{
				viewModel.Login(TextBox_Username.Text, PasswordBox_Password.Password);
			}
			else
			{
				viewModel.Login(TextBox_Username.Text, TextBox_Password.Text);
			}
			*/
			
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
			if (PasswordBox_Password.Password.Length > 0)
			{
				if (Label_PasswordPrompt.Visibility != Visibility.Hidden)
				{
					Label_PasswordPrompt.Visibility = Visibility.Hidden;
				}
			} else
			{
				Label_PasswordPrompt.Visibility = Visibility.Visible;
			}
		}

		private void TogglePasswordView_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox_Password.Visibility = Visibility.Hidden;
			Label_PasswordPrompt.Visibility = Visibility.Hidden;
			TextBox_Password.Visibility = Visibility.Visible;

				TogglePasswordView.Content = new Image{ 
				Source = new BitmapImage(
					new Uri(
						Environment.GetFolderPath(
							Environment.SpecialFolder.UserProfile
							)
						+ @"\source\repos\SplitBillsIntoFairShares\Admin Client\img\"
                        + "visibleno.png"
						)
					)
			};

			PasswordBox_Password.Focus();
		}

		private void TogglePasswordView_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox_Password.Visibility = Visibility.Visible;
			if (PasswordBox_Password.Password.Length == 0)
			{
				Label_PasswordPrompt.Visibility = Visibility.Visible;
			}
			TextBox_Password.Visibility = Visibility.Hidden;

			TogglePasswordView.Content = TogglePasswordView.Content = new Image
			{
				Source = new BitmapImage(
					new Uri(
						Environment.GetFolderPath(
							Environment.SpecialFolder.UserProfile
							)
						+ @"\Source\Repos\SplitBillsIntoFairShares\Admin Client\Img\"
						+ "visibleyes.png"
						)
					)
			};

			TextBox_Password.Focus();
		}

	}
}
