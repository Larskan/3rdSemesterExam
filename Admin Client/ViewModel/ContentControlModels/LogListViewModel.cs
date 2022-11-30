using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class LogListViewModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public LogListViewModel(TblUser user)
		{
            // TODO - GET LOGS FOR USER
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Information, "Get Logs for User: "+user.FldUserId + " " + user.FldFirstName + " " + user.FldFirstName));

            //ThreadPool.QueueUserWorkItem(UpdateGroupsListThread, new object[] { user });

        }

        #endregion

        #region Public Methods

        

        #endregion

        #region Private Methods

        #endregion

    }
}
