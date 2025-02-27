using Money.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Mortgage
    {
        private double mortgageValue { get; set; }
        private double mortgageRate { get; set; }
        private int years { get; set; }
        public MortgageModel MortgageModel { get; }

        public Mortgage(double mortgageValue, double mortgageRate, int years, bool verbose = false) 
        { 
            this.mortgageValue = mortgageValue;
            this.mortgageRate = mortgageRate;
            this.years = years;

            MortgageModel = GetMortgageModel();

            if (verbose) 
            { 
                Console.Write(ToString());
            }
        }

        public override string ToString()
        {
            string details = "";

            for (int i = 0; i < 110; i++) details += "*";
            details += Environment.NewLine;
            details += "*";
            details += $"{"MORTGAGE",54}";
            for (int i = 0; i < 54; i++) details += " ";
            details += "*";
            details += Environment.NewLine;
            for (int i = 0; i < 110; i++) details += "*";
            details += Environment.NewLine;

            details += $"*{"VALUE: ",9}{mortgageValue,12:C} {"RATE: " + mortgageRate,11}% {"TERM: " + years,12} "; // 49
            details += MortgageModel.ToString();

            details += Environment.NewLine;

            return details;
        }

        private double CalculateMonthlyPayment()
        {
            double monthlyRate = (mortgageRate / 12) / 100;
            int totalPayments = years * 12;

            if (monthlyRate == 0) // Edge case: 0% interest
                return (double)mortgageValue / totalPayments;

            double monthlyPayment = mortgageValue *
                (monthlyRate * Math.Pow(1 + monthlyRate, totalPayments)) /
                (Math.Pow(1 + monthlyRate, totalPayments) - 1);

            return monthlyPayment;
        }

        private MortgageModel GetMortgageModel()
        {
            double monthlyRate = (mortgageRate / 100) / 12;
            int totalPayments = years * 12;
            double monthlyPayment = CalculateMonthlyPayment();
            double remainingBalance = mortgageValue;

            MortgageModel mortgage = new MortgageModel(years, monthlyPayment);

            List<MortgageMonth> months = new List<MortgageMonth>();
            for (int month = 1; month <= totalPayments; month++)
            {
                double interestPayment = remainingBalance * monthlyRate;
                double principalPayment = monthlyPayment - interestPayment;
                remainingBalance -= principalPayment;

                months.Add(new MortgageMonth
                {
                    Month = month % 12 == 0 ? 12 : month % 12,
                    InterestPaid = interestPayment,
                    CashPaid = principalPayment
                });

                if (month % 12 == 0) // End of year tax application
                {
                    mortgage.Years.Add(new MortgageYear(months, month / 12, remainingBalance));

                    months = new(); // Reset months for next year
                }
            }
            return mortgage;
        }
    }
}
