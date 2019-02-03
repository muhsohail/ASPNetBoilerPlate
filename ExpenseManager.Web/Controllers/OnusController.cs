using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using Abp.Runtime.Validation;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Onus;
using ExpenseManager.Onus.Dto;
using ExpenseManager.Status;
using ExpenseManager.Status.Dto;
using ExpenseManager.Web.Models;

namespace ExpenseManager.Web.Controllers
{
    public class OnusController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public OnusController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: Onus
        public ActionResult Index()
        {

            IReadOnlyList<StatusDto> OnusStatuses = _httpCallingAppService.PostAppServiceData
                <StatusAppService, PagedResultDto<StatusDto>, APIResponseObject<PagedResultDto<StatusDto>>>
                ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                .Result.Items.ToList();

            ViewData["OnusStatuses"] = new SelectList(OnusStatuses, "Id", "Name");

            List<OnusDto> Onuses = base._httpCallingAppService.GetAppServiceData
                                        <OnusAppService, List<OnusDto>, APIResponseObject<List<OnusDto>>>
                                        ("GetAllOnus").Result;

            OnusViewModel model = new OnusViewModel();

            model.onusDtos = Onuses;
            model.OnusSummary = prepareOnusSummary(Onuses);

            return View(model);
        }

        private Dictionary<string, int> prepareOnusSummary(List<OnusDto> onuses)
        {
            Dictionary<string, int> summary = new Dictionary<string, int>();
            summary.Add("Planned", onuses.Where(o => o.OnusStatusName.ToUpper() == "PLANNED").Count());
            summary.Add("Started", onuses.Where(o => o.OnusStatusName.ToUpper() == "STARTED").Count());
            summary.Add("OnHold", onuses.Where(o => o.OnusStatusName.ToUpper() == "ONHOLD").Count());
            summary.Add("Completed", onuses.Where(o => o.OnusStatusName.ToUpper() == "COMPLETED").Count());
            return summary;
        }

        [HttpPost]
        public ActionResult Create(CreateOnusDto model)
        {
            string OnusStatusId = Request.Form["OnusStatuses"].ToString();
            model.OnusStatusId = Int32.Parse(OnusStatusId);

            OnusDto response = _httpCallingAppService.PostAppServiceData
                <OnusAppService, OnusDto, APIResponseObject<PagedResultDto<OnusDto>>>
                ("Create", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Onus");
            else
                return Json(new { status = "Something went wrong, please try again ." });
        }


        public ActionResult EditOnus(int OnusId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("OnusId", Convert.ToString(OnusId));


            UpdateOnusDto model = base._httpCallingAppService.GetAppServiceData
                            <OnusAppService, UpdateOnusDto, APIResponseObject<UpdateOnusDto>>
                            ("GetOnusUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            IReadOnlyList<StatusDto> OnusStatuses = _httpCallingAppService.PostAppServiceData
                <StatusAppService, PagedResultDto<StatusDto>, APIResponseObject<PagedResultDto<StatusDto>>>
                ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                .Result.Items.ToList();

            ViewData["OnusStatuses"] = new SelectList(OnusStatuses, "Id", "Name", model.OnusStatusId);


            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<StatusDto> onusStatuses = OnusStatuses.AsEnumerable();

            foreach (var item in onusStatuses)
            {

                SelectListItem tempItem = new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString() };
                if (item.Id == model.OnusStatusId)
                    tempItem.Selected = true;

                selectListItems.Add(tempItem);
            }




            UpdateOnusViewModel updateOnusViewModel = new UpdateOnusViewModel();
            updateOnusViewModel.UpdateOnusDto = model;

            updateOnusViewModel.ItemViewModel.SelectedItemId = model.OnusStatusId;
            updateOnusViewModel.ItemViewModel.Items = selectListItems;

            return View("_EditOnus", updateOnusViewModel);

        }

        
        [HttpPost]
        public ActionResult Update(UpdateOnusViewModel model)
        {
            model.UpdateOnusDto.UserId = AbpSession.UserId;
            model.UpdateOnusDto.OnusStatusId = model.ItemViewModel.SelectedItemId;

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <OnusAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateOnusDetails", model.UpdateOnusDto).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Onus");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        [HttpPost]
        public ActionResult DeleteOnus(int OnusId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("OnusId", Convert.ToString(OnusId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <OnusAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteOnus", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Onus");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        [HttpPost]
        public ActionResult UndoOnus(int OnusId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("OnusId", Convert.ToString(OnusId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <OnusAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("UndoOnus", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Onus");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}