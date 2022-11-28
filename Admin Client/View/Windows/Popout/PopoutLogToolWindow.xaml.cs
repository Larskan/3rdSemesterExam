using Admin_Client.ViewModel.WindowModels.Popout;
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

namespace Admin_Client.View.Windows.Popout
{
	/// <summary>
	/// Interaction logic for PopoutLogToolWindow.xaml
	/// </summary>
	public partial class PopoutLogToolWindow : Window
	{
		PopoutLogToolWindowModel windowModel = new PopoutLogToolWindowModel();
		public PopoutLogToolWindow(Window owner)
		{
			this.DataContext = windowModel;

			this.Owner = owner;

			InitializeComponent();
		}

		private void ListBox_Groups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (ListBox_LogFiles.SelectedItem != null)
			{
				if (ListBox_LogFiles.SelectedItem.ToString().Contains("Current"))
				{
					windowModel.OpenLogFile(DateTime.Parse(ListBox_LogFiles.SelectedItem.ToString().Replace(" ( Current )","")));
				} else
				{
					windowModel.OpenLogFile(DateTime.Parse(ListBox_LogFiles.SelectedItem.ToString()));
				}
			}
        }

		private void Window_Closed(object sender, EventArgs e)
		{
			windowModel.Closed();
		}
	}
}
