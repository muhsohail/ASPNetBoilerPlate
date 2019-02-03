namespace ExpenseManager.Helper
{
    public class ByIndividualExpenseSummary
    {
        public double TotalSpent { get; set; }
        public double PerPerson { get; set; }
        public double ToBeReturn { get; set; }
        public double Returned { get; set; }
        public double Pending { get; set; }
    }
}
