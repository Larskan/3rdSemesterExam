using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
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

		private void Change_Click(object sender, RoutedEventArgs e)
		{
			if (TextBox_Password.Text.Equals(TextBox_RetypePassword.Text))
			{
				windowModel.Change(TextBox_Password.Text);
			}
			else
			{
				TextBox_Password.Text = "";
				TextBox_RetypePassword.Text = "";
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Cancel();
		}

	}
}
