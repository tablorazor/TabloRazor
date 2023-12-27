using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace TabloRazor.Table.WebApiDataProvider
{
    public class MockableHttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient httpClient;
        public MockableHttpClient(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public string ServerAddress => httpClient?.BaseAddress?.ToString();

        public IHttpClient.Unauthorized OnUnauthorized { get; set; }

        public async Task<ServiceCallResult<T>> Delete<T>(string requestUri, CancellationToken cancellationToken)
        {
            var response = new ServiceCallResult<T>();
            response.Status = true;
            var results = await httpClient.DeleteAsync(requestUri, cancellationToken);
            if (results.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Status = false;
                response.Exception = new ExceptionHandlingResultModel { Message = "Unauthorized", ErrorCode = 401 };
                if (OnUnauthorized != null)
                {
                    OnUnauthorized("Unauthorized");
                }
            }
            else if (results.IsSuccessStatusCode)
            {
                var data = await results.Content.ReadFromJsonAsync<T>();
                response.Result = data;
            }
            else
            {
                response.Status = false;
                var exceptionData = await results.Content.ReadFromJsonAsync<ExceptionHandlingResultModel>();
                response.Exception = exceptionData;
            }
            return response;
        }

        public async Task<ServiceCallResult<T>> Get<T>(string requestUri, CancellationToken cancellationToken)
        {
            var response = new ServiceCallResult<T>();
            response.Status = true;
            var results = await httpClient.GetAsync(requestUri, cancellationToken);
            if (results.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Status = false;
                response.Exception = new ExceptionHandlingResultModel { Message = "Unauthorized", ErrorCode = 401 };
                if (OnUnauthorized != null)
                {
                    OnUnauthorized("Unauthorized");
                }
            }
            else if (results.IsSuccessStatusCode)
            {
                var data = await results.Content.ReadFromJsonAsync<T>();
                response.Result = data;
            }
            else
            {
                response.Status = false;
                var exceptionData = await results.Content.ReadFromJsonAsync<ExceptionHandlingResultModel>();
                response.Exception = exceptionData;
            }
            return response;
        }

        public async Task<ServiceCallResult<T>> Patch<T, K>(string requestUri, K request, CancellationToken cancellationToken)
        {
            var response = new ServiceCallResult<T>();
            response.Status = true;
#if NET7_0_OR_GREATER
            var results = await httpClient.PatchAsJsonAsync<K>(requestUri, request, cancellationToken);
#else
            var results = await httpClient.PatchAsync(requestUri, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), cancellationToken);
#endif
            if (results.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Status = false;
                response.Exception = new ExceptionHandlingResultModel { Message = "Unauthorized", ErrorCode = 401 };
                if (OnUnauthorized != null)
                {
                    OnUnauthorized("Unauthorized");
                }
            }
            else if (results.IsSuccessStatusCode)
            {
                var data = await results.Content.ReadFromJsonAsync<T>();
                response.Result = data;
            }
            else
            {
                response.Status = false;
                var exceptionData = await results.Content.ReadFromJsonAsync<ExceptionHandlingResultModel>();
                response.Exception = exceptionData;
            }
            return response;
        }

        public async Task<ServiceCallResult<T>> Post<T, K>(string requestUri, K request, CancellationToken cancellationToken)
        {
            var response = new ServiceCallResult<T>();
            response.Status = true;
            var results = await httpClient.PostAsJsonAsync<K>(requestUri, request, cancellationToken);
            if (results.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Status = false;
                response.Exception = new ExceptionHandlingResultModel { Message = "Unauthorized", ErrorCode = 401 };
                if (OnUnauthorized != null)
                {
                    OnUnauthorized("Unauthorized");
                }
            }
            else if (results.IsSuccessStatusCode)
            {
                var data = await results.Content.ReadFromJsonAsync<T>();
                response.Result = data;
            }
            else
            {
                response.Status = false;
                var exceptionData = await results.Content.ReadFromJsonAsync<ExceptionHandlingResultModel>();
                response.Exception = exceptionData;
            }
            return response;
        }

        public async Task<ServiceCallResult<T>> PostForm<T>(string requestUri, MultipartFormDataContent request, CancellationToken cancellationToken)
        {
            return await Post<T, MultipartFormDataContent>(requestUri, request, cancellationToken);
        }

        public async Task<ServiceCallResult<T>> Put<T, K>(string requestUri, K request, CancellationToken cancellationToken)
        {
            var response = new ServiceCallResult<T>();
            response.Status = true;
            var results = await httpClient.PutAsJsonAsync<K>(requestUri, request, cancellationToken);
            if (results.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                response.Status = false;
                response.Exception = new ExceptionHandlingResultModel { Message = "Unauthorized", ErrorCode = 401 };
                if (OnUnauthorized != null)
                {
                    OnUnauthorized("Unauthorized");
                }
            }
            else if (results.IsSuccessStatusCode)
            {
                var data = await results.Content.ReadFromJsonAsync<T>();
                response.Result = data;
            }
            else
            {
                response.Status = false;
                var exceptionData = await results.Content.ReadFromJsonAsync<ExceptionHandlingResultModel>();
                response.Exception = exceptionData;
            }
            return response;
        }
    }
}
