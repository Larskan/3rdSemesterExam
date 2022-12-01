using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	public class LogFile
	{

		public int Id { get; set; }

		public string Date { get; set; }

		public LogFile(object log)
		{
			this.Id = 1;
			this.Date = DateTime.Now.ToString();
		}

	}
}
