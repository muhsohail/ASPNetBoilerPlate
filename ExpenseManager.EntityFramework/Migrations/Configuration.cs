using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using ExpenseManager.Migrations.SeedData;
using EntityFramework.DynamicFilters;
using System;

namespace ExpenseManager.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ExpenseManager.EntityFramework.ExpenseManagerDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ExpenseManager";
        }

        protected override void Seed(ExpenseManager.EntityFramework.ExpenseManagerDbContext context)
        {

           // SeedExpenseCategories(context);

            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }

        private void SeedExpenseCategories(EntityFramework.ExpenseManagerDbContext context)
        {            
            context.ExpenseCategory.AddOrUpdate(new Model.ExpenseCategory
            {
                Name = "Common",
                IsDeleted = false,
                CreationTime = new DateTime(2018,5,15),
                LastModificationTime = new DateTime(2018, 5, 15)

            });

            context.SaveChanges();
            
        }
    }
}
