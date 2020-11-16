using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabProgram.Helper
{

    public class OpticalViewModel
    {
        public string ClientID { get; set; }
        public string MeterSerialNumber { get; set; }
        public string InstallDate { get; set; }
        public string TotalConsum { get; set; }
        public decimal CurrentMonthKiloWat { get; set; }
        public long Tarif { get; set; }
        public decimal RemainKilowWat { get; set; }
        public decimal BalanceInMeter { get; set; }
        public decimal TotalCredit { get; set; }
        public string LastChargeDate { get; set; }
        public long RemainDays { get; set; }
        public decimal DebitsInMeter { get; set; }
        public decimal DyonKilo { get; set; }
        public Int64 ROverLoad { get; set; }
        public string EarthOpen { get; set; }
        public Int64 DoorOpen { get; set; }
        public Int64 coveropen { get; set; }
        public string ReadDate { get; set; }
        public string RegisterDate { get; set; }
        public string overload { get; set; }
    }
}
