using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;

namespace ExpenseManager.IO
{
    [DependsOn(
       typeof(ExpenseManagerCoreModule))]
    public class ExpenseManagerIOModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
