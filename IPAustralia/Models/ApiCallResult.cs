namespace IPAustralia.Models
{
    public class ApiCallResult<TResponse> where TResponse : class
    {
        public ApiCallResult(bool succeeded, TResponse response, string errorMessage = null)
        {
            Response = response;
            ErrorMessage = errorMessage;
            Succeeded = succeeded;
        }

        public TResponse Response { get; set; }
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; }
    }
}
