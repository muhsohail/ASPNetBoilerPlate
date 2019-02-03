using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class StakeholderDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

    }
}
