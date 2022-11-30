using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Admin_Client.Model.DB
{
    public partial class TblTrip
    {
        public int FldTripId { get; set; }

        public double FldSum { get; set; }

        [JsonIgnore]
        public virtual ICollection<TblGroupToMoney> TblGroupToMoneys { get; } = new List<TblGroupToMoney>();

        [JsonIgnore]
        public virtual ICollection<TblReceipt> TblReceipts { get; } = new List<TblReceipt>();

        [JsonIgnore]
        public virtual ICollection<TblTripToUserExpense> TblTripToUserExpenses { get; } = new List<TblTripToUserExpense>();
    }
}


