using Client.ViewModel.WindowModels;
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
using Client.Model._1_Controller;
using Client.EF;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = new MainWindowViewModel();
		}

		//Just for concept proof that it works
		private void OnClick_CMD(object sender, RoutedEventArgs e)
		{
			AddingGroup(3, "LarsKan", true);
		}

        public void AddingGroup(int ID, string name, bool temp)
        {
            var context = new FairShareDBEntities();
            var group = new tblGroup()
            {
                fldGroupID = ID,
                fldGroupName = name,
                fldTempBool = temp
            };
            context.tblGroup.Add(group);
            context.SaveChanges();

        }
    }
}
