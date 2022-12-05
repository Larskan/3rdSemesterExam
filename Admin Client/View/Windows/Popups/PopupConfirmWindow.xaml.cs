using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.WindowModels.Popup;
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
using System.Windows.Shapes;

namespace Admin_Client.View.Windows.Popups
{
	public enum PopupMethod
	{
		Edit,
		Create,
		Delete
	}

	/// <summary>
	/// Interaction logic for PopupConfirmWindow.xaml
	/// </summary>
	public partial class PopupConfirmWindow : Window
	{
		PopupConfirmWindowModel windowModel;
		public PopupConfirmWindow(object target, PopupMethod popupMethod)
		{
			windowModel = new PopupConfirmWindowModel(this, target, popupMethod);
			this.DataContext= windowModel;

			this.Owner = MainWindowModelSingleton.Instance.GetMainWindow();

			InitializeComponent();
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Confirm();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Cancel();
		}

	}
}
