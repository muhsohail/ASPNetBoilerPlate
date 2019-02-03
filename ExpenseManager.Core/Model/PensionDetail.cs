using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace ExpenseManager.Model
{
    public class PensionDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public double Amount { get; set; }
        public string Reason { get; set; }
        public DateTime? DateSpent { get; set; }
        public double Total { get; set; }
        public double TotalSpent { get; set; }
        public double Remaining { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

        [ForeignKey("StakeholderDetail")]
        public int? StakeholderId { get; set; }
        public StakeholderDetail StakeholderDetail { get; set; }
    }
}
