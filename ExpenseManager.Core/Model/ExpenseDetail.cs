using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class ExpenseDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }
        public string Purpose { get; set; }
        public DateTime? DateSpent { get; set; }
        public double Amount { get; set; }
        public int ExpenseType { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public long? UserId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

        [ForeignKey("ExpenseCategory")]
        public int? ExpenseCatregoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
