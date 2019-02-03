using ExpenseManager.ExpenseSheet.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Web.Models
{
    public class UpdateExpenseViewModel
    {
        public UpdateExpenseViewModel()
        {

            UpdateExpenseSheetDto = new UpdateExpenseSheetDto();
            ItemViewModel = new ItemViewModel();
        }

        public UpdateExpenseSheetDto UpdateExpenseSheetDto { get; set; }
        public ItemViewModel ItemViewModel { get; set; }
    }
}