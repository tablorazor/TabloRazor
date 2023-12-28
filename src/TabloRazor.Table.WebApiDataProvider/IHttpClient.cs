namespace TabloRazor.Table.WebApiDataProvider
{
    public interface IHttpClient
    {
        public delegate void Unauthorized(string message);
        string ServerAddress { get; }
        public Unauthorized OnUnauthorized { get; set; }
        Task<ServiceCallResult<T>> Get<T>(string requestUri, CancellationToken cancellationToken);
        Task<ServiceCallResult<T>> Delete<T>(string requestUri, CancellationToken cancellationToken);
        Task<ServiceCallResult<T>> Post<T, K>(string requestUri, K request, CancellationToken cancellationToken);
        Task<ServiceCallResult<T>> Put<T, K>(string requestUri, K request, CancellationToken cancellationToken);
        Task<ServiceCallResult<T>> Patch<T, K>(string requestUri, K request, CancellationToken cancellationToken);
        Task<ServiceCallResult<T>> PostForm<T>(string requestUri, MultipartFormDataContent request, CancellationToken cancellationToken);
    }
}
