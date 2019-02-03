using System.Collections.Generic;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.ExpenseSheet.Dto;
using ExpenseManager.Helper;

namespace ExpenseManager.ExpenseSheet
{
    public interface IExpenseSheetAppService : IAsyncCrudAppService<ExpenseSheetDto, int, PagedResultRequestDto, CreateExpenseSheetDto, UpdateExpenseSheetDto>
    {
        ExpenseSheetDto CreateExpense(CreateExpenseSheetDto model);
        BaseResponse DeleteExpense(int ExpenseId);

        [HttpGet]
        UpdateExpenseSheetDto GetExpenseUpdateDetails(int ExpenseId);

        BaseResponse UpdateExpenseDetails(UpdateExpenseSheetDto model);

        [HttpGet]
        List<ExpenseSheetDto> GetAllExpense();

        [HttpGet]
        Dictionary<string, double> GetDashboardDataByExpenseCateogy();

        [HttpGet]
        List<ExpenseDetailByMonth> GetExpenseDetailByMonth();

        [HttpGet]
        Dictionary<string, List<ByIndividualSpentDetail>> GetAmountSpentByIndividual();

        [HttpGet]
        Dictionary<string, ByCommonExpenseSummary> GetByCommonExpenseSummary();

        [HttpPost]
        BaseResponse UndoExpense(int ExpenseId);

        [HttpGet]
        Dictionary<string, ByIndividualExpenseSummary> GetByIndividualExpenseSummary();
    }
}
