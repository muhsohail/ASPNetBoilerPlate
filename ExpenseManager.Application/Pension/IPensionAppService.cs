using System.Collections.Generic;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Pension.Dto;

namespace ExpenseManager.Pension
{
    public interface IPensionAppService : IAsyncCrudAppService<PensionDto, int, PagedResultRequestDto, CreatePensionDto, UpdatePensionDto>
    {
        [HttpGet]
        List<PensionDetailByStakeholder> GetPensionDetailsByStakeholder();

        [HttpGet]
        UpdatePensionDto GetPensionUpdateDetails(int PensionId);

        [HttpPost]
        BaseResponse UpdatePensionDetails(UpdatePensionDto model);

        [HttpGet]
        double GetTotalPensionUsed();

        [HttpGet]
        List<PensionDto> GetAllPensionEntries();

        [HttpPost]
        BaseResponse DeletePension(int PensionId);
    }
}
