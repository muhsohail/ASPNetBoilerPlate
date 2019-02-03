using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using ExpenseManager.Authorization.Roles;
using ExpenseManager.Authorization.Users;
using ExpenseManager.Model;
using ExpenseManager.MultiTenancy;

namespace ExpenseManager.EntityFramework
{
    public class ExpenseManagerDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual DbSet<OnusStatus> OnusStatus { get; set; }
        public virtual DbSet<OnusDetail> OnusDetails { get; set; }
        public virtual DbSet<PensionDetail> PensionDetail { get; set; }
        public virtual DbSet<StakeholderDetail> StakeholderDetail { get; set; }
        public virtual DbSet<SettlementDetail> SettlementDetails { get; set; }
        public virtual DbSet<PensionReceivableDetail> PensionReceivableDetail { get; set; }
        public virtual DbSet<LoanDetail> LoanDetails { get; set; }
        public virtual DbSet<CharityDetail> CharityDetails { get; set; }
        public virtual DbSet<BankAccountDetail> BankAccountDetails { get; set; }
        public virtual DbSet<SettlementCategory> SettlementCategory { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ExpenseManagerDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ExpenseManagerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ExpenseManagerDbContext since ABP automatically handles it.
         */
        public ExpenseManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ExpenseManagerDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ExpenseManagerDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
