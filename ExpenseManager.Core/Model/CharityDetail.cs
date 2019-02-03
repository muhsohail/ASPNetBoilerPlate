using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Model
{
    public class CharityDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }
        public string Purpose { get; set; }
        public DateTime? DateSpent { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public long? UserId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

    }
}
