using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ExpenseManager.Model;

namespace ExpenseManager.PersonnelExpense.Dto
{
    [AutoMapFrom(typeof(ExpenseDetail))]
    public class CreatePersonnelExpenseDto
    {
        public string Purpose { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateSpent { get; set; }
        public double Amount { get; set; }
        public int ExpenseCatregoryId { get; set; }
        public long? UserId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}
