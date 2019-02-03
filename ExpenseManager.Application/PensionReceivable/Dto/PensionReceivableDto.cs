using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.PensionReceivable.Dto
{
    [AutoMapFrom(typeof(PensionReceivableDetail))]
    public class PensionReceivableDto : EntityDto
    {
        public double Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
