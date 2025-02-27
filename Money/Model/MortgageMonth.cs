using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Model
{
    public class MortgageMonth
    {
        private double _cashPaid;
        private double _interestPaid;
        public int Month { get; set; }
        public double CashPaid
        {
            get { return _cashPaid; }
            set { _cashPaid = value; }
        }

        public double InterestPaid
        {
            get { return _interestPaid; }
            set { _interestPaid = value; }
        }
    }
}
