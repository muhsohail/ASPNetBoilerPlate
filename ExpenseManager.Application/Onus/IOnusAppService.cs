using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Onus.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace ExpenseManager.Onus
{
    public interface IOnusAppService : IAsyncCrudAppService<OnusDto, int, PagedResultRequestDto, CreateOnusDto, UpdateOnusDto>
    {
        [HttpGet]
        List<OnusDto> GetAllOnus();

        [HttpGet]
        UpdateOnusDto GetOnusUpdateDetails(int OnusId);

        [HttpPost]
        BaseResponse UpdateOnusDetails(UpdateOnusDto model);

        [HttpPost]
        BaseResponse DeleteOnus(int OnusId);

        [HttpPost]
        BaseResponse UndoOnus(int OnusId);
    }
}
