using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Helper
{
    public class PensionDetailByStakeholder
    {
        public int StakeholderId { get; set; }
        public string StakeholderName { get; set; }
        public double AmountUsed { get; set; }
        public double PercentageUsage { get; set; }
    }
}
