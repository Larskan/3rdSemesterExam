using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FairShareAPI.Models;

[DataContract(IsReference = true)]
[JsonObject(IsReference = false)]
public partial class tblUser
{
	[DataMember]
	public int fldUserId { get; set; }

	[DataMember]
	public string? fldEmail { get; set; }

	[DataMember]
	public string? fldFirstName { get; set; }

	[DataMember]
	public string? fldLastName { get; set; }

	[DataMember]
	public int? fldPhonenumber { get; set; }

	[DataMember]
	public bool? fldIsAdmin { get; set; }

	[DataMember]
	public string? fldPassword { get; set; }

	public virtual ICollection<tblReceipt> tblReceipts { get; } = new List<tblReceipt>();

	public virtual ICollection<tblUserExpense> tblUserExpenses { get; } = new List<tblUserExpense>();

	public virtual ICollection<tblUserToGroup> tblUserToGroups { get; } = new List<tblUserToGroup>();
}
