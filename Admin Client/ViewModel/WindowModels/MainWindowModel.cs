using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
using Admin_Client.Model.Domain;
using Admin_Client.Model.FileIO;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls.Special;
using Admin_Client.View.Windows.Popout;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Admin_Client.ViewModel.WindowModels
{
	public class MainWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables
		private Window mainWindow;

		private Grid Grid_Menu;
		private Grid Grid_AccountTab;
		private ContentControl CControl_Main;

		#endregion

		#region Parameters

		#region - Login

		private bool isloggetIn = false;

		public bool IsloggetIn
		{
			get { return isloggetIn; }
			set { isloggetIn = value; NotifyPropertyChanged(); }
		}

		private bool isPasswordHidden = false;

		public bool IsPasswordHidden
		{
			get { return isPasswordHidden; }
			set { isPasswordHidden = value; NotifyPropertyChanged(); }
		}

		#endregion

		#endregion

		#region Constructor

		public MainWindowModel()
		{
			ThreadPool.QueueUserWorkItem(APIFastConnectThread, new object());
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create relations to the Grids and CControl so they can be manipulated from this class
		/// </summary>
		/// <param name="Grid_Menu"></param>
		/// <param name="Grid_AccTab"></param>
		/// <param name="CC_Main"></param>
		public void CreateGridRelations(Grid Grid_Menu, Grid Grid_AccTab, ContentControl CC_Main)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Grid Relations --> Creating"));

			this.Grid_Menu = Grid_Menu;
			this.Grid_AccountTab = Grid_AccTab;
			this.CControl_Main = CC_Main;

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Grid Relations == Created"));
		}

		/// <summary>
		/// Set the ContentControls content, which is the middle of the window
		/// </summary>
		/// <param name="content">The userControl also known as View</param>
		public void SetMainContent(UserControl content)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));

			CControl_Main.Content = content;

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));
		}

		/// <summary>
		/// Set the ContentControls content, which is the middle of the window
		/// </summary>
		/// <param name="content">The userControl also known as View</param>
		/// <param name="isMenuActive">Show menu or not</param>
		public void SetMainContent(UserControl content, bool isMenuActive)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));

			CControl_Main.Content = content;

			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(isMenuActive);
		}

		/// <summary>
		/// Set the ContentControls content, which is the middle of the window
		/// </summary>
		/// <param name="content">The userControl also known as View</param>
		/// <param name="isMenuActive">Show menu or not</param>
		/// <param name="isAccountTabActive">Show account tab or not</param>
		public void SetMainContent(UserControl content, bool isMenuActive = true, bool isAccountTabActive = true)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(isMenuActive);
			IsAccountTabActive(isAccountTabActive);
		}

		/// <summary>
		/// Set the ContentControls content, which is the middle of the window, adds a back to previous view function
		/// </summary>
		/// <param name="content">The userControl also known as View</param>
		/// <param name="backToView">The backToView</param>
		/// <param name="isAccountTabActive">Show account tab or not</param>
		public void SetMainContent(UserControl content, UserControl backToView, bool isAccountTabActive = true)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(backToView);
			IsAccountTabActive(isAccountTabActive);
		}

		/// <summary>
		/// Set the ContentControls content, which is the middle of the window, adds a back to previous view function
		/// </summary>
		/// <param name="content">The userControl also known as View</param>
		/// <param name="backToView">The backToView</param>
		public void SetMainContent(UserControl content, UserControl backToView)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(backToView);
		}

		/// <summary>
		/// Gets the content from the MainWindows ContentControl
		/// </summary>
		/// <returns>The content, which most often is a View</returns>
		public UserControl GetMainContent()
		{
			return (UserControl)CControl_Main.Content;
		}

		/// <summary>
		/// Set the relation between the currentMainWindow and the MainWindow
		/// </summary>
		/// <param name="window"></param>
		public void SetMainWindow(Window window)
		{
			this.mainWindow = window;
		}

		/// <summary>
		/// Get the MainWindow object
		/// </summary>
		/// <returns></returns>
		public Window GetMainWindow()
		{
			return mainWindow;
		}

		#endregion

		#region StartWindows

		/// <summary>
		/// Start the PopoutLog
		/// </summary>
		/// <param name="dateTime"></param>
		public void StartPopoutLog(DateTime dateTime)
		{
			new PopoutLogWindow(dateTime).Show();
		}

		/// <summary>
		/// Start the PopoutLogTool
		/// </summary>
		public void StartPopoutLogTool()
		{
			new PopoutLogToolWindow(mainWindow).Show();
		}

		/// <summary>
		/// Start the PopupConfirm
		/// </summary>
		/// <param name="o">The targetet object</param>
		/// <param name="popupMethod">The method that gets invoked</param>
		/// <param name="linkedO">The linked object</param>
		public void StartPopupConfirm(object o, PopupMethod popupMethod, object linkedO = null)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, popupMethod + " Click --> Target " + o.GetType().Name));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupConfirm --> Starting"));

			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;
			new PopupConfirmWindow(o, popupMethod, linkedO).ShowDialog();

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupConfirm == Closed"));
		}

		/// <summary>
		/// Start the PopupParameterChange
		/// </summary>
		/// <param name="o">The targetet object</param>
		public void StartPopupParameterChange(object o)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Edit Click --> Target " + o.GetType().Name));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupParameterChange --> Starting"));

			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;
			new PopupParameterChangeWindow(mainWindow, o).ShowDialog();

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupParameterChange == Closed"));
		}

		/// <summary>
		/// Start the PopupPasswordChange
		/// </summary>
		/// <param name="user">The targetet user</param>
		public void StartPopupPasswordChange(tblUser user)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Edit Click --> Target " + user.fldUserID + " " + user.fldFirstName + " " + user.fldLastName));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupPasswordChange --> Starting"));

			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;
			new PopupPasswordChangeWindow(mainWindow, user).ShowDialog();

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupPasswordChange == Closed"));
		}

		/// <summary>
		/// Start the PopAddUser
		/// </summary>
		/// <param name="group">The current selected group</param>
		/// <param name="members">The current members of selected group</param>
		public void StartPopupAddUser(tblGroup group, List<tblUser> members)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Add Click --> New User"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopoutAddUser --> Starting"));

			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;
			new PopupAddUserWindow(mainWindow, group, members).ShowDialog();

			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopoutAddUser == Closed"));
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Set the Menu as inactive or active
		/// </summary>
		/// <param name="active">The boolean for it</param>
		private void IsMenuActive(bool active)
		{
			if (active)
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log("Menu --> True"));
			}
			else
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log("Menu/BackMenu --> False"));
			}

			if (active)
			{
				int currentMenuElements = this.Grid_Menu.Children.Count;
				if (currentMenuElements != 3)
				{
					if (currentMenuElements == 2)
					{
						IsMenuActive(false);
					}
					Border extraChild = new Border(); // This is created to add one more child so Menu has 3 and BackMenu has 2. Then operations can be handled via the amount of children
					Border border = new Border();
					Grid.SetColumn(border, 1);
					Grid.SetRow(border, 2);
					border.Background = Brushes.White;
					border.BorderBrush = Brushes.Gray;
					border.BorderThickness = new Thickness(0, 0, 1, 1);
					border.CornerRadius = new CornerRadius(0, 0, 10, 0);
					this.Grid_Menu.Children.Add(extraChild);
					this.Grid_Menu.Children.Add(border);

					this.Grid_Menu.Children.Add(new MenuView());
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Menu == True"));
				}
				else
				{
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "Menu == Already Active"));
				}
			}
			else
			{
				this.Grid_Menu.Children.Clear();
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Menu/BackMenu == False"));
			}
		}

		/// <summary>
		/// Set the menu to active and set the content in it
		/// </summary>
		/// <param name="backToView">The new content</param>
		private void IsMenuActive(UserControl backToView)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("BackMenu --> True\t ( --CallBack--> " + backToView.GetType().Name + " )"));
			int currentMenuElements = this.Grid_Menu.Children.Count;
			if (currentMenuElements != 2)
			{
				if (currentMenuElements == 3)
				{
					IsMenuActive(false);
				}

				Border border = new Border();
				Grid.SetColumn(border, 1);
				Grid.SetRow(border, 2);
				border.Background = Brushes.White;
				border.BorderBrush = Brushes.Gray;
				border.BorderThickness = new Thickness(0, 0, 1, 1);
				border.CornerRadius = new CornerRadius(0, 0, 10, 0);
				border.Height = 50;
				border.VerticalAlignment = VerticalAlignment.Top;
				this.Grid_Menu.Children.Add(border);

				this.Grid_Menu.Children.Add(new BackMenuView(backToView));
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "BackMenu == True\t ( --CallBack--> " + backToView.GetType().Name + " )"));
			}
			else
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "BackMenu == Already Active"));
			}
		}

		/// <summary>
		/// Set the account tab as active or inactive
		/// </summary>
		/// <param name="active">The boolean for it</param>
		private void IsAccountTabActive(bool active)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("AccountTab --> " + active));
			if (active)
			{
				if (this.Grid_AccountTab.Children.Count < 1)
				{
					Border border = new Border();
					Grid.SetColumn(border, 1);
					Grid.SetRow(border, 2);
					border.Background = Brushes.White;
					border.BorderBrush = Brushes.Gray;
					border.BorderThickness = new Thickness(1, 0, 0, 1);
					border.CornerRadius = new CornerRadius(0, 0, 0, 10);
					this.Grid_AccountTab.Children.Add(border);

					this.Grid_AccountTab.Children.Add(new AccountTabView());
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "AccountTab == True"));
				}
				else
				{
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "AccountTab == Already Active"));
				}
			}
			else
			{
				this.Grid_AccountTab.Children.Clear();
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "AccountTab == False"));
			}
		}

		#endregion

		#region APIFastConnect

		/// <summary>
		/// Gets all users on launch of the program, so the next call to the API is faster
		/// </summary>
		/// <param name="o"></param>
		public void APIFastConnectThread(object o)
		{
			Thread.Sleep(500);
			List<tblUser> users = HttpClientHandler.GetUsers();
		}

		#endregion

	}
}
