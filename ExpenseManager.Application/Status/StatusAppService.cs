using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Model;
using ExpenseManager.Status.Dto;

namespace ExpenseManager.Status
{
    public class StatusAppService : AsyncCrudAppService<OnusStatus, StatusDto, int, PagedResultRequestDto, CreateStatusDto, UpdateStatusDto>, IStatusAppService
    {
        private IObjectMapper _objectMapper;
        public StatusAppService(IObjectMapper objectMapper, IRepository<OnusStatus, int> repository) : base(repository)
        {
            _objectMapper = objectMapper;

        }

        public string GetOnusStatusName(int OnusStatusId)
        {
            return _objectMapper.Map<string>(Repository.Get(OnusStatusId).Name);
        }
    }
}
