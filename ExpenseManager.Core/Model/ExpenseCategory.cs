using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class ExpenseCategory : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        // TODO Define Type as Enum with values Common or Individual
        // Will help to remove hardcode conditions 

        public bool IsCommonCategory { get; set; }
        public CategoryType CategoryType { get; set; } 

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }

    public enum CategoryType
    {
        Single = 0,
        Double = 1,
        All = 2
    }
}
