using System.Collections.Generic;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Helper;

namespace ExpenseManager.BankDetails
{
    public interface IBankDetailsAppService : IAsyncCrudAppService<BankDetailsDto, int, PagedResultRequestDto, CreateBankDetailsDto, UpdateBankDetailsDto>
    {
        BankDetailsDto CreateBankDetails(CreateBankDetailsDto model);

        BaseResponse DeleteBankDetails(int BankDetailsId);

        [HttpGet]
        UpdateBankDetailsDto GetBankUpdateDetails(int BankDetailsId);

        BaseResponse UpdateBankAccountDetails(UpdateBankDetailsDto model);

        [HttpGet]
        List<BankDetailsDto> GetAllBankDetails();

        [HttpPost]
        BaseResponse UndoBankDetails(int BankDetailsId);
    }
}
