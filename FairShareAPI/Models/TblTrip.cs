using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblTrip
{
	[DataMember]
	public int fldTripId { get; set; }

	[DataMember]
	public double? fldSum { get; set; }

	[DataMember]
	public string? fldTripName { get; set; }

	[DataMember]
	public string? fldTripDate { get; set; }

	public virtual ICollection<tblGroupToTrip> tblGroupToTrip { get; } = new List<tblGroupToTrip>();

	public virtual ICollection<tblReceipt> tblReceipts { get; } = new List<tblReceipt>();

	public virtual ICollection<tblTripToUserExpense> tblTripToUserExpense { get; } = new List<tblTripToUserExpense>();
	public virtual ICollection<tblUserExpense> tblUserExpense { get; } = new List<tblUserExpense>();
}
