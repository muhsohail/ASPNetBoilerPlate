using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Model;
using ExpenseManager.Onus.Dto;
using System.Data.Entity;
using System;
using ExpenseManager.Status;
using ExpenseManager.Helper;
using System.Linq;

namespace ExpenseManager.Onus
{
    public class OnusAppService : AsyncCrudAppService<OnusDetail, OnusDto, int, PagedResultRequestDto, CreateOnusDto, UpdateOnusDto>, IOnusAppService
    {
        private IObjectMapper _objectMapper;
        private IRepository<OnusDetail, int> _onusRepositry;
        private IStatusAppService _statusAppService;

        public OnusAppService(
            IRepository<OnusDetail, int> repository,
            IObjectMapper objectMapper,
            IRepository<OnusDetail, int> onusRepositry,
            IStatusAppService statusAppService) : base(repository)
        {
            _objectMapper = objectMapper;
            _onusRepositry = onusRepositry;
            _statusAppService = statusAppService;
        }

        public List<OnusDto> GetAllOnus()
        {
            List<OnusDto> onuses = _objectMapper.Map<List<OnusDto>>(Repository.GetAllList().Where(x => !x.IsDeleted));
            foreach (OnusDto onus in onuses)
                onus.OnusStatusName = GetOnusStatusName(onus.OnusStatusId);

            return onuses;
        }

        private string GetOnusStatusName(int OnusId)
        {
            return _objectMapper.Map<string>(_statusAppService.GetOnusStatusName(OnusId));
        }



        public UpdateOnusDto GetOnusUpdateDetails(int OnusId)
        {
            return _objectMapper.Map<UpdateOnusDto>((Repository.Get(OnusId)));
        }

        public BaseResponse UpdateOnusDetails(UpdateOnusDto model)
        {
            Repository.Update(_objectMapper.Map<OnusDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }

        public BaseResponse DeleteOnus(int OnusId)
        {

            var existing = Repository.Get(OnusId);
            existing.IsDeleted = true;
            Repository.Update(existing);

            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }


        public BaseResponse UndoOnus(int OnusId)
        {

            var existing = Repository.Get(OnusId);
            existing.IsDeleted = false;
            Repository.Update(existing);

            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

    }
}