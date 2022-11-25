using Admin_Client.Model.Domain;
using Admin_Client.Model.FileIO;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.UserControls.Special;
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
			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("MainWindow is starting")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "MainWindow is shown"));
			}
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
			this.Grid_Menu = Grid_Menu;
			this.Grid_AccountTab = Grid_AccTab;
			this.CControl_Main = CC_Main;
		}

		public void IsMenuActive(bool active)
		{
			if (active)
			{
				if (this.Grid_Menu.Children.Count < 1)
				{
					Border border = new Border();
					Grid.SetColumn(border,1);
					Grid.SetRow(border,2);
					border.Background = Brushes.White;
					border.BorderBrush = Brushes.Gray;
					border.BorderThickness = new Thickness(0,0,1,1);
					border.CornerRadius = new CornerRadius(0,0,10,0);
					this.Grid_Menu.Children.Add(border);

					this.Grid_Menu.Children.Add(new MenuView());
				}
				else
				{
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "Menu is already shown"));
				}
			} else
			{
				this.Grid_Menu.Children.Clear();
			}
		}

		public void IsAccountTabActive(bool active)
		{
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
				}
				else
				{
					LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Warning, "AccountTab is already shown"));
				}
			}
			else
			{
				this.Grid_AccountTab.Children.Clear();
			}
		}

		public void SetMainContent(UserControl content)
		{
			CControl_Main.Content = content;
		}

		#endregion

		#region Private Methods


		#endregion

	}
}
