using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabProgram.Models
{

    public class MeterDataModel
    {
        public int Id { get; set; }
        public DateTime InstallDate { get; set; }
        public DateTime LastChargeDate { get; set; }
        public long MeterSerialNumber { get; set; }
        public long TotalConsum { get; set; }
        public double DebitsInMeter { get; set; }
        public double MaxLoad { get; set; }
        public double BalanceInMeter { get; set; }
        public string ClientName { get; set; }
    }
}
