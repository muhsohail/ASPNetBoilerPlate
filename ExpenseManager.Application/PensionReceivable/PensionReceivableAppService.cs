using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.Model;
using ExpenseManager.PensionReceivable.Dto;
using ExpenseManager.Stakeholder;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseManager.PensionReceivable
{
    public class PensionReceivableAppService : AsyncCrudAppService<PensionReceivableDetail, PensionReceivableDto, int, PagedResultRequestDto, CreatePensionReceivableDto, UpdatePensionReceivableDto>, IPensionReceivableAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IStakeholderAppService _stakeholderAppService;

        public PensionReceivableAppService(
            IRepository<PensionReceivableDetail, int> repository,
            IObjectMapper objectMapper,
            IStakeholderAppService stakeholderAppService)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _stakeholderAppService = stakeholderAppService;
        }


        public UpdatePensionReceivableDto GetPensionReceivableUpdateDetails(int pensionReceivableId)
        {
            return _objectMapper.Map<UpdatePensionReceivableDto>((Repository.Get(pensionReceivableId)));
        }

        public BaseResponse UpdatePensionReceivableDetails(UpdatePensionReceivableDto model)
        {
            Repository.Update(_objectMapper.Map<PensionReceivableDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }

        public BaseResponse DeletePensionReceivable(int PensionReceivableId)
        {

            var existing = Repository.Get(PensionReceivableId);
            existing.IsDeleted = true;
            Repository.Update(existing);

            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public double GetTotalPensionReceived()
        {
            List<PensionReceivableDto> pensionReceivableEntries = _objectMapper.Map<List<PensionReceivableDto>>(Repository.GetAllList().Where(x => !x.IsDeleted));

            return pensionReceivableEntries.Sum(x => x.Amount);
        }
    }
}