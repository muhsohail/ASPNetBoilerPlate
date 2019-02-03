using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ExpenseManager.Authorization.Users;
using ExpenseManager.ExpenseSheet.Dto;
using ExpenseManager.ExpenseType;
using ExpenseManager.Helper;
using ExpenseManager.Model;

namespace ExpenseManager.ExpenseSheet
{
    public class ExpenseSheetAppService : AsyncCrudAppService<ExpenseDetail, ExpenseSheetDto, int, PagedResultRequestDto, CreateExpenseSheetDto, UpdateExpenseSheetDto>, IExpenseSheetAppService
    {
        private IObjectMapper _objectMapper;
        private readonly IExpenseTypeAppService _expenseTypeAppService;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SettlementDetail, int> _settlementRepositry;

        public ExpenseSheetAppService
            (
            IObjectMapper objectMapper,
            IRepository<ExpenseDetail, int> repository,
            IExpenseTypeAppService expenseTypeAppService,
            IRepository<User, long> userRepository,
            IRepository<SettlementDetail, int> settlementRepositry)
            : base(repository)
        {
            _objectMapper = objectMapper;
            _expenseTypeAppService = expenseTypeAppService;
            _userRepository = userRepository;
            _settlementRepositry = settlementRepositry;

        }

        public ExpenseSheetDto CreateExpense(CreateExpenseSheetDto model)
        {
            return _objectMapper.Map<ExpenseSheetDto>((Repository.Insert(_objectMapper.Map<ExpenseDetail>(model))));
        }

        public BaseResponse DeleteExpense(int ExpenseId)
        {

            var existing = Repository.Get(ExpenseId);
            existing.IsDeleted = true;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }

        public UpdateExpenseSheetDto GetExpenseUpdateDetails(int ExpenseId)
        {
            return _objectMapper.Map<UpdateExpenseSheetDto>((Repository.Get(ExpenseId)));

        }

        public BaseResponse UpdateExpenseDetails(UpdateExpenseSheetDto model)
        {
            Repository.Update(_objectMapper.Map<ExpenseDetail>(model));
            return new BaseResponse { IsSucceeded = true, Message = "Update" };
        }


        public List<ExpenseSheetDto> GetAllExpense()
        {
            List<ExpenseSheetDto> expenseSheets = _objectMapper.Map<List<ExpenseSheetDto>>(Repository.GetAllList());

            foreach (ExpenseSheetDto expsnse in expenseSheets)
            {
                expsnse.ExpenseCategoryName = GetCategoryName(expsnse.ExpenseCatregoryId);
                expsnse.CreatedBy = GetCreatedByName(expsnse.UserId);
            }

            return expenseSheets;
        }

        private string GetCreatedByName(long? userId)
        {
            return _userRepository.Single(x => x.Id == userId).UserName;
        }

        private string GetCategoryName(int expenseCatregoryId)
        {
            if (expenseCatregoryId != 0)
                return _objectMapper.Map<string>(_expenseTypeAppService.GetCategoryName(expenseCatregoryId));
            else
                return "N0Category";
        }

        public Dictionary<string, double> GetDashboardDataByExpenseCateogy()
        {
            Dictionary<string, double> returnDataStructure = _objectMapper.Map<List<ExpenseSheetDto>>(Repository.GetAllIncluding(x =>x.ExpenseCategory))
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.ExpenseCategoryName)
                .ToDictionary(x => x.Key, y => y.Sum(z => z.Amount));

            return returnDataStructure;
        }

        public List<ExpenseDetailByMonth> GetExpenseDetailByMonth()
        {
            List<ExpenseDetailByMonth> detailsByMonth = new List<ExpenseDetailByMonth>();
            detailsByMonth = _objectMapper.Map<List<ExpenseSheetDto>>(Repository.GetAllList())
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
            DateTime cutOff = DateTime.Now.Subtract(new TimeSpan(168, 0, 0));
            // Get List of Users
            List<User> users = _userRepository.GetAllList();


            Dictionary<string, List<ByIndividualSpentDetail>> result  = Repository.GetAllList()
                    .Where(x => x.DateSpent.Value > cutOff && !x.IsDeleted)
                    .GroupBy(x => users.FirstOrDefault(n => n.Id == x.UserId).UserName)
                    .ToDictionary(x => x.Key, y => y.ToList().GroupBy(z => z.DateSpent.Value.Date).Select(z => new ByIndividualSpentDetail
                    {
                        When = z.Key.ToString("dddd"),
                        Amount = z.Sum(amount => amount.Amount)
                    }).ToList());

            return result;
        }

        // TODO - Before implementing this we need to know how much individual have paid back!
        public Dictionary<string, ByCommonExpenseSummary> GetByCommonExpenseSummary()
        {
            double TotalAmountSpent = 0;

            Dictionary<string, ByCommonExpenseSummary> summary = new Dictionary<string, ByCommonExpenseSummary>();

            List<ExpenseDetail> items = Repository.GetAllIncluding(x => x.ExpenseCategory)
                                        .Where(x => !x.IsDeleted && x.ExpenseCategory.IsCommonCategory).ToList();
            if (items.Count > 0)
                TotalAmountSpent = items.Sum(x => x.Amount);

            // Get List of Users
            List<string> allowedUsers = _userRepository.GetAllList().Where(x => x.IsActive).Select(x => x.UserName.ToUpper()).ToList();
            List<User> users = _userRepository.GetAllList().Where(x => allowedUsers.Contains(x.UserName.ToUpper())).ToList();
            DateTime cutOff = DateTime.Now.Subtract(new TimeSpan(168, 0, 0));
            summary = users
                .GroupBy(x => x.UserName)
                .ToDictionary(x => x.Key, y => y.Select(z => new ByCommonExpenseSummary
                {
                    PerPerson = y.Key.ToUpper().StartsWith("S") && y.Key.ToUpper().EndsWith("L") ? TotalAmountSpent * 0.55 : y.Key.ToUpper().StartsWith("J") && y.Key.ToUpper().EndsWith("D") ? TotalAmountSpent * 0.3 : TotalAmountSpent * 0.15,
                    ToBeReturn = y.Key.ToUpper().StartsWith("S") && y.Key.ToUpper().EndsWith("L") ? TotalAmountSpent * 0.55 : y.Key.ToUpper().StartsWith("J") && y.Key.ToUpper().EndsWith("D") ? TotalAmountSpent * 0.3 : TotalAmountSpent * 0.15,
                    Returned = _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 1).Count() > 0 ? _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 1).Sum(a => a.Amount) : 0,
                    Pending = (y.Key.ToUpper().StartsWith("S") && y.Key.ToUpper().EndsWith("L") ? TotalAmountSpent * 0.55 : y.Key.ToUpper().StartsWith("J") && y.Key.ToUpper().EndsWith("D") ? TotalAmountSpent * 0.3 : TotalAmountSpent * 0.15) - (_settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 1).Count() > 0 ? _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 1).Sum(a => a.Amount) : 0),
                    TotalSpent = TotalAmountSpent
                }).FirstOrDefault());

            return summary;
        }


        public Dictionary<string, ByIndividualExpenseSummary> GetByIndividualExpenseSummary()
        {
            double TotalAmountSpent = 0;
            double amoutSpentByaOnb = 0;
            Dictionary<string, ByIndividualExpenseSummary> summary = new Dictionary<string, ByIndividualExpenseSummary>();

            List<ExpenseDetail> a1 = Repository.GetAllIncluding(x => x.ExpenseCategory)
                                        .Where(x => !x.IsDeleted && !x.ExpenseCategory.IsCommonCategory).ToList();

            if (a1.Count > 0)
            {
                TotalAmountSpent = a1.Where(x => x.ExpenseCategory.CategoryType == CategoryType.Double)
                                        .Sum(x => x.Amount);

                amoutSpentByaOnb = a1.Where(x => x.ExpenseCategory.CategoryType == CategoryType.Single)
                                        .Sum(x => x.Amount);
            }           
            
            // Get List of Users
            List<string> allowedUsers = _userRepository.GetAllList().Where(x => x.IsActive && !x.Name.StartsWith("B")).Select(x => x.UserName.ToUpper()).ToList();
            List<User> users = _userRepository.GetAllList().Where(x => allowedUsers.Contains(x.UserName.ToUpper())).ToList();
            DateTime cutOff = DateTime.Now.Subtract(new TimeSpan(168, 0, 0));

            summary = users
                    .GroupBy(x => x.UserName)
                    .ToDictionary(x => x.Key, y => y.Select(z => new ByIndividualExpenseSummary
                    {
                        PerPerson = TotalAmountSpent * 0.5,
                        ToBeReturn = TotalAmountSpent * 0.5,
                        Returned = _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 2).Count() > 0 ? _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 2).Sum(a => a.Amount) : 0,
                        Pending = TotalAmountSpent * 0.5 - (_settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 2).Count() > 0 ? _settlementRepositry.GetAll().Where(a => a.UserName == y.Key && !a.IsDeleted && a.SettlementTypeId == 2).Sum(a => a.Amount) : 0),
                        TotalSpent = TotalAmountSpent
                    }).FirstOrDefault());


            if (summary.Count > 0)
            {
                string key = summary.Where(x => x.Key.StartsWith("s") || x.Key.StartsWith("S")).Select(x => x.Key).FirstOrDefault();
                summary[key].PerPerson = summary[key].PerPerson + amoutSpentByaOnb;

            }

            return summary;
        }

        public BaseResponse UndoExpense(int ExpenseId)
        {

            var existing = Repository.Get(ExpenseId);
            existing.IsDeleted = false;
            Repository.Update(existing);
            return new BaseResponse { IsSucceeded = true, Message = "Deleted" };
        }


        public string GetKeyFirstCharacter(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

    }
}


