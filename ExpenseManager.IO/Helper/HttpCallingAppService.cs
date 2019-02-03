using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Helper;
using Newtonsoft.Json;

namespace ExpenseManager.IO.Helper
{
    public class HttpCallingAppService : IHttpCallingAppService
    {
        public IAPIResponse<T> Get<T, ResponseType>(string url, Dictionary<string, string> requestHeaders = null)
        {
            return SendRequest<T, ResponseType>(HttpMethod.Get, url, requestHeaders ?? new Dictionary<string, string>());
        }
        public IAPIResponse<T> Post<T, ResponseType>(string url, HttpContent content, Dictionary<string, string> requestHeaders = null)
        {
            return SendRequest<T, ResponseType>(HttpMethod.Post, url, requestHeaders ?? new Dictionary<string, string>(), content);
        }
        public string PostToURL<T, ResponseType>(string url, HttpContent content = null, Dictionary<string, string> requestHeaders = null)
        {
            return SendRequestToURL<T, ResponseType>(HttpMethod.Post, url, requestHeaders ?? new Dictionary<string, string>(), content);
        }
        public IAPIResponse<T> SendRequest<T, ResponseType>(HttpMethod method, string url,
            Dictionary<string, string> requestHeaders = null, HttpContent content = null)
        {
            string responseString = string.Empty;
            IAPIResponse<T> deserializedProduct = default(IAPIResponse<T>);
            try

            {
                responseString = CreateHttpRequest(method, url, requestHeaders, content);
                //if (typeof(T) == typeof(string))
                //{
                //	return (IAPIResponse<T>)Convert.ChangeType(responseString, typeof(IAPIResponse<T>));
                //}
                //else
                //{
                deserializedProduct = JsonConvert.DeserializeObject<APIResponseObject<T>>(responseString);
                return deserializedProduct;
                //	}

            }
            catch (Exception ex)
            {
                return deserializedProduct;
            }
        }
        public string SendRequestToURL<T, ResponseType>(HttpMethod method, string url,
            Dictionary<string, string> requestHeaders = null, HttpContent content = null)
        {
            string responseString = string.Empty;
            var deserializedProduct = string.Empty;
            try
            {
                responseString = CreateHttpRequest(method, url, requestHeaders, content);
                if (typeof(T) == typeof(string))
                {
                    deserializedProduct = JsonConvert.DeserializeObject<string>(responseString);
                }

                return deserializedProduct;
            }
            catch (Exception ex)
            {
                return deserializedProduct;
            }
        }

        private static string CreateHttpRequest(HttpMethod method, string url, Dictionary<string, string> requestHeaders, HttpContent content)
        {
            string responseString;
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    client.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    request.Method = method;
                    request.RequestUri = new Uri(url);
                    if (requestHeaders != null)
                    {
                        foreach (var requestHeader in requestHeaders)
                        {
                            request.Headers.Add(requestHeader.Key, requestHeader.Value);
                        }
                    }
                    if (content != null)
                        request.Content = content;

                    var response = client.SendAsync(request).Result;
                    responseString = response.Content.ReadAsStringAsync().Result;
                }
            }

            return responseString;
        }



        public IAPIResponse<T> GetAppServiceData<AppService, T, ResponseType>(string methodName, Dictionary<string, string> requestHeaders = null, KeyValuePair<string, string>[] parameters = null)
        {
            string url = BuildApiUrl<AppService, T>(methodName);
            if (parameters != null && parameters.Length > 0)
            {
                url = !parameters.Any() ? url : $"{url}?{string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)))}";
            }

            return Get<T, ResponseType>(url, requestHeaders);
        }

        public IAPIResponse<T> PostAppServiceData<AppService, T, ResponseType>(string methodName, object content = null, Dictionary<string, string> requestHeaders = null, KeyValuePair<string, string>[] parameters = null)
        {
            string url = BuildApiUrl<AppService, T>(methodName);
            if (parameters != null && parameters.Length > 0)
            {
                url = !parameters.Any() ? url : $"{url}?{string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)))}";
            }
            StringContent contentString = new StringContent("");
            var dictionary = new Dictionary<string, string>();
            if (content != null)
            {
                contentString = new StringContent(JsonConvert.SerializeObject(content),
                Encoding.UTF8, "application/json");
            }

            var response = Post<T, ResponseType>(url, contentString, requestHeaders);
            return response;
        }


        private static string BuildApiUrl<AppService, T>(string methodName)
        {
            string url = BaseConfig.GetString("ApplicationServiceApiUrl", "");
            var appServiceName = typeof(AppService).Name.Contains("AppService") ? typeof(AppService).Name.Replace("AppService", "") : "";
            url += appServiceName + "/" + methodName;
            return url;
        }

        public static IDictionary<string, string> ToDictionary(object source)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary(property, source, dictionary);

            return dictionary;
        }

        private static void AddPropertyToDictionary(PropertyDescriptor property, object source, Dictionary<string, string> dictionary)
        {
            object value = property.GetValue(source);
            if (value == null)
            {
                dictionary.Add(property.Name, "null");

            }
            else
            {
                dictionary.Add(property.Name, value.ToString());
            }
        }
    }
}
