using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Helper;
using ExpenseManager.PensionReceivable.Dto;
using System.Web.Http;

namespace ExpenseManager.PensionReceivable
{
    public interface IPensionReceivableAppService : IAsyncCrudAppService<PensionReceivableDto, int, PagedResultRequestDto, CreatePensionReceivableDto, UpdatePensionReceivableDto>
    {
        [HttpGet]
        UpdatePensionReceivableDto GetPensionReceivableUpdateDetails(int pensionReceivableId);

        [HttpPost]
        BaseResponse UpdatePensionReceivableDetails(UpdatePensionReceivableDto model);

        [HttpPost]
        BaseResponse DeletePensionReceivable(int PensionReceivableId);

        [HttpGet]
        double GetTotalPensionReceived();
    }
}
