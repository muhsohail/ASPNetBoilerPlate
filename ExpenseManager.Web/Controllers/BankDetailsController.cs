using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.BankDetails;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;

namespace ExpenseManager.Web.Controllers
{
    public class BankDetailsController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public BankDetailsController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: BankDetails
        public ActionResult Index()
        {
            IReadOnlyList<BankDetailsDto> bankDetails = _httpCallingAppService.PostAppServiceData
                   <BankDetailsAppService, PagedResultDto<BankDetailsDto>, APIResponseObject<PagedResultDto<BankDetailsDto>>>
                   ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                   .Result.Items;

            return View(bankDetails);
        }


        [HttpPost]
        public ActionResult CreateBankDetails(CreateBankDetailsDto model)
        {
            BankDetailsDto response = _httpCallingAppService.PostAppServiceData
                <BankDetailsAppService, BankDetailsDto, APIResponseObject<PagedResultDto<BankDetailsDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Charity");
            else
                return Json(new { status = "Something went wrong, please try again ." });

        }

        [HttpPost]
        public ActionResult DeleteBankDetails(int BankDetailsId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("BankDetailsId", Convert.ToString(BankDetailsId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <BankDetailsAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteBankDetails", new Dictionary<string, string>(), null, keyValues).Result;



            if (response.IsSucceeded)
                return RedirectToAction("Index", "Charity", new { area = ""});
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        public ActionResult EditBankDetails(int BankDetailsId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("BankDetailsId", Convert.ToString(BankDetailsId));


            UpdateBankDetailsDto model = base._httpCallingAppService.GetAppServiceData
                            <BankDetailsAppService, UpdateBankDetailsDto, APIResponseObject<UpdateBankDetailsDto>>
                            ("GetBankUpdateDetails", new Dictionary<string, string>(), keyValues).Result;


            return View("../Charity/_EditBankDetails", model);

        }

        [HttpPost]
        public ActionResult Update(UpdateBankDetailsDto model)
        {
            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <BankDetailsAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateBankAccountDetails", model).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Charity", new { area = "" });
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}