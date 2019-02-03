using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.PensionReceivable.Dto
{
    [AutoMapFrom(typeof(PensionReceivableDetail))]
    public class CreatePensionReceivableDto
    {
        public double Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
