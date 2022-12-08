using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblGroupToTrip
{
	[DataMember]
	public int fldGroupToTripId { get; set; }

	[DataMember]
	public int? fldGroupId { get; set; }

	[DataMember]
	public int? fldTripId { get; set; }

	public virtual tblGroup? fldGroup { get; set; }

	public virtual tblTrip? fldTrip { get; set; }
}
