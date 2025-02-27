using Microsoft.Extensions.Configuration;
using Money;
using Money.Helper;
using Money.Model;

namespace HouseIncomeCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            if (!int.TryParse(config["RentIncomePerMonth"], out int rentIncome))
            {
                rentIncome = 0; // Set a default value or handle appropriately
            }

            AppConfig.RentIncomePerMonth = rentIncome;

            if (!double.TryParse(config["AnnualRentIncreasePercentage"], out double annualRentIncreasePercentage))
            {
                annualRentIncreasePercentage = 0; // Set a default value or handle appropriately
            }

            AppConfig.AnnualRentIncreasePercentage = annualRentIncreasePercentage;

            if (!double.TryParse(config["YieldToYear"], out double yieldToYear))
            {
                yieldToYear = 0; // Set a default value or handle appropriately
            }

            AppConfig.YieldToYear = yieldToYear;

            switch (config["CompoundType"]?.ToLower())
            {
                case "monthly":
                    AppConfig.CompoundType = CompoundType.Monthly;
                    break;
                case "yearly":
                    AppConfig.CompoundType = CompoundType.Yearly;
                    break;
                default:
                    AppConfig.CompoundType = CompoundType.Yearly;
                    break;
            }

            if (!int.TryParse(config["BlocValue"], out int blocValue))
            {
                blocValue = 0; // Set a default value or handle appropriately
            }

            AppConfig.BlocValue = blocValue;

            if (!int.TryParse(config["BlockOccupationPercentage"], out int blockOccupationPercentage))
            {
                blockOccupationPercentage = 0; // Set a default value or handle appropriately
            }

            AppConfig.BlockOccupationPercentage = blockOccupationPercentage;

            if (!int.TryParse(config["IncomeTaxPercentage"], out int incomeTaxPercentage))
            {
                incomeTaxPercentage = 0; // Set a default value or handle appropriately
            }

            AppConfig.IncomeTaxPercentage = incomeTaxPercentage;

            if (!double.TryParse(config["MortgagePercentage"], out double mortgagePercentage))
            {
                mortgagePercentage = 0; // Set a default value or handle appropriately
            }

            AppConfig.MortgagePercentage = mortgagePercentage;

            if (!int.TryParse(config["MortgageYears"], out int mortgageYears))
            {
                mortgageYears = 0; // Set a default value or handle appropriately
            }

            AppConfig.MortgageYears = mortgageYears;

            if (!bool.TryParse(config["ConsoleVerbose"], out bool consoleVerbose))
            {
                consoleVerbose = false; // Set a default value or handle appropriately
            }

            AppConfig.ConsoleVerbose = consoleVerbose;

            int year = 10;
            //double loanValue = AppConfig.BlocValue * 0.80;
            double loanValue = 200000;
            double mortagePercentage = 4.2;
            double investementRate = 3.5;
            double blockRevenu = 2000;
            double monthDeductible = 300;
            double inflation = 1.5;
            double impositionRate = 45;

            Mortgage mortgage = new Mortgage(loanValue, mortagePercentage, year, true);

            MonthMoney monthDeposited = new MonthMoney(blockRevenu, year * 12, inflation, CompoundType.Yearly);
            MonthMoney deductible = new MonthMoney(monthDeductible, year * 12, inflation, CompoundType.Yearly);

            List<double> mortageDeductible = ArrayHelper.Sum(deductible.MonthDeposits, 
                mortgage.MortgageModel.Years.SelectMany(m => m.Months.Select(m => m.InterestPaid)).ToList());

            List<double> mortagePaiementMonth = Enumerable.Repeat(mortgage.MortgageModel.MonthlyPayment, year * 12).ToList();
            List<double> monthPaiements = ArrayHelper.Substract(monthDeposited.MonthDeposits, mortagePaiementMonth);

            var investmentMortgage = new Investment(CompoundType.Yearly, monthPaiements, investementRate,
                year, mortageDeductible, loanValue, impositionRate, true);

            var investment = new Investment(CompoundType.Yearly, monthDeposited, investementRate,
                year, deductible, 0, impositionRate, true);

            //var investement = new Investment(CompoundType.Yearly, Enumerable.Repeat(100.0, 120).ToList(), 3, 10, Enumerable.Repeat(0.0, 120).ToList(), 0, 0, true);

            Console.ReadLine();
        }
    }
}
