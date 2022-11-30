using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Admin_Client.Model.DB
{
    public partial class TblTripToUserExpense
    {
        public int FldUserToExpense { get; set; }

        public int FldTripId { get; set; }

        public int FldExpensesId { get; set; }

        [JsonIgnore]
        public virtual TblUserExpense FldExpenses { get; set; }

        [JsonIgnore]
        public virtual TblTrip FldTrip { get; set; }
    }
}


