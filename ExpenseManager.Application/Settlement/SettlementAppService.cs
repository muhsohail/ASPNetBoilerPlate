using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Authorization.Users;
using ExpenseManager.Helper;
using ExpenseManager.Model;
using ExpenseManager.Settlement.Dto;

namespace ExpenseManager.Settlement
{
    public class SettlementAppService : AsyncCrudAppService<SettlementDetail, SettlementDto, int, PagedResultRequestDto, CreateSettlementDto, UpdateSettlementDto>, ISettlementAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SettlementCategory, int> _settlementRepositry;

        public SettlementAppService
            (
            IObjectMapper objectMapper,
            IRepository<SettlementDetail, int> repository,
            IRepository<User, long> userRepository,
            IRepository<SettlementCategory, int> settlementRepositry)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;
            _settlementRepositry = settlementRepositry;

        }
        public SettlementDto CreateSettlement(CreateSettlementDto model)
        {
            model.UserName = _userRepository.Get(model.UserId.Value).UserName;       
            return _objectMapper.Map<SettlementDto>((Repository.Insert(_objectMapper.Map<SettlementDetail>(model))));
        }

        public BaseResponse DeleteSettlement(int SettlementId)
        {

            var existing = Repository.Get(SettlementId);
            existing.IsDeleted = true;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public UpdateSettlementDto GetSettlementUpdateDetails(int SettlementId)
        {
            return _objectMapper.Map<UpdateSettlementDto>((Repository.Get(SettlementId)));

        }


        public List<SettlementDto> GetAllSettlements()
        {
            List<SettlementDto> settlements = _objectMapper.Map<List<SettlementDto>>(Repository.GetAllList());

            foreach (SettlementDto settlment in settlements)
            {
                settlment.ReturnedToName = GetRetunedToName(settlment.ReturnedTo);
                settlment.SettlementTypeName = getSettlementTypeName(settlment.SettlementTypeId);
            }

            return settlements;
        }

        private string getSettlementTypeName(int settlementTypeId)
        {
            if (settlementTypeId != 0)
               return _settlementRepositry.FirstOrDefault(x => x.Id == settlementTypeId).Name;
            else
                return "NoSettlementTypeFound";
        }

        private string GetRetunedToName(long ReturnedToId)
        {
            if (ReturnedToId != 0)
                return _userRepository.FirstOrDefault(x => x.Id == ReturnedToId).UserName;
            else
                return "NoUserFoundException!";
        }

        public BaseResponse UpdateSettlementDetails(UpdateSettlementDto model)
        {
            model.UserName = _userRepository.Get(model.UserId.Value).UserName;
            Repository.Update(_objectMapper.Map<SettlementDetail>(model));

            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }

        public BaseResponse UndoSettlement(int SettlementId)
        {
            var existing = Repository.Get(SettlementId);
            existing.IsDeleted = false;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }
    }
}