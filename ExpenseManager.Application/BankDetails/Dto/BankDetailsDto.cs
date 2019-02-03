using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.BankDetails.Dto
{
    [AutoMapFrom(typeof(BankAccountDetail))]
    public class BankDetailsDto : EntityDto
    {
        public string BankName { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
