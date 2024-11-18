namespace ECom.Constants.Exceptions
{
    public class InvalidArgumentException : AbstractException
    {
        public InvalidArgumentException() : base()
        { }
        public InvalidArgumentException(string message) : base(message)
        { }
        public InvalidArgumentException(string message, System.Exception innerException) : base(message, innerException)
        { }
    }
}
