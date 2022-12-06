using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblGroup
{
	[DataMember]
	public int fldGroupId { get; set; }

	[DataMember]
	public string? fldGroupName { get; set; }

	[DataMember]
	public bool? fldGroupBoolean { get; set; }

    public virtual ICollection<tblGroupToTrip> tblGroupToMoneys { get; } = new List<tblGroupToTrip>();

	public virtual ICollection<tblUserToGroup> tblUserToGroups { get; } = new List<tblUserToGroup>();
}
