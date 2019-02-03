using System.Linq;
using ExpenseManager.EntityFramework;
using ExpenseManager.MultiTenancy;

namespace ExpenseManager.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly ExpenseManagerDbContext _context;

        public DefaultTenantCreator(ExpenseManagerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
