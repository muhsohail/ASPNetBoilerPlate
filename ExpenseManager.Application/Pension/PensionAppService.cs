using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.Model;
using ExpenseManager.Pension.Dto;
using ExpenseManager.Stakeholder;
using ExpenseManager.Stakeholder.Dto;

namespace ExpenseManager.Pension
{
    public class PensionAppService : AsyncCrudAppService<PensionDetail, PensionDto, int, PagedResultRequestDto, CreatePensionDto, UpdatePensionDto>, IPensionAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IStakeholderAppService _stakeholderAppService;

        public PensionAppService(
            IRepository<PensionDetail, int> repository, 
            IObjectMapper objectMapper,
            IStakeholderAppService stakeholderAppService) 
            : base(repository)
        {
            _objectMapper = objectMapper;
            _stakeholderAppService = stakeholderAppService;
        }

        public List<PensionDetailByStakeholder> GetPensionDetailsByStakeholder()
        {            
            List<PensionDetailByStakeholder> pensionDetails = new List<PensionDetailByStakeholder>();

            pensionDetails = _objectMapper.Map<List<PensionDto>>(Repository.GetAllList())
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.StakeholderId)
                .Select(x => new PensionDetailByStakeholder
                {
                    StakeholderId = x.Key.Value,                    
                    AmountUsed = x.Sum(y => y.Amount),
                    PercentageUsage = x.Sum(y => y.Amount) / (6 * 50000)
                }).ToList();

            foreach (PensionDetailByStakeholder pension in pensionDetails)
                pension.StakeholderName = GetStakeholderName(pension.StakeholderId);

            return pensionDetails;
        }

        private string GetStakeholderName(int StakeholderId)
        {
            return _objectMapper.Map<string>(_stakeholderAppService.GetStakeholderName(StakeholderId));
        }

        public UpdatePensionDto GetPensionUpdateDetails(int PensionId)
        {
            UpdatePensionDto pensionEntries = _objectMapper.Map<UpdatePensionDto>((Repository.Get(PensionId)));
             pensionEntries.StakeholderName = GetStakeholderName(pensionEntries.StakeholderId.Value);
            return pensionEntries;
        }

        public BaseResponse UpdatePensionDetails(UpdatePensionDto model)
        {
            Repository.Update(_objectMapper.Map<PensionDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }

        public double GetTotalPensionUsed()
        {
            List<PensionDto> pensionEntries = _objectMapper.Map<List<PensionDto>>(Repository.GetAllList().Where(x => !x.IsDeleted));
            return pensionEntries.Sum(x => x.Amount);
        }


        public List<PensionDto> GetAllPensionEntries()
        {
            List<PensionDto> pensionEntries = _objectMapper.Map<List<PensionDto>>(Repository.GetAllList().Where(x => !x.IsDeleted));

            foreach (PensionDto pension in pensionEntries)
                pension.StakeholderName = GetStakeholderName(pension.StakeholderId.Value);

            return pensionEntries;
        }

        public BaseResponse DeletePension(int PensionId)
        {

            var existing = Repository.Get(PensionId);
            existing.IsDeleted = true;
            Repository.Update(existing);

            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }
    }
}