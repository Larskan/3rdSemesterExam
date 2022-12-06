using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblReceipt
{
	[DataMember]
	public int fldReceiptId { get; set; }

	[DataMember]
	public int? fldUserId { get; set; }

	[DataMember]
	public int? fldTripId { get; set; }

	[DataMember]
	public double? fldProjectedValue { get; set; }

	[DataMember]
	public double? fldAmountPaid { get; set; }

	public virtual tblTrip? fldTrip { get; set; }

	public virtual tblUser? fldUser { get; set; }
}
