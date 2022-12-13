using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblUserExpense
{
	[DataMember]
	public int fldExpenseId { get; set; }

	[DataMember]
	public int? fldUserId { get; set; }

	[DataMember]
	public int? fldTripId { get; set; }

	[DataMember]
	public double? fldExpense { get; set; }

	[DataMember]
	public string? fldNote { get; set; }

	[DataMember]
	public DateTime? fldDate { get; set; }

	public virtual tblUser? fldUser { get; set; }

	public virtual tblTrip? fldTrip { get; set; }

	public virtual ICollection<tblTripToUserExpense> tblTripToUserExpense { get; } = new List<tblTripToUserExpense>();
}
