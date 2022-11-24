using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

public partial class TblUser
{
    public int FldUserId { get; set; }

    public string? FldEmail { get; set; }

    public string? FldFirstName { get; set; }

    public string? FldLastName { get; set; }

    public int? FldPhonenumber { get; set; }

	[JsonIgnore]
	public virtual ICollection<TblLogin> TblLogins { get; } = new List<TblLogin>();

	[JsonIgnore]
	public virtual ICollection<TblReceipt> TblReceipts { get; } = new List<TblReceipt>();

	[JsonIgnore]
	public virtual ICollection<TblUserExpense> TblUserExpenses { get; } = new List<TblUserExpense>();

	[JsonIgnore]
	public virtual ICollection<TblUserToGroup> TblUserToGroups { get; } = new List<TblUserToGroup>();
}
