using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Money.Model
{
    public class InvestmentYear
    {
        public InvestmentYear(List<InvestmentMonth> months, int year, double taxRate)
        {
            Months = months;
            Year = year;
            TaxRate = taxRate;
        }
        public int Year { get; set; }
        public double TaxRate { get; set; }
        public List<InvestmentMonth> Months { get; set; }

        public double Interest
        {
            get
            {
                return Months.Sum(m => m.Interest);
            }
        }

        public double Deposited
        {
            get
            {
                return Months.Sum(m => m.Deposited);
            }
        }

        public double Deductible
        {
            get
            {
                return Months.Sum(m => m.Deductible);
            }
        }

        public double TaxableAmount
        {
            get
            {
                double amount = Interest * 0.5;
                return (Deductible > amount) ? 0 : amount - Deductible;
            }
        }

        public double TaxPaid
        {
            get
            {
                return TaxableAmount * (TaxRate / 100);
            }
        }

        public double Balance
        {
            get
            {
                return Months.OrderByDescending(m => m.Month).First().Balance - TaxPaid;
            }
        }

        public override string ToString()
        {
            string Details = "";
            //Details += Environment.NewLine;
            //Months.ForEach(x => Details += x);
            Details += $"Year: {Year,2} Yearly Deposited: {Deposited,12:C} Yearly Interest: {Interest,12:C} " +
                $"Deductible: {Deductible,12:C} Taxable Amout: {TaxableAmount,12:C} Tax Rate: {TaxRate,2}%  " +
                $"Tax Paid: {TaxPaid,10:C} Balance: {Balance,12:C}" + Environment.NewLine;

            return Details;
        }
    }
}
