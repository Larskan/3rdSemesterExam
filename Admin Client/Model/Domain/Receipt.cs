using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText;

namespace Admin_Client.Model.Domain
{
	public class Receipt
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string GroupName { get; set; }

		public string ProjectedValue { get; set; }

		public string AmountPaid { get; set; }

		public Receipt(tblReceipt receipt)
		{
			// TODO
			this.Id = 1;
			this.Name = "Spain trip";
			this.GroupName = "Butiful girls";
			this.ProjectedValue = 23.34 + " DKK";
			this.AmountPaid = 18.44 + "DKK";

		
		}
	}
}
