using Money.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseIncomeCalculator
{
    public static class AppConfig
    {
        public static int RentIncomePerMonth { get; set; }
        public static double AnnualRentIncreasePercentage { get; set; }
        public static double YieldToYear { get; set; }
        public static CompoundType CompoundType { get; set; }
        public static int BlocValue { get; set; }
        public static int BlockOccupationPercentage { get; set; }
        public static int IncomeTaxPercentage { get; set; }
        public static double MortgagePercentage { get; set; }
        public static int MortgageYears { get; set; }
        public static bool ConsoleVerbose { get; set; }
    }
}
