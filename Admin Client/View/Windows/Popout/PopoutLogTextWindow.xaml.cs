using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
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
	/// Interaction logic for PopoutLogTextWindow.xaml
	/// </summary>
	public partial class PopoutLogTextWindow : Window
	{
		public PopoutLogTextWindow(Window owner,string logFileName, string content)
		{
			this.Owner = owner;

			InitializeComponent();

			Label_LogFileName.Content = logFileName;
			TextBlock_Content.Text = content;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "LogText Close Click"));
		}
	}
}
