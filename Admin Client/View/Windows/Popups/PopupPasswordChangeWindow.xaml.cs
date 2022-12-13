using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.Model.Foundation;
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
	/// Interaction logic for PopupPasswordChangeWindow.xaml
	/// </summary>
	public partial class PopupPasswordChangeWindow : Window
	{
		PopupPasswordChangeWindowModel windowModel;
		public PopupPasswordChangeWindow(Window owner, tblUser user)
		{
			this.windowModel = new PopupPasswordChangeWindowModel(this, user);
			this.DataContext = windowModel;

			this.Owner = owner;

			InitializeComponent();
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			// ENCRYPT PASSWORD AND CHECK
			//pull pw from database, encrypt written new password and compare it	

			if (TextBox_NewPassword.Text.Equals(TextBox_RetypeNewPassword.Text))
			{
				bool loginSuccess = false;
				List<tblUser> users = HttpClientHandler.GetUsers();
				foreach (tblUser user in users)
				{
					if (user.fldIsAdmin && user.fldPassword.Equals(Encryption.Encrypt_Password(TextBox_Password.Text, Encryption.Salt_Password(TextBox_Password.Text))))
					{
						windowModel.Confirm(TextBox_Password.Text, TextBox_NewPassword.Text);
						loginSuccess = true;
						break;
					}
				}

			}
			else
			{
				TextBox_Password.Text = "";
				TextBox_NewPassword.Text = "";
				TextBox_RetypeNewPassword.Text = "";
				PasswordBox_Password.Password = "";
				PasswordBox_NewPassword.Password = "";
				PasswordBox_RetypeNewPassword.Password = "";
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Cancel();
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

			TogglePasswordView.Content = new Image
			{
				Source = new BitmapImage(
					new Uri(
						Environment.GetFolderPath(
							Environment.SpecialFolder.UserProfile
							)
						+ @"\Source\Repos\SplitBillsIntoFairShares\Admin Client\Img\"
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

		private void TextBox_NewPassword_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBox_NewPassword.IsFocused)
			{
				PasswordBox_NewPassword.Password = TextBox_NewPassword.Text;
				PasswordBox_NewPassword.SelectAll();
			}
		}

		private void PasswordBox_NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PasswordBox_NewPassword.IsFocused)
			{
				TextBox_NewPassword.Text = PasswordBox_NewPassword.Password;
				TextBox_NewPassword.Select(TextBox_NewPassword.Text.Length, 0);
			}
			if (PasswordBox_NewPassword.Password.Length > 0)
			{
				if (Label_NewPasswordPrompt.Visibility != Visibility.Hidden)
				{
					Label_NewPasswordPrompt.Visibility = Visibility.Hidden;
				}
			} else
			{
				Label_NewPasswordPrompt.Visibility = Visibility.Visible;
			}
		}

		private void ToggleNewPasswordView_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox_NewPassword.Visibility = Visibility.Hidden;
			Label_NewPasswordPrompt.Visibility = Visibility.Hidden;
			TextBox_NewPassword.Visibility = Visibility.Visible;

			ToggleNewPasswordView.Content = new Image
			{
				Source = new BitmapImage(
					new Uri(
						Environment.GetFolderPath(
							Environment.SpecialFolder.UserProfile
							)
						+ @"\Source\Repos\SplitBillsIntoFairShares\Admin Client\Img\"
						+ "visibleno.png"
						)
					)
			};

			PasswordBox_NewPassword.Focus();
		}

		private void ToggleNewPasswordView_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox_NewPassword.Visibility = Visibility.Visible;
			if (PasswordBox_NewPassword.Password.Length == 0)
			{
				Label_NewPasswordPrompt.Visibility = Visibility.Visible;
			}
			TextBox_NewPassword.Visibility = Visibility.Hidden;

			ToggleNewPasswordView.Content = new Image
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

			TextBox_NewPassword.Focus();
		}

		private void TextBox_RetypeNewPassword_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBox_RetypeNewPassword.IsFocused)
			{
				PasswordBox_RetypeNewPassword.Password = TextBox_RetypeNewPassword.Text;
				PasswordBox_RetypeNewPassword.SelectAll();
			}
		}

		private void PasswordBox_RetypeNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (PasswordBox_RetypeNewPassword.IsFocused)
			{
				TextBox_RetypeNewPassword.Text = PasswordBox_RetypeNewPassword.Password;
				TextBox_RetypeNewPassword.Select(TextBox_RetypeNewPassword.Text.Length, 0);
			}
			if (PasswordBox_RetypeNewPassword.Password.Length > 0)
			{
				if (Label_RetypeNewPasswordPrompt.Visibility != Visibility.Hidden)
				{
					Label_RetypeNewPasswordPrompt.Visibility = Visibility.Hidden;
				}
			} else
			{
				Label_RetypeNewPasswordPrompt.Visibility = Visibility.Visible;
			}
		}

		private void ToggleRetypeNewPasswordView_Checked(object sender, RoutedEventArgs e)
		{
			PasswordBox_RetypeNewPassword.Visibility = Visibility.Hidden;
			Label_RetypeNewPasswordPrompt.Visibility = Visibility.Hidden;
			TextBox_RetypeNewPassword.Visibility = Visibility.Visible;

			ToggleRetypeNewPasswordView.Content = new Image
			{
				Source = new BitmapImage(
					new Uri(
						Environment.GetFolderPath(
							Environment.SpecialFolder.UserProfile
							)
						+ @"\Source\Repos\SplitBillsIntoFairShares\Admin Client\Img\"
						+ "visibleno.png"
						)
					)
			};

			PasswordBox_RetypeNewPassword.Focus();
		}

		private void ToggleRetypeNewPasswordView_Unchecked(object sender, RoutedEventArgs e)
		{
			PasswordBox_RetypeNewPassword.Visibility = Visibility.Visible;
			if (PasswordBox_RetypeNewPassword.Password.Length == 0)
			{
				Label_RetypeNewPasswordPrompt.Visibility = Visibility.Visible;
			}
			TextBox_RetypeNewPassword.Visibility = Visibility.Hidden;

			ToggleRetypeNewPasswordView.Content = new Image
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

			TextBox_RetypeNewPassword.Focus();
		}

	}
}
