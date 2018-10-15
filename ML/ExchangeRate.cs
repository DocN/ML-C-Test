using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;


namespace ML
{
    class ExchangeRate
    {
        [Column("1")]
        public double USDToEUR;
        [Column("2")]
        public double USDToJPY;
        [Column("3")]
        public double USDToCZK;
        [Column("4")]
        public double USDToGBP;
        [Column("5")]
        public double USDToAUD;
        [Column("6")]
        public double USDToBRL;
        [Column("7")]
        public double USDToCNY;
        [Column("8")]
        public double USDToZAR;
        [Column("9")]
        public double USDToCAD;
        [Column("10")]
        public double USDToMXN;
        [Column("11")]
        public double USDToARS;
        [Column("12")]
        public double USDToCHF;
        [Column("13")]
        public double USDToINR;
        [Column("14")]
        public double USDToVND;
        [Column("15")]
        public double USDToZMW;
        [Column("16")]
        public double USDToIDR;
        [Column("17")]
        public double USDToIQD;
        [Column("18")]
        public double USDToIRR;
        [Column("19")]
        public int DateYear;
        [Column("20")]
        public int DateMonth;
        [Column("21")]
        public int DayofMonth;
        [Column("22")]
        public int DayOfWeek;
    }
}
