using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using ExpenseManager.Helper;
using ExpenseManager.IO.Helper;
using ExpenseManager.Settlement;
using ExpenseManager.Settlement.Dto;
using ExpenseManager.SettlementType;
using ExpenseManager.SettlementType.Dto;
using ExpenseManager.Users;
using ExpenseManager.Users.Dto;
using ExpenseManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Web.Controllers
{
    public class SettlementController : ExpenseManagerControllerBase
    {
        private readonly IObjectMapper _objectMapper;

        public SettlementController(IObjectMapper objectMapper, IHttpCallingAppService httpCallingAppService) : base(httpCallingAppService)
        {
            this._objectMapper = objectMapper;
        }

        // GET: Settlement
        public ActionResult Index()
        {

            IReadOnlyList<SettlementDto> settlements = base._httpCallingAppService.GetAppServiceData
                <SettlementAppService, List<SettlementDto>, APIResponseObject<List<SettlementDto>>>
                ("GetAllSettlements").Result;

            //IReadOnlyList<SettlementDto> settlements = _httpCallingAppService.PostAppServiceData
            //    <SettlementAppService, PagedResultDto<SettlementDto>, APIResponseObject<PagedResultDto<SettlementDto>>>
            //    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
            //    .Result.Items;


            // TODO convert the following to strongly typed drop down.
            // Like use of DropDownFor Helper method

            IReadOnlyList<UserDto> Users = _httpCallingAppService.PostAppServiceData
                <UserAppService, PagedResultDto<UserDto>, APIResponseObject<PagedResultDto<UserDto>>>
                ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                .Result.Items;

           
            ViewData["Users"] = new SelectList(Users.ToList().Where(x => x.IsActive), "Id", "Name");


            List<SettlementTypeDto> settlementTypes = getSettlementTypes();
            ViewData["SettlementTypes"] = new SelectList(settlementTypes, "Id", "Name");

            // sort by Settlement date
            settlements = settlements.OrderByDescending(x => x.ReturnDate).ToList();
            return View(settlements);
        }

        private List<SettlementTypeDto> getSettlementTypes()
        {
            return _httpCallingAppService.PostAppServiceData
                    <SettlementTypeAppService, PagedResultDto<SettlementTypeDto>, APIResponseObject<PagedResultDto<SettlementTypeDto>>>
                    ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                    .Result.Items.Where(x => !x.IsDeleted).ToList();
        }

        [HttpPost]
        public ActionResult Create(CreateSettlementDto model)
        {

            string ReturnedTo = Request.Form["Users"].ToString();
            string settlementTypeId = Request.Form["SettlementTypes"].ToString();

            model.UserId = (long)Convert.ToDouble(AbpSession.UserId);
            model.SettlementTypeId = Convert.ToInt32(settlementTypeId);
            model.ReturnedTo = (long)Convert.ToDouble(ReturnedTo);
            model.UserName = "Admin";
            
            SettlementDto response = _httpCallingAppService.PostAppServiceData
                <SettlementAppService, SettlementDto, APIResponseObject<PagedResultDto<SettlementDto>>>
                ("CreateSettlement", model)
                .Result;

            if (response != null)
                return RedirectToAction("Index", "Settlement");
            else
                return Json(new { status = "Something went wrong, please try again ." });

        }

        [HttpPost]
        public ActionResult DeleteSettlement(int SettlementId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("SettlementId", Convert.ToString(SettlementId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <SettlementAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("DeleteSettlement", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Settlement");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }


        public ActionResult EditSettlement(int SettlementId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("SettlementId", Convert.ToString(SettlementId));


            UpdateSettlementDto model = base._httpCallingAppService.GetAppServiceData
                            <SettlementAppService, UpdateSettlementDto, APIResponseObject<UpdateSettlementDto>>
                            ("GetSettlementUpdateDetails", new Dictionary<string, string>(), keyValues).Result;

            IReadOnlyList<UserDto> Users = _httpCallingAppService.PostAppServiceData
                <UserAppService, PagedResultDto<UserDto>, APIResponseObject<PagedResultDto<UserDto>>>
                ("GetAll", new PagedResultRequestDto { MaxResultCount = 100, SkipCount = 0 })
                .Result.Items;

            ViewData["Users"] = new SelectList(Users.ToList(), "Id", "Name", model.UserId);


            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<UserDto> users = Users.AsEnumerable();

            foreach (var user in users)
            {
                if (user.IsActive)
                {
                    SelectListItem tempUser = new SelectListItem { Text = user.Name.ToString(), Value = user.Id.ToString() };
                    if (user.Id == model.ReturnedTo)
                        tempUser.Selected = true;

                    selectListItems.Add(tempUser);
                }
            }

            // settlment types
            List<SettlementTypeDto> settlementTypes = getSettlementTypes();

            List<SelectListItem> selectListItemssettlementTypes = new List<SelectListItem>();
            foreach (SettlementTypeDto settlementType in settlementTypes)
            {
                if (!settlementType.IsDeleted)
                {
                    SelectListItem tempSettlementType = new SelectListItem { Text = settlementType.Name.ToString(), Value = settlementType.Id.ToString() };
                    if (settlementType.Id == model.SettlementTypeId)
                        tempSettlementType.Selected = true;

                    selectListItemssettlementTypes.Add(tempSettlementType);
                }
            }


            // populate model
            UpdateSettlementViewModel updateSettlementViewModel = new UpdateSettlementViewModel();
            updateSettlementViewModel.UpdateSettlementDto = model;

            updateSettlementViewModel.ItemViewModel.SelectedItemId = Convert.ToInt32(model.ReturnedTo);
            updateSettlementViewModel.ItemViewModel.Items = selectListItems;

            updateSettlementViewModel.SettlementTypeModel.Items = selectListItemssettlementTypes;
            updateSettlementViewModel.SettlementTypeModel.SelectedItemId = model.SettlementTypeId;

            // return to view
            return View("_EditSettlement", updateSettlementViewModel);

        }

        [HttpPost]
        public ActionResult Update(UpdateSettlementViewModel model)
        {
            model.UpdateSettlementDto.UserId = AbpSession.UserId;
            model.UpdateSettlementDto.ReturnedTo = model.ItemViewModel.SelectedItemId;
            model.UpdateSettlementDto.SettlementTypeId = model.SettlementTypeModel.SelectedItemId;

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                                        <SettlementAppService, BaseResponse, APIResponseObject<BaseResponse>>
                                        ("UpdateSettlementDetails", model.UpdateSettlementDto).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Settlement");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }

        [HttpPost]
        public ActionResult UndoSettlement(int SettlementId)
        {
            KeyValuePair<string, string>[] keyValues = new KeyValuePair<string, string>[1];
            keyValues[0] = new KeyValuePair<string, string>("SettlementId", Convert.ToString(SettlementId));

            BaseResponse response = base._httpCallingAppService.PostAppServiceData
                            <SettlementAppService, BaseResponse, APIResponseObject<BaseResponse>>
                            ("UndoSettlement", new Dictionary<string, string>(), null, keyValues).Result;

            if (response.IsSucceeded)
                return RedirectToAction("Index", "Settlement");
            else
                return Json(new { status = "Something went wrong, please try again." });
        }
    }
}