using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseManager.Helper;
using ExpenseManager.Onus.Dto;

namespace ExpenseManager.Web.Models
{
    public class DashboardViewModel
    {
        public Dictionary<string, double> GetDashboardDataByExpenseCateogy  { get; set;}
        public List<ExpenseDetailByMonth> GetExpenseDetailByMonth { get; set; }
        public Dictionary<string, List<ByIndividualSpentDetail>> GetAmountSpentByIndividual { get; set; }
        public List<OnusDto> Tasks { get; set; }
        public PensionUsageDetail PensionUsageDetail { get; set; }
        public Dictionary<string, ByCommonExpenseSummary> ByCommonExpenseSummary { get; set; }
        public Dictionary<string, ByIndividualExpenseSummary> ByIndividualExpenseSummary { get; set; }
    }
}