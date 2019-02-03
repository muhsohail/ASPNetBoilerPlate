using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.ExpenseSheet;
using ExpenseManager.ExpenseSheet.Dto;
using ExpenseManager.ExpenseType;
using ExpenseManager.ExpenseType.Dto;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Web.Models;

namespace ExpenseManager.Web.Controllers
{
    public class ExpenseController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public ExpenseController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: Expense
        public ActionResult Index()
        {

            var ExpenseSheetList = base._httpCallingAppService.GetAppServiceData
                 <ExpenseSheetAppService, List<ExpenseSheetDto>, APIResponseObject<List<ExpenseSheetDto>>>
                 ("GetAllExpense").Result;

            IReadOnlyList<ExpenseTypeDto> ExpenseTypes = _httpCallingAppService.PostAppServiceData
                <ExpenseTypeAppService, PagedResultDto<ExpenseTypeDto>, APIResponseObject<PagedResultDto<ExpenseTypeDto>>>
                ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                .Result.Items;

            ViewData["ExpenseTypes"] = new SelectList(ExpenseTypes.ToList(), "Id", "Name");
            ExpenseSheetList = ExpenseSheetList.OrderByDescending(x => x.DateSpent ?? DateTime.MaxValue).ToList();

            return View(ExpenseSheetList);
        }

        public class ExpenseType
        {
            public int Id { get; set; }
            public string Value { get; set; }

        }

        [HttpPost]
        public ActionResult Create(ExpenseSheetViewModel model)
        {
            string ExpenseTypeValue = Request.Form["ExpenseTypes"].ToString();
            model.UserId = AbpSession.UserId;
            model.ExpenseCatregoryId = Int32.Parse(ExpenseTypeValue);

            ExpenseSheetDto response = _httpCallingAppService.PostAppServiceData
                <ExpenseSheetAppService, ExpenseSheetDto, APIResponseObject<PagedResultDto<ExpenseSheetDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Expense");
            else
                return Json(new { status = "Something went wrong, please try again ." });
        }


        [HttpPost]
        public ActionResult DeleteExpense(int ExpenseId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("ExpenseId", Convert.ToString(ExpenseId));

            DateTime StartTime = DateTime.Now;
            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <ExpenseSheetAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteExpense", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Expense");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        public ActionResult EditExpense(int ExpenseId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("ExpenseId", Convert.ToString(ExpenseId));

            // Temp Code
            DateTime StartTime = DateTime.Now;
            UpdateExpenseSheetDto model = base._httpCallingAppService.GetAppServiceData
                            <ExpenseSheetAppService, UpdateExpenseSheetDto, APIResponseObject<UpdateExpenseSheetDto>>
                            ("GetExpenseUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            IReadOnlyList<ExpenseTypeDto> ExpenseTypes = _httpCallingAppService.PostAppServiceData
                            <ExpenseTypeAppService, PagedResultDto<ExpenseTypeDto>, APIResponseObject<PagedResultDto<ExpenseTypeDto>>>
                            ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                            .Result.Items;

            ViewData["ExpenseTypes"] = new SelectList(ExpenseTypes.ToList(), "Id", "Name", model.ExpenseCatregoryId);


            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<ExpenseTypeDto> expenseTypes = ExpenseTypes.AsEnumerable();

            foreach (var item in expenseTypes)
            {

                SelectListItem tempItem = new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString() };
                if (item.Id == model.ExpenseCatregoryId)
                    tempItem.Selected = true;

                selectListItems.Add(tempItem);
            }

            UpdateExpenseViewModel updateExpenseViewModel = new UpdateExpenseViewModel();
            updateExpenseViewModel.UpdateExpenseSheetDto = model;

            updateExpenseViewModel.ItemViewModel.SelectedItemId = model.ExpenseCatregoryId;
            updateExpenseViewModel.ItemViewModel.Items = selectListItems;

            return View("_EditExpense", updateExpenseViewModel);

        }

        [HttpPost]
        public ActionResult Update(UpdateExpenseViewModel model)
        {
            model.UpdateExpenseSheetDto.UserId = AbpSession.UserId;
            model.UpdateExpenseSheetDto.ExpenseCatregoryId = model.ItemViewModel.SelectedItemId;

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <ExpenseSheetAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateExpenseDetails", model.UpdateExpenseSheetDto).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Expense");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        [HttpPost]
        public ActionResult UndoExpense(int ExpenseId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("ExpenseId", Convert.ToString(ExpenseId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <ExpenseSheetAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("UndoExpense", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Expense");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}