using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Model
{
    public class MortgageModel
    {
        public MortgageModel(int years, double monthlyPaiement) 
        {
            Years = new List<MortgageYear>();
            _monthlyPayment = monthlyPaiement;
        }

        private double _monthlyPayment;

        public double MonthlyPayment
        {
            get { return _monthlyPayment; }
        }

        public double TotalInterestPaid
        {
            get
            {
                return Years.Sum(x => x.InterestPaid);
            }
        }

        public List<MortgageYear> Years { get; set; }

        public override string ToString()
        {
            string details = $"{"Montlhy Paiement: ",18}{MonthlyPayment,-12:C}{"Total Interest: ",18}{TotalInterestPaid,-12:C}*"; // 61

            details += Environment.NewLine;
            details += "*";
            for (int i = 0; i < 108; i++) details += " ";
            details += "*";

            details += Environment.NewLine;
            details += "*";
            for (int i = 0; i < 108; i++) details += " ";
            details += "*";
            details += Environment.NewLine;

            Years.ForEach(m => details += m.ToString());

            for (int i = 0; i < 110; i++) details += "*";

            details += Environment.NewLine;
            return details;
        }
    }
}
