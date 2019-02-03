using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Model
{
    public class LoanDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public string PersonName { get; set; }
        public double LoanAmount { get; set; }
        public double AmountReturned { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int ReturnedByUserId { get; set; }
        public string ReturnedByUserName { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

    }
}
