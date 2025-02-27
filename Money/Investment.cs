using Money.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    public class Investment
    {
        private CompoundType CompoundType { get; set; }
        private double StartingAmount { get; set; }
        private List<double> MonthlyDeposits { get; set; }
        private double ImpositionPercentage { get; set; }
        private int InvestmentTerm { get; set; }
        private double InterestPercentage { get; set; }
        private InvestmentModel InvestmentModel { get; set; }
        private List<double> Deductible { get; set; }

        public Investment(CompoundType compoundType, MonthMoney monthDeposit, double interestPercentage,
            int investmentTerm, MonthMoney? deductible = null, double startingAmount = 0, double impositionPercentage = 0,
            bool verbose = false)
        {
            CompoundType = compoundType;
            StartingAmount = startingAmount;
            MonthlyDeposits = monthDeposit.MonthDeposits;
            InterestPercentage = interestPercentage;
            ImpositionPercentage = impositionPercentage;
            InvestmentTerm = investmentTerm;

            Deductible = (deductible ??
                new MonthMoney(Enumerable.Repeat(0.0, investmentTerm * 12).ToList())).MonthDeposits;

            InvestmentModel = CalculateCompoundInterestWithTax();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }

        public Investment(CompoundType compoundType, List<double> monthDeposit, double interestPercentage,
            int investmentTerm, MonthMoney? deductible = null, double startingAmount = 0, double impositionPercentage = 0,
            bool verbose = false)
        {
            CompoundType = compoundType;
            StartingAmount = startingAmount;
            MonthlyDeposits = monthDeposit;
            InterestPercentage = interestPercentage;
            ImpositionPercentage = impositionPercentage;
            InvestmentTerm = investmentTerm;

            Deductible = (deductible ??
                new MonthMoney(Enumerable.Repeat(0.0, investmentTerm * 12).ToList())).MonthDeposits;

            InvestmentModel = CalculateCompoundInterestWithTax();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }

        public Investment(CompoundType compoundType, MonthMoney monthDeposit, double interestPercentage,
            int investmentTerm, List<double>? deductible = null, double startingAmount = 0, double impositionPercentage = 0,
            bool verbose = false)
        {
            CompoundType = compoundType;
            StartingAmount = startingAmount;
            MonthlyDeposits = monthDeposit.MonthDeposits;
            InterestPercentage = interestPercentage;
            ImpositionPercentage = impositionPercentage;
            InvestmentTerm = investmentTerm;

            Deductible = (deductible ??
                Enumerable.Repeat(0.0, investmentTerm * 12).ToList());

            InvestmentModel = CalculateCompoundInterestWithTax();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }

        public Investment(CompoundType compoundType, List<double> monthDeposit, double interestPercentage,
            int investmentTerm, List<double>? deductible = null, double startingAmount = 0, double impositionPercentage = 0,
            bool verbose = false)
        {
            CompoundType = compoundType;
            StartingAmount = startingAmount;
            MonthlyDeposits = monthDeposit;
            InterestPercentage = interestPercentage;
            ImpositionPercentage = impositionPercentage;
            InvestmentTerm = investmentTerm;

            Deductible = (deductible ??
                Enumerable.Repeat(0.0, investmentTerm * 12).ToList());

            InvestmentModel = CalculateCompoundInterestWithTax();

            if (verbose)
            {
                Console.Write(ToString());
            }
        }

        public override string ToString()
        {
            string Details = $"Compound Type: {CompoundType,6} Starting Amount: {StartingAmount,12:C} " +
                $"Interest Percentage: {InterestPercentage,2}% Impot Percentage: {ImpositionPercentage,2}% " +
                $"Term: {InvestmentTerm,2}" + Environment.NewLine + Environment.NewLine;
            Details += InvestmentModel.ToString();
            return Details;
        }

        private InvestmentModel CalculateCompoundInterestWithTax()
        {
            InvestmentModel investmentModel = new InvestmentModel(StartingAmount);
            double monthlyRate = (InterestPercentage / 100) / 12;
            int totalMonths = InvestmentTerm * 12;
            double accountValue = StartingAmount;

            List<InvestmentMonth> investmentMonths = new();

            for (int month = 1; month <= totalMonths; month++)
            {
                double deposit = MonthlyDeposits[month - 1];
                accountValue += deposit;
                double interestEarned = Math.Round(accountValue * monthlyRate, 2);

                if (CompoundType == CompoundType.Monthly)
                    accountValue += interestEarned;

                investmentMonths.Add(new InvestmentMonth
                {
                    Month = month % 12 == 0 ? 12 : month % 12,
                    Deposited = deposit,
                    Interest = interestEarned,
                    Deductible = Deductible[month - 1],
                    Balance = accountValue
                });

                if (month % 12 == 0) // End of year tax application
                {
                    if (CompoundType == CompoundType.Yearly)
                        investmentMonths.OrderByDescending(m => m.Month).First().Balance =
                            accountValue + investmentMonths.Sum(x => x.Interest);

                    var investmentYear = new InvestmentYear(investmentMonths, month / 12, ImpositionPercentage);
                    investmentModel.Years.Add(investmentYear);

                    accountValue = investmentYear.Balance;

                    investmentMonths = new(); // Reset months for next year
                }
            }
            return investmentModel;
        }
    }
}
