using ExpenseManager.ExpenseSheet.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Web.Models
{
    public class ExpenseSheetViewModel
    {
        //public ExpenseSheetViewModel()
        //{

        //    createExpenseSheetDto = new CreateExpenseSheetDto();
        //    ItemViewModel = new ItemViewModel();
        //}

        //public CreateExpenseSheetDto createExpenseSheetDto { get; set; }
        //public ItemViewModel ItemViewModel { get; set; }


        public string Purpose { get; set; }
        public DateTime? DateSpent { get; set; }
        public double Amount { get; set; }
        public int ExpenseCatregoryId { get; set; }
        public long? UserId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
    }
}