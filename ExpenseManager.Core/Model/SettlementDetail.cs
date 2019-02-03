using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Model
{
    public class SettlementDetail : Entity, IHasCreationTime, IHasModificationTime
    {
        public override int Id { get; set; }

        public double Amount { get; set; }
        public DateTime ReturnDate { get; set; }

        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public long? UserId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }

        public long ReturnedTo { get; set; }


        [ForeignKey("SettlementCategory")]
        public int? SettlementTypeId { get; set; }
        public SettlementCategory SettlementCategory { get; set; }

    }
}
