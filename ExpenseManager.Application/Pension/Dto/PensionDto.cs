using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Pension.Dto
{
    [AutoMapFrom(typeof(PensionDetail))]
    public class PensionDto : EntityDto
    {
        public double Amount { get; set; }
        public string Reason { get; set; }
        public DateTime? DateSpent { get; set; }

        [ScaffoldColumn(false)]
        public double Total { get; set; }

        [ScaffoldColumn(false)]
        public double TotalSpent { get; set; }

        [ScaffoldColumn(false)]
        public double Remaining { get; set; }

        public int? StakeholderId { get; set; }
        public string StakeholderName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
