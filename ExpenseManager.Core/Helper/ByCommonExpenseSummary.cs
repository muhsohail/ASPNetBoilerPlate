using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public class ByCommonExpenseSummary
    {
        public double TotalSpent { get; set; }
        public double PerPerson { get; set; }
        public double ToBeReturn { get; set; }
        public double Returned { get; set; }
        public double Pending { get; set; }
        public double IndividualPending1 { get; set; }
        public double IndividualPending2 { get; set; }
        public double TotalPending { get; set; }
    }
}
