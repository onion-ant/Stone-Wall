namespace StoneWall.Services.Exceptions
{
    public class ExternalApiException : ApplicationException
    {
        public ExternalApiException(string message) : base(message) { }
    }
}
