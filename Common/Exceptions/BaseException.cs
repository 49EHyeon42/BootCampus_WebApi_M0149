namespace Common.Exceptions
{
    public class BaseException(int httpStatusCode = 500, string message = "INTERNAL_SERVER_ERROR") : Exception(message)
    {
        public int HttpStatusCode { get; } = httpStatusCode;
    }
}
