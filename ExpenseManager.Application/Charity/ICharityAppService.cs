using System.Collections.Generic;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.Charity.Dto;
using ExpenseManager.Helper;

namespace ExpenseManager.Charity
{
    public interface ICharityAppService : IAsyncCrudAppService<CharityDto, int, PagedResultRequestDto, CreateCharityDto, UpdateCharityDto>
    {
        CharityDto CreateCharity(CreateCharityDto model);

        BaseResponse DeleteCharity(int CharityId);

        [HttpGet]
        UpdateCharityDto GetCharityUpdateDetails(int CharityId);

        BaseResponse UpdateCharityDetails(UpdateCharityDto model);

        [HttpGet]
        List<CharityDto> GetAllCharity();

        [HttpPost]
        BaseResponse UndoCharity(int CharityId);
    }
}
