using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Pension;
using ExpenseManager.Pension.Dto;
using ExpenseManager.PensionReceivable;
using ExpenseManager.PensionReceivable.Dto;
using ExpenseManager.Stakeholder;
using ExpenseManager.Stakeholder.Dto;
using ExpenseManager.Web.Models;

namespace ExpenseManager.Web.Controllers
{
    public class PensionController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public PensionController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: Pension
        public ActionResult Index()
        {
            PensionViewModel model = new PensionViewModel();

            IReadOnlyList<StakeholderDto> Stakeholders = _httpCallingAppService.PostAppServiceData
                    <StakeholderAppService, PagedResultDto<StakeholderDto>, APIResponseObject<PagedResultDto<StakeholderDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items.ToList();


            ViewData["Stakeholders"] = new SelectList(Stakeholders, "Id", "Relationship");

            //IReadOnlyList<PensionDto> Pension = _httpCallingAppService.PostAppServiceData
            //        <PensionAppService, PagedResultDto<PensionDto>, APIResponseObject<PagedResultDto<PensionDto>>>
            //        ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
            //        .Result.Items;

            IReadOnlyList<PensionDto> Pension = base._httpCallingAppService.GetAppServiceData
                    <PensionAppService, List<PensionDto>, APIResponseObject<List<PensionDto>>>
                    ("GetAllPensionEntries").Result;

            IReadOnlyList<PensionReceivableDto> PensionReceived = _httpCallingAppService.PostAppServiceData
                    <PensionReceivableAppService, PagedResultDto<PensionReceivableDto>, APIResponseObject<PagedResultDto<PensionReceivableDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items;

            model.pensionDtos = Pension.ToList();
            model.summary = prepareSummary(model.pensionDtos, PensionReceived.ToList());

            return View(model);
        }

        private Summary prepareSummary(List<PensionDto> pensionDtos, List<PensionReceivableDto> receivedPension)
        {
            return new Summary {
                Total = receivedPension.Where(x => !x.IsDeleted).Sum(x =>x.Amount),
                Used = pensionDtos.Where(x => !x.IsDeleted).Sum(x => x.Amount),
                Pending = receivedPension.Where(x => !x.IsDeleted).Sum(x => x.Amount) - pensionDtos.Where(x => !x.IsDeleted).Sum(x => x.Amount)
            };
        }

        [HttpPost]
        public ActionResult Create(CreatePensionDto model)
        {
            string StakeholderId = Request.Form["Stakeholders"].ToString();
            model.StakeholderId = Int32.Parse(StakeholderId);

            PensionDto response = _httpCallingAppService.PostAppServiceData
                <PensionAppService, PensionDto, APIResponseObject<PagedResultDto<PensionDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Pension");
            else
                return Json(new { status = "Something went wrong, please try again ." });
        }

        public ActionResult EditPension(int pensionId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("pensionId", Convert.ToString(pensionId));


            UpdatePensionDto model = base._httpCallingAppService.GetAppServiceData
                            <PensionAppService, UpdatePensionDto, APIResponseObject<UpdatePensionDto>>
                            ("GetPensionUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            IReadOnlyList<StakeholderDto> Stakeholders = _httpCallingAppService.PostAppServiceData
                    <StakeholderAppService, PagedResultDto<StakeholderDto>, APIResponseObject<PagedResultDto<StakeholderDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items.ToList();

            StakholderItem stakeHolderItem = new StakholderItem
            {
                Id = model.StakeholderId.Value,
                Name = null,
                Relationship = model.StakeholderName
            };


            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<StakeholderDto> stakHolders = Stakeholders.AsEnumerable();

            foreach (var item in stakHolders)
            {

                SelectListItem tempItem = new SelectListItem { Text = item.Relationship.ToString(), Value = item.Id.ToString() };
                if (item.Id == stakeHolderItem.Id)
                    tempItem.Selected = true;


                selectListItems.Add(tempItem);

            }

            UpdatePensionViewModel updatePensionViewModel = new UpdatePensionViewModel();
            updatePensionViewModel.UpdatePensionDto = model;
            updatePensionViewModel.StakeholderViewModel.SelectedItemId = model.StakeholderId.Value;
            updatePensionViewModel.StakeholderViewModel.Items = selectListItems;


            return View("_EditPension", updatePensionViewModel);

        }

        [HttpPost]
        public ActionResult Update(UpdatePensionViewModel model)
        {
            model.UpdatePensionDto.StakeholderId = model.StakeholderViewModel.SelectedItemId;

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <PensionAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdatePensionDetails", model.UpdatePensionDto).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Pension");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        [HttpPost]
        public ActionResult DeletePension(int PensionId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("PensionId", Convert.ToString(PensionId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <PensionAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeletePension", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Pension");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

    }
}