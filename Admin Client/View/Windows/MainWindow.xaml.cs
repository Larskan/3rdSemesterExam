using Admin_Client.Model.Domain;
using Admin_Client.Model.FileIO;
using Admin_Client.Singleton;
using Admin_Client.ViewModel.WindowModels;
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
		MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
        public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this.MainWindowViewModel;
        }

	}
}
