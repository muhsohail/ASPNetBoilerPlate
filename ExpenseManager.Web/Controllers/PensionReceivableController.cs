using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.PensionReceivable;
using ExpenseManager.PensionReceivable.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Web.Controllers
{
    public class PensionReceivableController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public PensionReceivableController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: PensionReceivable
        public ActionResult Index()
        {
            IReadOnlyList<PensionReceivableDto> PensionReceivable = _httpCallingAppService.PostAppServiceData
                    <PensionReceivableAppService, PagedResultDto<PensionReceivableDto>, APIResponseObject<PagedResultDto<PensionReceivableDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items;

            return View(PensionReceivable.Where(x => !x.IsDeleted).ToList());
        }

        [HttpPost]
        public ActionResult Create(CreatePensionReceivableDto model)
        {
            PensionReceivableDto response = _httpCallingAppService.PostAppServiceData
                <PensionReceivableAppService, PensionReceivableDto, APIResponseObject<PagedResultDto<PensionReceivableDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "PensionReceivable");
            else
                return Json(new { status = "Something went wrong, please try again ." });
        }

        public ActionResult EditPensionReceivable(int pensionReceivableId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("pensionReceivableId", Convert.ToString(pensionReceivableId));


            UpdatePensionReceivableDto model = base._httpCallingAppService.GetAppServiceData
                            <PensionReceivableAppService, UpdatePensionReceivableDto, APIResponseObject<UpdatePensionReceivableDto>>
                            ("GetPensionReceivableUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            return View("_EditPensionReceivable", model);

        }


        [HttpPost]
        public ActionResult Update(UpdatePensionReceivableDto model)
        {
            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <PensionReceivableAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdatePensionReceivableDetails", model).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "PensionReceivable");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        [HttpPost]
        public ActionResult DeletePensionReceivable(int pensionReceivableId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("PensionReceivableId", Convert.ToString(pensionReceivableId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <PensionReceivableAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeletePensionReceivable", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "PensionReceivable");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}