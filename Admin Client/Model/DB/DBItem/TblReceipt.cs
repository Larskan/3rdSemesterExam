using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Admin_Client.Model.DB
{
    public partial class TblReceipt
    {
        public int FldReceiptId { get; set; }

        public int FldUserId { get; set; }

        public int FldTripId { get; set; }

        public double FldProjectedValue { get; set; }

        public double FldAmountPaid { get; set; }

        [JsonIgnore]
        public virtual TblTrip FldTrip { get; set; }

        [JsonIgnore]
        public virtual TblUser FldUser { get; set; }


    }
}


