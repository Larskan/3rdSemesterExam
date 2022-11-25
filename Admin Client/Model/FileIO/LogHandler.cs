using Admin_Client.Model.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
			try
			{
				DateTime dateTime = DateTime.Now;
				string dateTimeFileName = ToFileName(dateTime);
				File.WriteAllText(PATH + ToPath(dateTimeFileName, "txt"), "");
				LogFilePath = ToPath(dateTimeFileName, "txt");
				WriteToLogFile(new Log(LogType.Success, "Log file has been created"));
			} catch
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Writes a log to the current logfile
		/// </summary>
		/// <param name="log">The log that is targetet</param>
		/// <returns>True if the file has been read, false if not</returns>
		public bool WriteToLogFile(Log log)
		{
			try
			{
				string fileContent = ReadLogFile();
				File.WriteAllText(PATH + LogFilePath, fileContent + "[" + log.DateTime + "] {" + log.LogType + "} " + log.LogTxt + "\n");
			} catch
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Reads the contents of the current log file
		/// </summary>
		/// <returns>The logfile's logs as a string</returns>
		public string ReadLogFile()
		{
			try
			{
				return File.ReadAllText(PATH + LogFilePath);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Reads the contents of a log file specified by the date
		/// </summary>
		/// <param name="dateTime">The date of the targeted logfile</param>
		/// <returns>The logfile's logs as a string</returns>
		public string ReadLogFile(DateTime dateTime)
		{
			try
			{
				string content = File.ReadAllText(PATH + ToPath(ToFileName(dateTime), "txt"));
				WriteToLogFile(new Log(LogType.Success, "Read targetet file " + ToPath(ToFileName(dateTime), "txt")));
				return content;
			}
			catch
			{
				WriteToLogFile(new Log(LogType.Error, "Could not read targetet file " + ToPath(ToFileName(dateTime), "txt")));
				return null;
			}
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
			try
			{
				string[] logFiles = Directory.GetFiles(PATH);
				if (logFiles.Length >= amountOfLogFilesStored)
				{
					File.Delete(logFiles[0]);
				}
			}
			catch
			{
				return false;
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
			return dateTime.ToString().Replace(':', '_');
		}

		/// <summary>
		/// Formats the string to dateTime datatype, which can be used to get the time
		/// </summary>
		/// <param name="fileName">The fileName</param>
		/// <returns>The dateTime datatype</returns>
		private DateTime ToDateTime(string fileName)
		{
			return DateTime.Parse(fileName.Replace('_', ':').Replace(".txt", ""));
		}

		#endregion

	}
}
