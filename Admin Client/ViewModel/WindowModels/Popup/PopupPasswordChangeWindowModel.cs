﻿using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupPasswordChangeWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		#endregion

		#region Properties

		#endregion

		#region Constructor

		public PopupPasswordChangeWindowModel()
		{

			if (LogHandlerSingleton.Instance.WriteToLogFile(new Log("Open Popup PasswordChangeWindow")))
			{
				LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "PasswordChangeWindow is shown"));
			}

		}

		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion

	}
}