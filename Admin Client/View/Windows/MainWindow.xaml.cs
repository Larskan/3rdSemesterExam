using Admin_Client.Singleton;
using Admin_Client.View.UserControls;
using Admin_Client.View.UserControls.Special;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin_Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        public MainWindow()
		{
			InitializeComponent();

			this.CControl_Main.Content = new LoginView();

			this.DataContext = MainWindowModelSingleton.Instance;

			MainWindowModelSingleton.Instance.CreateGridRelations(Grid_Menu, Grid_Account, CControl_Main);
        }

		/// <summary>
		/// Animation for extention of the menu, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Menu_MouseEnter(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["OpenMenu"] as Storyboard;
			sb.Begin(Grid_Menu);
		}
		/// <summary>
		/// Animetion for retracton of the menu, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Menu_MouseLeave(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["CloseMenu"] as Storyboard;
			sb.Begin(Grid_Menu);
		}
		/// <summary>
		/// Animation for extention of the menu, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Menu_GotFocus(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["OpenMenu"] as Storyboard;
			sb.Begin(Grid_Menu);
		}
		/// <summary>
		/// Animetion for retracton of the menu, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Menu_LostFocus(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["CloseMenu"] as Storyboard;
			sb.Begin(Grid_Menu);
		}
		/// <summary>
		/// Animation for extention of the account, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Account_MouseEnter(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["OpenAccountTab"] as Storyboard;
			sb.Begin(Grid_Account);
		}
		/// <summary>
		/// Animation for retreaction of the account, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Account_MouseLeave(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["CloseAccountTab"] as Storyboard;
			sb.Begin(Grid_Account);
		}
		/// <summary>
		/// Animation for extention of the account, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Account_GotFocus(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["OpenAccountTab"] as Storyboard;
			sb.Begin(Grid_Account);
		}
		/// <summary>
		/// Animation for retreaction of the account, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_Account_LostFocus(object sender, RoutedEventArgs e)
		{
			Storyboard sb = Resources["CloseAccountTab"] as Storyboard;
			sb.Begin(Grid_Account);
		}

	}
}
