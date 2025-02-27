using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Model
{
    public class InvestmentModel
    {
        public InvestmentModel(double startinAmount)
        {
            _startingAmount = startinAmount;
            Years = new List<InvestmentYear>();
        }

        private double _startingAmount;

        public override string ToString()
        {
            string Details = "";
            Years.ForEach(x => Details += x);
            Details += Environment.NewLine + $"Account Balamce: {AccountBalance, 12:C} " +
                $"Started Amount: {StartingAmount,12:C} Total Deposited: {TotalDeposited,12:C} " +
                $"Total Interest Earn: {TotalInterestEarn,12:C} Total Tax Paid: {TotalTaxPaid,12:C}";
            Details += Environment.NewLine + Environment.NewLine;
            return Details;
        }

        public double StartingAmount => _startingAmount;
        public double TotalInterestEarn
        {
            get
            {
                return Years.Sum(m => m.Interest);
            }
        }

        public double TotalDeposited
        {
            get
            {
                return Years.Sum(m => m.Deposited);
            }
        }

        public double TotalTaxPaid
        {
            get
            {
                return Years.Sum(m => m.TaxPaid);
            }
        }

        public double AccountBalance
        {
            get
            {
                return Years.OrderByDescending(m => m.Year).First().Balance;
            }
        }

        public List<InvestmentYear> Years { get; set; }
    }
}
