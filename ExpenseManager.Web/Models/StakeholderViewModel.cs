using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseManager.Web.Models
{
    public class ItemViewModel
    {
        public int SelectedItemId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

    }
}
