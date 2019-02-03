using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;

namespace ExpenseManager.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(ExpenseManagerApplicationModule))]
    public class ExpenseManagerWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(ExpenseManagerApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
            ConfigureSwaggerUi();
        }


        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SwaggerIntegrationDemo.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(ExpenseManagerWebApiModule)), "ExpenseManager.WebApi.Api.Scripts.Swagger-Custom.js");
                });
        }
    }
}
