using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.Model;
using ExpenseManager.Loan.Dto;
using ExpenseManager.Stakeholder;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseManager.Loan
{
    public class LoanAppService : AsyncCrudAppService<LoanDetail, LoanDto, int, PagedResultRequestDto, CreateLoanDto, UpdateLoanDto>, ILoanAppService
    {
        private IObjectMapper _objectMapper;
        public LoanAppService(
            IRepository<LoanDetail, int> repository,
            IObjectMapper objectMapper)
            : base(repository)
        {
            _objectMapper = objectMapper;
        }


        public UpdateLoanDto GetLoanUpdateDetails(int loanId)
        {
            return _objectMapper.Map<UpdateLoanDto>((Repository.Get(loanId)));
        }

        public BaseResponse UpdateLoanDetails(UpdateLoanDto model)
        {
            Repository.Update(_objectMapper.Map<LoanDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }

        public BaseResponse DeleteLoan(int LoanId)
        {

            var existing = Repository.Get(LoanId);
            existing.IsDeleted = true;
            Repository.Update(existing);

            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }
    }
}