using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Loan.Dto;
using System.Web.Http;

namespace ExpenseManager.Loan
{
    public interface ILoanAppService : IAsyncCrudAppService<LoanDto, int, PagedResultRequestDto, CreateLoanDto, UpdateLoanDto>
    {
        [HttpGet]
        UpdateLoanDto GetLoanUpdateDetails(int loanId);

        [HttpPost]
        BaseResponse UpdateLoanDetails(UpdateLoanDto model);

        [HttpPost]
        BaseResponse DeleteLoan(int LoanId);

    }
}
