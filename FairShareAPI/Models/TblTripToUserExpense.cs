using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblTripToUserExpense
{
	[DataMember]
	public int fldUserToExpense { get; set; }

	[DataMember]
	public int? fldTripId { get; set; }

	[DataMember]
	public int? fldExpensesId { get; set; }

	public virtual tblUserExpense? fldExpenses { get; set; }

	public virtual tblTrip? fldTrip { get; set; }
}
