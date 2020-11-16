using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LabProgram.Helper
{

    public class ReadMeterCardWrapper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct METER_READ_DATA
        {

            public uint TotalConsum;
            public uint RemainPound;
            public uint RemainPiasters;
            public uint CurrConsum;
            public uint CurrentMonthlyConsum;
            public uint CurrentsliceConsum;
            public uint TotalBalance;
            public uint OpenTrials;
            public uint Battery;
            public uint EarthFrauds;
            public uint Cardreads;
            public uint MaxLoadKW;
            public uint MaxLoadAM;
            public uint DateR;
            public uint Time;
            public uint Operator;
            public uint overload;
            public uint RemainKW;
            public uint RemainCurrSlice;
            public uint SliceNo;
            public uint consm1;
            public uint consm2;
            public uint consm3;
            public uint consm4;
            public uint consm5;
            public uint consm6;
            public uint consm7;
            public uint consm8;
            public uint consm9;
            public uint consm10;
            public uint consm11;
            public uint consm12;
            public uint MeterStat;
            public uint chargeCnt;
            public uint RemainInDays;
            public uint RHours;
            public uint ReadDate;
            public uint EarthFraudDuration;
            public uint AvergDailyConsum;
            public uint ReadID;
            public uint PrevMonthConsum;
            public uint OpenCoverDate;
            public uint PowerFactor;
            public uint DebitsInMeterPnd;
            public uint DebitsInMeterPstr;
            public uint SumOfAllChargesPND;
            public uint SumOfAllChargesPSTR;
            public uint OpenKW;
            public uint chargeID;
            public uint LastChargeCodeSentBySystem;
            public uint CrdLastBalance;
            public uint LastChargeDate;
            public uint MeterSerialNumber;
            public uint DebitConsum;
            public uint TotalConsumVAR;
            public uint CurrMonthConsumVAR;
            public uint PreviousMonthConsumVAR;
            public uint PH1_Fail_Count;
            public uint PH1_Fail_Duration;
            public uint PH2_Fail_Count;
            public uint PH2_Fail_Duration;
            public uint PH3_Fail_Count;
            public uint PH3_Fail_Duration;
            public uint All_Fail_Count;
            public uint CoverEventDate;
            public uint DTLastChargeDate;
            public uint DTReadDate;
            public uint OpenKiloWatt;
            public uint readcount;
            public uint chargeCount;
            public uint ChargeID;
            public uint ConsumInCurrSlic;
            public uint coveropen;
            public uint CoverOpenMinute;
            public uint CoverOpenHours;
            public uint CoverOpenDays;
            public uint CoverOpenMonthes;
            public uint CoverOpenYears;
            public uint RelayOpen;
            public uint EarthOpenMinute;
            public uint EarthOpenHours;
            public uint EarthOpenDays;
            public uint EarthOpenMonthes;
            public uint EarthOpenYears;
            public uint EarthOpen;

            public uint ReverseOpenMinute;
            public uint ReverseOpenHours;
            public uint ReverseOpenDays;
            public uint ReverseOpenMonthes;
            public uint ReverseOpenYears;
            public uint ReverseOpen;

            public uint Ph1OpenMinute;
            public uint Ph1OpenHours;
            public uint Ph1OpenDays;
            public uint Ph1OpenMonthes;
            public uint Ph1OpenYears;
            public uint Ph1Open;

            public uint Ph2OpenMinute;
            public uint Ph2OpenHours;
            public uint Ph2OpenDays;
            public uint Ph2OpenMonthes;
            public uint Ph2OpenYears;
            public uint Ph2Open;

            public uint Ph3OpenMinute;
            public uint Ph3OpenHours;
            public uint Ph3OpenDays;
            public uint Ph3OpenMonthes;
            public uint Ph3OpenYears;
            public uint Ph3Open;

            public uint RelayOpenMinute;
            public uint RelayOpenHours;
            public uint RelayOpenDays;
            public uint RelayOpenMonthes;
            public uint RelayOpenYears;

            public uint ROverLoad;
            public uint MeterState;

            public uint ControlCardID1;
            public uint ControlCardID2;
            public uint ControlCardID3;
            public uint Rdays;
            public uint Rmonthes;
            public uint Ryears;
            public uint DebitsInMeterPstrPrevious;
            public uint DebitsInMeterPndPrevious;

            public uint PowerFactor_1;
            public uint PowerFactor_2;
            public uint PowerFactor_3;
            public uint PowerFactor_4;
            public uint MaximumDemand_1;
            public uint MaximumDemand_2;
            public uint MaximumDemand_3;
            public uint MaximumDemand_4;
            public uint ConsumptionKVARPos;
            public uint ConsumptionKVARNeg;
            public uint SettledDepitsPiasters;

            public uint ClientID;
            public uint FirstHalfID;
            public uint LastHalfID;

            public uint CT;
            public uint DoorOpenMinute;
            public uint DoorOpenHours;
            public uint DoorOpenDays;
            public uint DoorOpenMonthes;
            public uint DoorOpenYears;
            public uint DoorOpen;
            public uint consm_kvar1;
            public uint consm_kvar2;
            public uint consm_kvar3;
            public uint consm_kvar4;
            public uint consm_kvar5;
            public uint consm_kvar6;
            public uint consm_kvar7;
            public uint consm_kvar8;
            public uint consm_kvar9;
            public uint consm_kvar10;
            public uint consm_kvar11;
            public uint consm_kvar12;
            public uint PowerFactor_month1;
            public uint PowerFactor_month2;
            public uint PowerFactor_month3;
            public uint PowerFactor_month4;
            public uint PowerFactor_month5;
            public uint PowerFactor_month6;
            public uint PowerFactor_month7;
            public uint PowerFactor_month8;
            public uint PowerFactor_month9;
            public uint PowerFactor_month10;
            public uint PowerFactor_month11;
            public uint PowerFactor_month12;
            public uint Challenge1;
            public uint Challenge2;
            public uint Moneyconsm01;
            public uint Moneyconsm02;
            public uint Moneyconsm03;
            public uint Moneyconsm04;
            public uint Moneyconsm05;
            public uint Moneyconsm06;
            public uint Moneyconsm07;
            public uint Moneyconsm08;
            public uint Moneyconsm09;
            public uint Moneyconsm10;
            public uint Moneyconsm11;
            public uint Moneyconsm12;
            public uint CurrentMonthMoneyConsum;
            public byte ControlCardStateBefore1;
            public byte ControlCardStateAfter1;
            public byte ControlCardStateBefore2;
            public byte ControlCardStateAfter2;
            public byte ControlCardStateBefore3;
            public byte ControlCardStateAfter3;
            public uint InstallCardID;
            public byte MeterInstallMinute;
            public byte MeterInstallHours;
            public byte MeterInstallDays;
            public byte MeterInstallMonthes;
            public UInt16 MeterInstallYears;
            public uint ConsumptionInReverse;
            public uint ConsumptionInEarth;

        };

        [DllImport("ReadMeterCard.dll")]
        internal static extern int ReadCollectionCardRecord(byte[] RetBuffer, ref METER_READ_DATA FeedBack);
    }
}
