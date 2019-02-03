using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.BankDetails;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Charity;
using ExpenseManager.Charity.Dto;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Web.Models;

namespace ExpenseManager.Web.Controllers
{
    public class CharityController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public CharityController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }
        // GET: Charity
        public ActionResult Index()
        {
            CharityViewModel model = new CharityViewModel();
            IReadOnlyList<CharityDto> charityDetails = _httpCallingAppService.PostAppServiceData
                    <CharityAppService, PagedResultDto<CharityDto>, APIResponseObject<PagedResultDto<CharityDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items;

            IReadOnlyList<BankDetailsDto> bankDetails = _httpCallingAppService.PostAppServiceData
                   <BankDetailsAppService, PagedResultDto<BankDetailsDto>, APIResponseObject<PagedResultDto<BankDetailsDto>>>
                   ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                   .Result.Items;

            model.charityDto = charityDetails.ToList();
            model.bankDetailsDto = bankDetails.Where(x => !x.IsDeleted).ToList();

            model.charitySummary = prepareCharitySummary(model.charityDto, model.bankDetailsDto);

            return View(model);
        }

        private CharitySummary prepareCharitySummary(List<CharityDto> charityDto, List<BankDetailsDto> bankDetailsDto)
        {
            return new CharitySummary {
                Total = bankDetailsDto.Where(x => !x.IsDeleted).Sum(x => x.Amount),
                Used = charityDto.Where(x => !x.IsDeleted).Sum(x =>x.Amount),
                Pending = bankDetailsDto.Where(x => !x.IsDeleted).Sum(x => x.Amount) - charityDto.Where(x => !x.IsDeleted).Sum(x => x.Amount)
            };
        }

        [HttpPost]
        public ActionResult Create(CreateCharityDto model)
        {
            model.UserId = AbpSession.UserId;

            CharityDto response = _httpCallingAppService.PostAppServiceData
                <CharityAppService, CharityDto, APIResponseObject<PagedResultDto<CharityDto>>>
                ("Create", model)
                .Result;
            if (response != null)
                return RedirectToAction("Index", "Charity");
            else
                return Json(new { status = "Something went wrong, please try again ." });

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
        public ActionResult DeleteCharity(int CharityId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("CharityId", Convert.ToString(CharityId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <CharityAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteCharity", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Charity");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        public ActionResult EditCharity(int CharityId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("CharityId", Convert.ToString(CharityId));


            UpdateCharityDto model = base._httpCallingAppService.GetAppServiceData
                            <CharityAppService, UpdateCharityDto, APIResponseObject<UpdateCharityDto>>
                            ("GetCharityUpdateDetails", new Dictionary<string, string>(), keyValues).Result;


            return View("_EditCharity", model);

        }

        [HttpPost]
        public ActionResult Update(UpdateCharityDto model)
        {
            model.UserId = AbpSession.UserId;

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <CharityAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateCharityDetails", model).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Charity");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        [HttpPost]
        public ActionResult UndoCharity(int CharityId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("CharityId", Convert.ToString(CharityId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <CharityAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("UndoExpense", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Charity");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}