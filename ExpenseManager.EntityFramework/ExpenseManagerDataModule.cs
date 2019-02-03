using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using ExpenseManager.EntityFramework;

namespace ExpenseManager
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(ExpenseManagerCoreModule))]
    public class ExpenseManagerDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ExpenseManagerDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
