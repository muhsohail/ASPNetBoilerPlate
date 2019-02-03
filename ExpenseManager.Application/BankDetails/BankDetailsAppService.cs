using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Authorization.Users;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Model;

namespace ExpenseManager.BankDetails
{
    public class BankDetailsAppService : AsyncCrudAppService<BankAccountDetail, BankDetailsDto, int, PagedResultRequestDto, CreateBankDetailsDto, UpdateBankDetailsDto>, IBankDetailsAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;

        public BankDetailsAppService
            (
            IObjectMapper objectMapper,
            IRepository<BankAccountDetail, int> repository,
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;

        }

        public BankDetailsDto CreateBankDetails(CreateBankDetailsDto model)
        {
            return _objectMapper.Map<BankDetailsDto>((Repository.Insert(_objectMapper.Map<BankAccountDetail>(model))));
        }

        public BaseResponse DeleteBankDetails(int BankDetailsId)
        {

            var existing = Repository.Get(BankDetailsId);
            existing.IsDeleted = true;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public UpdateBankDetailsDto GetBankUpdateDetails(int BankDetailsId)
        {
            return _objectMapper.Map<UpdateBankDetailsDto>((Repository.Get(BankDetailsId)));

        }

        public BaseResponse UpdateBankAccountDetails(UpdateBankDetailsDto model)
        {
            Repository.Update(_objectMapper.Map<BankAccountDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }


        public List<BankDetailsDto> GetAllBankDetails()
        {
            List<BankDetailsDto> Charities = _objectMapper.Map<List<BankDetailsDto>>(Repository.GetAllList());

               return Charities;
        }

        private string GetCreatedByName(long? userId)
        {
            return _userRepository.Single(x => x.Id == userId).UserName;
        }


        // TODO - Before implementing this we need to know how much individual have paid back!

        public BaseResponse UndoBankDetails(int BankDetailsId)
        {

            var existing = Repository.Get(BankDetailsId);
            existing.IsDeleted = false;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

    }
}


