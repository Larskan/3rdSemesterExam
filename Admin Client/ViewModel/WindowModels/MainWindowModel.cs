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
using System.Linq;
using System.Text;
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

		public void SetMainContent(UserControl content)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));
		}

		public void SetMainContent(UserControl content, bool isMenuActive)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(isMenuActive);
		}

		public void SetMainContent(UserControl content, bool isMenuActive = true, bool isAccountTabActive = true)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(isMenuActive);
			IsAccountTabActive(isAccountTabActive);
		}

		public void SetMainContent(UserControl content, UserControl backToView, bool isAccountTabActive = true)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(backToView);
			IsAccountTabActive(isAccountTabActive);
		}

		public void SetMainContent(UserControl content, UserControl backToView)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, content.GetType().Name + " Click"));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("Content --> " + content.GetType().Name));
			CControl_Main.Content = content;
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Content == " + content.GetType().Name));

			IsMenuActive(backToView);
		}

		public UserControl GetMainContent()
		{
			return (UserControl)CControl_Main.Content;
		}

		public void SetMainWindow(Window window)
		{
			this.mainWindow = window;
		}

		public Window GetMainWindow()
		{
			return mainWindow;
		}

		#endregion

		#region StartWindows

		public void StartPopupConfirm(object o, PopupMethod popupMethod)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, popupMethod + " Click --> Target " + o.GetType().Name));
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupConfirm --> Starting"));
			new PopupConfirmWindow(o, popupMethod).ShowDialog();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("PopupConfirm == Closed"));
		}

		public void StartPopoutLog(DateTime dateTime)
		{
			new PopoutLogWindow(dateTime).Show();
		}

		public void StartPopoutLogTool()
		{
			new PopoutLogToolWindow().Show();
		}

		#endregion

		#region Private Methods

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

	}
}
