using ExpenseManager.Helper;
using ExpenseManager.Loan.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Web.Models
{
    public class LoanViewModel
    {
        public LoanViewModel()
        {
            LoanDto = new List<LoanDto>();

        }
        public List<LoanDto> LoanDto { get; set; }
        public LoanSummary LoanSummary { get; set; }
    }
}