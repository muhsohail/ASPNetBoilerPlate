using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseManager.Onus.Dto;

namespace ExpenseManager.Web.Models
{
    public class OnusViewModel
    {
        public List<OnusDto> onusDtos { get; set; }
        public Dictionary<string, int> OnusSummary { get; set; }
    }
}