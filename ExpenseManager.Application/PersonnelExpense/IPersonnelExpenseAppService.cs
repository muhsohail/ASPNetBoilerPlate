using System.Collections.Generic;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ExpenseManager.PersonnelExpense.Dto;
using ExpenseManager.Helper;

namespace ExpenseManager.PersonnelExpenseSheet
{
    public interface IPersonnelExpenseSheetAppService : IAsyncCrudAppService<PersonnelExpenseDto, int, PagedResultRequestDto, CreatePersonnelExpenseDto, UpdatePersonnelExpenseDto>
    {
        PersonnelExpenseDto CreateExpense(CreatePersonnelExpenseDto model);
        BaseResponse DeleteExpense(int ExpenseId);

        [HttpGet]
        UpdatePersonnelExpenseDto GetExpenseUpdateDetails(int ExpenseId);

        BaseResponse UpdateExpenseDetails(UpdatePersonnelExpenseDto model);

        [HttpGet]
        List<PersonnelExpenseDto> GetAllExpense();

        [HttpGet]
        Dictionary<int, double> GetDashboardDataByExpenseCateogy();

        [HttpGet]
        List<ExpenseDetailByMonth> GetExpenseDetailByMonth();

        [HttpGet]
        Dictionary<string, List<ByIndividualSpentDetail>> GetAmountSpentByIndividual();

    }
}
