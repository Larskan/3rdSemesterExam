using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.ContentControlModels.Special;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin_Client.View.UserControls.Special
{
	/// <summary>
	/// Interaction logic for AccountTabView.xaml
	/// </summary>
	public partial class AccountTabView : UserControl
	{
		AccountTabViewModel viewModel = new AccountTabViewModel();
		public AccountTabView()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log("AccountTab is starting"));
			InitializeComponent();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "AccountTab is shown"));

			this.DataContext = viewModel;
		}
	}
}
