using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Helper
{
    public static class ArrayHelper
    {
        public static List<double> Substract(List<double> original, List<double> remove)
        {
            if (original.Count() != remove.Count())
                throw new ArgumentOutOfRangeException(nameof(remove));

            return original.Zip(remove, (o, r) => o - r).ToList();
        }

        public static List<double> Sum(List<double> original, List<double> add)
        {
            if (original.Count() != add.Count())
                throw new ArgumentOutOfRangeException(nameof(add));

            return original.Zip(add, (o, r) => o + r).ToList();
        }
    }
}
