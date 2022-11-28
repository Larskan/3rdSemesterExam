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
	/// Interaction logic for PopupParameterChangeWindow.xaml
	/// </summary>
	public partial class PopupParameterChangeWindow : Window
	{
		PopupParameterChangeWindowModel windowModel;
		public PopupParameterChangeWindow(Window owner, object o)
		{
			this.windowModel = new PopupParameterChangeWindowModel(this, o);
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
		}

		private void Change_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Change();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Cancel();
		}
	}
}
