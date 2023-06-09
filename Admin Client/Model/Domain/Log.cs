﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{

	/// <summary>
	/// Used to store all data for a log entry
	/// </summary>
	public class Log
	{

		public DateTime DateTime { get; set; }
		public string DateTimeString { get; set; }
		public LogType LogType { get; set; }
		public string LogTxt { get; set; }

		/// <summary>
		/// <para>Create log object</para>
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="logType"></param>
		/// <param name="logTxt"></param>
		public Log(DateTime dateTime, LogType logType, string logTxt)
		{
			this.DateTime = dateTime;
			this.DateTimeString = this.DateTime.ToString();
			this.LogType = logType;
			this.LogTxt = logTxt;
		}

		/// <summary>
		/// <para>Create log object</para>
		/// 
		/// <para>DateTime = DateTime.Now</para>
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="logTxt"></param>
		public Log(LogType logType, string logTxt)
		{
			this.DateTime = DateTime.Now;
			this.DateTimeString = this.DateTime.ToString();
			this.LogType = logType;
			this.LogTxt = logTxt;
		}

		/// <summary>
		/// <para>Create log object</para>
		/// 
		/// <para>
		/// <br>DateTime = DateTime.Now</br>
		/// <br>LogType = LogType.Information</br>
		/// </para>
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="logTxt"></param>
		public Log(string logTxt)
		{
			this.DateTime = DateTime.Now;
			this.DateTimeString = this.DateTime.ToString();
			this.LogType = LogType.Information;
			this.LogTxt = logTxt;
		}

	}
}
