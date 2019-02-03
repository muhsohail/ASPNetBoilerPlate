using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Loan.Dto
{
    [AutoMapFrom(typeof(LoanDetail))]
    public class LoanDto : EntityDto
    {
        public string PersonName { get; set; }
        public double LoanAmount { get; set; }
        public double AmountReturned { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int ReturnedByUserId { get; set; }
        public string ReturnedByUserName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
