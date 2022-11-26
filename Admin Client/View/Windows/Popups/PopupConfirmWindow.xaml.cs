using Admin_Client.Model.DB;
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
	public enum PopupTarget
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
		public PopupConfirmWindow(TblUser user, PopupTarget popupTarget)
		{
			switch (popupTarget)
			{
				case PopupTarget.Edit: windowModel = new PopupConfirmWindowModel(this, user, Edit); break;
				case PopupTarget.Create: windowModel = new PopupConfirmWindowModel(this, user, Create); break;
				case PopupTarget.Delete: windowModel = new PopupConfirmWindowModel(this, user, Delete); break;
			}
			this.Owner = MainWindowModelSingleton.Instance.GetMainWindow();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;

			InitializeComponent();
		}

		public PopupConfirmWindow(TblGroup group, PopupTarget popupTarget)
		{
			switch (popupTarget)
			{
				case PopupTarget.Edit: windowModel = new PopupConfirmWindowModel(this, group, Edit); break;
				case PopupTarget.Create: windowModel = new PopupConfirmWindowModel(this, group, Create); break;
				case PopupTarget.Delete: windowModel = new PopupConfirmWindowModel(this, group, Delete); break;
			}
			this.Owner = MainWindowModelSingleton.Instance.GetMainWindow();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = false;

			InitializeComponent();
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Confirm();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			windowModel.Cancel();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		#region ActionsToSend

		private void Edit(object o)
		{
			switch (o.GetType().Name)
			{
				case "TblUser": /*Do Stuff*/ break;
				case "TblGroup": /*Do Stuff*/ break;
				default: /* */ break;
			}
		}

		private void Create(object o)
		{
			switch (o.GetType().Name)
			{
				case "TblUser": /*Do Stuff*/ break;
				case "TblGroup": /*Do Stuff*/ break;
				default: /* */ break;
			}
		}

		private void Delete(object o)
		{
			switch (o.GetType().Name)
			{
				case "TblUser": /*Do Stuff*/ break;
				case "TblGroup": /*Do Stuff*/ break;
				default: /* */ break;
			}
		}

		#endregion

	}
}
