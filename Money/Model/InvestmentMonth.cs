using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Money.Model
{
    public class InvestmentMonth
    {
        public int Month { get; set; }

        private double _deposited;
        private double _interest;
        private double _balance;
        private double _deductible;

        public double Deposited
        {
            get { return Math.Round(_deposited, 2); }
            set { _deposited = value; }
        }
        public double Interest
        {
            get { return Math.Round(_interest, 2); }
            set { _interest = value; }
        }

        public double Balance
        {
            get { return Math.Round(_balance, 2); }
            set { _balance = value; }
        }

        public double Deductible
        {
            get { return Math.Round(_deductible, 2); }
            set { _deductible = value; }
        }

        public override string ToString()
        {
            string Details = $"Month: {Month.ToString("00")} Deposited: {Deposited:C} " +
                        $"Interest: {Interest:C} Deductible: {Deductible:C} Balance: {Balance:C}" + 
                        Environment.NewLine;
            return Details;
        }
    }
}
