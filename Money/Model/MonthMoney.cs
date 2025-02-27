using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Money.Model
{
    public class MonthMoney
    {
        private double? DepositPerMonth { get; set; }
        private double? IncreasePercentage { get; set; }
        private int TermInMonth { get; set; }
        private CompoundType CompoundType { get; set; }

        public List<double> MonthDeposits { get; set; }
        public MonthMoney(double depositPerMonth, int termInMonth, double increasePercentage = 0, 
            CompoundType compoundType = CompoundType.Yearly, bool verbose = false)
        {
            DepositPerMonth = depositPerMonth;
            TermInMonth = termInMonth;
            IncreasePercentage = increasePercentage;
            CompoundType = compoundType;
            MonthDeposits = GenerateMonths();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }
        public MonthMoney(List<double> monthsDeposits, bool verbose = false) 
        {
            MonthDeposits = monthsDeposits;
            TermInMonth = monthsDeposits.Count();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }

        public override string ToString()
        {
            string details = string.Join(Environment.NewLine, MonthDeposits) + Environment.NewLine + Environment.NewLine;
            return details; 
        }

        private List<double> GenerateMonths()
        {
            if (DepositPerMonth is null)
                throw new ArgumentNullException(nameof(DepositPerMonth));

            double depositValue = DepositPerMonth.Value;
            double increase = (IncreasePercentage ?? 0.0) / 100;
            List<double> months = new();

            for (int month = 1; month <= TermInMonth; month++)
            {
                months.Add(depositValue);

                if (CompoundType == CompoundType.Monthly)
                {
                    depositValue += depositValue * increase;
                }
                else if (CompoundType == CompoundType.Yearly && month % 12 == 0)
                {
                    depositValue += depositValue * increase;
                }
            }

            return months;
        }
    }
}
