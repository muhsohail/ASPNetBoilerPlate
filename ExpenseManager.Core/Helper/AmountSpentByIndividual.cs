using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public class AmountSpentByIndividual
    {
        public Dictionary<string, List<ByIndividualSpentDetail>> SpentByIndividual { get; set; }
    }

    public class ByIndividualSpentDetail
    {
        public string When { get; set; }
        public double Amount { get; set; }
    }
}
