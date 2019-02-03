using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class BankAccountDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public string BankName { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
