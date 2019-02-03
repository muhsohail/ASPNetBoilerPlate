using System.Collections.Generic;
using ExpenseManager.Helper;
using ExpenseManager.Pension.Dto;

namespace ExpenseManager.Web.Models
{
    public class PensionViewModel
    {
        public List<PensionDto> pensionDtos { get; set; }
        public Summary summary { get; set; }
    }
}