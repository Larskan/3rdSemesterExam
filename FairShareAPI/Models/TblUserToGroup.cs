using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

public partial class TblUserToGroup
{
    public int FldUserToGroupId { get; set; }

    public int? FldUserId { get; set; }

    public int? FldGroupId { get; set; }

	[JsonIgnore]
	public virtual TblGroup? FldGroup { get; set; }

	[JsonIgnore]
	public virtual TblUser? FldUser { get; set; }
}
