using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client
{
	/// <summary>
	/// The different types of log states
	/// </summary>
	public enum LogType
	{
		FatalError = 999,
		Warning = 500,
		UserAction = 300,
		Information = 200,
		Success = 100,
	}
}
