using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Authorization.Users;
using ExpenseManager.Charity.Dto;
using ExpenseManager.Helper;
using ExpenseManager.Model;

namespace ExpenseManager.Charity
{
    public class CharityAppService : AsyncCrudAppService<CharityDetail, CharityDto, int, PagedResultRequestDto, CreateCharityDto, UpdateCharityDto>, ICharityAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;

        public CharityAppService
            (
            IObjectMapper objectMapper,
            IRepository<CharityDetail, int> repository,
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _userRepository = userRepository;

        }

        public CharityDto CreateCharity(CreateCharityDto model)
        {
            return _objectMapper.Map<CharityDto>((Repository.Insert(_objectMapper.Map<CharityDetail>(model))));
        }

        public BaseResponse DeleteCharity(int CharityId)
        {

            var existing = Repository.Get(CharityId);
            existing.IsDeleted = true;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public UpdateCharityDto GetCharityUpdateDetails(int CharityId)
        {
            return _objectMapper.Map<UpdateCharityDto>((Repository.Get(CharityId)));

        }

        public BaseResponse UpdateCharityDetails(UpdateCharityDto model)
        {
            Repository.Update(_objectMapper.Map<CharityDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }


        public List<CharityDto> GetAllCharity()
        {
            List<CharityDto> Charities = _objectMapper.Map<List<CharityDto>>(Repository.GetAllList());

               return Charities;
        }

        private string GetCreatedByName(long? userId)
        {
            return _userRepository.Single(x => x.Id == userId).UserName;
        }


        // TODO - Before implementing this we need to know how much individual have paid back!

        public BaseResponse UndoCharity(int CharityId)
        {

            var existing = Repository.Get(CharityId);
            existing.IsDeleted = false;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

    }
}


