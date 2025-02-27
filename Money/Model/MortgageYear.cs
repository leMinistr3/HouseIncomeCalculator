using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Model
{
    public class MortgageYear
    {
        public MortgageYear(List<MortgageMonth> months, int year, double balance)
        {
            Months = months;
            Year = year;
            Balance = balance;
        }

        private double _balance;

        public int Year { get; set; }

        public double InterestPaid
        {
            get
            {
                return Months.Sum(x => x.InterestPaid);
            }
        }

        public double CashPaid
        {
            get
            {
                return Months.Sum(x => x.CashPaid);
            }
        }

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public List<MortgageMonth> Months { get; set; }

        public override string ToString()
        {
            string details = $"*  Years: {Year,2} Interest : {InterestPaid,12:C} Cash Paid: {CashPaid,12:C} " +
                $"Total Paid: {(InterestPaid + CashPaid),12:C} Balance : {Balance,12:C} *" + Environment.NewLine;
            return details;
        }
    }
}
