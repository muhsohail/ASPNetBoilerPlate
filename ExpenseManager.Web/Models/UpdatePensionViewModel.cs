using ExpenseManager.Pension.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Web.Models
{
    public class UpdatePensionViewModel
    {
        public UpdatePensionViewModel()
        {

            UpdatePensionDto = new UpdatePensionDto();
            StakeholderViewModel = new ItemViewModel();
        }
        public UpdatePensionDto UpdatePensionDto { get; set; }
        public ItemViewModel StakeholderViewModel { get; set; }

    }
}