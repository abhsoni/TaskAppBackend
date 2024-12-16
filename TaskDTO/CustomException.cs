namespace TaskAppDTO
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception innerException) : base(message, innerException) { }
        public CustomException(string message, int statusCode) : this(message)
        {
            StatusCode = statusCode;
        }

    }
}
