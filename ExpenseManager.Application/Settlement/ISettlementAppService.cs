using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Settlement.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace ExpenseManager.Settlement
{
    public interface ISettlementAppService : IAsyncCrudAppService<SettlementDto, int, PagedResultRequestDto, CreateSettlementDto, UpdateSettlementDto>
    {
        SettlementDto CreateSettlement(CreateSettlementDto model);

        [HttpPost]
        BaseResponse DeleteSettlement(int SettlementId);

        [HttpGet]
        UpdateSettlementDto GetSettlementUpdateDetails(int SettlementId);

        [HttpGet]
        List<SettlementDto> GetAllSettlements();

        [HttpPost]
        BaseResponse UpdateSettlementDetails(UpdateSettlementDto model);

        [HttpPost]
        BaseResponse UndoSettlement(int SettlementId);
    }
}
