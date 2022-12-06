using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblLogin
{
	[DataMember]
	public int fldLoginId { get; set; }

	[DataMember]
	public int? fldUserId { get; set; }

	[DataMember]
	public string? fldPassword { get; set; }

	public virtual tblUser? fldUser { get; set; }
}
