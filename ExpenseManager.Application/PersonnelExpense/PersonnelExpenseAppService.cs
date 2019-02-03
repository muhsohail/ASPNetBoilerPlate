using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Authorization.Users;
using ExpenseManager.PersonnelExpense.Dto;
using ExpenseManager.ExpenseType;
using ExpenseManager.Helper;
using ExpenseManager.Model;

namespace ExpenseManager.PersonnelExpenseSheet
{
    public class PersonnelExpenseSheetAppService : AsyncCrudAppService<PersonnelExpenseDetail, PersonnelExpenseDto, int, PagedResultRequestDto, CreatePersonnelExpenseDto, UpdatePersonnelExpenseDto>, IPersonnelExpenseSheetAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IExpenseTypeAppService _expenseTypeAppService;
        private readonly IRepository<User, long> _userRepository;

        public PersonnelExpenseSheetAppService
            (
            IObjectMapper objectMapper,
            IRepository<PersonnelExpenseDetail, int> repository,
            IExpenseTypeAppService expenseTypeAppService,
            IRepository<User, long> userRepository)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _expenseTypeAppService = expenseTypeAppService;
            _userRepository = userRepository;

        }

        public PersonnelExpenseDto CreateExpense(CreatePersonnelExpenseDto model)
        {
            return _objectMapper.Map<PersonnelExpenseDto>((Repository.Insert(_objectMapper.Map<PersonnelExpenseDetail>(model))));
        }

        public BaseResponse DeleteExpense(int ExpenseId)
        {

            var existing = Repository.Get(ExpenseId);
            existing.IsDeleted = true;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public UpdatePersonnelExpenseDto GetExpenseUpdateDetails(int ExpenseId)
        {
            return _objectMapper.Map<UpdatePersonnelExpenseDto>((Repository.Get(ExpenseId)));


        }

        public BaseResponse UpdateExpenseDetails(UpdatePersonnelExpenseDto model)
        {
            Repository.Update(_objectMapper.Map<PersonnelExpenseDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }


        public List<PersonnelExpenseDto> GetAllExpense()
        {
            List<PersonnelExpenseDto> expenseSheets = _objectMapper.Map<List<PersonnelExpenseDto>>(Repository.GetAllList());
            foreach (PersonnelExpenseDto expsnse in expenseSheets)
                expsnse.ExpenseCategoryName = GetCategoryName(expsnse.ExpenseCatregoryId);

            return expenseSheets;
        }
        private string GetCategoryName(int expenseCatregoryId)
        {
            if (expenseCatregoryId != 0)
                return _objectMapper.Map<string>(_expenseTypeAppService.GetCategoryName(expenseCatregoryId));
            else
                return "N0Category";
        }

        public Dictionary<int, double> GetDashboardDataByExpenseCateogy()
        {
            Dictionary<int, double> returnDataStructure = _objectMapper.Map<List<PersonnelExpenseDto>>(Repository.GetAllList())
                .GroupBy(x => x.ExpenseCatregoryId)
                .ToDictionary(x => x.Key, y => y.Sum(z => z.Amount));

            return returnDataStructure;
        }

        public List<ExpenseDetailByMonth> GetExpenseDetailByMonth()
        {
            List<ExpenseDetailByMonth> detailsByMonth = new List<ExpenseDetailByMonth>();
            detailsByMonth = _objectMapper.Map<List<PersonnelExpenseDto>>(Repository.GetAllList())
                .GroupBy(x => x.ExpenseCatregoryId)
                .Select(x => new ExpenseDetailByMonth
                {
                    Month = x.Key,
                    Amount = x.Sum(y => y.Amount)
                }).ToList();


            return detailsByMonth;
        }

        public Dictionary<string, List<ByIndividualSpentDetail>> GetAmountSpentByIndividual()
        {
            // Get List of Users
            List<User> users = _userRepository.GetAllList();


            Dictionary<string, List<ByIndividualSpentDetail>> result  = Repository.GetAllList()
                    .GroupBy(x => users.FirstOrDefault(n => n.Id == x.UserId).UserName)
                    .ToDictionary(x => x.Key, y => y.ToList().GroupBy(z => z.DateSpent.Value.Date).Select(z => new ByIndividualSpentDetail
                    {
                        When = z.Key.ToString("dddd"),
                        Amount = z.Sum(amount => amount.Amount)
                    }).ToList());

            return result;
        }
    }
}