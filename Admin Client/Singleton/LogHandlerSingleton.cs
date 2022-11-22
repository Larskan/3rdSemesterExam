using Admin_Client.Model.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
	/// <summary>
	/// Used to read and write logs
	/// </summary>
	public sealed class LogHandlerSingleton : LogHandler
	{
		private LogHandlerSingleton() {}
		private static LogHandlerSingleton instance = null;

		/// <summary>
		/// <para>Create Instance or retreive the active one</para>
		/// 
		/// <para>
		/// <para>
		/// <br>Write log = <b>WriteToLogFile</b>(new <b>Log</b>(<i>LOGTEXT</i>))</br>
		/// <br>Write log = <b>WriteToLogFile</b>(new <b>Log</b>(<i>LOGTYPE</i>, <i>LOGTEXT</i>))</br>
		/// <br>Write log = <b>WriteToLogFile</b>(new <b>Log</b>(<i>DATETIME</i>, <i>LOGTYPE</i>, <i>LOGTEXT</i>))</br>
		/// </para>
		/// <para>
		/// <br>Read log = <b>ReadLogFile</b>()</br>
		/// <br>Read log = <b>ReadLogFile</b>(<i>DATETIME</i>)</br>
		/// </para>
		/// </para>
		/// </summary>
		public static LogHandlerSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new LogHandlerSingleton();
				}
				return instance;
			}
		}
	}
}
