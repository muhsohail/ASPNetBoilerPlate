using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class SettlementCategory : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
