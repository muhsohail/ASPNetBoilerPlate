using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using ExpenseManager.Authorization.Roles;
using ExpenseManager.Authorization.Users;
using ExpenseManager.BankDetails.Dto;
using ExpenseManager.Charity.Dto;
using ExpenseManager.ExpenseSheet.Dto;
using ExpenseManager.ExpenseType.Dto;
using ExpenseManager.Loan.Dto;
using ExpenseManager.Model;
using ExpenseManager.Onus.Dto;
using ExpenseManager.Pension.Dto;
using ExpenseManager.PensionReceivable.Dto;
using ExpenseManager.Roles.Dto;
using ExpenseManager.Settlement.Dto;
using ExpenseManager.Stakeholder.Dto;
using ExpenseManager.Users.Dto;

namespace ExpenseManager
{
    [DependsOn(typeof(ExpenseManagerCoreModule), typeof(AbpAutoMapperModule))]
    public class ExpenseManagerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());

                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<ExpenseSheetDto, ExpenseDetail>();
                cfg.CreateMap<CreateExpenseSheetDto, ExpenseDetail>();
                cfg.CreateMap<CreateExpenseSheetDto, ExpenseDetail>().ForMember(x => x.ExpenseCategory, opt => opt.Ignore());
                cfg.CreateMap<UpdateExpenseSheetDto, ExpenseDetail>();
                cfg.CreateMap<UpdateExpenseSheetDto, ExpenseDetail>().ForMember(x => x.ExpenseCategory, opt => opt.Ignore());

                cfg.CreateMap<ExpenseTypeDto, ExpenseCategory>();
                cfg.CreateMap<CreateExpenseTypeDto, ExpenseCategory>();
                cfg.CreateMap<UpdateExpenseTypeDto, ExpenseCategory>();

                cfg.CreateMap<OnusDto, OnusDetail>();

                cfg.CreateMap<CreateOnusDto, OnusDetail>();
                cfg.CreateMap<CreateOnusDto, OnusDetail>().ForMember(x => x.OnusStatus, opt => opt.Ignore());

                cfg.CreateMap<UpdateOnusDto, OnusDetail>();
                cfg.CreateMap<UpdateOnusDto, OnusDetail>().ForMember(x => x.OnusStatus, opt => opt.Ignore());

                cfg.CreateMap<PensionDto, PensionDetail>();

                cfg.CreateMap<CreatePensionDto, PensionDetail>();
                cfg.CreateMap<CreatePensionDto, PensionDetail>().ForMember(x => x.TotalSpent, opt => opt.Ignore());
                cfg.CreateMap<CreatePensionDto, PensionDetail>().ForMember(x => x.Total, opt => opt.Ignore());
                cfg.CreateMap<CreatePensionDto, PensionDetail>().ForMember(x => x.Remaining, opt => opt.Ignore());
                cfg.CreateMap<CreatePensionDto, PensionDetail>().ForMember(x => x.StakeholderDetail, opt => opt.Ignore());

                cfg.CreateMap<UpdatePensionDto, PensionDetail>();
                cfg.CreateMap<UpdatePensionDto, PensionDetail>().ForMember(x => x.TotalSpent, opt => opt.Ignore());
                cfg.CreateMap<UpdatePensionDto, PensionDetail>().ForMember(x => x.Total, opt => opt.Ignore());
                cfg.CreateMap<UpdatePensionDto, PensionDetail>().ForMember(x => x.Remaining, opt => opt.Ignore());
                cfg.CreateMap<UpdatePensionDto, PensionDetail>().ForMember(x => x.StakeholderDetail, opt => opt.Ignore());

                cfg.CreateMap<StakeholderDto, StakeholderDetail>();

                // Settlement
                cfg.CreateMap<SettlementDto, SettlementDetail>();
                cfg.CreateMap<CreateSettlementDto, SettlementDetail>();
                cfg.CreateMap<UpdateSettlementDto, SettlementDetail>();

                // PensionReceivable
                cfg.CreateMap<PensionReceivableDto, PensionReceivableDetail>();
                cfg.CreateMap<CreatePensionReceivableDto, PensionReceivableDetail>();
                cfg.CreateMap<UpdatePensionReceivableDto, PensionReceivableDetail>();

                // Loan
                cfg.CreateMap<LoanDto, LoanDetail>();
                cfg.CreateMap<CreateLoanDto, LoanDetail>();
                cfg.CreateMap<UpdateLoanDto, LoanDetail>();

                // Charity
                cfg.CreateMap<CharityDto, CharityDetail>();
                cfg.CreateMap<CreateCharityDto, CharityDetail>();
                cfg.CreateMap<UpdateCharityDto, CharityDetail>();

                // Bank Details
                cfg.CreateMap<BankDetailsDto, BankAccountDetail>();
                cfg.CreateMap<CreateBankDetailsDto, BankAccountDetail>();
                cfg.CreateMap<UpdateBankDetailsDto, BankAccountDetail>();
            });
        }
    }
}
