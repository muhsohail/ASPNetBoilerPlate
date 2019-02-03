using System.Collections.Generic;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Charity.Dto;
using ExpenseManager.Helper;

namespace ExpenseManager.Web.Models
{
    public class CharityViewModel
    {
        public CharityViewModel()
        {
            charityDto = new List<CharityDto>();
            bankDetailsDto = new List<BankDetailsDto>();
        }

        public List<CharityDto> charityDto { get; set; }
        public List<BankDetailsDto> bankDetailsDto { get; set; }
        public CharitySummary charitySummary { get; set; }
    }
}