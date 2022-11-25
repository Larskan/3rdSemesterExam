﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

public partial class TblLogin
{
    public int FldLoginId { get; set; }

    public int? FldUserId { get; set; }

    public string? FldPassword { get; set; }

	[JsonIgnore]
	public virtual TblUser? FldUser { get; set; }
}