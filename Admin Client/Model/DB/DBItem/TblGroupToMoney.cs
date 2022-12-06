using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Admin_Client.Model.DB
{
    public partial class TblGroupToTrip
    {
        //public int FldGroupToTripId { get; set; }

        public int FldGroupId { get; set; }

        public int FldTripId { get; set; }

        [JsonIgnore]
        public virtual TblGroup FldGroup { get; set; }

        [JsonIgnore]
        public virtual TblTrip FldTrip { get; set; }
    }
}


