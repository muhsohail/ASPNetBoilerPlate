using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using Abp.Web.Mvc.Authorization;
using ExpenseManager.ExpenseSheet;
using ExpenseManager.ExpenseSheet.Dto;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Onus;
using ExpenseManager.Onus.Dto;
using ExpenseManager.Pension;
using ExpenseManager.PensionReceivable;
using ExpenseManager.Web.Models;

namespace ExpenseManager.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public HomeController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }
        public ActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            
            Dictionary<string, double> ExpenseDetailsByCategory = _httpCallingAppService.GetAppServiceData
                    <ExpenseSheetAppService, Dictionary<string, double>, APIResponseObject<Dictionary<string, double>>>
                    ("GetDashboardDataByExpenseCateogy").Result;

            dashboardViewModel.GetDashboardDataByExpenseCateogy = ExpenseDetailsByCategory;
            dashboardViewModel.GetAmountSpentByIndividual = GetAmountSpentByIndividual();
            dashboardViewModel.Tasks = GetTasks();
            dashboardViewModel.PensionUsageDetail = GetPensionUsageDetails();
            dashboardViewModel.ByCommonExpenseSummary = GetByCommonExpenseSummary();
            dashboardViewModel.ByIndividualExpenseSummary = getByIndividualExpenseSummary();
            return View(dashboardViewModel);
        }

        private Dictionary<string, ByIndividualExpenseSummary> getByIndividualExpenseSummary()
        {
            Dictionary<string, ByIndividualExpenseSummary> ByIndividualSummary = _httpCallingAppService.GetAppServiceData
                    <ExpenseSheetAppService, Dictionary<string, ByIndividualExpenseSummary>, APIResponseObject<ByCommonExpenseSummary>>
                    ("GetByIndividualExpenseSummary").Result;

            return ByIndividualSummary;
        }

        [HttpGet]
        public Dictionary<string, ByCommonExpenseSummary> GetByCommonExpenseSummary()
        {
            Dictionary<string, ByCommonExpenseSummary> ByCommonSummary = _httpCallingAppService.GetAppServiceData
                    <ExpenseSheetAppService, Dictionary<string, ByCommonExpenseSummary>, APIResponseObject<ByCommonExpenseSummary>>
                    ("GetByCommonExpenseSummary").Result;

            return ByCommonSummary;
        }

        private PensionUsageDetail GetPensionUsageDetails()
        {
            double TotalPensionUsed = _httpCallingAppService.GetAppServiceData
                    <PensionAppService, double, APIResponseObject<double>>
                    ("GetTotalPensionUsed").Result;

            double TotalPensionReceived = _httpCallingAppService.GetAppServiceData
                    <PensionReceivableAppService, double, APIResponseObject<double>>
                    ("GetTotalPensionReceived").Result;

            return new PensionUsageDetail
            {
                Total = TotalPensionReceived,
                Spent = TotalPensionUsed,
                Remaining = TotalPensionReceived - TotalPensionUsed
            };
        }


        [HttpGet]
        public JsonResult GetPensionDetails()
        {
            List<PensionDetailByStakeholder> ExpenseDetailByMonth = _httpCallingAppService.GetAppServiceData
                    <PensionAppService, List<PensionDetailByStakeholder>, APIResponseObject<List<PensionDetailByStakeholder>>>
                    ("GetPensionDetailsByStakeholder").Result;

            return Json(ExpenseDetailByMonth, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExpenseDetailByMonth()
        {
            List<ExpenseDetailByMonth> ExpenseDetailByMonth = _httpCallingAppService.GetAppServiceData
                    <ExpenseSheetAppService, List<ExpenseDetailByMonth>, APIResponseObject<List<ExpenseDetailByMonth>>>
                    ("GetExpenseDetailByMonth").Result;

            return Json(ExpenseDetailByMonth, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public Dictionary<string, List<ByIndividualSpentDetail>> GetAmountSpentByIndividual()
        {
            Dictionary<string, List<ByIndividualSpentDetail>> ExpenseDetailByMonth = _httpCallingAppService.GetAppServiceData
                    <ExpenseSheetAppService, Dictionary<string, List<ByIndividualSpentDetail>>, APIResponseObject<List<ExpenseDetailByMonth>>>
                    ("GetAmountSpentByIndividual").Result;

            return ExpenseDetailByMonth;
            //return Json(ExpenseDetailByMonth, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public List<OnusDto> GetTasks()
        {
            //IReadOnlyList<OnusDto> Tasks = _httpCallingAppService.PostAppServiceData<OnusAppService, PagedResultDto<OnusDto>, APIResponseObject<PagedResultDto<OnusDto>>>("GetAll",
            //        new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 }).Result.Items.ToList();

            IReadOnlyList<OnusDto> Tasks = base._httpCallingAppService.GetAppServiceData
                    <OnusAppService, List<OnusDto>, APIResponseObject<List<OnusDto>>>
                    ("GetAllOnus").Result;


            return Tasks.ToList();

        }

    }
}