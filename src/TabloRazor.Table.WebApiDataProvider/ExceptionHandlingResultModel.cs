namespace TabloRazor.Table.WebApiDataProvider
{
    public class ExceptionHandlingResultModel
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public List<ExceptionOrValidationError> Errors { get; set; }
    }
}
