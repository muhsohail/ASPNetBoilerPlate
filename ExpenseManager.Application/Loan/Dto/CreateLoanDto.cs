using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Loan.Dto
{
    [AutoMapFrom(typeof(LoanDetail))]
    public class CreateLoanDto
    {
        public string PersonName { get; set; }
        public double LoanAmount { get; set; }
        public double AmountReturned { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int ReturnedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        //public string ReturnedByUserName { get; set; }
    }
}
