using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDSD.Models
{
    public class Vessel
    {
        [PrimaryKey, AutoIncrement]
        public int vesselId { get; set; }
        public string vesselName { get; set; }
        public string vesselRegNumber { get; set; }
        public string vesselType { get; set; }
        public string vesselBackground { get; set; }
    }
    public class VesselType
    {
        public int vesselTypeId { get; set; }
        public string vesselType { get; set; }
    }

}
