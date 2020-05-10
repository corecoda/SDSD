using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDSD.Models
{
    public class Crew
    {
        [PrimaryKey, AutoIncrement]
        public int crewId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string role { get; set; }
        public string crewImage { get; set; }
        public string vesselType { get; set; }
    }
}
