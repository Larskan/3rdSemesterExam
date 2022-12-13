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
	public int fldTripToUserExpenseId { get; set; }

	[DataMember]
	public int? fldTripId { get; set; }

	[DataMember]
	public int? fldExpenseId { get; set; }

	public virtual tblUserExpense? fldExpense { get; set; }

	public virtual tblTrip? fldTrip { get; set; }
}
