using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using ExpenseManager.EntityFramework;

namespace ExpenseManager.Migrator
{
    [DependsOn(typeof(ExpenseManagerDataModule))]
    public class ExpenseManagerMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ExpenseManagerDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}