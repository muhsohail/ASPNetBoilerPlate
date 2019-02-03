using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Loan;
using ExpenseManager.Loan.Dto;
using ExpenseManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Web.Controllers
{
    public class LoanController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public LoanController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: Loan
        public ActionResult Index()
        {
            LoanViewModel model = new LoanViewModel();

            IReadOnlyList<LoanDto> loan = _httpCallingAppService.PostAppServiceData
                     <LoanAppService, PagedResultDto<LoanDto>, APIResponseObject<PagedResultDto<LoanDto>>>
                     ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                     .Result.Items;
            model.LoanDto = loan.Where(x => !x.IsDeleted).ToList();
            model.LoanSummary = PrepareLoanSummary(model.LoanDto);

            return View(model);
        }

        private LoanSummary PrepareLoanSummary(List<LoanDto> loanDto)
        {
            LoanSummary summary = new LoanSummary
            {
                TotalLoan = loanDto.Sum(x => x.LoanAmount),
                Returned = loanDto.Sum(x =>x.AmountReturned),
                Pending = loanDto.Sum(x => x.LoanAmount) - loanDto.Sum(x => x.AmountReturned)
            };

            return summary;
        }

        [HttpPost]
        public ActionResult Create(CreateLoanDto model)
        {
            LoanDto response = _httpCallingAppService.PostAppServiceData
                <LoanAppService, LoanDto, APIResponseObject<PagedResultDto<LoanDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Loan");
            else
                return Json(new { status = "Something went wrong, please try again ." });
        }

        public ActionResult EditLoan(int loanId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("loanId", Convert.ToString(loanId));


            UpdateLoanDto model = base._httpCallingAppService.GetAppServiceData
                            <LoanAppService, UpdateLoanDto, APIResponseObject<UpdateLoanDto>>
                            ("GetLoanUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            return View("_EditLoan", model);

        }

        [HttpPost]
        public ActionResult Update(UpdateLoanDto model)
        {
            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <LoanAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateLoanDetails", model).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Loan");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        [HttpPost]
        public ActionResult DeleteLoan(int LoanId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("LoanId", Convert.ToString(LoanId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <LoanAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteLoan", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Loan");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}