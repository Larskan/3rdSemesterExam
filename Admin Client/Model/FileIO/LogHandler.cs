using Admin_Client.Model.Domain;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Admin_Client.Model.FileIO
{
	public class LogHandler
	{

		#region Variables

		private static string ROOTPATH = AppDomain.CurrentDomain.BaseDirectory;
		private static string PATH = ROOTPATH + @"Data";

		private string LogFilePath = "";

		#endregion

		#region Constructor

		/// <summary>
		/// Handles all writing and reading between program and files
		/// </summary>
		protected LogHandler()
		{
			CreateDataDir();
			CleanUpFolder();
			CreateLogFile();
		}

		#endregion

		#region Folder

		/// <summary>
		/// Makes sure the Data folder exists otherwise it gets created
		/// </summary>
		/// <returns>True if created/exists, false if not</returns>
		public bool CreateDataDir()
		{
			bool found = false;
			try
			{
				foreach (var item in Directory.GetFiles(ROOTPATH))
				{
					if (item.Equals(PATH))
					{
						found = true;
					}
				}
				if (!found)
				{
					Directory.CreateDirectory(PATH);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		#endregion

		#region LogFile

		/// <summary>
		/// Creates a logfile and saves the direction to the FileHandler for future use
		/// </summary>
		/// <returns>True if logfile has been created/exists, false if not</returns>
		public bool CreateLogFile()
		{
			DateTime dateTime = DateTime.Now;
			string dateTimeFileName = ToFileName(dateTime);
			File.WriteAllText(PATH + ToPath(dateTimeFileName, "txt"), "");
			LogFilePath = ToPath(dateTimeFileName, "txt");
			WriteToLogFile(new Log(LogType.Success, "Log file has been created"));
			return true;
		}

		/// <summary>
		/// Writes a log to the current logfile
		/// </summary>
		/// <param name="log">The log that is targetet</param>
		/// <returns>True if the file has been read, false if not</returns>
		public bool WriteToLogFile(Log log)
		{
			string fileContent = "";
			string spacingItem = "";

			foreach (var item in ReadLogFile())
			{
				switch (item.LogType)
				{
					case LogType.Success: spacingItem = "\t\t\t\t\t\t\t\t"; break;
					case LogType.Information: spacingItem = "\t\t\t\t\t\t\t"; break;
					case LogType.UserAction: spacingItem = "\t"; break;
					case LogType.Warning: spacingItem = "\t\t\t\t\t\t\t\t"; break;
					case LogType.FatalError: spacingItem = "\t\t\t\t\t\t\t"; break;
				}
				fileContent += "[" + item.DateTime + "] {" + item.LogType + "}" + spacingItem + " " + item.LogTxt + "\n";
			}
			string spacing = "";
			switch (log.LogType)
			{
				case LogType.Success: spacing = "\t\t\t\t\t\t\t\t"; break;
				case LogType.Information: spacing = "\t\t\t\t\t\t\t"; break;
				case LogType.UserAction: spacing = "\t"; break;
				case LogType.Warning: spacing = "\t\t\t\t\t\t\t\t"; break;
				case LogType.FatalError: spacing = "\t\t\t\t\t\t\t"; break;
			}
			File.WriteAllText(PATH + LogFilePath, fileContent + "[" + log.DateTime + "] {" + log.LogType + "}" + spacing + " " + log.LogTxt + "\n");

			return true;
		}

		/// <summary>
		/// Reads the contents of the current log file
		/// </summary>
		/// <returns>The logfile's logs as a List</returns>
		public List<Log> ReadLogFile()
		{
			while (true)
			{
				if (IsFileReady(PATH + LogFilePath))
				{
					return StringsToLogs(File.ReadAllText(PATH + LogFilePath).Split('\n'));
				} else
				{
					Thread.Sleep(500);
				}
			}
		}

		/// <summary>
		/// Reads the contents of a log file specified by the date
		/// </summary>
		/// <param name="dateTime">The date of the targeted logfile</param>
		/// <returns>The logfile's logs as a List</returns>
		public List<Log> ReadLogFile(DateTime dateTime)
		{
			try
			{
				List<Log> content = StringsToLogs(File.ReadAllText(PATH + ToPath(ToFileName(dateTime), "txt")).Split('\n'));
				WriteToLogFile(new Log(LogType.Success, "Read targetet file " + ToPath(ToFileName(dateTime), "txt")));
				return content;
			}
			catch
			{
				WriteToLogFile(new Log(LogType.Warning, "Could not find/read targetet file " + ToPath(ToFileName(dateTime), "txt")));
				return new List<Log>();
			}
		}

		/// <summary>
		/// Get all logfiles on the local PC
		/// </summary>
		/// <returns>A list of names in DateTime format</returns>
		public List<DateTime> GetLocalLogFiles()
		{
			if (!CreateDataDir())
			{
				WriteToLogFile(new Log(LogType.Warning, "Could not find DataDir creating new DataDir"));
			}

			List<DateTime> files = new List<DateTime>();

			foreach (var item in Directory.GetFiles(PATH))
			{
				DateTime fileDateTime = ToDateTime(ToFileName(item));

				files.Add(fileDateTime);
			}

			return files;

		}

		#endregion

		#region CleanUp

		/// <summary>
		/// Makes sure that only a curtain amount of files are stored
		/// </summary>
		/// <returns>True if cleanUp was a success, false if not</returns>
		public bool CleanUpFolder()
		{
			int amountOfLogFilesStored = 25;

			string[] logFiles = Directory.GetFiles(PATH);
			if (logFiles.Length >= amountOfLogFilesStored)
			{
				string deleteTarget = logFiles[0];
				foreach (var item in logFiles) 
				{
					if (DateTime.Compare(ToDateTime(ToFileName(deleteTarget)),ToDateTime(ToFileName(item))) > 0)
					{
						deleteTarget = item;
					}
				}
				File.Delete(deleteTarget);
			}

			return true;
		}

		#endregion CleanUp

		#region Convertions

		/// <summary>
		/// Formats the string to represent a path
		/// </summary>
		/// <param name="fileName">The name of the file</param>
		/// <param name="fileType">The file extention</param>
		/// <returns>The formatted string</returns>
		private string ToPath(string fileName, string fileType)
		{
			return "\\" + fileName + "." + fileType;
		}

		/// <summary>
		/// Formats the string to represent a fileName
		/// </summary>
		/// <param name="path">The path to the file</param>
		/// <returns>The formatted string</returns>
		private string ToFileName(string path)
		{
			return path.Split('\\').Last();
		}

		/// <summary>
		/// Formats the datetime datatype to string, which can be used as a filename
		/// </summary>
		/// <param name="dateTime">The datetime</param>
		/// <returns>The formattet string</returns>
		private string ToFileName(DateTime dateTime)
		{
			return dateTime.ToString().Replace(':', '_').Replace('/','-');
		}

		/// <summary>
		/// Formats the string to dateTime datatype, which can be used to get the time
		/// </summary>
		/// <param name="fileName">The fileName</param>
		/// <returns>The dateTime datatype</returns>
		private DateTime ToDateTime(string fileName)
		{
			if (fileName.Equals(""))
			{
				return DateTime.MinValue;
			}
			return DateTime.Parse(fileName.Replace('_', ':').Replace(".txt", ""));
		}

		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		private List<Log> StringsToLogs(string[] strings)
		{
			List<Log> logs = new List<Log>();
			foreach (var item in strings)
			{
				string fItem = item.Replace("\t", "");
				string[] fItemParts = fItem.Split(' ');

				if (!fItemParts[0].Equals(""))
				{
					DateTime dateTime = DateTime.Parse(fItemParts[0].Substring(1) + " " + fItemParts[1].Substring(0, fItemParts[1].Length - 1));
					LogType logType = (LogType)Enum.Parse(typeof(LogType), fItemParts[2].Substring(1, fItemParts[2].Length - 2));
					string text = fItemParts[3];
					foreach (var parts in fItemParts.Skip(4))
					{
						text += " " + parts;
					}

					logs.Add(new Log(dateTime, logType, text));
				}
			}
			return logs;
		}

		#endregion

		#region IsFileLocked

		private bool IsFileReady(string fileName)
		{
			try
			{
				File.ReadAllText(fileName);
				return true;
			}
			catch (IOException) 
			{ 
				return false;
			}
		}

		#endregion

	}
}
