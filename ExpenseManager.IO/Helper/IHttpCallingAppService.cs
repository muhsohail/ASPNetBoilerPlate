using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using ExpenseManager.Helper;

namespace ExpenseManager.IO.Helper
{
    public interface IHttpCallingAppService : IApplicationService
    {
        IAPIResponse<T> Get<T, ResponseType>(string url, Dictionary<string, string> requestHeaders = null);
        IAPIResponse<T> Post<T, ResponseType>(string url, HttpContent content, Dictionary<string, string> requestHeaders = null);
        IAPIResponse<T> GetAppServiceData<AppService, T, ResponseType>(string methodName, Dictionary<string, string> requestHeaders = null, KeyValuePair<string, string>[] parameters = null);
        IAPIResponse<T> PostAppServiceData<AppService, T, ResponseType>(string methodName, object content = null, Dictionary<string, string> requestHeaders = null, KeyValuePair<string, string>[] parameters = null);
        string PostToURL<T, ResponseType>(string url, HttpContent content = null, Dictionary<string, string> requestHeaders = null);

    }
}
