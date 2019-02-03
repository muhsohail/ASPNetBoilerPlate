using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Settlement.Dto
{
    [AutoMapFrom(typeof(SettlementDetail))]
    public class SettlementDto : EntityDto
    {
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public bool IsDeleted { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

        public long ReturnedTo { get; set; }
        public string ReturnedToName { get; set; }

        public int SettlementTypeId { get; set; }
        public string SettlementTypeName { get; set; }
    }
}
