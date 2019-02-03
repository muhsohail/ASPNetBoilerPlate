using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class OnusDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public string Task { get; set; }
        public string AssignedTo { get; set; }
        public int Progress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

        [ForeignKey("OnusStatus")]
        public int? OnusStatusId { get; set; }
        public OnusStatus OnusStatus { get; set; }
    }
}
