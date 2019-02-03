using ExpenseManager.Onus.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.Web.Models
{
    public class UpdateOnusViewModel
    {
        public UpdateOnusViewModel()
        {

            UpdateOnusDto = new UpdateOnusDto();
            ItemViewModel = new ItemViewModel();
        }

        public UpdateOnusDto UpdateOnusDto { get; set; }
        public ItemViewModel ItemViewModel { get; set; }
    }
}