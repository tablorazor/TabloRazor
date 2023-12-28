namespace TabloRazor.Table.WebApiDataProvider
{
    public class ServiceCallResult<T>
    {
        public T Result { get; set; }
        public ExceptionHandlingResultModel Exception { get; set; }
        public bool Status { get; set; }
    }
}
