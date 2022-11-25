using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.ContentControlModels.Special
{
	public class BackMenuViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public BackMenuViewModel()
		{

		}

		#endregion

		#region Public Methods

		public void Back(UserControl content)
		{
			MainWindowModelSingleton.Instance.IsAccountTabActive(true);
			if (content.GetType().Name.Equals("OverviewView"))
			{
				MainWindowModelSingleton.Instance.IsMenuActive(false);
			} else
			{
				MainWindowModelSingleton.Instance.IsMenuActive(true);
			}
			MainWindowModelSingleton.Instance.SetMainContent(content);
		}

		#endregion

		#region Private Methods

		#endregion

	}
}
