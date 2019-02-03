using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Model
{
    public class PensionReceivableDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public double Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
