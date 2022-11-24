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

			//TEST
			this.CControl_Account.Children.Add(new AccountTabView());
			this.CControl_Menu.Children.Add(new MenuView());
			this.CControl_Main.Content = new OverviewView();
			//TEST

			this.DataContext = MainWindowModelSingleton.Instance;
        }

		/// <summary>
		/// Animation for extention of the menu, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CControl_Menu_MouseEnter(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["OpenMenu"] as Storyboard;
			sb.Begin(CControl_Menu);
		}
		/// <summary>
		/// Animetion for retracton of the menu, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CControl_Menu_MouseLeave(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["CloseMenu"] as Storyboard;
			sb.Begin(CControl_Menu);
		}
		/// <summary>
		/// Animation for extention of the account, triggered on mouseEntering
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CControl_Account_MouseEnter(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["OpenAccountTab"] as Storyboard;
			sb.Begin(CControl_Account);
		}
		/// <summary>
		/// Animation for retreaction of the account, triggered on mouseLeaving
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CControl_Account_MouseLeave(object sender, MouseEventArgs e)
		{
			Storyboard sb = Resources["CloseAccountTab"] as Storyboard;
			sb.Begin(CControl_Account);
		}
		
	}
}
