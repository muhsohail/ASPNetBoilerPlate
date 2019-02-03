using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.Onus.Dto
{
    [AutoMapFrom(typeof(OnusDetail))]
    public class OnusDto : EntityDto
    {
        public string Task { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public int Progress { get; set; }
        public bool IsDeleted { get; set; }
        public int OnusStatusId { get; set; }
        public string OnusStatusName { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
